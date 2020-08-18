using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApplicationService
{
    public class AmigoServices
    {
        private AmigoRepository Repository { get; set; }
        public AmigoServices()
        {
            this.Repository = new AmigoRepository();
        }
        public List<Amigo> GetAll()
        {
            return Repository.GetAll();
        }

        public Amigo GetAmigoById(Guid id)
        {
            return Repository.GetAmigoById(id);
        }

        public void Save(Amigo amigo)
        {
            if (this.GetAmigoByEmail(amigo.Email) != null)
            {
                throw new Exception("Email Existente! Já existe um amigo com este email, por favor cadastre outro email");
            }

            var anoAtual = DateTime.Now.Year;
            if ((anoAtual - amigo.DataNascimento.Date.Year) < 18)
            {
                throw new Exception("Para se cadastrar é necessário ser maior de 18 anos");
            }

            amigo.Status = Status.EM_CONFIRMACAO_EMAIL;
            amigo.Id = Guid.NewGuid();
            this.Repository.Save(amigo);
        }

        public void Update(Guid id, Amigo amigo)
        {
            throw new NotImplementedException();
        }

        public Amigo GetAmigoByEmail(string email)
        {
            return Repository.GetAmigoByEmail(email);
        }

        public Amigo GetAmigoByData(DateTime dataNascimento)
        {
            return Repository.GetAmigoByData(dataNascimento);
        }

        public void Delete(Guid id)
        {
            Repository.Delete(id);
        }

        public void Update(Amigo amigo)
        {
            throw new NotImplementedException();
        }
    }
}
