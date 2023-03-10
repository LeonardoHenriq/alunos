using Aluno.Domain;
using Aluno.Persistence.Util;

namespace Aluno.Persistence.Contratos
{
    public interface IAlunos
    {
        void AddAluno(Alunos aluno,bool IncludeMaterias);
        void EditAluno(Alunos aluno, int id);
        void DeleteAluno(int id);
        void AddMateriaForAluno(int idAluno,int idMateria);
        void DeleteMateriaForAluno(int idAluno, int idMateria);
    }
}
