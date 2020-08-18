using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Repository
{
    public class AmigoRepository
    {
        private string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Amigos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Amigo> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                return connection.Query<Amigo>("SELECT * FROM AMIGO").ToList();
            }
        }

        public Amigo GetAmigoById(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Amigo>("SELECT * FROM AMIGO WHERE ID = @P1", new { P1 = id });
            }

        }

        public void Save(Amigo amigo)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Execute(@"INSERT INTO AMIGO(ID, NOME, SOBRENOME, EMAIL, DATANASCIMENTO, STATUS)
                                                    VALUES(@P1,@P2,@P3,@P4,@P5,@P6)
                ", new { P1 = amigo.Id, P2 = amigo.Nome, P3 = amigo.Sobrenome, P4 = amigo.Email, P5 = amigo.DataNascimento, P6 = amigo.Status });
            }

        }

        public void Update(Guid id, Amigo amigo)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Execute(@"UPDATE AMIGO(ID, NOME, SOBRENOME, EMAIL, DATANASCIMENTO, STATUS)
                                                    VALUES(@P1,@P2,@P3.@P4,@P5,@P6)
                ", new { P1 = amigo.Id, P2 = amigo.Nome, P3 = amigo.Sobrenome, P4 = amigo.Email, P5 = amigo.DataNascimento, P6 = amigo.Status });
            }

        }

        public Amigo GetAmigoByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Amigo>("SELECT * FROM AMIGO WHERE EMAIL = @P1", new { P1 = email });
            }

        }

        public Amigo GetAmigoByData(DateTime dataNascimento)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Amigo>("SELECT * FROM AMIGO WHERE DATANASCIMENTO = @P1", new { P1 = dataNascimento });
            }

        }


        public void Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            {
                connection.Execute("DELETE FREOM AMIGO WHERE ID = @P1", new { P1 = id });
            }

        }
    }


    /*    public class AmigoRepository
        {
            public static List<Amigo> Amigos { get; set; } = new List<Amigo>();

            public List<Amigo> GetAll()
            {
                return Amigos;
            }
            public Amigo GetAmigoById(Guid id)
            {
                return Amigos.FirstOrDefault(x => x.Id == id);
            }

            public Amigo GetAmigoByEmail(string email)
            {
                return Amigos.FirstOrDefault(x => x.Email == email);
            }

            public void Save(Amigo amigo)
            {
                Amigos.Add(amigo);
            }


    //        public void UpDate(Guid id, Amigo amigo)
    //        {
    //            Amigos.FirstOrDefault(x => x.Id == id);
    //        }
            public void Delete(Guid id)
            {
                var amigo = this.GetAmigoById(id);
                if (amigo != null)
                    Amigos.Remove(amigo);
            }
        }*/
}
