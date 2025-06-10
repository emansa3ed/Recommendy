using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryOppotunityExtensions
    {

        //  Handles pagination
        public static IQueryable<TEntity> ApplyPaging<TEntity, TParameters>(
           this IQueryable<TEntity> query,
           TParameters parameters) where TParameters : RequestParameters
        {
            return query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);
        }


        // Search functionality for Universities
        public static IQueryable<University> ApplyUniversitySearch(
            this IQueryable<University> universities,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return universities;

            return universities.Where(u =>
                u.User.UserName.Contains(searchTerm) ||
                u.UniversityUrl.Contains(searchTerm));
        }

         //  Search functionality for Companies
        public static IQueryable<Company> ApplyCompanySearch(
            this IQueryable<Company> companies,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return companies;

            return companies.Where(c =>
                c.User.UserName.Contains(searchTerm) ||
                c.CompanyUrl.Contains(searchTerm));
        }

        // Filter unverified entities
        public static IQueryable<TEntity> ApplyUnverifiedFilter<TEntity>(
            this IQueryable<TEntity> query) where TEntity : class
        {
            return typeof(TEntity).Name switch
            {
                nameof(University) => (query as IQueryable<University>)!
                    .Where(u => !u.IsVerified)
                    .Cast<TEntity>(),

                nameof(Company) => (query as IQueryable<Company>)!
                    .Where(c => !c.IsVerified)
                    .Cast<TEntity>(),

                _ => query
            };
        }

        //  Convert to PagedList
        public static async Task<PagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> query,
            RequestParameters parameters) where T : class
        {
            return await PagedList<T>.CreateAsync(
                query,
                parameters.PageNumber,
                parameters.PageSize);
        }
    }
}
