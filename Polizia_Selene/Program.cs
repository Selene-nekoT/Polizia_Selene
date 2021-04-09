using System;
using System.Collections.Generic;

namespace Polizia_Selene
{
    class Program
    {
        static void Main(string[] args)
        {

            //MOSTRA TUTTI GLI AGENTI
            Console.Write("-----MOSTRA TUTTI GLI AGENTI-------");

            List<Agente> elencoAgenti = DAL.MostraTuttiGliAgenti();
            foreach (Agente a in elencoAgenti)
            {
                // Agente.ProspettoAgente(a);
                Console.WriteLine($"{a.CodiceFiscale} - {a.Nome} {a.Cognome} - {a.AnniDiServizio} anni di servizio");
            }


            Console.Write("-----MOSTRA AGENTI ASSEGNATI PER AREA DATA-------");

            //MOSTRA GLI AGENTI ASSEGNATI AD AREA CON INPUT UTENTE
            string codiceArea = "NEPHE";
            List<Agente> AgentiPerArea = DAL.AgentiPerArea(codiceArea);
            foreach (Agente a in AgentiPerArea)
            {
                // Agente.ProspettoAgente(a);
                Console.WriteLine($"{a.CodiceFiscale} - {a.Nome} {a.Cognome} - {a.AnniDiServizio} anni di servizio");
            }


            Console.Write("------MOSTRA AGENTI CON DATI ANNI DI SERVIZIO------");

            //MOSTRA GLI AGENTI CON ANNI DI SERVIZIO >= INPUT UTENTE
            int inputUtente = 7;
            List<Agente> FiltroAgenti = DAL.FiltraAgenti(inputUtente);
            foreach (Agente a in FiltroAgenti)
            {
                // Agente.ProspettoAgente(a);
                Console.WriteLine($"{a.CodiceFiscale} - {a.Nome} {a.Cognome} - {a.AnniDiServizio} anni di servizio");
            }

            //INSERISCI AGENTE: l'equals restituisce un errore in classe persona quindi l'ho commentato
            //e qui l'ho lasciato così per runnarlo

            DAL.InserisciAgente("Charles", "Vane", "CHRVANF19530UJQ2", new DateTime(09 / 04 / 1995), 4);



            //Questa parte sarebbe implementabile per l'override dell'equals. Di prova l'ho fatto paragonando quello nuovo ad 
            //uno casuale ma andrebbe paragonato con tutti gli agenti presenti nel DB




            //Agente nuovoAgente = new Agente(0, "Charles", "Vane", "CHRVANF19530UJQ2", new DateTime(09 / 04 / 1995), 4);
            //Agente agenteParagone = new Agente(1, "James", "Karl", "CHRVANF19530UJQ2", new DateTime(59 / 14 / 1995), 4);

            //if (nuovoAgente.Equals(agenteParagone))
            //{
            //    Console.WriteLine("Persona già esistente");
            //}
            //else
            //{
            //    //Le persone sono diverse e posso aggiungerlo
            //    DAL.InserisciAgente("Charles", "Vane", "CHRVANF19530UJQ2", new DateTime(09 / 04 / 1995), 4);
            //    //Implementato come comando diretto ma sarebbe utile restituire il nuovo agente come OBJ

            //}



            Console.ReadLine();
        }
    }
}
