USE [Appointments]
GO

/****** Object:  Table [dbo].[Pacient]    Script Date: 10/02/2020 6:25:18 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pacient](
	[IdKey] [int] IDENTITY(1,1) NOT NULL,
	[Id] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pacient]
ADD UNIQUE (Id);


