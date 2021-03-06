USE [CryptoStore]
GO
/****** Object:  Table [dbo].[Trade]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trade](
	[TradeId] [int] IDENTITY(1,1) NOT NULL,
	[ApiId] [int] NOT NULL,
	[MarketId] [int] NOT NULL,
	[Amount] [decimal](10, 9) NULL,
	[Rate] [decimal](10, 9) NULL,
	[Cost] [decimal](10, 9) NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_Trade] PRIMARY KEY CLUSTERED 
(
	[TradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Api] FOREIGN KEY([ApiId])
REFERENCES [dbo].[Api] ([ApiId])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Api]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Market] FOREIGN KEY([MarketId])
REFERENCES [dbo].[Market] ([MarketId])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Market]
GO
