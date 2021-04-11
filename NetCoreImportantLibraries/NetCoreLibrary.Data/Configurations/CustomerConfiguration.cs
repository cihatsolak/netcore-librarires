using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreLibrary.Core.Domain;

namespace NetCoreLibrary.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Addresses).WithOne(p => p.Customer).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
