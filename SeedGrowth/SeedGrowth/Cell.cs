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
        public List<Seed> ConqueringSeeds { get; set; } = new List<Seed>();
        public List<int> ConqueringSeedsCount { get; set; } = new List<int>();
        public int Energy { get; set; } = -1;
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
            for (int i = 0; i < ConqueringSeeds.Count; i++)
            {
                if(seed.ID == ConqueringSeeds[i].ID)
                {
                    ConqueringSeedsCount[i]++;
                    isPresent = true;
                    break;
                }
            }
            if(!isPresent)
            {
                ConqueringSeeds.Add(seed);
                ConqueringSeedsCount.Add(1);
            }
            IsInitialized = true;
        }
        public void ChooseDominantSeed()
        {
            if(ConqueringSeeds.Count < 1)
            {
                throw new Exception("This cell has no conquering seeds!");
            }

            Seed dominant = ConqueringSeeds[0];
            int dominantCount = ConqueringSeedsCount[0];
            for (int i = 1; i < ConqueringSeeds.Count; i++)
            {
                if(ConqueringSeedsCount[i] > dominantCount)
                {
                    dominant = ConqueringSeeds[i];
                }
            }
            ParentSeed = dominant;
            IsInitialized = false;
            IsClaimed = true;
        }
        public int CalculateEnergy(Seed seed = null)
        {
            if(seed == null)
            {
                seed = ParentSeed;
            }

            int energy = 0;
            for (int i = 0; i < ConqueringSeeds.Count; i++)
            {
                if(seed.ID != ConqueringSeeds[i].ID)
                {
                    energy += ConqueringSeedsCount[i];
                }
            }
            return energy;
        }
        public void ChangeEnergyMonteCarlo()
        {
            int randomIndex;
            Seed randomSeed;
            while (true)
            {
                randomIndex = Rules.Random.Next(0, ConqueringSeeds.Count);
                randomSeed = ConqueringSeeds[randomIndex];
                if (randomSeed.ID == ParentSeed.ID && ConqueringSeeds.Count > 1)
                {
                    continue;
                }
                else break;
            }

            int newEnergy = CalculateEnergy(randomSeed);

            if(newEnergy <= Energy)
            {
                Energy = newEnergy;
                ParentSeed = randomSeed;
            }
            else
            {
                int delta = newEnergy - Energy;
                double test = -delta / Rules.Kt;
                double probability = Math.Exp(-delta / Rules.Kt);
                double chance = Rules.Random.NextDouble();
                if(chance <= probability)
                {
                    Energy = newEnergy;
                    ParentSeed = randomSeed;
                }
            }
        }
    }
}
