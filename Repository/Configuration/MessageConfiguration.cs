using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MSFC.Entities;
using MSFC.Contracts;
using System.Text;

namespace Repository.Configuration
{
	public class MessageConfiguration : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{

			var cipher = new MSFCipher().GetVernamCipher(Environment.GetEnvironmentVariable("SecretKey"));

			var converter = new ValueConverter<string, string>(
				v => cipher.Encrypt(v),
				v => cipher.Decrypt(v));

			builder.Property(m => m.Message).HasConversion(converter);
		}
	}
}
