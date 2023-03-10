using Aluno.Domain;

namespace Aluno.Persistence.Contratos
{
    public interface INotas
    {
        void AddNota(Notas nota);
        void EditNota(Notas nota, int idNota);
        void DeleteNota(int idNota);
    }
}
