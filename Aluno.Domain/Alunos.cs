namespace Aluno.Domain
{
    public class Alunos
    {
        public int IdAluno { get; set; }
        public string NomeAluno { get; set; }
        public string Email { get; set; }
        public List<Materias> MateriasMatriculadas { get; set; }
    }
}
