using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Admin
{
    public record AdminDashboardStatsDto
    {
        public UserStatistics Users { get; init; }
        public OrganizationStatistics Organizations { get; init; }
        public OpportunityStatistics Opportunities { get; init; }
    }

    public record UserStatistics
    {
        public int TotalUsers { get; init; }
        public int ActiveUsers { get; init; }
        public int BannedUsers { get; init; }
        public UserTypeDistribution UserTypes { get; init; }
    }

    public record UserTypeDistribution
    {
        public UserTypeStats Students { get; init; }
        public UserTypeStats Companies { get; init; }
        public UserTypeStats Universities { get; init; }
        public UserTypeStats Admins { get; init; }
    }
    public record UserTypeStats
    {
        public int Total { get; init; }
        public int Active { get; init; }
        public int Banned { get; init; }
    }

    public record OrganizationStatistics
    {
        public CompanyAnalytics Companies { get; init; }
        public UniversityAnalytics Universities { get; init; }
    }

    public record CompanyAnalytics
    {
        public int TotalCompanies { get; init; }
        public int VerifiedCompanies { get; init; }
        public int UnverifiedCompanies { get; init; }
        public InternshipMetrics InternshipMetrics { get; init; }
    }

    public record UniversityAnalytics
    {
        public int TotalUniversities { get; init; }
        public int VerifiedUniversities { get; init; }
        public int UnverifiedUniversities { get; init; }
        public ScholarshipMetrics ScholarshipMetrics { get; init; }
    }

    public record OpportunityStatistics
    {
        public InternshipStatistics Internships { get; init; }
        public ScholarshipStatistics Scholarships { get; init; }
    }

    public record InternshipStatistics
    {
        public GeneralMetrics General { get; init; }
        public TimeBasedMetrics Timeline { get; init; }
        public CompensationMetrics Compensation { get; init; }
        public Dictionary<string, int> PopularPositions { get; init; }
    }

    public record ScholarshipStatistics
    {
        public GeneralMetrics General { get; init; }
        public TimeBasedMetrics Timeline { get; init; }
        public FundingMetrics Funding { get; init; }
        public Dictionary<string, int> DegreeDistribution { get; init; }
    }

    public record GeneralMetrics
    {
        public int Total { get; init; }
        public int Active { get; init; }
        public int Banned { get; init; }
    }

    public record TimeBasedMetrics
    {
        public int OpenForApplications { get; init; }
        public int ClosedApplications { get; init; }
        public int StartingThisMonth { get; init; }
        public int EndingThisMonth { get; init; }
    }

    public record CompensationMetrics
    {
        public int PaidOpportunities { get; init; }
        public int UnpaidOpportunities { get; init; }
        public decimal PaidPercentage { get; init; }
    }

    public record FundingMetrics
    {
        public int FullyFunded { get; init; }
        public int PartiallyFunded { get; init; }
        public int Unfunded { get; init; }
        public Dictionary<string, decimal> AverageCostByDegree { get; init; }
    }
    public record InternshipMetrics
    {
        public int CompaniesWithInternships { get; init; }
    }

    public record ScholarshipMetrics
    {
        public int UniversitiesWithScholarships { get; init; }
    }
}
