using Aluno.Domain;
using Aluno.Persistence.Contratos;
using Aluno.Persistence.Util;
using System.Data.SqlClient;

namespace Aluno.Persistence
{
    public class MateriaPersist : IMaterias
    {
        private Connection Conexao;
        private SqlCommand sql;
        private SqlDataReader dr;
        public void AddMateria(Materias materia)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"INSERT INTO Materias 
                                      (Nome,Descricao) VALUES
                                      (@Nome,@Descricao)");

                sql.Parameters.AddWithValue("@Nome", materia.NomeMateria);
                sql.Parameters.AddWithValue("@Descricao", materia.Descricao);

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

        public void DeleteMateria(int idMateria)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"DELETE FROM Materias 
                                       WHERE Id = @Id");

                sql.Parameters.AddWithValue("@Id", idMateria);

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

        public void EditMateria(Materias materia, int idMateria)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"UPDATE Materias 
                                      SET Nome = @Nome ,Descricao = @Descricao
                                      WHERE Id = @Id");

                sql.Parameters.AddWithValue("@Nome", materia.NomeMateria);
                sql.Parameters.AddWithValue("@Email", materia.Descricao);
                sql.Parameters.AddWithValue("@Id", idMateria);

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
