



CREATE TABLE [dbo].[Escola](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](70) NOT NULL,
	[Logradouro] [varchar](70) NOT NULL,
	[Numero] [varchar](70) NOT NULL,
	[Complemento] [varchar](70) NULL,
	[Cidade] [varchar](70) NOT NULL,
	[Estado] [varchar](70) NOT NULL,
	[Telefone] [varchar](70) NULL,
	[Diretor] [varchar](70) NULL,
 CONSTRAINT [Escola_Id_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[Turma](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EscolaId] [int] NOT NULL,
	[Codigo] [varchar](70) NOT NULL,
	[Professor] [varchar](70) NOT NULL,
	[Descricao] [varchar](max) NULL,
 CONSTRAINT [Turma_Id_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


ALTER TABLE [dbo].[Turma]  WITH CHECK ADD  CONSTRAINT [Turma_Escola_FK] FOREIGN KEY([EscolaId])
REFERENCES [dbo].[Escola] ([Id])
GO
ALTER TABLE [dbo].[Turma] CHECK CONSTRAINT [Turma_Escola_FK]
GO



CREATE TABLE [dbo].[Configuracao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdiomaId] [int] NOT NULL
 CONSTRAINT [Configuracao_Id_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO


CREATE TABLE [dbo].[Idioma](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identificador] [varchar](10) NOT NULL,
	[Nome] [varchar](70) NOT NULL,
	[FlagName][varchar](70) NOT NULL,
 CONSTRAINT [Idioma_Id_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT [dbo].[Idioma] ([Identificador], [Nome], [FlagName]) VALUES (N'en-US', N'English', N'United-States.png')
INSERT [dbo].[Idioma] ([Identificador], [Nome], [FlagName]) VALUES (N'pt-BR', N'Português', N'Brazil.png')
GO

DECLARE @IdiomaId INT=(SELECT ID FROM Idioma WHERE Identificador='pt-BR')

INSERT [dbo].[Configuracao] ([IdiomaId]) VALUES (@IdiomaId)
GO


ALTER TABLE [dbo].[Configuracao]  WITH CHECK ADD  CONSTRAINT [Configuracao_Idioma_FK] FOREIGN KEY([IdiomaId])
REFERENCES [dbo].[Idioma] ([Id])
GO
ALTER TABLE [dbo].[Configuracao] CHECK CONSTRAINT [Configuracao_Idioma_FK]
GO


CREATE TABLE [dbo].[Aluno](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TurmaId] [int] NOT NULL,
	[Nome] [varchar](70) NOT NULL,
	[Email] [varchar](70) NULL,
	[Telefone] [varchar](70) NULL
 CONSTRAINT [Aluno_Id_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




ALTER TABLE [dbo].[Aluno]  WITH CHECK ADD  CONSTRAINT [Aluno_Turma_FK] FOREIGN KEY([TurmaId])
REFERENCES [dbo].[Turma] ([Id])
GO
ALTER TABLE [dbo].[Aluno] CHECK CONSTRAINT [Aluno_Turma_FK]
GO

