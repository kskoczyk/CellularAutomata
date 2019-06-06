using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedGrowth
{
    class Seed
    {
        static int seedsID = 0;
        public int ID { get; set; }
        public Color SeedColor { get; set; }
        public Seed(Color color)
        {
            SeedColor = color;
            ID = seedsID;
            seedsID++;
        }
    }
}
