using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Contracts
{
    public interface IRepositoryManager
    {

        ICountryRepository Country { get; }
        IStudentRepository Student { get; }
        IUserRepository User { get; }
        ICompanyRepository Company { get; }
        IUniversityRepository university { get; }
        IScholarshipRepository Scholarship { get; } 

        IFileRepository File { get; }

        IInternshipRepository Intership { get; }

        IInternshipPositionRepository InternshipPosition { get; }

        IPositionRepository PositionRepository { get; }

        IUserCodeRepository UserCodeRepository { get; }

        IOpportunityRepository OpportunityRepository { get; }
		public IFeedbackRepository FeedbackRepository { get; }
		public INotificationRepository NotificationRepository { get; }
        IReportRepository ReportRepository { get; }
        IChatUsersRepository ChatUsersRepository { get; }

        IChatMessagesRepository ChatMessagesRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
