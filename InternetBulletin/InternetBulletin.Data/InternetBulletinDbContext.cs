// *********************************************************************************
//	<copyright file="InternetBulletinDbContext.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Database Context Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data
{
	using System.Diagnostics.CodeAnalysis;
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// The Database Context Class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class InternetBulletinDbContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InternetBulletinDbContext"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <param name="configuration">The configuration.</param>
		public InternetBulletinDbContext(DbContextOptions<InternetBulletinDbContext> options) : base(options)
		{
		}

		/// <summary>
		/// Gets or sets the posts.
		/// </summary>
		/// <value>
		/// The posts.
		/// </value>
		public DbSet<Post> Posts { get; set; }

		/// <summary>
		/// Override this method to further configure the model that was discovered by convention from the entity types
		/// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
		/// and re-used for subsequent instances of your derived context.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
		/// define extension methods on this object that allow you to configure aspects of the model that are specific
		/// to a given database.</param>
		/// <remarks>
		/// <para>
		/// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
		/// then this method will not be run. However, it will still run when creating a compiled model.
		/// </para>
		/// <para>
		/// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
		/// examples.
		/// </para>
		/// </remarks>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultContainer(ConfigurationConstants.ContainerNameConstant);
			modelBuilder.Entity<Post>().HasPartitionKey(c => c.PostId);
		}
	}
}
