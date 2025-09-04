﻿using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Posts;
using Microsoft.EntityFrameworkCore;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants.DatabaseConstants;

namespace IBBS.Infrastructure.Persistence.Adapters;

/// <summary>
/// The SQL DB Context.
/// </summary>
/// <param name="options">The db context options.</param>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
public class SqlDbContext(DbContextOptions<SqlDbContext> options) : DbContext(options)
{
	/// <summary>
	/// Gets or sets the users.
	/// </summary>
	/// <value>
	/// The users.
	/// </value>
	public virtual DbSet<User> Users { get; set; }

	/// <summary>
	/// Gets or sets the posts.
	/// </summary>
	/// <value>
	/// The posts.
	/// </value>
	public virtual DbSet<PostDomain> Posts { get; set; }

	/// <summary>
	/// Gets or sets the post ratings.
	/// </summary>
	/// <value>
	/// The post ratings.
	/// </value>
	public virtual DbSet<PostRatingDomain> PostRatings { get; set; }

	/// <summary>
	/// Gets or sets the AI usages.
	/// </summary>
	/// <value>
	/// The usage details.
	/// </value>
	public virtual DbSet<AiUsageDomain> AiUsages { get; set; }

	/// <summary>
	/// Gets or sets the lookup master.
	/// </summary>
	/// <value>
	/// The lookup master.
	/// </value>
	public virtual DbSet<LookupMasterDomain> LookupMaster { get; set; }

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
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<User>(entity =>
		{
			entity.ToTable(UsersTableConstant);
			entity.HasKey(e => e.Id);

			entity.Property(e => e.Id).IsRequired();
			entity.Property(e => e.DisplayName).IsRequired();
			entity.Property(e => e.EmailAddress).IsRequired();
			entity.Property(e => e.DateCreated).IsRequired();
			entity.Property(e => e.IsActive).IsRequired();
			entity.Property(e => e.IsAdmin).IsRequired();
		});

		modelBuilder.Entity<PostDomain>(entity =>
		{
			entity.ToTable(PostsTableConstant);
			entity.HasKey(e => e.PostId).HasName(PrimaryKeyPostsConstant);

			entity.Property(e => e.PostId).HasColumnType(UniqueIdentifierDataTypeConstant).IsRequired();
			entity.Property(e => e.PostTitle).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
			entity.Property(e => e.PostContent).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
			entity.Property(e => e.PostCreatedDate).HasColumnType(DateTimeDataTypeConstant).IsRequired();
			entity.Property(e => e.PostOwnerUserName).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
			entity.Property(e => e.Ratings).HasColumnType(IntegerDataTypeConstant).HasDefaultValue(0).IsRequired();
			entity.Property(e => e.IsActive).HasColumnType(BitDataTypeConstant).HasDefaultValue(1).IsRequired();
		});

		modelBuilder.Entity<PostRatingDomain>(entity =>
		{
			entity.ToTable(PostRatingsTableNameConstant);
			entity.HasKey(e => e.PostRatingId);
			entity.Property(e => e.PostRatingId).HasColumnType(IntegerDataTypeConstant).UseIdentityColumn().IsRequired();
			entity.Property(e => e.PostId).HasColumnType(UniqueIdentifierDataTypeConstant).IsRequired();
			entity.Property(e => e.UserName).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
			entity.Property(e => e.RatedOn).HasColumnType(DateTimeDataTypeConstant).IsRequired();
			entity.Property(e => e.RatingValue).HasColumnType(IntegerDataTypeConstant).IsRequired();
			entity.Property(e => e.IsActive).HasColumnType(BitDataTypeConstant).HasDefaultValue(1).IsRequired();
		});

		modelBuilder.Entity<AiUsageDomain>(entity =>
		{
			entity.ToTable(AiUsagesTableNameConstant);
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Id).HasColumnType(IntegerDataTypeConstant).UseIdentityColumn().IsRequired();
			entity.Property(e => e.UserName).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
			entity.Property(e => e.Usage).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
			entity.Property(e => e.UsageTime).HasColumnType(DateTimeDataTypeConstant).IsRequired();
			entity.Property(e => e.TotalTokensConsumed).HasColumnType(IntegerDataTypeConstant);
			entity.Property(e => e.CandidatesTokenCount).HasColumnType(IntegerDataTypeConstant);
			entity.Property(e => e.PromptTokenCount).HasColumnType(IntegerDataTypeConstant);
			entity.Property(e => e.IsActive).HasColumnType(BitDataTypeConstant).HasDefaultValue(1).IsRequired();
		});

		modelBuilder.Entity<LookupMasterDomain>(entity =>
		{
			entity.ToTable(LookupMasterTableName).HasKey(e => e.Id);
		});
	}
}
