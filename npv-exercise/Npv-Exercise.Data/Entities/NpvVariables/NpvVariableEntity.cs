using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npv_Exercise.Data.Helpers;
using System.Collections.Generic;

namespace Npv_Exercise.Data.Entities.NpvVariables
{
    public class NpvVariableEntity : BaseEntity
    {        
        public decimal InitialValue { get; set; }
        public decimal LowerBoundRate { get; set; }
        public decimal UpperBoundRate { get; set; }
        public decimal Increment { get; set; }

        public virtual List<NpvVariableCashflowEntity> CashflowEntities { get; set; }
    }

    public class NpvVariableEntityMapping : IEntityTypeConfiguration<NpvVariableEntity>
    {
        public void Configure(EntityTypeBuilder<NpvVariableEntity> builder)
        {
            builder.ToTable("NpvVariable");
            EntityMappingHelper.MapBaseEntity(builder);

            builder.Property(x => x.InitialValue).HasColumnName("InitialValue").IsRequired();
            builder.Property(x => x.LowerBoundRate).HasColumnName("LowerBoundRate").IsRequired();
            builder.Property(x => x.UpperBoundRate).HasColumnName("UpperBoundRate").IsRequired();
            builder.Property(x => x.Increment).HasColumnName("Increment").IsRequired();

            builder.HasMany(x => x.CashflowEntities).WithOne(y => y.NpvVariableEntity).HasForeignKey(y => y.NpvVariableId);
        }
    }
}
