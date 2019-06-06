using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedGrowth
{
    static class Rules
    {
        public static bool OpenBoundary { get; set; } = true;
        public static bool SquareCells { get; set; } = true;
        //TODO: a checkbox
        public static bool DrawGrid { get; set; } = true;
        public static bool DrawWeights { get; set; } = true;
        public static Random Random { get; set; } = new Random();
        //TODO: better handling
        public static bool RandomPentagonal { get; set; } = false;
        public static bool RandomHexagonal { get; set; } = false;
    }
}
