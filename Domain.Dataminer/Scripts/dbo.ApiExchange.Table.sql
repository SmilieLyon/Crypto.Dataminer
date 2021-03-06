USE [CryptoStore]
GO
/****** Object:  Table [dbo].[ApiExchange]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiExchange](
	[ApiId] [int] NOT NULL,
	[ExchangeId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[ExternalId] [varchar](50) NULL,
	[Fee] [decimal](10, 9) NULL,
	[BalanceEnabled] [bit] NULL,
	[TradeEnabled] [bit] NULL,
	[PairsLastUpdated] [date] NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_ApiExchange] PRIMARY KEY CLUSTERED 
(
	[ApiId] ASC,
	[ExchangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApiExchange]  WITH CHECK ADD  CONSTRAINT [FK_ApiExchange_Api] FOREIGN KEY([ApiId])
REFERENCES [dbo].[Api] ([ApiId])
GO
ALTER TABLE [dbo].[ApiExchange] CHECK CONSTRAINT [FK_ApiExchange_Api]
GO
ALTER TABLE [dbo].[ApiExchange]  WITH CHECK ADD  CONSTRAINT [FK_ApiExchange_Exchange] FOREIGN KEY([ExchangeId])
REFERENCES [dbo].[Exchange] ([ExchangeId])
GO
ALTER TABLE [dbo].[ApiExchange] CHECK CONSTRAINT [FK_ApiExchange_Exchange]
GO
