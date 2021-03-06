USE [CryptoStore]
GO
/****** Object:  Table [dbo].[Market]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Market](
	[MarketId] [int] IDENTITY(1,1) NOT NULL,
	[ExchangeId] [int] NOT NULL,
	[PrimaryAssetId] [int] NOT NULL,
	[SecondaryAssetId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Market] PRIMARY KEY CLUSTERED 
(
	[MarketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Market]  WITH CHECK ADD  CONSTRAINT [FK_Market_AssetSecondary] FOREIGN KEY([SecondaryAssetId])
REFERENCES [dbo].[Asset] ([AssetId])
GO
ALTER TABLE [dbo].[Market] CHECK CONSTRAINT [FK_Market_AssetSecondary]
GO
ALTER TABLE [dbo].[Market]  WITH CHECK ADD  CONSTRAINT [FK_Market_Exchange] FOREIGN KEY([ExchangeId])
REFERENCES [dbo].[Exchange] ([ExchangeId])
GO
ALTER TABLE [dbo].[Market] CHECK CONSTRAINT [FK_Market_Exchange]
GO
ALTER TABLE [dbo].[Market]  WITH CHECK ADD  CONSTRAINT [FK_Market_PrimaryAsset] FOREIGN KEY([PrimaryAssetId])
REFERENCES [dbo].[Asset] ([AssetId])
GO
ALTER TABLE [dbo].[Market] CHECK CONSTRAINT [FK_Market_PrimaryAsset]
GO
