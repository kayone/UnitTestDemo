CREATE TABLE [dbo].[Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Path] [varchar](1000) NOT NULL,
	[Size] [bigint] NOT NULL,
	[FileName] [varchar](50) NOT NULL
) ON [PRIMARY]