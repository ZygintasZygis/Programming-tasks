using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISprogDerinimas
{
    internal class Siunta
    {
        public int Numeris {  get; set; }
        public int Laikas { get; set; }
        public int Prioritetas { get; set; }
        public int Svoris {  get; set; }
        public int Ivertis { get; set; }

        public Siunta(int numeris, int laikas, int prioritetas, int svoris, int ivertis)
        {
            this.Numeris = numeris;
            this.Laikas = laikas;
            this.Prioritetas = prioritetas;
            this.Svoris = svoris;
            this.Ivertis = ivertis;
        }
    }
}
