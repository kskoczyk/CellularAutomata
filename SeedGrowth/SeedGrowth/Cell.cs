using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedGrowth
{
    class Cell
    {
        List<Seed> conqueringSeeds = new List<Seed>();
        List<int> conqueringSeedsCount = new List<int>();
        public PointF Weight { get; set; } = new Point(0, 0);
        public bool IsClaimed { get; set; } = false;
        public bool IsInitialized { get; set; } = false;
        public PointF Position { get; set; } = new Point(0, 0);
        public Seed ParentSeed { get; set; } = null;
        public Cell() { }
        public Cell(Seed seed)
        {
            ParentSeed = seed;
            Weight = new PointF((float)Rules.Random.NextDouble(), (float)Rules.Random.NextDouble());
        }
        public Cell(PointF position)
        {
            Position = position;
        }
        public void AddSeed(Seed seed)
        {
            bool isPresent = false;
            for (int i = 0; i < conqueringSeeds.Count; i++)
            {
                if(seed.ID == conqueringSeeds[i].ID)
                {
                    conqueringSeedsCount[i]++;
                    isPresent = true;
                    break;
                }
            }
            if(!isPresent)
            {
                conqueringSeeds.Add(seed);
                conqueringSeedsCount.Add(1);
            }
            IsInitialized = true;
        }
        public void ChooseDominantSeed()
        {
            if(conqueringSeeds.Count < 1)
            {
                throw new Exception("This cell has no conquering seeds!");
            }

            Seed dominant = conqueringSeeds[0];
            int dominantCount = conqueringSeedsCount[0];
            for (int i = 1; i < conqueringSeeds.Count; i++)
            {
                if(conqueringSeedsCount[i] > dominantCount)
                {
                    dominant = conqueringSeeds[i];
                }
            }
            ParentSeed = dominant;
            IsInitialized = false;
            IsClaimed = true;
        }
    }
}
