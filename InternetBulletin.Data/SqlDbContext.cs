// *********************************************************************************
//	<copyright file="SqlDbContext.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The SQL DB Context Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data
{
	using InternetBulletin.Data.Entities;
	using InternetBulletin.Shared.Constants;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using static InternetBulletin.Shared.Constants.DatabaseConstants;

	/// <summary>
	/// The SQL DB Context Class.
	/// </summary>
	/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
	public class SqlDbContext : DbContext
	{
		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDbContext"/> class.
		/// </summary>
		/// <remarks>
		/// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
		/// for more information and examples.
		/// </remarks>
		public SqlDbContext()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SqlDbContext"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		/// <param name="configuration">The Configuration.</param>
		public SqlDbContext(DbContextOptions<SqlDbContext> options, IConfiguration configuration) : base(options)
		{
			this._configuration = configuration;
		}

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		/// <value>
		/// The users.
		/// </value>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Gets or sets the posts.
		/// </summary>
		/// <value>
		/// The posts.
		/// </value>
		public DbSet<Post> Posts { get; set; }

		/// <summary>
		/// Override this method to configure the database (and other options) to be used for this context.
		/// This method is called for each instance of the context that is created.
		/// The base implementation does nothing.
		/// </summary>
		/// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
		/// typically define extension methods on this object that allow you to configure the context.</param>
		/// <remarks>
		/// <para>
		/// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
		/// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
		/// the options have already been set, and skip some or all of the logic in
		/// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
		/// </para>
		/// <para>
		/// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
		/// for more information and examples.
		/// </para>
		/// </remarks>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = this._configuration[ConfigurationConstants.SqlConnectionStringConstant];
			optionsBuilder.UseSqlServer(connectionString);
		}

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
			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable(UsersTableConstant);
				entity.HasKey(e => e.UserId).HasName(PrimaryKeyUsersConstant);
				entity.Property(e => e.UserId).HasColumnName(UserIdConstant).HasColumnType(IntegerDataTypeConstant).ValueGeneratedOnAdd();

				entity.Property(p => p.Name).HasColumnName(NameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(p => p.UserEmail).HasColumnName(UserEmailConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(p => p.UserAlias).HasColumnName(UserAliasConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(p => p.UserPassword).HasColumnName(UserPasswordConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(p => p.IsActive).HasColumnName(IsActiveConstant).HasColumnType(BitDataTypeConstant).HasDefaultValue(1).IsRequired();
				entity.Property(p => p.IsAdmin).HasColumnName(IsAdminConstant).HasColumnType(BitDataTypeConstant).HasDefaultValue(0).IsRequired();
			});

			modelBuilder.Entity<Post>(entity =>
			{
				entity.ToTable(PostsTableConstant);
				entity.HasKey(e => e.PostId).HasName(PrimaryKeyPostsConstant);

				entity.Property(e => e.PostId).HasColumnName(PostIdConstant).HasColumnType(UniqueIdentifierDataTypeConstant).IsRequired();
				entity.Property(e => e.PostTitle).HasColumnName(PostTitleConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.PostContent).HasColumnName(PostContentConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.PostCreatedDate).HasColumnName(PostCreatedDateConstant).HasColumnType(DateTimeDataTypeConstant).IsRequired();
				entity.Property(e => e.PostOwnerUserName).HasColumnName(PostOwnerUserNameConstant).HasColumnType(NVarCharMaxDataTypeConstant).IsRequired();
				entity.Property(e => e.Rating).HasColumnName(RatingConstant).HasColumnType(IntegerDataTypeConstant).HasDefaultValue(0).IsRequired();
				entity.Property(e => e.IsActive).HasColumnName(IsActiveConstant).HasColumnType(BitDataTypeConstant).HasDefaultValue(1).IsRequired();
			});
		}
	}
}
