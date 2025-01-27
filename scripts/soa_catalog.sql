USE [SOA_catalog]
GO
/****** Object:  Table [dbo].[books]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](255) NULL,
	[author] [nvarchar](100) NULL,
	[genereId] [int] NULL,
	[publisher] [nvarchar](255) NULL,
	[language] [nvarchar](50) NULL,
	[pageNumber] [int] NULL,
	[yearPublished] [int] NULL,
	[copiesTotal] [int] NULL,
	[copiesAvailable] [int] NULL,
	[createdAt] [date] NULL,
	[updatedAt] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[genres]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[genres](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[books] ON 

INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (1, N'Cuộc phiêu lưu của Sherlock vô gia cư', N'Truyện trinh thám nổi tiếng của Arthur Conan Doyle.', N'Arthur Conan Doyle', 2, N'NXB Văn Học', N'Tiếng Việt', 350, 2020, 10, 9, CAST(N'2025-01-05' AS Date), CAST(N'2025-01-11' AS Date))
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (2, N'Bí mật hành tinh đỏ', N'Tiểu thuyết khoa học viễn tưởng về khám phá không gian.', N'Isaac Asimov', 1, N'NXB Khoa Học', N'Tiếng Việt', 420, 2018, 15, 14, CAST(N'2025-01-05' AS Date), CAST(N'2025-01-11' AS Date))
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (4, N'Truyện Kiều', N'Tác phẩm nổi tiếng của Nguyễn Du', N'Nguyễn Du', 1, N'Nhà xuất bản Văn học', N'Tiếng Việt', 325, 1820, 10, 10, CAST(N'2025-01-05' AS Date), NULL)
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (5, N'Dế Mèn Phiêu Lưu Ký', N'Câu chuyện phiêu lưu của chú dế mèn', N'Tô Hoài', 2, N'Nhà xuất bản Kim Đồng', N'Tiếng Việt', 250, 1941, 15, 15, CAST(N'2025-01-05' AS Date), CAST(N'2025-01-07' AS Date))
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (6, N'Chí Phèo', N'Tác phẩm kinh điển phản ánh xã hội thời kỳ trước cách mạng', N'Nam Cao', 1, N'Nhà xuất bản Giáo dục', N'Tiếng Việt', 150, 1941, 8, 8, CAST(N'2025-01-05' AS Date), CAST(N'2025-01-07' AS Date))
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (7, N'Lão Hạc', N'Câu chuyện cảm động về tình người', N'Nam Cao', 1, N'Nhà xuất bản Giáo dục', N'Tiếng Việt', 120, 1938, 12, 12, CAST(N'2025-01-05' AS Date), NULL)
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (8, N'Vợ Nhặt', N'Tác phẩm tiêu biểu trong văn học Việt Nam', N'Kim Lân', 1, N'Nhà xuất bản Văn học', N'Tiếng Việt', 200, 1945, 10, 10, CAST(N'2025-01-05' AS Date), NULL)
INSERT [dbo].[books] ([id], [title], [description], [author], [genereId], [publisher], [language], [pageNumber], [yearPublished], [copiesTotal], [copiesAvailable], [createdAt], [updatedAt]) VALUES (9, N'Nhà Đau dạ dày', N'Cuốn sách kể về hành trình đi tìm kho báu của cậu bé chăn cừu Santiago, qua đó truyền tải thông điệp về ý nghĩa của việc theo đuổi ước mơ.', N'Paulo Coelho', 2, N'NXB Văn Học', N'Tiếng Việt', 228, 1988, 100, 100, CAST(N'2025-01-11' AS Date), CAST(N'2025-01-11' AS Date))
SET IDENTITY_INSERT [dbo].[books] OFF
GO
SET IDENTITY_INSERT [dbo].[genres] ON 

INSERT [dbo].[genres] ([id], [name]) VALUES (1, N'Khoa học viễn tưởng')
INSERT [dbo].[genres] ([id], [name]) VALUES (2, N'Trinh thám')
INSERT [dbo].[genres] ([id], [name]) VALUES (3, N'Tình cảm lãng mạn')
INSERT [dbo].[genres] ([id], [name]) VALUES (4, N'Kinh dị')
INSERT [dbo].[genres] ([id], [name]) VALUES (5, N'Hài hước')
INSERT [dbo].[genres] ([id], [name]) VALUES (6, N'Phiêu lưu')
INSERT [dbo].[genres] ([id], [name]) VALUES (7, N'Tâm lý xã hội')
INSERT [dbo].[genres] ([id], [name]) VALUES (8, N'Lịch sử')
INSERT [dbo].[genres] ([id], [name]) VALUES (9, N'Thể thao')
INSERT [dbo].[genres] ([id], [name]) VALUES (10, N'Kỹ năng sống')
INSERT [dbo].[genres] ([id], [name]) VALUES (11, N'Thần thoại')
INSERT [dbo].[genres] ([id], [name]) VALUES (12, N'Văn học cổ điển')
INSERT [dbo].[genres] ([id], [name]) VALUES (13, N'Giáo dục')
INSERT [dbo].[genres] ([id], [name]) VALUES (14, N'Tôn giáo')
INSERT [dbo].[genres] ([id], [name]) VALUES (15, N'Văn hóa dân gian')
INSERT [dbo].[genres] ([id], [name]) VALUES (16, N'Phát triển bản thân')
INSERT [dbo].[genres] ([id], [name]) VALUES (17, N'Trẻ em')
INSERT [dbo].[genres] ([id], [name]) VALUES (18, N'Du ký')
INSERT [dbo].[genres] ([id], [name]) VALUES (19, N'Truyện ngắn')
INSERT [dbo].[genres] ([id], [name]) VALUES (20, N'Học thuật')
SET IDENTITY_INSERT [dbo].[genres] OFF
GO
ALTER TABLE [dbo].[books] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[books]  WITH CHECK ADD FOREIGN KEY([genereId])
REFERENCES [dbo].[genres] ([id])
GO
/****** Object:  StoredProcedure [dbo].[sp_add_books]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_books]
@title NVARCHAR(255),
@description NVARCHAR(MAX),
@author NVARCHAR(100),
@genereId INT,
@publisher NVARCHAR(255),
@language NVARCHAR(50),
@pageNumber INT,
@yearPublished INT,
@copiesTotal INT,
@id int output
AS
BEGIN
	INSERT INTO books
	(title, description, author, genereId, publisher, language, pageNumber, yearPublished, copiesTotal, copiesAvailable)
	VALUES(@title, @description, @author, @genereId, @publisher, @language, @pageNumber, @yearPublished, @copiesTotal, @copiesTotal)
	SET @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_book]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_delete_book]
@id INT
AS
BEGIN
	DELETE FROM books
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_edit_book]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[sp_edit_book]  
@id INT,  
@title NVARCHAR(255) = null,  
@description NVARCHAR(MAX) = null,  
@author NVARCHAR(100) = null,  
@genereId INT = null,  
@publisher NVARCHAR(255) = null,  
@language NVARCHAR(50) = null,  
@pageNumber INT = null,  
@yearPublished INT = null,  
@copiesTotal INT = null,  
@copiesAvailable INT = null  
AS  
BEGIN
	
	DECLARE @new_quantity INT;

    SELECT @new_quantity = copiesAvailable
    FROM books
    WHERE id = @id;

	IF @copiesAvailable IS NOT NULL
    BEGIN
        SET @new_quantity = @new_quantity + @copiesAvailable;
    END

	 IF @new_quantity < 0
    BEGIN
        RAISERROR ('The number of book is not enough', 16, 1);
        RETURN;
    END

	 UPDATE books  
	 SET title = CASE WHEN @title IS NULL  
	  THEN title ELSE @title end,  
	 description = CASE WHEN @description IS NULL  
	  THEN description ELSE @description end,  
	 author = CASE WHEN @author IS NULL  
	   THEN author ELSE @author end,  
	 genereId = CASE WHEN @genereId IS NULL  
	   THEN genereId ELSE @genereId end,  
	 publisher = CASE WHEN @publisher IS NULL  
	   THEN publisher ELSE @publisher end,  
	 language = CASE WHEN @language IS NULL  
	   THEN language ELSE @language end,  
	 pageNumber = CASE WHEN @pageNumber IS NULL  
	   THEN pageNumber ELSE @pageNumber end,  
	 yearPublished = CASE WHEN @yearPublished IS NULL  
	   THEN yearPublished ELSE @yearPublished end,  
	 copiesTotal = CASE WHEN @copiesTotal IS NULL  
	   THEN copiesTotal ELSE @copiesTotal end,  
	 copiesAvailable = @new_quantity,  
	 updatedAt = GETDATE()  
	 WHERE id = @id  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_book]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_book]
@id INT
AS
BEGIN
	SELECT b.id,
	b.title,
	b.author,
	b.description,
	g.name AS genre,
	b.publisher,
	b.language,
	b.pageNumber,
	b.yearPublished,
	b.copiesTotal,
	b.copiesAvailable
	FROM books AS b
	INNER JOIN genres AS g ON g.id = b.genereId
	WHERE b.id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_books]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_books]
@title NVARCHAR(255) = NULL,
@author NVARCHAR(100) = NULL,
@genreId INT = NULL,
@skip INT = 0,
@take INT = 15
AS
BEGIN
	SELECT b.id,
	b.title,
	b.author,
	g.name AS genre,
	b.publisher,
	b.language,
	b.pageNumber,
	b.yearPublished,
	b.copiesTotal,
	b.copiesAvailable
	FROM books AS b
	INNER JOIN genres AS g ON g.id = b.genereId
	WHERE title LIKE '%' +
		(CASE WHEN @title IS NULL THEN '' ELSE @title END) +
	'%'
	AND author LIKE '%' +
		(CASE WHEN @author IS NULL THEN '' ELSE @author END) +
	'%'
	AND genereId = CASE WHEN @genreId IS NULL THEN genereId ELSE @genreId END
	ORDER BY CASE
		WHEN copiesAvailable = 0 THEN 0
		ELSE 1 END
	OFFSET @skip ROWS
	FETCH NEXT @take ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_genres]    Script Date: 1/14/2025 12:05:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_genres]
AS
BEGIN
	SELECT * FROM genres
END
GO
