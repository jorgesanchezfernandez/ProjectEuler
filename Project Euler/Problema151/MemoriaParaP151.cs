using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Euler.Problema151
{
    class MemoriaParaP151
    {
        public List<int> nodo { get; set; }

        public List<double> probabilityNodeToleafPer0SinglesSheets { get; set; }
        public List<double> probabilityNodeToleafPer1SinglesSheets { get; set; }
        public List<double> probabilityNodeToleafPer2SinglesSheets { get; set; }
        public List<double> probabilityNodeToleafPer3SinglesSheets { get; set; }


        public MemoriaParaP151()
        {
            this.nodo = new List<int>();
            this.probabilityNodeToleafPer0SinglesSheets = new List<double>();
            this.probabilityNodeToleafPer1SinglesSheets = new List<double>();
            this.probabilityNodeToleafPer2SinglesSheets = new List<double>();
            this.probabilityNodeToleafPer3SinglesSheets = new List<double>();
        }

        public MemoriaParaP151(List<int> nodo, List<double> probabilityNodeToleafPer0SinglesSheets, List<double> probabilityNodeToleafPer1SinglesSheets, List<double> probabilityNodeToleafPer2SinglesSheets, List<double> probabilityNodeToleafPer3SinglesSheets)
        {
            this.nodo = nodo;
            this.probabilityNodeToleafPer0SinglesSheets = probabilityNodeToleafPer0SinglesSheets;
            this.probabilityNodeToleafPer1SinglesSheets = probabilityNodeToleafPer1SinglesSheets;
            this.probabilityNodeToleafPer2SinglesSheets = probabilityNodeToleafPer2SinglesSheets;
            this.probabilityNodeToleafPer3SinglesSheets = probabilityNodeToleafPer3SinglesSheets;
        }
    }
}
