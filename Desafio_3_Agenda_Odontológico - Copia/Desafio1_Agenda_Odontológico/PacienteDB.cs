using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Desafio3_Agenda_Odontológico;


namespace Desafio3_Agenda_Odontológico
{
    public class PacienteDB : IDisposable
    {
        private SqlConnection conexao;

        public PacienteDB()
        {
            this.conexao = new SqlConnection("Server=SOUTH\\SQLEXPRESS;Database=db_pacientes;Trusted_Connection=true;");
            this.conexao.Open();
        }

        public void Dispose()
        {
            this.conexao.Close();
        }
    }
}