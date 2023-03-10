CREATE TABLE [dbo].[Notas]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nota] FLOAT NOT NULL, 
    [Data] DATETIME NOT NULL, 
    [IdAluno] INT NOT NULL, 
    [IdMateria] INT NOT NULL, 
    CONSTRAINT [FK_Notas_Alunos] FOREIGN KEY ([IdAluno]) REFERENCES [Alunos]([Id]), 
    CONSTRAINT [FK_Notas_Materias] FOREIGN KEY ([IdMateria]) REFERENCES [Materias]([Id])
)
