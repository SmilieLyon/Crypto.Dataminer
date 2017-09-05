USE [CryptoStore]
GO
/****** Object:  Table [dbo].[Api]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Api](
	[ApiId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[LastUpdated] [date] NULL,
 CONSTRAINT [PK_Api] PRIMARY KEY CLUSTERED 
(
	[ApiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
