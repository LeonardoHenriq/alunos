namespace Aluno.Domain
{
    public class Notas
    {
        public int IdNota { get; set; }
        public double ValorNota { get; set; }
        public DateTime Data { get; set; }

        public Alunos Aluno { get; set; }

        public Materias Materia { get; set; }
    }
}
