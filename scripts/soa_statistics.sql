USE [SOA_statistics]
GO
/****** Object:  Table [dbo].[bookReports]    Script Date: 1/14/2025 12:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bookReports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[totalAmount] [int] NULL,
	[createdAt] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[bookReports] ON 

INSERT [dbo].[bookReports] ([id], [totalAmount], [createdAt]) VALUES (2, 4, CAST(N'2025-01-07' AS Date))
SET IDENTITY_INSERT [dbo].[bookReports] OFF
GO
ALTER TABLE [dbo].[bookReports] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_report]    Script Date: 1/14/2025 12:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_report]
@totalAmount INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM bookReports WHERE createdAt = CAST(CURRENT_TIMESTAMP AS DATE))
	BEGIN
		DELETE FROM bookReports WHERE createdAt = CAST(CURRENT_TIMESTAMP AS DATE)
	END

	INSERT INTO bookReports(totalAmount) VALUES (@totalAmount)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_add_reports]    Script Date: 1/14/2025 12:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_add_reports]
@totalAmount INT
AS
BEGIN
	INSERT INTO bookReports(totalAmount) VALUES (@totalAmount)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_report]    Script Date: 1/14/2025 12:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_report]
AS
BEGIN
	SELECT * FROM bookReports
	WHERE createdAt = CAST(CURRENT_TIMESTAMP AS DATE)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_reports]    Script Date: 1/14/2025 12:07:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_reports]
@from DATE,
@to DATE
AS
BEGIN
	SELECT * FROM bookReports
	WHERE createdAt >= @from AND createdAt <= @to
END
GO
