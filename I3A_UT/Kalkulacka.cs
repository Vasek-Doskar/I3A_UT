using System;
using System.Collections.Generic;
using System.Text;

namespace I3A_UT
{
    public class Kalkulacka
    {
        private string _name;
        public string Name => _name;
        public double Soucet(double a,double b) => a + b;

        public double Podil(double a,double b)
        {
            if (b == 0) throw new DivideByZeroException("Pokus o dělení nulou!");
            return a / b;
        }
        public void SetName(string name) => _name = name; 
    }
}
