
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio3_Agenda_Odontológico
{
    /// <summary>
    /// Possui a listagem dos pacientes cadastrados e a listagem da consulta agendadas para cada paciente.
    /// </summary>
    public class PacienteAgenda
    {

        private List<Paciente> listadepacientes;
        public List<Paciente> ListaDePacientes { get { return listadepacientes; } }

        private List<Consulta> agenda;
        public List<Consulta> Agenda { get { return agenda; } }

        public PacienteAgenda()
        {
            try
            {
                Console.WriteLine("Carregando...");
                listadepacientes = new List<Paciente>();
                agenda = new List<Consulta>();

                var contexto = new ConsultorioContext();
                agenda = contexto.Consultas.ToList();
                listadepacientes = contexto.Pacientes.ToList();
            }
            catch (Microsoft.Data.SqlClient.SqlException)
            {
                Console.Clear();
                Console.WriteLine("Erro: Não foi possivel conectar ao Banco de Dados");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Environment.Exit(1);
            }


        }
        /// <summary>
        /// Adiciona um paciente e seus referidos dados na listagem de pacientes
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool AddPaciente(Paciente paciente)
        {
            Paciente _paciente = listadepacientes.FirstOrDefault(w => !string.IsNullOrEmpty(w.Cpf) && w.Cpf.Equals(paciente.Cpf));

            if(_paciente == null)
            {
                this.listadepacientes.Add(paciente);
                return true;
            }
            if(paciente.Cpf == _paciente.Cpf)
            {
                return false;
            }
            this.listadepacientes.Add(paciente);
            return true;
        }
        /// <summary>
        /// Remove um paciente da listagem de pacientes
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool RemovePaciente(Paciente paciente)
        {
            return listadepacientes.Remove(paciente);
        }
        /// <summary>
        /// Método para retornar o paciente da lista que possui o CPF igual a desejado.
        /// </summary>
        /// <param name="_cpf"></param>
        /// <returns></returns>
        public Paciente GetPaciente(string _cpf)
        {
            Paciente _paciente = listadepacientes.FirstOrDefault(w => !string.IsNullOrEmpty(w.Cpf) && w.Cpf.Equals(_cpf));

            return _paciente;
        }
        /// <summary>
        /// Verifica se o horário que o paciente deseja marcar a consulta está disponível.
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public bool CheckHorario(Consulta consulta)
        {

            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Data != consulta.Data)
                {
                    return true;
                }
                else if (DateTime.Parse(agenda[i].HoraInicial) == DateTime.Parse(consulta.HoraInicial))
                {
                    return false;
                }
                else if (((DateTime.Parse(agenda[i].HoraInicial) > DateTime.Parse(consulta.HoraInicial)) && (DateTime.Parse(agenda[i].HoraFinal) == DateTime.Parse(consulta.HoraFinal))))
                {
                    return false;
                }
                else if (((DateTime.Parse(agenda[i].HoraInicial) < DateTime.Parse(consulta.HoraInicial)) && (DateTime.Parse(agenda[i].HoraFinal) > DateTime.Parse(consulta.HoraFinal))))
                {
                    return false;
                }
                else if (((DateTime.Parse(agenda[i].HoraInicial) > DateTime.Parse(consulta.HoraInicial)) && (DateTime.Parse(agenda[i].HoraFinal) < DateTime.Parse(consulta.HoraFinal))))
                {
                    return false;
                }
                else if ((DateTime.Parse(agenda[i].HoraInicial) == DateTime.Parse(consulta.HoraFinal) && (DateTime.Parse(agenda[i].HoraFinal) >  DateTime.Parse(consulta.HoraFinal))))
                {
                    return true;
                }
            }
            return true;
        }
        /// <summary>
        /// Ao excluir um paciente do cadastro verifica se o mesmo possui alguma consulta agendada, caso possua alguma consulta futura não permite que o paciente seja excluído
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PacienteTemAgendamento(Paciente paciente)
        {
            DateTime checkData = DateTime.Now;
            DateTime checkDataConsulta;
            for (int i = 0; i < agenda.Count; i++)
            {
                DateTime.TryParse(agenda[i].Data, out checkDataConsulta);
                if (agenda[i].PacienteCPF.Equals(paciente.Cpf) && checkDataConsulta > checkData)
                {
                    throw new Exception("Erro: paciente está agendado para " +
                        agenda[i].Data + " as " +
                        agenda[i].HoraInicial.PadLeft(4, ' ') + "h.");
                }
            }

            return false;
        }
        /// <summary>
        /// Verifica se o paciente já possui consulta marcada, caso a consulta seja futura não permite o agendamento de outra consulta.
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public bool PodeAgendar(Consulta consulta)
        {
            DateTime checkData = DateTime.Now;
            DateTime checkDataConsulta;
            for (int i = 0; i < agenda.Count; i++)
            {
                DateTime.TryParse(agenda[i].Data, out checkDataConsulta);

                if (agenda[i].PacienteCPF.Equals(consulta.PacienteCPF) && checkDataConsulta > checkData && DateTime.Parse(consulta.Data) >= checkDataConsulta)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Adiciona uma nova consulta para um paciente caso ele esteja cadastrado.
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public bool AddAgendamento(Paciente paciente, Consulta consulta)
        {
            if (!listadepacientes.Contains(paciente))
            {
                return false;
            }
            this.agenda.Add(consulta);
            return true;
        }
        /// <summary>
        /// Remove um agendamento feito por um paciente.
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="consulta"></param>
        /// <param name="_horainicial"></param>
        /// <returns></returns>
        public bool RemoveAgendamento(Consulta consulta)
        {
             return agenda.Remove(consulta);
        }
        /// <summary>
        ///Método para retornar uma consulta da lista que possui Data igual
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public Consulta GetConsulta(string _data)
        {
            Consulta _consulta = agenda.FirstOrDefault(w => !string.IsNullOrEmpty(w.Data) &&  w.Data.Equals(_data));

            return _consulta;
        }
    }
}
