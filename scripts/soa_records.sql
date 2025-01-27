USE [SOA_Records]
GO
/****** Object:  Table [dbo].[borrows]    Script Date: 1/14/2025 12:06:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[borrows](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [nvarchar](20) NULL,
	[bookId] [int] NULL,
	[borrowDate] [date] NULL,
	[dueDate] [date] NULL,
	[returnDate] [date] NULL,
	[status] [int] NULL,
	[createdAt] [date] NULL,
	[updatedAt] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[borrows] ON 

INSERT [dbo].[borrows] ([id], [userId], [bookId], [borrowDate], [dueDate], [returnDate], [status], [createdAt], [updatedAt]) VALUES (17, N'khu1243fdf32', 6, CAST(N'2025-01-07' AS Date), CAST(N'2025-01-08' AS Date), CAST(N'2025-01-07' AS Date), 1, CAST(N'2025-01-07' AS Date), CAST(N'2025-01-07' AS Date))
INSERT [dbo].[borrows] ([id], [userId], [bookId], [borrowDate], [dueDate], [returnDate], [status], [createdAt], [updatedAt]) VALUES (18, N'khu1243fdf32', 6, CAST(N'2025-01-07' AS Date), CAST(N'2025-01-04' AS Date), CAST(N'2025-01-07' AS Date), 1, CAST(N'2025-01-07' AS Date), CAST(N'2025-01-07' AS Date))
INSERT [dbo].[borrows] ([id], [userId], [bookId], [borrowDate], [dueDate], [returnDate], [status], [createdAt], [updatedAt]) VALUES (19, N'dsdds32', 2, CAST(N'2025-01-07' AS Date), CAST(N'2025-01-04' AS Date), NULL, 0, CAST(N'2025-01-07' AS Date), NULL)
INSERT [dbo].[borrows] ([id], [userId], [bookId], [borrowDate], [dueDate], [returnDate], [status], [createdAt], [updatedAt]) VALUES (20, N'khu1243fdf32', 2, CAST(N'2025-01-07' AS Date), CAST(N'2025-01-10' AS Date), NULL, 0, CAST(N'2025-01-07' AS Date), NULL)
INSERT [dbo].[borrows] ([id], [userId], [bookId], [borrowDate], [dueDate], [returnDate], [status], [createdAt], [updatedAt]) VALUES (21, N'ph12duongfds', 1, CAST(N'2025-01-11' AS Date), CAST(N'2025-01-13' AS Date), NULL, 0, CAST(N'2025-01-11' AS Date), NULL)
INSERT [dbo].[borrows] ([id], [userId], [bookId], [borrowDate], [dueDate], [returnDate], [status], [createdAt], [updatedAt]) VALUES (22, N'ph12duongfds', 2, CAST(N'2025-01-11' AS Date), CAST(N'2025-01-13' AS Date), CAST(N'2025-01-11' AS Date), 1, CAST(N'2025-01-11' AS Date), CAST(N'2025-01-11' AS Date))
SET IDENTITY_INSERT [dbo].[borrows] OFF
GO
ALTER TABLE [dbo].[borrows] ADD  DEFAULT (getdate()) FOR [borrowDate]
GO
ALTER TABLE [dbo].[borrows] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[borrows] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
/****** Object:  StoredProcedure [dbo].[sp_borrow_book]    Script Date: 1/14/2025 12:06:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_borrow_book]
@userId NVARCHAR(20),
@bookId INT,
@dueDate DATE,
@id INT OUTPUT
AS
BEGIN
	INSERT INTO borrows(userId, bookId, dueDate) VALUES(@userId, @bookId, @dueDate)

	SET @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_borrow_record]    Script Date: 1/14/2025 12:06:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_borrow_record]
@id INT
AS
BEGIN
	SELECT id, userId, bookId,status FROM borrows
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_records]    Script Date: 1/14/2025 12:06:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_records]
@userId NVARCHAR(20) = NULL
AS
BEGIN
	SELECT * FROM borrows
	WHERE userId = CASE WHEN @userId IS NULL
					THEN userId 
					ELSE @userId
				   END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_return_book]    Script Date: 1/14/2025 12:06:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_return_book]
@id INT
AS
BEGIN
	UPDATE borrows
	SET returnDate = GETDATE(),
	status = 1,
	updatedAt = GETDATE()
	WHERE id = @id

	SELECT DATEDIFF(day, dueDate, GETDATE())  FROM borrows
	WHERE id = @id AND GETDATE() > dueDate
END
GO
