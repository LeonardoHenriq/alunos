using Aluno.Domain;

namespace Aluno.Persistence.Contratos
{
    public interface IMaterias
    {
        void AddMateria(Materias materia);
        void EditMateria(Materias materia, int idMateria);
        void DeleteMateria(int idMateria);
    }
}
