using System;
using System.Collections.Generic;
using System.Text;

namespace Polizia_Selene
{
    abstract class Persona
    {
        public string Nome { get; }
        public string Cognome { get; }
        public string CodiceFiscale { get; }
        public Persona(string nome, string cognome, string codiceFiscale)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceFiscale = codiceFiscale;
        }

        //public override bool Equals(Persona ps)
        //{
        //    if (ps.CodiceFiscale == this.CodiceFiscale)
        //        return false;

        //    return true;
        //}



    }
}
