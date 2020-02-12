using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npv_Exercise.Data.Helpers;

namespace Npv_Exercise.Data.Entities.NpvVariables
{
    public class NpvVariableCashflowEntity : BaseEntity
    {
        public int NpvVariableId { get; set; }
        public virtual NpvVariableEntity NpvVariableEntity { get; set; }

        public decimal Cashflow { get; set; }
        public int Order { get; set; }
    }

    public class NpvVariableCashflowEntityMapping : IEntityTypeConfiguration<NpvVariableCashflowEntity>
    {
        public void Configure(EntityTypeBuilder<NpvVariableCashflowEntity> builder)
        {
            builder.ToTable("NpvVariableCashflow");
            EntityMappingHelper.MapBaseEntity(builder);

            builder.Property(x => x.NpvVariableId).HasColumnName("NpvVariableId").IsRequired();
            builder.Property(x => x.Cashflow).HasColumnName("Cashflow").IsRequired();
            builder.Property(x => x.Order).HasColumnName("Order").IsRequired();

            builder.HasOne(x => x.NpvVariableEntity).WithMany(y => y.CashflowEntities).HasForeignKey(x => x.NpvVariableId);
        }
    }
}
