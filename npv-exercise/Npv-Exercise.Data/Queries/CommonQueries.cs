using Npv_Exercise.Data.Entities;
using System.Linq;

namespace Npv_Exercise.Data.Queries
{
    public static class CommonQueries
    {
        public static IQueryable<T> FilterById<T>(this IQueryable<T> query, int id) where T : BaseEntity
        {
            return query.Where(x => x.Id == id);
        }            
    }
}
