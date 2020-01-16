using System;
using System.Collections.Generic;
using System.Text;

namespace tomate_en_italien.util
{
    class Task
    {
        public int Id { get; set; }
        public String Libelle { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{ Libelle }";
        }
    }
}
