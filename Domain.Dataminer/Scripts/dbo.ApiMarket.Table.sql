USE [CryptoStore]
GO
/****** Object:  Table [dbo].[ApiMarket]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiMarket](
	[ApiId] [int] NOT NULL,
	[MarketId] [int] NOT NULL,
	[ExternalName] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_ApiMarket] PRIMARY KEY CLUSTERED 
(
	[ApiId] ASC,
	[MarketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApiMarket]  WITH CHECK ADD  CONSTRAINT [FK_ApiMarket_Api] FOREIGN KEY([ApiId])
REFERENCES [dbo].[Api] ([ApiId])
GO
ALTER TABLE [dbo].[ApiMarket] CHECK CONSTRAINT [FK_ApiMarket_Api]
GO
ALTER TABLE [dbo].[ApiMarket]  WITH CHECK ADD  CONSTRAINT [FK_ApiMarket_Market] FOREIGN KEY([MarketId])
REFERENCES [dbo].[Market] ([MarketId])
GO
ALTER TABLE [dbo].[ApiMarket] CHECK CONSTRAINT [FK_ApiMarket_Market]
GO
