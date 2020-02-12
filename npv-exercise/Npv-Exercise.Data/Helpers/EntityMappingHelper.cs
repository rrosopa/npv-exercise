using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npv_Exercise.Data.Entities;

namespace Npv_Exercise.Data.Helpers
{
    public static class EntityMappingHelper
    {
        public static void MapBaseEntity<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
		{
			builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            // add other common stuffs here...
		}
    }
}
