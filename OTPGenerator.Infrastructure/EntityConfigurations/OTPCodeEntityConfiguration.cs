using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OTPGenerator.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Infrastructure.EntityConfigurations
{
    internal class OTPCodeEntityConfiguration : IEntityTypeConfiguration<OTPCode>
    {
        public void Configure(EntityTypeBuilder<OTPCode> builder)
        {
            builder.ToTable("OTPCodes");
            builder.HasKey(c => c.Id).HasName("PK_otpCode_id");
            builder.HasOne(p => p.Tenant).
                  WithMany(a => a.OTPCode).
                  HasForeignKey(p => p.Tenant.Id).
                  HasConstraintName("FK_tenant_otpCode_id");
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.PhoneNumber).IsRequired();
            builder.Property(c => c.Dni).IsRequired();
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.TransactionId).IsRequired();
            builder.Property(c => c.Attempts).IsRequired();
            builder.Property(c => c.CreateDate).HasDefaultValueSql("getdate()");
        }
    }
}
