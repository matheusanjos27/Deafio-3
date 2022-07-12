using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Desafio3_Agenda_Odontológico;

namespace Desafio3_Agenda_Odontológico
{
    public class Consulta
    {
        /// <summary>
        /// A consulta possui as propriedades Objeto Paciente, Data, Hora Inicial, Hora Final e Tempo da Consulta.
        /// </summary>
        /// 
        public int Id { get; internal set; }

        public string PacienteCPF { get; set; }

        public string Data { get; set; }

        public string HoraInicial { get; set; }

        public string HoraFinal { get; set; }

        public string Tempo { get; set; }

        public DateTime HoraInicialDT { get { return DateTime.Parse(HoraInicial); } }
        public DateTime HoraFinalDT { get { return DateTime.Parse(HoraFinal); } }

        public TimeSpan TempoTS
        {
            get
            {
                return (this.HoraFinalDT - this.HoraInicialDT);
            }
        }

        public Consulta() { }

        public Consulta(string _strdata, string _horainicial, string _horafinal, string pacientecpf)
        {

            this.PacienteCPF = pacientecpf;
            this.Data = _strdata;
            this.HoraInicial = _horainicial;
            this.HoraFinal = _horafinal;


            this.Tempo = TempoTS.ToString();
        }
    }
}
