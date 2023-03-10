using Aluno.Domain;
using Aluno.Persistence.Contratos;
using Aluno.Persistence.Util;
using System.Data.SqlClient;

namespace Aluno.Persistence
{
    public class AlunoPersist : IAlunos
    {
        private Connection Conexao;
        private SqlCommand sql;
        private SqlDataReader dr;
        public void AddAluno(Alunos aluno, bool IncludeMaterias)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"INSERT INTO Alunos 
                                      (Nome,Email) VALUES
                                      (@Nome,@Email)
                                      SELECT @@IDENTITY Id");

                sql.Parameters.AddWithValue("@Nome", aluno.NomeAluno);
                sql.Parameters.AddWithValue("@Email", aluno.Email);

                dr = Conexao.GetDataReader(sql);

                if (dr.Read())
                    aluno.IdAluno = Convert.ToInt32(dr["Id"]);

                if (IncludeMaterias && aluno.IdAluno > 0)
                {
                    aluno.MateriasMatriculadas.ForEach(materias =>
                    {
                        AddMateriaForAluno(aluno.IdAluno, materias.IdMateria);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr?.Close();
                Conexao.Close();
            }
        }

        public void DeleteAluno(int id)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"BEGIN TRAN 

                                            BEGIN TRY
                                                DELETE FROM Notas WHERE IdAluno = @ID
                                            END TRY
                                            BEGIN CATCH 
                                                ROLLBACK TRAN
                                            END CATCH

                                            BEGIN TRY 
                                                DELETE FROM AlunosHasMaterias WHERE IdAluno = @ID
                                            END TRY
                                            BEGIN CATCH 
                                                ROLLBACK TRAN
                                            END CATCH

                                            BEGIN TRY 
                                                DELETE FROM Alunos WHERE Id = @ID
                                            END TRY
                                            BEGIN CATCH 
                                                ROLLBACK TRAN
                                            END CATCH
                                        COMMIT");
                sql.Parameters.AddWithValue("@ID", id);
                Conexao.Execute(sql);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conexao.Close();
            }
        }

        public void EditAluno(Alunos aluno, int id)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"UPDATE Alunos 
                                      SET Nome = @Nome ,Email = @Email
                                      WHERE Id = @Id");

                sql.Parameters.AddWithValue("@Nome", aluno.NomeAluno);
                sql.Parameters.AddWithValue("@Email", aluno.Email);
                sql.Parameters.AddWithValue("@Id", id);

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

        public void AddMateriaForAluno(int idAluno, int idMateria)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"INSERT INTO AlunosHasMaterias 
                                      (IdAluno,IdMateria) VALUES
                                      (@IdAluno,@IdMateria)");

                sql.Parameters.AddWithValue("@IdAluno", idAluno);
                sql.Parameters.AddWithValue("@IdMateria", idMateria);

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
        public void DeleteMateriaForAluno(int idAluno, int idMateria)
        {
            Conexao = new Connection();
            try
            {
                sql = new SqlCommand(@"DELETE FROM AlunosHasMaterias 
                                       WHERE IdAluno = @IdAluno AND IdMateria = @IdMateria");

                sql.Parameters.AddWithValue("@IdAluno", idAluno);
                sql.Parameters.AddWithValue("@IdMateria", idMateria);

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
