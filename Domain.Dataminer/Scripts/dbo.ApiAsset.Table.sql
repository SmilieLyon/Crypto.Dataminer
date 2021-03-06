USE [CryptoStore]
GO
/****** Object:  Table [dbo].[ApiAsset]    Script Date: 5/09/2017 11:52:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApiAsset](
	[ApiId] [int] NOT NULL,
	[AssetId] [int] NOT NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ApiAsset] PRIMARY KEY CLUSTERED 
(
	[ApiId] ASC,
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApiAsset]  WITH CHECK ADD  CONSTRAINT [FK_ApiAsset_Api] FOREIGN KEY([ApiId])
REFERENCES [dbo].[Api] ([ApiId])
GO
ALTER TABLE [dbo].[ApiAsset] CHECK CONSTRAINT [FK_ApiAsset_Api]
GO
ALTER TABLE [dbo].[ApiAsset]  WITH CHECK ADD  CONSTRAINT [FK_ApiAsset_Asset] FOREIGN KEY([AssetId])
REFERENCES [dbo].[Asset] ([AssetId])
GO
ALTER TABLE [dbo].[ApiAsset] CHECK CONSTRAINT [FK_ApiAsset_Asset]
GO
