using System;
using System.Collections.Generic;
using System.Text;

namespace ConsAppCurrency
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Nominal { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Id + "\t| " + Name + "\t| " 
                + Code + "\t| " + Nominal + "\t| " 
                + Value + "\t| " + Date.ToShortDateString();
        }
    }
}
