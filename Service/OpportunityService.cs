using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTO.Internship;
using Shared.DTO.Notification;
using Shared.DTO.opportunity;
using Shared.DTO.Scholaship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
		public OpportunityService( IRepositoryManager repositoryManager , IMapper mapper,INotificationService notificationService) 
        { 
            _repositoryManager  = repositoryManager;
            _mapper = mapper;
			_notificationService = notificationService;
		}
         
       public async Task SavedOpportunity(SavedOpportunityDto savedOpportunityDto)
        {
            var student = _repositoryManager.Student.GetStudent(savedOpportunityDto.StudentId,false);
            if (student is null)
                throw new StudentNotFoundException(savedOpportunityDto.StudentId);



            var result=  await _repositoryManager.OpportunityRepository.GetSavedOpportunity(savedOpportunityDto.StudentId, savedOpportunityDto.PostId, savedOpportunityDto.Type);

            object p;
            if (result == null)
            {
                if (savedOpportunityDto.Type == 'I')
                {
                    var internship = _repositoryManager.Intership.GetInternshipById(savedOpportunityDto.PostId, false);
                    if (internship == null)
                    throw new InternshipNotFoundException(savedOpportunityDto.PostId);
                    p = internship;
				}
				else
                {
                    var scholarship = _repositoryManager.Scholarship.GetScholarshipById(savedOpportunityDto.PostId, false);
                    if (scholarship == null)
                     throw new ScholarshipNotFoundException(savedOpportunityDto.PostId);
					p = scholarship;

				}
				
                    var savedPost = _mapper.Map<SavedPost>(savedOpportunityDto);
                    await _repositoryManager.OpportunityRepository.SavedOpportunity(savedPost);
                    await _repositoryManager.SaveAsync();
                    var post = (Internship)p;
                    await _notificationService.CreateNotificationAsync(new NotificationCreationDto { ActorID = savedOpportunityDto.StudentId,ReceiverID = post.CompanyId, Content = "NOT"});
					
                
               
            }
           
        }
        public async Task DeleteOpportunity(SavedOpportunityDto savedOpportunityDto)
        {
            var student = _repositoryManager.Student.GetStudent(savedOpportunityDto.StudentId, false);
            if (student is null)
                throw new StudentNotFoundException(savedOpportunityDto.StudentId);

            var result =  await _repositoryManager.OpportunityRepository.GetSavedOpportunity(savedOpportunityDto.StudentId, savedOpportunityDto.PostId, savedOpportunityDto.Type);
            if (result != null)
            {
                if (savedOpportunityDto.Type == 'I')
                {
                    var internship = _repositoryManager.Intership.GetInternshipById(savedOpportunityDto.PostId, false);
                    if (internship == null)
                        throw new InternshipNotFoundException(savedOpportunityDto.PostId);

                }
                else
                {
                    var scholarship = _repositoryManager.Scholarship.GetScholarshipById(savedOpportunityDto.PostId, false);
                    if (scholarship == null)
                        throw new ScholarshipNotFoundException(savedOpportunityDto.PostId);
                }



                var savedPost = _mapper.Map<SavedPost>(savedOpportunityDto);
                await _repositoryManager.OpportunityRepository.DeleteOpportunity(savedPost);
                await _repositoryManager.SaveAsync();
              
            }


            else
            {
                throw new SavedPostNotFoundException();

            }

        }
        public async Task<IEnumerable<GetScholarshipDto>> GetSavedScholarshipsAsync(string studentId)
        {

            var student = _repositoryManager.Student.GetStudent(studentId,false);
            if (student is null)
                throw new StudentNotFoundException(studentId);

            var savedPosts = await _repositoryManager.OpportunityRepository.GetSavedScholarshipsAsync(studentId, trackChanges: false);

                var scholarships = new List<GetScholarshipDto>();

                foreach (var savedPost in savedPosts)
                {
                    var scholarship = _repositoryManager.Scholarship.GetScholarshipById(savedPost.PostId, trackChanges: false);
                    if (scholarship != null)
                    {
                        var scholarshipDto = _mapper.Map<GetScholarshipDto>(scholarship);
                        scholarships.Add(scholarshipDto);
                    }
                }

                return scholarships;
           
        }
        public async Task<IEnumerable<InternshipDto>> GetSavedInternshipsAsync(string studentId)
        {

            var student = _repositoryManager.Student.GetStudent(studentId, false);
            if (student is null)
                throw new StudentNotFoundException(studentId);

            var savedPosts = await _repositoryManager.OpportunityRepository.GetSavedInternshipsAsync(studentId, trackChanges: false);

            var internships = new List<InternshipDto>();

            foreach (var savedPost in savedPosts)///بدل الفور دي كنت علمت  ليست جوه ال ماب
            {
                var internship = _repositoryManager.Intership.GetInternshipById(savedPost.PostId, trackChanges: false);
                if (internship != null)
                {
                    var internshipDto = _mapper.Map<InternshipDto>(internship);
                    internships.Add(internshipDto);
                }
            }

            return internships;
        }
    }
}
