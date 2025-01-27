USE [SOA_users]
GO
/****** Object:  Table [dbo].[users]    Script Date: 1/14/2025 12:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[cityzenNumber] [nvarchar](20) NOT NULL,
	[fullName] [nvarchar](100) NOT NULL,
	[username] [nvarchar](100) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[phoneNumber] [nvarchar](11) NOT NULL,
	[role] [nvarchar](10) NULL,
	[createdAt] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[cityzenNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[users] ([cityzenNumber], [fullName], [username], [password], [phoneNumber], [role], [createdAt]) VALUES (N'43kfd434343f', N'Huỳnh Văn Giảng', N'congvien', N'123456', N'0392664625', N'admin', CAST(N'2025-01-04' AS Date))
INSERT [dbo].[users] ([cityzenNumber], [fullName], [username], [password], [phoneNumber], [role], [createdAt]) VALUES (N'94934kdsdsds', N'Thanh Tuyen', N'thanhtuyen', N'123456', N'032532432', N'employee', CAST(N'2025-01-11' AS Date))
INSERT [dbo].[users] ([cityzenNumber], [fullName], [username], [password], [phoneNumber], [role], [createdAt]) VALUES (N'94kfd43ksds', N'Mi Keo', N'mikeo', N'123456', N'09434343', N'customer', CAST(N'2025-01-11' AS Date))
INSERT [dbo].[users] ([cityzenNumber], [fullName], [username], [password], [phoneNumber], [role], [createdAt]) VALUES (N'khu1243fdf32', N'Minh Khue', N'minhkhue', N'123456', N'039122421', N'customer', CAST(N'2025-01-04' AS Date))
INSERT [dbo].[users] ([cityzenNumber], [fullName], [username], [password], [phoneNumber], [role], [createdAt]) VALUES (N'ph12duongfds', N'Mai Phuong', N'maiphuong', N'123456', N'0391224431', N'customer', CAST(N'2025-01-04' AS Date))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__users__F3DBC5723B214C6A]    Script Date: 1/14/2025 12:08:21 PM ******/
ALTER TABLE [dbo].[users] ADD UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ('customer') FOR [role]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
/****** Object:  StoredProcedure [cong-vien].[sp_get_customers]    Script Date: 1/14/2025 12:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [cong-vien].[sp_get_customers]
@username NVARCHAR(100) = null
AS
BEGIN
	SELECT cityzenNumber, fullName, role FROM users
	WHERE role = 'customer'
	AND (@username IS NULL OR username LIKE '%' + @username + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_add_user]    Script Date: 1/14/2025 12:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_user]
@cityzenNumber NVARCHAR(20),
@fullName NVARCHAR(100),
@username NVARCHAR(100),
@password NVARCHAR(50),
@phoneNumber NVARCHAR(11),
@role NVARCHAR(10)
AS
BEGIN
    BEGIN TRANSACTION;

    IF EXISTS (SELECT 1 FROM users WHERE cityzenNumber = @cityzenNumber)
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 50000, 'The cityzenNumber already exists in the system.', 1;
    END

    IF EXISTS (SELECT 1 FROM users WHERE username = @username)
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 50001, 'The username already exists in the system.', 1;
    END

    INSERT INTO users (cityzenNumber, fullName, username, password, phoneNumber, role)
    VALUES (@cityzenNumber, @fullName, @username, @password, @phoneNumber, @role);

    COMMIT TRANSACTION;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_add_users]    Script Date: 1/14/2025 12:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_add_users]
@cityzenNumber NVARCHAR(20),
@fullName NVARCHAR(100),
@username NVARCHAR(100),
@password NVARCHAR(50),
@phoneNumber NVARCHAR(11),
@role NVARCHAR(10),
@result int output
AS
BEGIN
	INSERT INTO users(cityzenNumber,fullName,username,password,phoneNumber,role)
	VALUES(@cityzenNumber, @fullName, @username, @password, @phoneNumber, @role)
	SET @result = (SELECT 1 FROM users WHERE cityzenNumber = @cityzenNumber)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 1/14/2025 12:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_login]
@username NVARCHAR(100),
@password NVARCHAR(100)
AS
BEGIN
	SELECT cityzenNumber,fullName, role FROM users
	WHERE username = @username
	AND password = @password
END
GO
