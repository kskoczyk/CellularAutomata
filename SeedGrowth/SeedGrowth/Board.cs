using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeedGrowth
{
    class Board
    {
        Random random = Rules.Random;

        public List<List<Cell>> cells;
        public List<Seed> Seeds { get; set; } = new List<Seed>();
        // changes accordingly to the rules, no more than 8
        public List<bool> NeighbourPattern { get; set; } = new List<bool>() { false, false, false, false, false, false, false, false };
        public int SizeX { get; set; } = 0;
        public int SizeY { get; set; } = 0;
        public int DeadCells { get; set; } = 0;
        public double DimX { get; set; } = 0;
        public double DimY { get; set; } = 0;
        public bool IsInitialized { get; set; } = false;

        public Board()
        {
            Seeds.Add((new Seed(Color.White)));
        }
        public void CreateCells()
        {
            Rules.IsSeeded = false;
            cells = new List<List<Cell>>();
            for (int i = 0; i < SizeY; i++)
            {
                cells.Add(new List<Cell>());
                for (int j = 0; j < SizeX; j++)
                {
                    cells[i].Add(new Cell(Seeds[0]));
                }
            }
            DeadCells = SizeX * SizeY;
            IsInitialized = true;
        }
        public void DrawBoard(PictureBox pb, Graphics g, Bitmap bm)
        {
            if (cells == null)
            {
                return;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    g.FillRectangle(new SolidBrush(cell.ParentSeed.SeedColor), (float)(j * DimX), (float)(i * DimY), (float)DimX, (float)DimY);
                    if (Rules.DrawWeights)
                    {
                        int thickness = 3;
                        g.FillRectangle(new SolidBrush(Color.Red), (float)((cell.Weight.X + j) * DimX - thickness / 2), (float)((cell.Weight.Y + i) * DimY - thickness / 2), (float)thickness, (float)thickness);
                    }
                }
            }

            if (Rules.DrawGrid)
            {
                Pen pen = new Pen(Color.Black);
                // horizontal
                for (int i = 0; i <= SizeY; i++)
                {
                    g.DrawLine(pen, 0, (float)(i * DimY), (float)(SizeX * DimX), (float)(i * DimY));
                }

                // vertical
                for (int j = 0; j <= SizeX; j++)
                {
                    g.DrawLine(pen, (float)(j * DimX), 0, (float)(j * DimX), (float)(SizeY * DimY));
                }
            }

            pb.Image = bm;
        }
        public void DrawEnergy(PictureBox pb, Graphics g, Bitmap bm)
        {
            if (!Rules.IsSeeded)
            {
                return;
            }
            int maxEnergy = 0;
            for (int i = 0; i < NeighbourPattern.Count; i++)
            {
                if (NeighbourPattern[i] == true)
                {
                    maxEnergy++;
                }
            }

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    double intensity = Map(cell.Energy, 0, maxEnergy, 0, 255);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255 - (int)intensity, 255, 255)), (float)(j * DimX), (float)(i * DimY), (float)DimX, (float)DimY);
                }
            }

            pb.Image = bm;
        }
        public void DrawEnergyRadius(PictureBox pb, Graphics g, Bitmap bm, int radius)
        {
            if (!Rules.IsSeeded)
            {
                return;
            }
            //unsafe
            int maxEnergy = (int)Math.Pow(1 + 2 * (double)radius, 2) - 1;

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    double intensity = Map(cell.Energy, 0, maxEnergy, 0, 255);
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255 - (int)intensity, 255, 255)), (float)(j * DimX), (float)(i * DimY), (float)DimX, (float)DimY);
                }
            }

            pb.Image = bm;
        }
        public double Map(double value, double fromSource, double toSource, double fromTarget, double toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }
        public void GenerateSeeds(int count)
        {
            Seeds.RemoveRange(1, Seeds.Count - 1);
            for (int i = 0; i < count; i++)
            {
                Seeds.Add(new Seed(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))));
            }
        }
        public void DistributeSeeds(Label errorLabel)
        {
            int toDistribute = Seeds.Count - 1; // don't count the "0" one
            int i = 1;
            while (toDistribute > 0)
            {
                if (DeadCells < 1)
                {
                    errorLabel.Text = "Cannot generate more seeds!";
                    break;
                }
                int randomY = random.Next(0, SizeY);
                int randomX = random.Next(0, SizeX);

                if (!cells[randomY][randomX].IsClaimed)
                {
                    Cell cell = cells[randomY][randomX];
                    cell.ParentSeed = Seeds[i];
                    cell.IsClaimed = true;
                    DeadCells--;
                    toDistribute--;
                    i++;
                }
            }
            if (toDistribute == 0)
            {
                errorLabel.Text = "OK";
            }
        }
        public void DistributeSeedsHomogenous(int x, int y, Label errorLabel)
        {
            int dx = SizeX / x;
            int dy = SizeY / y;
            if (dx == 0 || dy == 0)
            {
                errorLabel.Text = "Too packed!";
            }
            else
            {
                errorLabel.Text = "OK";
            }

            int seed = 1;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Cell cell = cells[dy / 2 + i * dy][dx / 2 + j * dx];
                    if (!cell.IsClaimed)
                    {
                        cell.ParentSeed = Seeds[seed];
                        cell.IsClaimed = true;
                        DeadCells--;
                        seed++;
                    }
                }
            }
        }
        public void DistributeSeedsRadius(int radius, Label errorLabel, int attempts = 100)
        {
            int toDistribute = Seeds.Count - 1; // don't count the "0" one
            int failures = 0;
            int seed = 1;
            List<Point> distributed = new List<Point>();
            while (toDistribute > 0 && failures < attempts)
            {
                if (DeadCells < 1)
                {
                    //TODO: throw some error that it's impossible to generate more
                    break;
                }
                int randomY = random.Next(0, SizeY);
                int randomX = random.Next(0, SizeX);

                if (!cells[randomY][randomX].IsClaimed)
                {
                    Cell cell = cells[randomY][randomX];
                    Point cellCoords = new Point(randomX, randomY);

                    bool readyToDistribute = true;
                    for (int i = 0; i < distributed.Count; i++)
                    {
                        if (Math.Pow(cellCoords.X - distributed[i].X, 2) + Math.Pow(cellCoords.Y - distributed[i].Y, 2) < Math.Pow(radius, 2))
                        {
                            readyToDistribute = false;
                        }
                        if (Rules.OpenBoundary)
                        {
                            if (Math.Pow(cellCoords.X + SizeX - distributed[i].X, 2) + Math.Pow(cellCoords.Y + SizeY - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            if (Math.Pow(cellCoords.X - SizeX - distributed[i].X, 2) + Math.Pow(cellCoords.Y - SizeY - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            if (Math.Pow(cellCoords.X + SizeX - distributed[i].X, 2) + Math.Pow(cellCoords.Y - SizeY - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            if (Math.Pow(cellCoords.X - SizeX - distributed[i].X, 2) + Math.Pow(cellCoords.Y + SizeY - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            //
                            if (Math.Pow(cellCoords.X + SizeX - distributed[i].X, 2) + Math.Pow(cellCoords.Y - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            if (Math.Pow(cellCoords.X - SizeX - distributed[i].X, 2) + Math.Pow(cellCoords.Y - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            if (Math.Pow(cellCoords.X - distributed[i].X, 2) + Math.Pow(cellCoords.Y - SizeY - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                            if (Math.Pow(cellCoords.X - distributed[i].X, 2) + Math.Pow(cellCoords.Y + SizeY - distributed[i].Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToDistribute = false;
                            }
                        }
                    }

                    if (readyToDistribute)
                    {
                        distributed.Add(cellCoords);
                        cell.ParentSeed = Seeds[seed];
                        cell.IsClaimed = true;
                        DeadCells--;
                        seed++;
                        toDistribute--;
                        failures = 0;
                    }
                    else
                    {
                        failures++;
                    }
                }
            }

            if (toDistribute > 0)
            {
                errorLabel.Text = "Couldn't assign all the seeds!";
            }
            else
            {
                errorLabel.Text = "OK";
            }
        }
        /* Neighbourpattern:
         * 0   1   2
         * 7   X   3
         * 6   5   4
         */
        public void IterateAll()
        {
            int attempts = 0;
            int maxAttempts = 5;
            while (true)
            {
                int last = DeadCells;
                Iterate();
                if (last == DeadCells)
                {
                    if (attempts > maxAttempts)
                    {
                        break;
                    }
                    else
                    {
                        attempts++;
                    }
                }
                else
                {
                    attempts = 0;
                }
            }
            Rules.IsSeeded = true;
        }
        public void IterateAllWithRadius(int radius)
        {
            while (true)
            {
                int last = DeadCells;
                IterateWithRadius(radius);
                if (last == DeadCells)
                {
                    break;
                }
            }
            Rules.IsSeeded = true;
        }
        public void Iterate()
        {
            if (!IsInitialized)
            {
                return;
            }

            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    if (Rules.RandomPentagonal)
                    {
                        SetRandomPentagonal();
                    }
                    if (Rules.RandomHexagonal)
                    {
                        SetRandomHexagonal();
                    }
                    Cell cell = cells[i][j];
                    // alive cell's nucleation
                    if (cell.IsClaimed)
                    {
                        if (i > 0)
                        {
                            if (j > 0)
                            {
                                if (!cells[i - 1][j - 1].IsClaimed && NeighbourPattern[0])
                                {
                                    cells[i - 1][j - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[i - 1][SizeX - 1].IsClaimed && NeighbourPattern[0])
                                {
                                    cells[i - 1][SizeX - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            if (!cells[i - 1][j].IsClaimed && NeighbourPattern[1])
                            {
                                cells[i - 1][j].AddSeed(cell.ParentSeed);
                                DeadCells--;
                            }
                            if (j < SizeX - 1)
                            {
                                if (!cells[i - 1][j + 1].IsClaimed && NeighbourPattern[2])
                                {
                                    cells[i - 1][j + 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[i - 1][0].IsClaimed && NeighbourPattern[2])
                                {
                                    cells[i - 1][0].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (j > 0)
                            {
                                if (!cells[SizeY - 1][j - 1].IsClaimed && NeighbourPattern[0])
                                {
                                    cells[SizeY - 1][j - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[SizeY - 1][SizeX - 1].IsClaimed && NeighbourPattern[0])
                                {
                                    cells[SizeY - 1][SizeX - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            if (!cells[SizeY - 1][j].IsClaimed && NeighbourPattern[1])
                            {
                                cells[SizeY - 1][j].AddSeed(cell.ParentSeed);
                                DeadCells--;
                            }
                            if (j < SizeX - 1)
                            {
                                if (!cells[SizeY - 1][j + 1].IsClaimed && NeighbourPattern[2])
                                {
                                    cells[SizeY - 1][j + 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[SizeY - 1][0].IsClaimed && NeighbourPattern[2])
                                {
                                    cells[SizeY - 1][0].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                        }
                        // i in the middle - always valid
                        if (true)
                        {
                            if (j > 0)
                            {
                                if (!cells[i][j - 1].IsClaimed && NeighbourPattern[7])
                                {
                                    cells[i][j - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[i][SizeX - 1].IsClaimed && NeighbourPattern[7])
                                {
                                    cells[i][SizeX - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            if (j < SizeX - 1)
                            {
                                if (!cells[i][j + 1].IsClaimed && NeighbourPattern[3])
                                {
                                    cells[i][j + 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[i][0].IsClaimed && NeighbourPattern[3])
                                {
                                    cells[i][0].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                        }
                        if (i < SizeY - 1)
                        {
                            if (j > 0)
                            {
                                if (!cells[i + 1][j - 1].IsClaimed && NeighbourPattern[6])
                                {
                                    cells[i + 1][j - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[i + 1][SizeX - 1].IsClaimed && NeighbourPattern[6])
                                {
                                    cells[i + 1][SizeX - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            if (!cells[i + 1][j].IsClaimed && NeighbourPattern[5])
                            {
                                cells[i + 1][j].AddSeed(cell.ParentSeed);
                                DeadCells--;
                            }
                            if (j < SizeX - 1)
                            {
                                if (!cells[i + 1][j + 1].IsClaimed && NeighbourPattern[4])
                                {
                                    cells[i + 1][j + 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[i + 1][0].IsClaimed && NeighbourPattern[4])
                                {
                                    cells[i + 1][0].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (j > 0)
                            {
                                if (!cells[0][j - 1].IsClaimed && NeighbourPattern[6])
                                {
                                    cells[0][j - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[0][SizeX - 1].IsClaimed && NeighbourPattern[6])
                                {
                                    cells[0][SizeX - 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            if (!cells[0][j].IsClaimed && NeighbourPattern[5])
                            {
                                cells[0][j].AddSeed(cell.ParentSeed);
                                DeadCells--;
                            }
                            if (j < SizeX - 1)
                            {
                                if (!cells[0][j + 1].IsClaimed && NeighbourPattern[4])
                                {
                                    cells[0][j + 1].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                            else if (Rules.OpenBoundary)
                            {
                                if (!cells[0][0].IsClaimed && NeighbourPattern[4])
                                {
                                    cells[0][0].AddSeed(cell.ParentSeed);
                                    DeadCells--;
                                }
                            }
                        }
                    }
                }
            }

            // alive cell's growth
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    Cell cell = cells[i][j];
                    if (cell.IsInitialized)
                    {
                        cell.ChooseDominantSeed();
                    }
                }
            }
        }
        //TODO: make private
        public void IterateWithRadius(int radius)
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    Cell cell = cells[i][j];
                    if (cell.IsClaimed)
                    {
                        //square area around cell
                        for (int icell = 0; icell <= radius; icell++)
                        {
                            int indexY = i + icell;
                            if (indexY > SizeY - 1)
                            {
                                if (Rules.OpenBoundary)
                                {
                                    indexY = indexY - SizeY;
                                }
                                else break;
                            }

                            for (int jcell = 0; jcell <= radius; jcell++)
                            {
                                int indexX = j + jcell;
                                if (indexX > SizeX - 1)
                                {
                                    if (Rules.OpenBoundary)
                                    {
                                        indexX = indexX - SizeX;
                                    }
                                    else break;
                                }

                                if (!cells[indexY][indexX].IsClaimed)
                                {
                                    //PointF cellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    //cells[indexY][indexX].IsClaimed = true;
                                    PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                                    bool readyToBeClaimed = false;
                                    if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                    {
                                        readyToBeClaimed = true;
                                    }
                                    if (Rules.OpenBoundary)
                                    {
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        //
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                    }
                                    if (readyToBeClaimed)
                                    {
                                        cells[indexY][indexX].AddSeed(cell.ParentSeed);
                                        DeadCells--;
                                    }
                                }
                            }
                            for (int jcell = 0; jcell >= -radius; jcell--)
                            {
                                int indexX = j + jcell;
                                if (indexX < 0)
                                {
                                    if (Rules.OpenBoundary)
                                    {
                                        indexX = indexX + SizeX;
                                    }
                                    else break;
                                }

                                if (!cells[indexY][indexX].IsClaimed)
                                {
                                    //PointF cellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    //cells[indexY][indexX].IsClaimed = true;
                                    PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                                    bool readyToBeClaimed = false;
                                    if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                    {
                                        readyToBeClaimed = true;
                                    }
                                    if (Rules.OpenBoundary)
                                    {
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        //
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                    }
                                    if (readyToBeClaimed)
                                    {
                                        cells[indexY][indexX].AddSeed(cell.ParentSeed);
                                        DeadCells--;
                                    }
                                }
                            }
                        }
                        for (int icell = 0; icell >= -radius; icell--)
                        {
                            int indexY = i + icell;
                            if (indexY < 0)
                            {
                                if (Rules.OpenBoundary)
                                {
                                    indexY = indexY + SizeY;
                                }
                                else break;
                            }

                            for (int jcell = 0; jcell <= radius; jcell++)
                            {
                                int indexX = j + jcell;
                                if (indexX > SizeX - 1)
                                {
                                    if (Rules.OpenBoundary)
                                    {
                                        indexX = indexX - SizeX;
                                    }
                                    else break;
                                }

                                if (!cells[indexY][indexX].IsClaimed)
                                {
                                    //PointF cellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    //cells[indexY][indexX].IsClaimed = true;
                                    PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                                    bool readyToBeClaimed = false;
                                    if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                    {
                                        readyToBeClaimed = true;
                                    }
                                    if (Rules.OpenBoundary)
                                    {
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        //
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                    }
                                    if (readyToBeClaimed)
                                    {
                                        cells[indexY][indexX].AddSeed(cell.ParentSeed);
                                        DeadCells--;
                                    }
                                }
                            }
                            for (int jcell = 0; jcell >= -radius; jcell--)
                            {
                                int indexX = j + jcell;
                                if (indexX < 0)
                                {
                                    if (Rules.OpenBoundary)
                                    {
                                        indexX = indexX + SizeX;
                                    }
                                    else break;
                                }

                                if (!cells[indexY][indexX].IsClaimed)
                                {
                                    //PointF cellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    //cells[indexY][indexX].IsClaimed = true;
                                    PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                                    PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                                    bool readyToBeClaimed = false;
                                    if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                    {
                                        readyToBeClaimed = true;
                                    }
                                    if (Rules.OpenBoundary)
                                    {
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        //
                                        if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                        if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                        {
                                            readyToBeClaimed = true;
                                        }
                                    }
                                    if (readyToBeClaimed)
                                    {
                                        cells[indexY][indexX].AddSeed(cell.ParentSeed);
                                        DeadCells--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // alive cell's growth
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    Cell cell = cells[i][j];
                    if (cell.IsInitialized)
                    {
                        cell.ChooseDominantSeed();
                    }
                }
            }
        }
        public void SetMoore()
        {
            for (int i = 0; i < NeighbourPattern.Count; i++)
            {
                NeighbourPattern[i] = true;
            }
        }
        public void SetVonNeumann()
        {
            NeighbourPattern = new List<bool> { false, true, false, true, false, true, false, true };
        }
        public void SetHexagonalLeft()
        {
            NeighbourPattern = new List<bool> { false, true, true, true, false, true, true, true };
        }
        public void SetHexagonalRight()
        {
            NeighbourPattern = new List<bool> { true, true, false, true, true, true, false, true };
        }
        //TODO: true random
        public void SetRandomHexagonal()
        {
            int choice = Rules.Random.Next(2);
            if (choice == 0)
            {
                SetHexagonalLeft();
            }
            else
            {
                SetHexagonalRight();
            }
        }
        public void SetPentagonalTop()
        {
            NeighbourPattern = new List<bool> { true, true, true, true, false, false, false, true };
        }
        public void SetPentagonalBottom()
        {
            NeighbourPattern = new List<bool> { false, false, false, true, true, true, true, true };
        }
        public void SetPentagonalLeft()
        {
            NeighbourPattern = new List<bool> { true, true, false, false, false, true, true, true };
        }
        public void SetPentagonalRight()
        {
            NeighbourPattern = new List<bool> { false, true, true, true, true, true, false, false };
        }
        public void SetRandomPentagonal()
        {
            int choice = Rules.Random.Next(1, 5);
            if (choice == 1)
            {
                SetPentagonalTop();
            }
            else if (choice == 2)
            {
                SetPentagonalBottom();
            }
            else if (choice == 3)
            {
                SetPentagonalLeft();
            }
            else
            {
                SetPentagonalRight();
            }
        }
        public void ClearNeighbours()
        {
            if(!Rules.IsSeeded)
            {
                return;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    cell.ConqueringSeeds = new List<Seed>();
                    cell.ConqueringSeedsCount = new List<int>();
                }
            }
        }
        public void DistributeEnergy()
        {
            if (!Rules.IsSeeded)
            {
                return;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    if (i > 0)
                    {
                        if (j > 0)
                        {
                            if (NeighbourPattern[0])
                            {
                                cells[i - 1][j - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[0])
                            {
                                cells[i - 1][SizeX - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        if (NeighbourPattern[1])
                        {
                            cells[i - 1][j].AddSeed(cell.ParentSeed);
                        }
                        if (j < SizeX - 1)
                        {
                            if (NeighbourPattern[2])
                            {
                                cells[i - 1][j + 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[2])
                            {
                                cells[i - 1][0].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                    else if (Rules.OpenBoundary)
                    {
                        if (j > 0)
                        {
                            if (NeighbourPattern[0])
                            {
                                cells[SizeY - 1][j - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[0])
                            {
                                cells[SizeY - 1][SizeX - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        if (NeighbourPattern[1])
                        {
                            cells[SizeY - 1][j].AddSeed(cell.ParentSeed);
                        }
                        if (j < SizeX - 1)
                        {
                            if (NeighbourPattern[2])
                            {
                                cells[SizeY - 1][j + 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[2])
                            {
                                cells[SizeY - 1][0].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                    // i in the middle - always valid
                    if (true)
                    {
                        if (j > 0)
                        {
                            if (NeighbourPattern[7])
                            {
                                cells[i][j - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[7])
                            {
                                cells[i][SizeX - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        if (j < SizeX - 1)
                        {
                            if (NeighbourPattern[3])
                            {
                                cells[i][j + 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[3])
                            {
                                cells[i][0].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                    if (i < SizeY - 1)
                    {
                        if (j > 0)
                        {
                            if (NeighbourPattern[6])
                            {
                                cells[i + 1][j - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[6])
                            {
                                cells[i + 1][SizeX - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        if (NeighbourPattern[5])
                        {
                            cells[i + 1][j].AddSeed(cell.ParentSeed);
                        }
                        if (j < SizeX - 1)
                        {
                            if (NeighbourPattern[4])
                            {
                                cells[i + 1][j + 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[4])
                            {
                                cells[i + 1][0].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                    else if (Rules.OpenBoundary)
                    {
                        if (j > 0)
                        {
                            if (NeighbourPattern[6])
                            {
                                cells[0][j - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[6])
                            {
                                cells[0][SizeX - 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        if (NeighbourPattern[5])
                        {
                            cells[0][j].AddSeed(cell.ParentSeed);
                        }
                        if (j < SizeX - 1)
                        {
                            if (NeighbourPattern[4])
                            {
                                cells[0][j + 1].AddSeed(cell.ParentSeed);
                            }
                        }
                        else if (Rules.OpenBoundary)
                        {
                            if (NeighbourPattern[4])
                            {
                                cells[0][0].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                }
            }
        }
        public void DistributeEnergyWithRadius(int radius)
        {
            if (!Rules.IsSeeded)
            {
                return;
            }

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    //square area around cell
                    for (int icell = 0; icell <= radius; icell++)
                    {
                        int indexY = i + icell;
                        if (indexY > SizeY - 1)
                        {
                            if (Rules.OpenBoundary)
                            {
                                indexY = indexY - SizeY;
                            }
                            else break;
                        }

                        for (int jcell = 0; jcell <= radius; jcell++)
                        {
                            int indexX = j + jcell;
                            if (indexX > SizeX - 1)
                            {
                                if (Rules.OpenBoundary)
                                {
                                    indexX = indexX - SizeX;
                                }
                                else break;
                            }

                            PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                            PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                            bool readyToBeClaimed = false;
                            if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToBeClaimed = true;
                            }
                            if (Rules.OpenBoundary)
                            {
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                //
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                            }
                            if (readyToBeClaimed)
                            {
                                cells[indexY][indexX].AddSeed(cell.ParentSeed);
                            }
                        }
                        for (int jcell = 0; jcell >= -radius; jcell--)
                        {
                            int indexX = j + jcell;
                            if (indexX < 0)
                            {
                                if (Rules.OpenBoundary)
                                {
                                    indexX = indexX + SizeX;
                                }
                                else break;
                            }

                            PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                            PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                            bool readyToBeClaimed = false;
                            if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToBeClaimed = true;
                            }
                            if (Rules.OpenBoundary)
                            {
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                //
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                            }
                            if (readyToBeClaimed)
                            {
                                cells[indexY][indexX].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                    for (int icell = 0; icell >= -radius; icell--)
                    {
                        int indexY = i + icell;
                        if (indexY < 0)
                        {
                            if (Rules.OpenBoundary)
                            {
                                indexY = indexY + SizeY;
                            }
                            else break;
                        }

                        for (int jcell = 0; jcell <= radius; jcell++)
                        {
                            int indexX = j + jcell;
                            if (indexX > SizeX - 1)
                            {
                                if (Rules.OpenBoundary)
                                {
                                    indexX = indexX - SizeX;
                                }
                                else break;
                            }

                            PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                            PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                            bool readyToBeClaimed = false;
                            if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToBeClaimed = true;
                            }
                            if (Rules.OpenBoundary)
                            {
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                //
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                            }
                            if (readyToBeClaimed)
                            {
                                cells[indexY][indexX].AddSeed(cell.ParentSeed);
                            }
                        }
                        for (int jcell = 0; jcell >= -radius; jcell--)
                        {
                            int indexX = j + jcell;
                            if (indexX < 0)
                            {
                                if (Rules.OpenBoundary)
                                {
                                    indexX = indexX + SizeX;
                                }
                                else break;
                            }

                            PointF originalCellWeight = new PointF(j + cell.Weight.X, i + cell.Weight.Y);
                            PointF neighbourCellWeight = new PointF(indexX + cells[indexY][indexX].Weight.X, indexY + cells[indexY][indexX].Weight.Y);

                            bool readyToBeClaimed = false;
                            if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                            {
                                readyToBeClaimed = true;
                            }
                            if (Rules.OpenBoundary)
                            {
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                //
                                if (Math.Pow(originalCellWeight.X + SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - SizeX - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y - SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                                if (Math.Pow(originalCellWeight.X - neighbourCellWeight.X, 2) + Math.Pow(originalCellWeight.Y + SizeY - neighbourCellWeight.Y, 2) < Math.Pow(radius, 2))
                                {
                                    readyToBeClaimed = true;
                                }
                            }
                            if (readyToBeClaimed)
                            {
                                cells[indexY][indexX].AddSeed(cell.ParentSeed);
                            }
                        }
                    }
                }
            }
        }
        public void CalculateEnergy()
        {
            if(!Rules.IsSeeded)
            {
                return;
            }
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    cell.Energy = cell.CalculateEnergy();
                }
            }
        }
        public void MonteCarlo()
        {
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    cell.ChangeEnergyMonteCarlo();
                    ClearNeighbours();
                    DistributeEnergy();
                    CalculateEnergy();
                }
            }
        }
        public void MonteCarloRadius(int radius)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    Cell cell = cells[i][j];
                    cell.ChangeEnergyMonteCarlo();
                    ClearNeighbours();
                    DistributeEnergyWithRadius(radius);
                    CalculateEnergy();
                }
            }
        }
    }
}