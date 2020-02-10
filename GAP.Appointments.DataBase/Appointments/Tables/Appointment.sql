USE [Appointments]
GO

/****** Object:  Table [dbo].[Appointment]    Script Date: 10/02/2020 6:25:53 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Appointment](
	[IdKey] [int] IDENTITY(1,1) NOT NULL,
	[IdKeyPacient] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[IdType] [int] NOT NULL,
	[IdState] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([IdKeyPacient])
REFERENCES [dbo].[Pacient] ([IdKey])
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([IdState])
REFERENCES [dbo].[State] ([IdKey])
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD FOREIGN KEY([IdType])
REFERENCES [dbo].[Type] ([IdKey])
GO


