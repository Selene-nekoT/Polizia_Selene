using System;
using System.Collections.Generic;
using System.Text;

namespace Polizia_Selene
{
    class Agente: Persona
    {
        public int IdAgente { get; }
        public DateTime DataDiNascita { get; }
        public int AnniDiServizio { get; }
        public Agente(int idAgente, string nome, string cognome, string codiceFiscale, DateTime dataDiNascita, int anniDiServizio )
        : base(nome, cognome, codiceFiscale)
        {
            IdAgente = idAgente;
            DataDiNascita = dataDiNascita;
            AnniDiServizio = anniDiServizio;
        }

        public void ProspettoAgente(Agente a)  //CREATO MA NON RIESCO A VISUALIZZARLO DAL MAIN
        {
            Console.WriteLine( $"{a.CodiceFiscale} - {a.Nome} {a.Cognome} - {a.AnniDiServizio} anni di servizio");
        }
    }
}
