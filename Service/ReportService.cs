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


namespace Service
{
    public class ReportService : IReportService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ReportService(IRepositoryManager repository, IMapper mapper) { 

            _repository = repository;
            _mapper = mapper;

        }  

        public  async Task   CreateReport(  string StudentId , int  PostId  , ReportDtoCreation reportDtoCreation)
        {
          

            var student = _repository.Student.GetStudent(StudentId, false);
            //Console.WriteLine(student);
            if(student == null )
                throw new StudentNotFoundException(StudentId);

            if (reportDtoCreation.Type == ReportType.Internship)
            {
                var internship = _repository.Intership.GetInternshipById(PostId,false);
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
        public async Task<ReportDto> GetReport(int ReportId)
        {
            var report = await _repository.ReportRepository.GetReportAsync(ReportId, false);

            if (report == null)
                throw new ReportNotFoundException(ReportId);

            var result=  _mapper.Map<ReportDto>(report);


            return result;

        }


        public async Task<PagedList<ReportDto>> GetReportsAsync(ReportParameters reportParameters, bool trackChanges=false)
        {
            var result = await _repository.ReportRepository.GetReportsAsync(reportParameters, trackChanges);
            var mappedResult = _mapper.Map<List<ReportDto>>(result);
           // return mappedResult;
            return new PagedList<ReportDto>(mappedResult, result.MetaData.TotalCount, reportParameters.PageNumber, reportParameters.PageSize);


        }

    }
}
