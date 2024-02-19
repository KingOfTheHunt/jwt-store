using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Contexts.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120);

        builder.Property(x => x.Image)
            .IsRequired()
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        
        // Mapeando os value object
        builder.OwnsOne(x => x.Email)
            .Property(y => y.Address)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.Code)
            .IsRequired()
            .HasColumnName("EmailVerificationCode")
            .HasColumnType("VARCHAR")
            .HasMaxLength(6);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.ExpiresAt)
            .IsRequired(false)
            .HasColumnName("EmailVerificationExpiresAt")
            .HasColumnType("TIME");

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Property(z => z.VerifiedAt)
            .IsRequired(false)
            .HasColumnName("EmailVerificationVerifiedAt")
            .HasColumnType("TIME");

        builder.OwnsOne(x => x.Email)
            .OwnsOne(y => y.Verification)
            .Ignore(z => z.IsActive);

        builder.OwnsOne(x => x.Password)
            .Property(y => y.Hash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR");

        builder.OwnsOne(x => x.Password)
            .Property(y => y.ResetCode)
            .IsRequired()
            .HasColumnName("PasswordRestCode")
            .HasColumnType("VARCHAR")
            .HasMaxLength(8);
    }
}