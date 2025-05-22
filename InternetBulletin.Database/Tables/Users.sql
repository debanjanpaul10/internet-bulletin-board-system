CREATE TABLE [dbo].[Users]
(
  [Id] UNIQUEIDENTIFIER NOT NULL,
  [DisplayName] NVARCHAR(MAX) NOT NULL,
  [UserName] NVARCHAR(300) NOT NULL,
  [EmailAddress] NVARCHAR(300) NOT NULL,
  [DateCreated] DATETIME2 NOT NULL,
  [IsActive] BIT NOT NULL DEFAULT 1,
  [IsAdmin] BIT NOT NULL DEFAULT 0,
  CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Add indexes
CREATE NONCLUSTERED INDEX [IX_Users_EmailAddress] ON [dbo].[Users] ([UserName], [EmailAddress]);
CREATE NONCLUSTERED INDEX [IX_Users_IsActive] ON [dbo].[Users] ([IsActive]);
