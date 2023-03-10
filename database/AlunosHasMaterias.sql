CREATE TABLE [dbo].[AlunosHasMaterias]
(
	[IdAluno] INT NOT NULL, 
    [IdMateria] INT NOT NULL, 
    CONSTRAINT [FK_AlunosHasMaterias_Alunos] FOREIGN KEY ([IdAluno]) REFERENCES [Alunos]([Id]), 
    CONSTRAINT [FK_AlunosHasMaterias_Materias] FOREIGN KEY ([IdMateria]) REFERENCES [Materias]([Id]) 
)
