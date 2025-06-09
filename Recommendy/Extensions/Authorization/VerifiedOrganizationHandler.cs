using Microsoft.AspNetCore.Authorization;
using Contracts;
using Entities.Exceptions;
using System.Threading.Tasks;

namespace Recommendy.Extensions.Authorization
{
    public class VerifiedOrganizationHandler : AuthorizationHandler<VerifiedOrganizationRequirement>
    {
        private readonly IRepositoryManager _repository;

        public VerifiedOrganizationHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            VerifiedOrganizationRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                throw new UnverifiedOrganizationException("User is not authenticated");
            }

            var userId = context.User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnverifiedOrganizationException("User ID not found in claims");
            }

            bool isVerified = false;

            if (context.User.IsInRole("University"))
            {
                var university = await _repository.university.GetUniversityAsync(userId, false);
                isVerified = university?.IsVerified ?? false;
            }
            else if (context.User.IsInRole("Company"))
            {
                var company = _repository.Company.GetCompany(userId, false);
                isVerified = company?.IsVerified ?? false;
            }

            if (isVerified)
            {
                context.Succeed(requirement);
            }
            else
            {
                throw new UnverifiedOrganizationException("Organization must be verified to perform this action");
            }
        }
    }
}