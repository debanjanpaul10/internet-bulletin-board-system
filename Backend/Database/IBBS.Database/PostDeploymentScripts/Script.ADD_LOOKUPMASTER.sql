
IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='SamplePrompts' AND [KeyName]='RAG' AND [KeyValue]='Tell me about this application')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'SamplePrompts', N'RAG', N'Tell me about this application', 1, CAST(N'2025-09-05' AS Date), N'System', CAST(N'2025-09-05' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='SamplePrompts' AND [KeyName]='SQL' AND [KeyValue]='Give me the number of posts available')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'SamplePrompts', N'SQL', N'Give me the number of posts available', 1, CAST(N'2025-09-05' AS Date), N'System', CAST(N'2025-09-05' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='SamplePrompts' AND [KeyName]='SQL' AND [KeyValue]='Give me the list of all NSFW false posts')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'SamplePrompts', N'SQL', N'Give me the list of all NSFW false posts', 1, CAST(N'2025-09-05' AS Date), N'System', CAST(N'2025-09-05' AS Date), N'System')
END

-- BUG STATUS

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugStatus' AND [KeyName]='Not Started')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugStatus', N'Not Started', N'1', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugStatus' AND [KeyName]='Active')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugStatus', N'Active', N'2', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugStatus' AND [KeyName]='Resolved')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugStatus', N'Resolved', N'3', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugStatus' AND [KeyName]='Closed')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugStatus', N'Closed', N'4', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

-- BUG SEVERITY

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugSeverity' AND [KeyName]='High')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugSeverity', N'High', N'1', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugSeverity' AND [KeyName]='Medium')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugSeverity', N'Medium', N'2', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugSeverity' AND [KeyName]='Low')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugSeverity', N'Low', N'3', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END

IF NOT EXISTS (SELECT TOP 1
	1
FROM [dbo].[LookupMaster]
WHERE [IsActive]=1 AND [Type]='BugSeverity' AND [KeyName]='NA')
BEGIN
	INSERT [dbo].[LookupMaster]
		([Type], [KeyName], [KeyValue], [IsActive], [DateCreated], [CreatedBy], [DateModified], [ModifiedBy])
	VALUES
		(N'BugSeverity', N'NA', N'4', 1, CAST(N'2025-09-08' AS Date), N'System', CAST(N'2025-09-08' AS Date), N'System')
END
