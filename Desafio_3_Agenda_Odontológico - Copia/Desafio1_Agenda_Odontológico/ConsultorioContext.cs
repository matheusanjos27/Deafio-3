using System;
using Microsoft.EntityFrameworkCore;


namespace Desafio3_Agenda_Odontológico
{
    class ConsultorioContext : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SOUTH\\SQLEXPRESS;Database=db_pacientes;Trusted_Connection=true;");
        }
    }
}
