using Aluno.Domain;
using Aluno.Persistence.Contratos;
using Aluno.Persistence.Util;
using System.Data.SqlClient;

namespace Aluno.Persistence
{
    public class NotaPersist : INotas
    {
        private Connection Conexao;
        private SqlCommand sql;
        private SqlDataReader dr;
        public void AddNota(Notas nota)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"INSERT INTO Notas 
                                      (Nota,Data,IdAluno,IdMateria) VALUES
                                      (@Nota,@Data,@IdAluno,@IdMateria)");

                sql.Parameters.AddWithValue("@Nota", nota.ValorNota);
                sql.Parameters.AddWithValue("@Data", nota.Data);
                sql.Parameters.AddWithValue("@IdAluno", nota.Aluno.IdAluno);
                sql.Parameters.AddWithValue("@IdMateria", nota.Materia.IdMateria);

                dr = Conexao.GetDataReader(sql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
        }

        public void DeleteNota(int idNota)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"DELETE FROM Notas 
                                       WHERE Id = @Id");

                sql.Parameters.AddWithValue("@Id", idNota);

                Conexao.Execute(sql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
        }

        public void EditNota(Notas nota, int idNota)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"UPDATE Notas 
                                      SET Nota = @Nota, Data = @Data
                                      WHERE Id = @Id");

                sql.Parameters.AddWithValue("@Nota", nota.ValorNota);
                sql.Parameters.AddWithValue("@Data", nota.Data);
                sql.Parameters.AddWithValue("@Id", idNota);

                Conexao.Execute(sql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}
