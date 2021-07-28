using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapping
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabela
            builder.ToTable("Post");

            // Chaves e índices
            builder.HasKey(x => x.Id);

            // Relacionamentos
            builder.HasOne(x => x.Category);
            builder.HasOne(x => x.Author);

            // Propriedades
            builder.Property(x => x.Title).IsRequired().HasMaxLength(160).HasColumnType("VARCHAR");
            builder.Property(x => x.Summary).IsRequired().HasMaxLength(255).HasColumnType("VARCHAR");
            builder.Property(x => x.Body).IsRequired().HasColumnType("TEXT");
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(80).HasColumnType("VARCHAR");
            builder.Property(x => x.CreateDate).IsRequired().HasColumnType("DATETIME");
            builder.Property(x => x.LastUpdateDate).IsRequired().HasColumnType("DATETIME");
        }
    }
}