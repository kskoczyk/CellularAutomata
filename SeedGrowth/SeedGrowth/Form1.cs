using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeedGrowth
{
    public partial class Form1 : Form
    {
        Size formSize;

        private readonly Graphics g;
        private readonly Graphics gSeed;
        private readonly Bitmap bitmap;
        private readonly Bitmap bitmapSeed;
        Random random = Rules.Random;
        Board board;
        Seed chosenSeed;

        public Form1()
        {
            InitializeComponent();

            formSize = this.Size;

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bitmapSeed = new Bitmap(pictureBoxSeed.Width, pictureBoxSeed.Height);
            g = Graphics.FromImage(bitmap);
            gSeed = Graphics.FromImage(bitmapSeed);
            g.Clear(Color.White);
            pictureBox1.Image = bitmap;

            board = new Board();
            chosenSeed = board.Seeds[0];
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // TODO: auto-resize
            pictureBox1.Size = new Size((int)(0.555 * this.Size.Width), (int)(0.555 * this.Size.Height));
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            board.IterateAll();
            g.Clear(Color.White);
            board.DrawBoard(pictureBox1, g, bitmap);
        }

        private void buttonSetSize_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxX.Text, out int valX);
            int.TryParse(textBoxY.Text, out int valY);

            if (valX > 0 && valY > 0)
            {
                board.SizeX = valX;
                board.SizeY = valY;

                board.DimX = pictureBox1.Width / (double)valX;
                board.DimY = pictureBox1.Height / (double)valY;
                if(Rules.SquareCells)
                {
                    board.DimX = Math.Min(board.DimX, board.DimY);
                    board.DimY = Math.Min(board.DimX, board.DimY);
                }
                board.CreateCells();
                g.Clear(Color.White);
                board.DrawBoard(pictureBox1, g, bitmap);
            }
        }

        private void buttonGenerateRandomly_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxSeeds.Text, out int noSeeds);
            if(noSeeds > 0)
            {
                Label errorLab = labelError;
                board.CreateCells();
                board.GenerateSeeds(noSeeds);
                board.DistributeSeeds(errorLab);
                g.Clear(Color.White);
                board.DrawBoard(pictureBox1, g, bitmap);
            }
        }

        private void buttonIterate_Click(object sender, EventArgs e)
        {
            board.Iterate();
            g.Clear(Color.White);
            board.DrawBoard(pictureBox1, g, bitmap);
        }

        private void checkBoxBoundaries_CheckedChanged(object sender, EventArgs e)
        {
            Rules.OpenBoundary = !Rules.OpenBoundary;
        }

        private void buttonHomogenous_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxHomoX.Text, out int x);
            int.TryParse(textBoxHomoY.Text, out int y);
            if (x > 0 && y > 0)
            {
                Label errorLab = labelError;
                board.CreateCells();
                board.GenerateSeeds(x * y);
                board.DistributeSeedsHomogenous(x, y, labelError);
                g.Clear(Color.White);
                board.DrawBoard(pictureBox1, g, bitmap);
            }
        }

        private void checkBoxWeights_CheckedChanged(object sender, EventArgs e)
        {
            Rules.DrawWeights = !Rules.DrawWeights;
        }

        private void buttonGenerateRadius_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxSeeds.Text, out int seeds);
            int.TryParse(textBoxGenerateRadius.Text, out int radius);
            if(seeds > 0 && radius > 0)
            {
                Label lab = labelError;
                board.CreateCells();
                board.GenerateSeeds(seeds);
                board.DistributeSeedsRadius(radius, lab);
                g.Clear(Color.White);
                board.DrawBoard(pictureBox1, g, bitmap);
            }
        }

        private void buttonMoore_Click(object sender, EventArgs e)
        {
            board.SetMoore();
        }

        private void buttonVonNeumann_Click(object sender, EventArgs e)
        {
            board.SetVonNeumann();
        }

        private void buttonHexagonalLeft_Click(object sender, EventArgs e)
        {
            board.SetHexagonalLeft();
        }

        private void buttonHexagonalRight_Click(object sender, EventArgs e)
        {
            board.SetHexagonalRight();
        }

        private void buttonHexagonalRandom_Click(object sender, EventArgs e)
        {
            board.SetRandomHexagonal();
            Rules.RandomHexagonal = true;
            Rules.RandomPentagonal = false;
        }

        private void buttonPentagonalRandom_Click(object sender, EventArgs e)
        {
            board.SetRandomPentagonal();
            Rules.RandomPentagonal = true;
            Rules.RandomHexagonal = false;
        }

        private void buttonIterateRadius_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxIterateRadius.Text, out int radius);
            if(radius > 0)
            {
                board.IterateWithRadius(radius);
                g.Clear(Color.White);
                board.DrawBoard(pictureBox1, g, bitmap);
            }
        }

        private void buttonDisableRandom_Click(object sender, EventArgs e)
        {
            Rules.RandomPentagonal = false;
            Rules.RandomHexagonal = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            PointF coordinates = me.Location;

            int indexX = (int)(coordinates.X / board.DimX);
            int indexY = (int)(coordinates.Y / board.DimY);

            board.cells[indexY][indexX].ParentSeed = chosenSeed;
            if(chosenSeed.ID == 0)
            {
                board.cells[indexY][indexX].IsClaimed = false;
                board.cells[indexY][indexX].IsInitialized = false;
            }
            else
            {
                board.cells[indexY][indexX].IsClaimed = true;
                board.cells[indexY][indexX].IsInitialized = false;
            }
            g.Clear(Color.White);
            board.DrawBoard(pictureBox1, g, bitmap);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {      
            int.TryParse(textBoxSetSeed.Text, out int seedNumber);
            if(seedNumber > -1 && seedNumber < board.Seeds.Count)
            {
                chosenSeed = board.Seeds[seedNumber];
                gSeed.Clear(chosenSeed.SeedColor);
                pictureBoxSeed.Image = bitmapSeed;
            }
        }

        private void buttonSetSeed_Click(object sender, EventArgs e)
        {
            /*int.TryParse(textBoxIterateRadius.Text, out int seedNumber);
            if(seedNumber > -1 && seedNumber < board.Seeds.Count)
            {
                chosenSeed = board.Seeds[seedNumber];
                gSeed.Clear(chosenSeed.SeedColor);
                pictureBoxSeed.Image = bitmapSeed;
            }*/
        }

        private void buttonGoWithRadius_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxIterateRadius.Text, out int radius);
            board.IterateAllWithRadius(radius);
            g.Clear(Color.White);
            board.DrawBoard(pictureBox1, g, bitmap);
        }
    }
}
