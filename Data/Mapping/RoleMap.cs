using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Tabela
            builder.ToTable("Role");

            // Chaves e índices
            builder.HasKey(x => x.Id);

            // Relacionamentos

            // Propriedades
            builder.Property(x => x.Name).IsRequired().HasMaxLength(80).HasColumnType("VARCHAR");
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(80).HasColumnType("VARCHAR");
        }
    }
}