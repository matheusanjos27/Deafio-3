using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio3_Agenda_Odontológico
{
    /// <summary>
    /// O paciente possui as propriedades Nome, CPF, Data de nascimento e Idade.
    /// </summary>
    public class Paciente
    {
        public int Id { get; internal set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string DataNasc { get; set; }


        public int Idade { get; set; }

        public Paciente() { }

        public Paciente(string _nome, string _cpf, string _datanasc)
        {
            this.Nome = _nome;
            this.Cpf = _cpf;
            this.DataNasc = _datanasc;
            this.Idade = this.CalcularIdade(_datanasc);
        }
        
        /// <summary>
        /// Calcula a idade o paciente a partir da data de nascimento e a data atual.
        /// </summary>
        /// <param name = "_datanasc" ></ param >
        /// < returns ></ returns >
        public int CalcularIdade(string _datanasc)
        {
            DateTime hoje = DateTime.Now;
            DateTime idadedata;

            DateTime.TryParse(_datanasc, out idadedata);
            return hoje.Year - idadedata.Year;
        }
        /// <summary>
        /// Verifica se dois pacientes são iguais pelo CPF.
        /// </summary>
        /// <param name = "paciente" ></ param >
        /// < returns ></ returns >
        /// < exception cref= "Exception" ></ exception >
        public bool PacienteIgual(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado!");
            }
            if (this.Cpf.Equals(paciente.Cpf))
            {
                return true;
            }
            return false;
        }

    }
}
