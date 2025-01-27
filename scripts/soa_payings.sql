USE [SOA_payings]
GO
/****** Object:  Table [dbo].[balances]    Script Date: 1/14/2025 12:06:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[balances](
	[userId] [nvarchar](20) NOT NULL,
	[coin] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transactions]    Script Date: 1/14/2025 12:06:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transactions](
	[paymentId] [nvarchar](50) NULL,
	[userId] [nvarchar](20) NULL,
	[amount] [int] NULL,
	[type] [nvarchar](20) NULL,
	[description] [nvarchar](100) NULL,
	[createdAt] [datetime] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[balances] ([userId], [coin]) VALUES (N'94kfd43ksds', 15000)
INSERT [dbo].[balances] ([userId], [coin]) VALUES (N'khu1243fdf32', 15000)
INSERT [dbo].[balances] ([userId], [coin]) VALUES (N'ph12duongfds', 11000)
GO
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-gFYQI1wrGz5J', N'ph12duongfds', 15000, N'DEPOSIT', N'nap card 15k', CAST(N'2025-01-11T15:54:12.533' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'129372', N'ph12duongfds', 0, N'DEPOSIT', N'nap card 15k', CAST(N'2025-01-11T15:57:22.140' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'43590', N'94kfd43ksds', 15000, N'DEPOSIT', N'nap card 15k', CAST(N'2025-01-11T15:59:59.050' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-KxwtrqORmgG6', N'ph12duongfds', -2000, N'DEPOSIT', N'tien thue sach', CAST(N'2025-01-11T16:17:23.677' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'146766', N'khu1243fdf32', 4000, N'DEPOSIT', N'nap card 20k', CAST(N'2025-01-14T11:21:27.690' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'648087', N'khu1243fdf32', 0, N'DEPOSIT', N'nap card 20k', CAST(N'2025-01-14T11:36:36.127' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'9826', N'khu1243fdf32', 0, N'DEPOSIT', N'nap card 2k', CAST(N'2025-01-14T11:41:36.237' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'929225', N'khu1243fdf32', 0, N'DEPOSIT', N'nap card 2k', CAST(N'2025-01-14T11:45:04.190' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'387437', N'khu1243fdf32', 0, N'DEPOSIT', N'nap card 2k', CAST(N'2025-01-14T11:49:20.760' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-CCHSwkkhbuMj', N'khu1243fdf32', -2000, N'DEPOSIT', N'tien thue sach', CAST(N'2025-01-07T16:44:26.680' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-PlqB4CupXcix', N'khu1243fdf32', -2000, N'DEPOSIT', N'tien thue sach', CAST(N'2025-01-07T17:03:29.593' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-CoSDu1EHOZEN', N'khu1243fdf32', -3000, N'DEPOSIT', N'tien thue sach', CAST(N'2025-01-07T17:05:28.863' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-Mqg5lcWcEX46', N'khu1243fdf32', 20000, N'DEPOSIT', N'nap card 20k', CAST(N'2025-01-07T16:21:24.780' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-liE5XjLtC0z0', N'khu1243fdf32', -2000, N'DEPOSIT', N'tien thua sach', CAST(N'2025-01-07T16:22:03.947' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-PrQoOWYtVRyd', N'khu1243fdf32', -2000, N'DEPOSIT', N'tien thue sach', CAST(N'2025-01-07T22:08:07.810' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'paycash-Q3BLsSZiF8ju', N'ph12duongfds', -2000, N'BORROW', N'tien thue sach', CAST(N'2025-01-11T16:24:20.190' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'623851', N'khu1243fdf32', 0, N'DEPOSIT', N'nap card 2k', CAST(N'2025-01-14T11:56:49.893' AS DateTime))
INSERT [dbo].[transactions] ([paymentId], [userId], [amount], [type], [description], [createdAt]) VALUES (N'774822', N'khu1243fdf32', 2000, N'DEPOSIT', N'nap card 2k', CAST(N'2025-01-14T12:00:36.780' AS DateTime))
GO
ALTER TABLE [dbo].[transactions] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_transaction]    Script Date: 1/14/2025 12:06:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[sp_add_transaction]  
@paymentId NVARCHAR(20),  
@userId NVARCHAR(20) = null,  
@amount INT,  
@type NVARCHAR(20) = null,  
@description NVARCHAR(100) = null  
AS  
BEGIN  
  BEGIN TRY  
  BEGIN TRANSACTION  
  
   -- Retrieve userId from transactions if it is not provided 
   IF @userId IS NULL
   BEGIN
		SELECT @userId = userId FROM transactions WHERE paymentId = @paymentId;
   END  
  
  -- Check if the user exists in balances, if not, insert with default coin = 0  
   IF NOT EXISTS (  
            SELECT 1 FROM balances WHERE userId = @userId  
        )  
        BEGIN  
            INSERT INTO balances(userId, coin)  
            VALUES(@userId, 0);  
        END  
  
  -- Check if adding the amount would make the balance negative
  DECLARE @currentBalance INT;
  SELECT @currentBalance = coin FROM balances WHERE userId = @userId;
  IF (@currentBalance + @amount < 0)
  BEGIN THROW 50001, 'Transaction amount exceeds current balance.', 1;
  END  
  
   -- Insert or update transaction  
  IF NOT EXISTS (  
   SELECT 1 FROM transactions WHERE paymentId = @paymentId  
  )  
  BEGIN  
   INSERT INTO transactions(paymentId,userId, amount, type, description)  
   VALUES(@paymentId, @userId, @amount, @type, @description)  
  END  
  ELSE  
  BEGIN  
   UPDATE transactions  
   SET amount += @amount  
   WHERE paymentId = @paymentId  
  END  
  
    
    
  -- Update the balance  
  UPDATE balances  
  SET coin += @amount  
  WHERE userId = @userId  
  COMMIT TRANSACTION;  
  END TRY  
    BEGIN CATCH  
        ROLLBACK TRANSACTION;  
        THROW;  
    END CATCH
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_check_success_transaction]    Script Date: 1/14/2025 12:06:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[sp_check_success_transaction]
  @paymentId NVARCHAR(20)
  AS
  BEGIN
	SELECT 1 AS flag FROM transactions WHERE paymentId = @paymentId AND amount != 0
  END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_coin]    Script Date: 1/14/2025 12:06:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_get_coin]
@userId NVARCHAR(20)
AS
BEGIN
	SELECT coin FROM balances
	WHERE userId = @userId
END
GO
