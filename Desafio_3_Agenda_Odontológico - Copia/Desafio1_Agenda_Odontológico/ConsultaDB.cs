using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;


namespace Desafio3_Agenda_Odontológico
{
    public class ConsultaDB : IDisposable
    {
        private SqlConnection conexao;

        public ConsultaDB()
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