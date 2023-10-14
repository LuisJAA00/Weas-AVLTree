using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaEstructuras
{
    internal class Nodo
    {
        public int Data { get; set; }
        public String AulaID { get; set; }
        public String Edificio { get; set; }
        public Boolean Recursos { get; set; }
        public Nodo Left { get; set; }
        public Nodo Right { get; set; }
        public int Height { get; set; }


        public Nodo(int data, string aulaID, string edificio,Boolean recursos)
        {
            Data = data;
            AulaID = aulaID;
            Edificio = edificio;
            Recursos = recursos;

            Left = null;
            Right = null;
            Height = 1; // A new node has a height of 1 by default.
            
        }



        public int CompareTo(Nodo other)
        {
            if (other == null) return 1;

            return this.Data.CompareTo(other.Data);
        }

        public override string ToString()
        {
            return $"{AulaID} capacity: {Data} edificio: {Edificio}\nproyector: {Recursos}";
        }

    }
}
