using Eleraki.SchoolManagementEngine.Domain.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.SchoolManagementEngine.Infrastructure.Configurations;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(id => id.Value, value => ClassId.From(value));

        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Grade).HasMaxLength(50).IsRequired();
        builder.Property(c => c.MaxCapacity).IsRequired();
        builder.Property(c => c.IsActive).IsRequired();
    }
}
