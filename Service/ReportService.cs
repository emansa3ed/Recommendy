using Shared.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Shared.RequestFeatures;
using Shared.DTO.Feedback;
using Org.BouncyCastle.Utilities;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Shared.DTO.Scholaship;
using Shared.DTO.Internship;


namespace Service
{
    public class ReportService : IReportService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
		private readonly MyMemoryCache _memoryCache;

		public ReportService(IRepositoryManager repository, IMapper mapper, MyMemoryCache memoryCache) { 

            _repository = repository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }  

        public  async Task   CreateReport(  string StudentId , int  PostId  , ReportDtoCreation reportDtoCreation)
        {
          

            var student = _repository.Student.GetStudent(StudentId, false);
            //Console.WriteLine(student);
            if(student == null )
                throw new StudentNotFoundException(StudentId);

            if (reportDtoCreation.Type == ReportType.Internship)
            {
                var internship = _repository.Intership.InternshipById(PostId,false);
                Console.WriteLine(internship);

                if (internship == null )
                    throw new InternshipNotFoundException(PostId);

            }
            else
            {
                var scholarship = _repository.Scholarship.GetScholarshipById(PostId, false);
                if (scholarship == null )
                    throw new ScholarshipNotFoundException(PostId);

            }

            var  report = _mapper.Map<Report>(reportDtoCreation);
            report.UserId = StudentId;
            report.TypeId= PostId;

            _repository.ReportRepository.CreateReport(report);

            await _repository.SaveAsync();

            
        }

        public  async Task DeleteReport(int  ReportId) 
        {
            var report = await _repository.ReportRepository.GetReportAsync(ReportId, false);

            if(report == null ) 
              throw new  ReportNotFoundException(ReportId);

            _repository.ReportRepository.DeleteReport(report);

           await   _repository.SaveAsync();



        }
        public async Task<ReportDto> GetReport(int reportId)
        {
            var report = await _repository.ReportRepository.GetReportAsync(reportId, false);
            if (report == null) throw new ReportNotFoundException(reportId);

            var reportDto = _mapper.Map<ReportDto>(report);

            reportDto.ReportedItem = await GetReportedItem(report.Type, report.TypeId);

            return reportDto;
        }

        private async Task<object> GetReportedItem(ReportType type, int typeId)
        {
            try
            {
                switch (type)
                {
                    case ReportType.Scholarship:
                        var scholarship =  _repository.Scholarship.ScholarshipById(typeId, trackChanges: false);
                        if (scholarship == null) return new { Error = "Scholarship not found" };
                        var scholarshipDto = _mapper.Map<GetScholarshipDto>(scholarship);
                        return scholarshipDto;

                    case ReportType.Internship:
                        var internship =  _repository.Intership.InternshipById(typeId, trackChanges: false);
                        if (internship == null) return new { Error = "Internship not found" };
                        var internshipDto = _mapper.Map<InternshipDto>(internship);
                        return internshipDto;

                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return new { Error = "Failed to load reported item details" };
            }
        }



        public async Task<PagedList<ReportDto>> GetReportsAsync(ReportParameters reportParameters, bool trackChanges=false)
        {

			if (!_memoryCache.Cache.TryGetValue(reportParameters.ToString()+ "GetReportsAsync", out PagedList<Report> cacheValue))
			{
				cacheValue = await _repository.ReportRepository.GetReportsAsync(reportParameters, trackChanges);

				var jsonSize = JsonSerializer.SerializeToUtf8Bytes(cacheValue).Length;


				var cacheEntryOptions = new MemoryCacheEntryOptions()
					.SetSize(jsonSize)
					.SetSlidingExpiration(TimeSpan.FromSeconds(5))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

				_memoryCache.Cache.Set(reportParameters.ToString() + "GetReportsAsync", cacheValue, cacheEntryOptions);
			}

            var result = cacheValue;

			var mappedResult = _mapper.Map<List<ReportDto>>(result);
           // return mappedResult;
            return new PagedList<ReportDto>(mappedResult, result.MetaData.TotalCount, reportParameters.PageNumber, reportParameters.PageSize);


        }



        public async Task UpdateReportStatus(int reportId, UpdateReportStatusDto dto)
        {
            var report = await _repository.ReportRepository.GetReportAsync(reportId, trackChanges: true);
            if (report == null) throw new ReportNotFoundException(reportId);

            report.Status = "Reviewed";

            if (dto.BanPost.HasValue || dto.DeletePost == true)
            {
                if (report.Type == ReportType.Scholarship)
                {
                    var scholarship =  _repository.Scholarship.ScholarshipById(report.TypeId, true);
                    if (scholarship == null) throw new ScholarshipNotFoundException(report.TypeId);

                    if (dto.DeletePost == true)
                    {
                        _repository.Scholarship.DeleteScholarship(scholarship);
                    }
                    else if (dto.BanPost.HasValue)
                    {
                        scholarship.IsBanned = dto.BanPost.Value;
                        _repository.Scholarship.UpdateScholarship(scholarship);
                    }
                }
                else if (report.Type == ReportType.Internship)
                {
                    var internship =  _repository.Intership.InternshipById(report.TypeId, true);
                    if (internship == null) throw new InternshipNotFoundException(report.TypeId);

                    if (dto.DeletePost == true)
                    {
                        _repository.Intership.DeleteIntership(internship.Id , true);
                    }
                    else if (dto.BanPost.HasValue)
                    {
                        internship.IsBanned = dto.BanPost.Value;
                        _repository.Intership.UpdateIntership(internship);
                    }
                }
            }

            _repository.ReportRepository.UpdateReport(report);
            await _repository.SaveAsync();
        }
    }
}
