using LENA.WebAppCase.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LENA.WebAppCase.Repository.Configurations
{
    public class FormConfiguration : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(x => x.id);

            builder.HasIndex(x => x.id).IsUnique();

            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.createdBy).IsRequired();
            builder.Property(x => x.description).IsRequired();
            builder.Property(x => x.name).IsRequired();
            builder.Property(x => x.createdAt).IsRequired();

            builder.Navigation(x => x.fields).AutoInclude();

            builder.HasMany(x => x.fields);
        }
    }
}
