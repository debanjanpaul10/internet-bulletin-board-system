using IBBS.Domain.DomainEntities;
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
	/// Gets or sets the bug report domain.
	/// </summary>
	/// <value>
	/// The bug report domain.
	/// </value>
	public virtual DbSet<BugReportDomain> BugReportData { get; set; }

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

			if (Database.IsSqlServer())
			{
				entity.Property(e => e.PostId).HasColumnType(UniqueIdentifierDataTypeConstant);
				entity.Property(e => e.PostTitle).HasColumnType(NVarCharMaxDataTypeConstant);
				entity.Property(e => e.PostContent).HasColumnType(NVarCharMaxDataTypeConstant);
				entity.Property(e => e.PostCreatedDate).HasColumnType(DateTimeDataTypeConstant);
				entity.Property(e => e.PostOwnerUserName).HasColumnType(NVarCharMaxDataTypeConstant);
				entity.Property(e => e.Ratings).HasColumnType(IntegerDataTypeConstant);
				entity.Property(e => e.IsActive).HasColumnType(BitDataTypeConstant);
			}

			entity.Property(e => e.PostId).IsRequired();
			entity.Property(e => e.PostTitle).IsRequired();
			entity.Property(e => e.PostContent).IsRequired();
			entity.Property(e => e.PostCreatedDate).IsRequired();
			entity.Property(e => e.PostOwnerUserName).IsRequired();
			entity.Property(e => e.Ratings).HasDefaultValue(0).IsRequired();
			entity.Property(e => e.IsActive).HasDefaultValue(1).IsRequired();
		});

		modelBuilder.Entity<PostRatingDomain>(entity =>
		{
			entity.ToTable(PostRatingsTableNameConstant);
			entity.HasKey(e => e.PostRatingId);

			if (Database.IsSqlServer())
			{
				entity.Property(e => e.PostRatingId).HasColumnType(IntegerDataTypeConstant).UseIdentityColumn();
				entity.Property(e => e.PostId).HasColumnType(UniqueIdentifierDataTypeConstant);
				entity.Property(e => e.UserName).HasColumnType(NVarCharMaxDataTypeConstant);
				entity.Property(e => e.RatedOn).HasColumnType(DateTimeDataTypeConstant);
				entity.Property(e => e.RatingValue).HasColumnType(IntegerDataTypeConstant);
				entity.Property(e => e.IsActive).HasColumnType(BitDataTypeConstant);
			}
			else
			{
				entity.Property(e => e.PostRatingId).UseIdentityByDefaultColumn();
			}

			entity.Property(e => e.PostRatingId).IsRequired();
			entity.Property(e => e.PostId).IsRequired();
			entity.Property(e => e.UserName).IsRequired();
			entity.Property(e => e.RatedOn).IsRequired();
			entity.Property(e => e.RatingValue).IsRequired();
			entity.Property(e => e.IsActive).HasDefaultValue(1).IsRequired();
		});

		modelBuilder.Entity<AiUsageDomain>(entity =>
		{
			entity.ToTable(AiUsagesTableNameConstant);
			entity.HasKey(e => e.Id);

			if (Database.IsSqlServer())
			{
				entity.Property(e => e.Id).HasColumnType(IntegerDataTypeConstant).UseIdentityColumn();
				entity.Property(e => e.UserName).HasColumnType(NVarCharMaxDataTypeConstant);
				entity.Property(e => e.Usage).HasColumnType(NVarCharMaxDataTypeConstant);
				entity.Property(e => e.UsageTime).HasColumnType(DateTimeDataTypeConstant);
				entity.Property(e => e.TotalTokensConsumed).HasColumnType(IntegerDataTypeConstant);
				entity.Property(e => e.CandidatesTokenCount).HasColumnType(IntegerDataTypeConstant);
				entity.Property(e => e.PromptTokenCount).HasColumnType(IntegerDataTypeConstant);
				entity.Property(e => e.IsActive).HasColumnType(BitDataTypeConstant);
			}
			else
			{
				entity.Property(e => e.Id).UseIdentityByDefaultColumn();
			}

			entity.Property(e => e.Id).IsRequired();
			entity.Property(e => e.UserName).IsRequired();
			entity.Property(e => e.Usage).IsRequired();
			entity.Property(e => e.UsageTime).IsRequired();
			entity.Property(e => e.IsActive).HasDefaultValue(1).IsRequired();
		});

		modelBuilder.Entity<LookupMasterDomain>(entity =>
		{
			entity.ToTable(LookupMasterTableName).HasKey(e => e.Id);
			if (Database.IsNpgsql())
			{
				entity.Property(e => e.Id).UseIdentityByDefaultColumn();
			}
		});

		modelBuilder.Entity<BugReportDomain>(entity =>
		{
			entity.ToTable(BugReportTableName).HasKey(e => e.Id);
			if (Database.IsNpgsql())
			{
				entity.Property(e => e.Id).UseIdentityByDefaultColumn();
			}
		});

		if (Database.IsNpgsql())
		{
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				entity.SetTableName(entity.GetTableName()?.ToLower());
				foreach (var property in entity.GetProperties())
				{
					property.SetColumnName(property.GetColumnName().ToLower());
				}
			}
		}
	}
}
