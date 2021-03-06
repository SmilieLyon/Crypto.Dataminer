USE [CryptoStore]
GO
/****** Object:  Table [dbo].[TradeRangeInfo]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TradeRangeInfo](
	[TradeRangeInfoId] [int] IDENTITY(1,1) NOT NULL,
	[MarketId] [int] NOT NULL,
	[ApiId] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[High] [decimal](10, 9) NULL,
	[Low] [decimal](10, 9) NULL,
	[OpenRange] [decimal](10, 9) NULL,
	[CloseRange] [decimal](10, 9) NULL,
	[Volume] [decimal](10, 9) NULL,
	[Last] [decimal](10, 9) NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_TradeRangeInfo] PRIMARY KEY CLUSTERED 
(
	[TradeRangeInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TradeRangeInfo]  WITH CHECK ADD  CONSTRAINT [FK_TradeRangeInfo_Api] FOREIGN KEY([ApiId])
REFERENCES [dbo].[Api] ([ApiId])
GO
ALTER TABLE [dbo].[TradeRangeInfo] CHECK CONSTRAINT [FK_TradeRangeInfo_Api]
GO
ALTER TABLE [dbo].[TradeRangeInfo]  WITH CHECK ADD  CONSTRAINT [FK_TradeRangeInfo_Market] FOREIGN KEY([MarketId])
REFERENCES [dbo].[Market] ([MarketId])
GO
ALTER TABLE [dbo].[TradeRangeInfo] CHECK CONSTRAINT [FK_TradeRangeInfo_Market]
GO
