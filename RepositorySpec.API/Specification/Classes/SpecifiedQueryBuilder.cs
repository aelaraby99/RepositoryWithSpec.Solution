using Microsoft.EntityFrameworkCore;
using RepositorySpec.API.Models;
using RepositorySpec.API.Specification.Interfaces;

namespace RepositorySpec.API.Specification.Classes
{
    public static class SpecifiedQueryBuilder<T> where T : BaseEntity
    {
        public static IQueryable<T> BuildQuery( IQueryable<T> DbSet , ISpecification<T> Specs )
        {
            //  _context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).skip(int).take(int);
            var query = DbSet;

            if (Specs.Criteria != null)
            {
                query = query.Where(Specs.Criteria);
            }
            if (Specs.Includes != null && Specs.Includes.Any())
            {
                ///From Plumbers Market
                ///foreach(var include in Specs.Includes)
                ///{
                ///    query = query.Include(include);
                ///}
                query = Specs.Includes.Aggregate(query , ( current , include ) => current.Include(include));
            }
            if (Specs.OrderBy != null)
            {
                query = query.OrderBy(Specs.OrderBy);
            }
            if (Specs.OrderByDesc != null)
            {
                query = query.OrderByDescending(Specs.OrderByDesc);
            }
            if (Specs.IsPagingEnabled)
            {
                query = query.Skip((Specs.Skip - 1) * Specs.Take).Take(Specs.Take);
            }
            return query;
        }
    }
}
