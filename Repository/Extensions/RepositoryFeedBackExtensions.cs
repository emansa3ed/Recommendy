using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
	public static  class RepositoryFeedBackExtensions
	{
		public static IQueryable<Feedback> Paging(this IQueryable<Feedback> Scholarship, int PageNumber, int PageSize)
		{
			return Scholarship
			.Skip((PageNumber - 1) * PageSize)
			.Take(PageSize);
		}
	}
}
