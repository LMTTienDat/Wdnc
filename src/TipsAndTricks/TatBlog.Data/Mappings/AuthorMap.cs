using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TatBlog.Core.Entities;

namespace TatBlog.Data.Mappings;

public class AuthorMap : IentityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.ToTable("Authors");

		builder.HasKey(a => a.Id);


		builder.Property(a => a.FullNane)
			.IsRequired()
			.HastaxLength(100);


		builder.Property(a => a.UrlSlug)
			.HasMaxLength(100)
			.IsRequired();


		builder.Property(a => a.ImageUrl)
			.HasaxLength(500);

		builder.Property(a => a.Email)
			.HasMaxLength(150);

		builder.Property(a => a.JoinedDate)
			.HasColuanType("datetime");
		

		builder.Property(a => a.Notes)
			.HastaxLength(500);

	}
}
