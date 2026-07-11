using Eleraki.IdentityEngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eleraki.IdentityEngine.Infrastructure.Configurations;

/// <summary>
/// Entity configuration for User aggregate.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, value => UserId.From(value));

        builder.Property(u => u.Name)
            .HasConversion(name => name.Value, value => UserName.Create(value))
            .HasMaxLength(UserName.MaxLength);

        builder.Property(u => u.Email)
            .HasConversion(email => email.Value, value => global::Eleraki.SharedKernel.ValueObjects.Email.Create(value))
            .HasMaxLength(254);

        builder.Property(u => u.Password)
            .HasConversion(password => password.Value, value => UserPassword.FromHash(value))
            .HasMaxLength(UserPassword.MaxLength);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.Property(u => u.CreatedOn)
            .IsRequired();

        builder.Property(u => u.ModifiedOn)
            .IsRequired();
    }
}
