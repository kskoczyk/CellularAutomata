namespace SeedGrowth
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.goButton = new System.Windows.Forms.Button();
            this.labelSize = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.buttonSetSize = new System.Windows.Forms.Button();
            this.labelSeeds = new System.Windows.Forms.Label();
            this.textBoxSeeds = new System.Windows.Forms.TextBox();
            this.buttonGenerateRandomly = new System.Windows.Forms.Button();
            this.buttonIterate = new System.Windows.Forms.Button();
            this.checkBoxBoundaries = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonHomogenous = new System.Windows.Forms.Button();
            this.textBoxHomoX = new System.Windows.Forms.TextBox();
            this.textBoxHomoY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGenerateRadius = new System.Windows.Forms.Button();
            this.textBoxGenerateRadius = new System.Windows.Forms.TextBox();
            this.checkBoxWeights = new System.Windows.Forms.CheckBox();
            this.buttonMoore = new System.Windows.Forms.Button();
            this.buttonVonNeumann = new System.Windows.Forms.Button();
            this.buttonHexagonalLeft = new System.Windows.Forms.Button();
            this.buttonHexagonalRight = new System.Windows.Forms.Button();
            this.buttonHexagonalRandom = new System.Windows.Forms.Button();
            this.buttonPentagonalRandom = new System.Windows.Forms.Button();
            this.buttonIterateRadius = new System.Windows.Forms.Button();
            this.textBoxIterateRadius = new System.Windows.Forms.TextBox();
            this.buttonGoWithRadius = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.buttonDisableRandom = new System.Windows.Forms.Button();
            this.textBoxSetSeed = new System.Windows.Forms.TextBox();
            this.labelCurrentSeed = new System.Windows.Forms.Label();
            this.pictureBoxSeed = new System.Windows.Forms.PictureBox();
            this.buttonSetSeed = new System.Windows.Forms.Button();
            this.buttonEnergy = new System.Windows.Forms.Button();
            this.buttonMonteCarlo = new System.Windows.Forms.Button();
            this.textBoxKt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSeed)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(444, 444);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.Location = new System.Drawing.Point(572, 406);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "GO!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(697, 9);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(35, 17);
            this.labelSize.TabIndex = 2;
            this.labelSize.Text = "Size";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(639, 35);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(21, 17);
            this.labelX.TabIndex = 3;
            this.labelX.Text = "X:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(711, 35);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(21, 17);
            this.labelY.TabIndex = 4;
            this.labelY.Text = "Y:";
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(666, 32);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(27, 22);
            this.textBoxX.TabIndex = 5;
            this.textBoxX.Text = "10";
            this.textBoxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(738, 32);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(27, 22);
            this.textBoxY.TabIndex = 6;
            this.textBoxY.Text = "20";
            this.textBoxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSetSize
            // 
            this.buttonSetSize.Location = new System.Drawing.Point(642, 60);
            this.buttonSetSize.Name = "buttonSetSize";
            this.buttonSetSize.Size = new System.Drawing.Size(123, 23);
            this.buttonSetSize.TabIndex = 7;
            this.buttonSetSize.Text = "Set";
            this.buttonSetSize.UseVisualStyleBackColor = true;
            this.buttonSetSize.Click += new System.EventHandler(this.buttonSetSize_Click);
            // 
            // labelSeeds
            // 
            this.labelSeeds.AutoSize = true;
            this.labelSeeds.Location = new System.Drawing.Point(470, 110);
            this.labelSeeds.Name = "labelSeeds";
            this.labelSeeds.Size = new System.Drawing.Size(52, 17);
            this.labelSeeds.TabIndex = 8;
            this.labelSeeds.Text = "Seeds:";
            // 
            // textBoxSeeds
            // 
            this.textBoxSeeds.Location = new System.Drawing.Point(521, 134);
            this.textBoxSeeds.Name = "textBoxSeeds";
            this.textBoxSeeds.Size = new System.Drawing.Size(27, 22);
            this.textBoxSeeds.TabIndex = 9;
            this.textBoxSeeds.Text = "1";
            this.textBoxSeeds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonGenerateRandomly
            // 
            this.buttonGenerateRandomly.Location = new System.Drawing.Point(473, 162);
            this.buttonGenerateRandomly.Name = "buttonGenerateRandomly";
            this.buttonGenerateRandomly.Size = new System.Drawing.Size(123, 42);
            this.buttonGenerateRandomly.TabIndex = 10;
            this.buttonGenerateRandomly.Text = "Generate randomly";
            this.buttonGenerateRandomly.UseVisualStyleBackColor = true;
            this.buttonGenerateRandomly.Click += new System.EventHandler(this.buttonGenerateRandomly_Click);
            // 
            // buttonIterate
            // 
            this.buttonIterate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIterate.Location = new System.Drawing.Point(486, 406);
            this.buttonIterate.Name = "buttonIterate";
            this.buttonIterate.Size = new System.Drawing.Size(75, 23);
            this.buttonIterate.TabIndex = 11;
            this.buttonIterate.Text = "Iterate";
            this.buttonIterate.UseVisualStyleBackColor = true;
            this.buttonIterate.Click += new System.EventHandler(this.buttonIterate_Click);
            // 
            // checkBoxBoundaries
            // 
            this.checkBoxBoundaries.AutoSize = true;
            this.checkBoxBoundaries.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxBoundaries.Checked = true;
            this.checkBoxBoundaries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBoundaries.Location = new System.Drawing.Point(730, 338);
            this.checkBoxBoundaries.Name = "checkBoxBoundaries";
            this.checkBoxBoundaries.Size = new System.Drawing.Size(140, 21);
            this.checkBoxBoundaries.TabIndex = 12;
            this.checkBoxBoundaries.Text = "Open boundaries";
            this.checkBoxBoundaries.UseVisualStyleBackColor = true;
            this.checkBoxBoundaries.CheckedChanged += new System.EventHandler(this.checkBoxBoundaries_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(780, 318);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Rule settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(615, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(678, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Y:";
            // 
            // buttonHomogenous
            // 
            this.buttonHomogenous.Location = new System.Drawing.Point(609, 162);
            this.buttonHomogenous.Name = "buttonHomogenous";
            this.buttonHomogenous.Size = new System.Drawing.Size(123, 42);
            this.buttonHomogenous.TabIndex = 16;
            this.buttonHomogenous.Text = "Generate homogenous";
            this.buttonHomogenous.UseVisualStyleBackColor = true;
            this.buttonHomogenous.Click += new System.EventHandler(this.buttonHomogenous_Click);
            // 
            // textBoxHomoX
            // 
            this.textBoxHomoX.Location = new System.Drawing.Point(642, 134);
            this.textBoxHomoX.Name = "textBoxHomoX";
            this.textBoxHomoX.Size = new System.Drawing.Size(27, 22);
            this.textBoxHomoX.TabIndex = 17;
            this.textBoxHomoX.Text = "1";
            this.textBoxHomoX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxHomoY
            // 
            this.textBoxHomoY.Location = new System.Drawing.Point(705, 134);
            this.textBoxHomoY.Name = "textBoxHomoY";
            this.textBoxHomoY.Size = new System.Drawing.Size(27, 22);
            this.textBoxHomoY.TabIndex = 18;
            this.textBoxHomoY.Text = "1";
            this.textBoxHomoY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(767, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Radius:";
            // 
            // buttonGenerateRadius
            // 
            this.buttonGenerateRadius.Location = new System.Drawing.Point(747, 162);
            this.buttonGenerateRadius.Name = "buttonGenerateRadius";
            this.buttonGenerateRadius.Size = new System.Drawing.Size(123, 42);
            this.buttonGenerateRadius.TabIndex = 20;
            this.buttonGenerateRadius.Text = "Generate with radius";
            this.buttonGenerateRadius.UseVisualStyleBackColor = true;
            this.buttonGenerateRadius.Click += new System.EventHandler(this.buttonGenerateRadius_Click);
            // 
            // textBoxGenerateRadius
            // 
            this.textBoxGenerateRadius.Location = new System.Drawing.Point(829, 134);
            this.textBoxGenerateRadius.Name = "textBoxGenerateRadius";
            this.textBoxGenerateRadius.Size = new System.Drawing.Size(27, 22);
            this.textBoxGenerateRadius.TabIndex = 21;
            this.textBoxGenerateRadius.Text = "1";
            this.textBoxGenerateRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxWeights
            // 
            this.checkBoxWeights.AutoSize = true;
            this.checkBoxWeights.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxWeights.Checked = true;
            this.checkBoxWeights.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWeights.Location = new System.Drawing.Point(757, 365);
            this.checkBoxWeights.Name = "checkBoxWeights";
            this.checkBoxWeights.Size = new System.Drawing.Size(113, 21);
            this.checkBoxWeights.TabIndex = 22;
            this.checkBoxWeights.Text = "Draw weights";
            this.checkBoxWeights.UseVisualStyleBackColor = true;
            this.checkBoxWeights.CheckedChanged += new System.EventHandler(this.checkBoxWeights_CheckedChanged);
            // 
            // buttonMoore
            // 
            this.buttonMoore.Location = new System.Drawing.Point(473, 226);
            this.buttonMoore.Name = "buttonMoore";
            this.buttonMoore.Size = new System.Drawing.Size(84, 23);
            this.buttonMoore.TabIndex = 23;
            this.buttonMoore.Text = "Set Moore";
            this.buttonMoore.UseVisualStyleBackColor = true;
            this.buttonMoore.Click += new System.EventHandler(this.buttonMoore_Click);
            // 
            // buttonVonNeumann
            // 
            this.buttonVonNeumann.Location = new System.Drawing.Point(473, 255);
            this.buttonVonNeumann.Name = "buttonVonNeumann";
            this.buttonVonNeumann.Size = new System.Drawing.Size(84, 44);
            this.buttonVonNeumann.TabIndex = 24;
            this.buttonVonNeumann.Text = "Set Von Neumann";
            this.buttonVonNeumann.UseVisualStyleBackColor = true;
            this.buttonVonNeumann.Click += new System.EventHandler(this.buttonVonNeumann_Click);
            // 
            // buttonHexagonalLeft
            // 
            this.buttonHexagonalLeft.Location = new System.Drawing.Point(563, 226);
            this.buttonHexagonalLeft.Name = "buttonHexagonalLeft";
            this.buttonHexagonalLeft.Size = new System.Drawing.Size(106, 43);
            this.buttonHexagonalLeft.TabIndex = 25;
            this.buttonHexagonalLeft.Text = "Set hexagonal left";
            this.buttonHexagonalLeft.UseVisualStyleBackColor = true;
            this.buttonHexagonalLeft.Click += new System.EventHandler(this.buttonHexagonalLeft_Click);
            // 
            // buttonHexagonalRight
            // 
            this.buttonHexagonalRight.Location = new System.Drawing.Point(563, 275);
            this.buttonHexagonalRight.Name = "buttonHexagonalRight";
            this.buttonHexagonalRight.Size = new System.Drawing.Size(106, 43);
            this.buttonHexagonalRight.TabIndex = 26;
            this.buttonHexagonalRight.Text = "Set hexagonal right";
            this.buttonHexagonalRight.UseVisualStyleBackColor = true;
            this.buttonHexagonalRight.Click += new System.EventHandler(this.buttonHexagonalRight_Click);
            // 
            // buttonHexagonalRandom
            // 
            this.buttonHexagonalRandom.Location = new System.Drawing.Point(675, 226);
            this.buttonHexagonalRandom.Name = "buttonHexagonalRandom";
            this.buttonHexagonalRandom.Size = new System.Drawing.Size(106, 43);
            this.buttonHexagonalRandom.TabIndex = 27;
            this.buttonHexagonalRandom.Text = "Set random hexagonal";
            this.buttonHexagonalRandom.UseVisualStyleBackColor = true;
            this.buttonHexagonalRandom.Click += new System.EventHandler(this.buttonHexagonalRandom_Click);
            // 
            // buttonPentagonalRandom
            // 
            this.buttonPentagonalRandom.Location = new System.Drawing.Point(675, 275);
            this.buttonPentagonalRandom.Name = "buttonPentagonalRandom";
            this.buttonPentagonalRandom.Size = new System.Drawing.Size(106, 43);
            this.buttonPentagonalRandom.TabIndex = 28;
            this.buttonPentagonalRandom.Text = "Set random pentagonal";
            this.buttonPentagonalRandom.UseVisualStyleBackColor = true;
            this.buttonPentagonalRandom.Click += new System.EventHandler(this.buttonPentagonalRandom_Click);
            // 
            // buttonIterateRadius
            // 
            this.buttonIterateRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIterateRadius.Location = new System.Drawing.Point(653, 434);
            this.buttonIterateRadius.Name = "buttonIterateRadius";
            this.buttonIterateRadius.Size = new System.Drawing.Size(108, 23);
            this.buttonIterateRadius.TabIndex = 29;
            this.buttonIterateRadius.Text = " with radius";
            this.buttonIterateRadius.UseVisualStyleBackColor = true;
            this.buttonIterateRadius.Click += new System.EventHandler(this.buttonIterateRadius_Click);
            // 
            // textBoxIterateRadius
            // 
            this.textBoxIterateRadius.Location = new System.Drawing.Point(688, 406);
            this.textBoxIterateRadius.Name = "textBoxIterateRadius";
            this.textBoxIterateRadius.Size = new System.Drawing.Size(27, 22);
            this.textBoxIterateRadius.TabIndex = 30;
            this.textBoxIterateRadius.Text = "1";
            this.textBoxIterateRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonGoWithRadius
            // 
            this.buttonGoWithRadius.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGoWithRadius.Location = new System.Drawing.Point(770, 434);
            this.buttonGoWithRadius.Name = "buttonGoWithRadius";
            this.buttonGoWithRadius.Size = new System.Drawing.Size(75, 23);
            this.buttonGoWithRadius.TabIndex = 31;
            this.buttonGoWithRadius.Text = "GO!";
            this.buttonGoWithRadius.UseVisualStyleBackColor = true;
            this.buttonGoWithRadius.Click += new System.EventHandler(this.buttonGoWithRadius_Click);
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(470, 13);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(74, 17);
            this.labelError.TabIndex = 32;
            this.labelError.Text = "Error label";
            // 
            // buttonDisableRandom
            // 
            this.buttonDisableRandom.Location = new System.Drawing.Point(787, 255);
            this.buttonDisableRandom.Name = "buttonDisableRandom";
            this.buttonDisableRandom.Size = new System.Drawing.Size(106, 43);
            this.buttonDisableRandom.TabIndex = 33;
            this.buttonDisableRandom.Text = "Disable randoms";
            this.buttonDisableRandom.UseVisualStyleBackColor = true;
            this.buttonDisableRandom.Click += new System.EventHandler(this.buttonDisableRandom_Click);
            // 
            // textBoxSetSeed
            // 
            this.textBoxSetSeed.Location = new System.Drawing.Point(563, 48);
            this.textBoxSetSeed.Name = "textBoxSetSeed";
            this.textBoxSetSeed.Size = new System.Drawing.Size(27, 22);
            this.textBoxSetSeed.TabIndex = 35;
            this.textBoxSetSeed.Text = "0";
            this.textBoxSetSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSetSeed.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // labelCurrentSeed
            // 
            this.labelCurrentSeed.AutoSize = true;
            this.labelCurrentSeed.Location = new System.Drawing.Point(467, 51);
            this.labelCurrentSeed.Name = "labelCurrentSeed";
            this.labelCurrentSeed.Size = new System.Drawing.Size(94, 17);
            this.labelCurrentSeed.TabIndex = 34;
            this.labelCurrentSeed.Text = "Current seed:";
            // 
            // pictureBoxSeed
            // 
            this.pictureBoxSeed.BackColor = System.Drawing.Color.White;
            this.pictureBoxSeed.Location = new System.Drawing.Point(509, 71);
            this.pictureBoxSeed.Name = "pictureBoxSeed";
            this.pictureBoxSeed.Size = new System.Drawing.Size(48, 36);
            this.pictureBoxSeed.TabIndex = 36;
            this.pictureBoxSeed.TabStop = false;
            // 
            // buttonSetSeed
            // 
            this.buttonSetSeed.Location = new System.Drawing.Point(563, 84);
            this.buttonSetSeed.Name = "buttonSetSeed";
            this.buttonSetSeed.Size = new System.Drawing.Size(42, 23);
            this.buttonSetSeed.TabIndex = 37;
            this.buttonSetSeed.Text = "Set";
            this.buttonSetSeed.UseVisualStyleBackColor = true;
            this.buttonSetSeed.Click += new System.EventHandler(this.buttonSetSeed_Click);
            // 
            // buttonEnergy
            // 
            this.buttonEnergy.Location = new System.Drawing.Point(771, 29);
            this.buttonEnergy.Name = "buttonEnergy";
            this.buttonEnergy.Size = new System.Drawing.Size(123, 25);
            this.buttonEnergy.TabIndex = 38;
            this.buttonEnergy.Text = "Draw energy";
            this.buttonEnergy.UseVisualStyleBackColor = true;
            this.buttonEnergy.Click += new System.EventHandler(this.buttonEnergy_Click);
            // 
            // buttonMonteCarlo
            // 
            this.buttonMonteCarlo.Location = new System.Drawing.Point(771, 60);
            this.buttonMonteCarlo.Name = "buttonMonteCarlo";
            this.buttonMonteCarlo.Size = new System.Drawing.Size(123, 23);
            this.buttonMonteCarlo.TabIndex = 39;
            this.buttonMonteCarlo.Text = "Monte Carlo";
            this.buttonMonteCarlo.UseVisualStyleBackColor = true;
            this.buttonMonteCarlo.Click += new System.EventHandler(this.buttonMonteCarlo_Click);
            // 
            // textBoxKt
            // 
            this.textBoxKt.Location = new System.Drawing.Point(818, 89);
            this.textBoxKt.Name = "textBoxKt";
            this.textBoxKt.Size = new System.Drawing.Size(27, 22);
            this.textBoxKt.TabIndex = 40;
            this.textBoxKt.Text = "0.1";
            this.textBoxKt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 469);
            this.Controls.Add(this.textBoxKt);
            this.Controls.Add(this.buttonMonteCarlo);
            this.Controls.Add(this.buttonEnergy);
            this.Controls.Add(this.buttonSetSeed);
            this.Controls.Add(this.pictureBoxSeed);
            this.Controls.Add(this.textBoxSetSeed);
            this.Controls.Add(this.labelCurrentSeed);
            this.Controls.Add(this.buttonDisableRandom);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.buttonGoWithRadius);
            this.Controls.Add(this.textBoxIterateRadius);
            this.Controls.Add(this.buttonIterateRadius);
            this.Controls.Add(this.buttonPentagonalRandom);
            this.Controls.Add(this.buttonHexagonalRandom);
            this.Controls.Add(this.buttonHexagonalRight);
            this.Controls.Add(this.buttonHexagonalLeft);
            this.Controls.Add(this.buttonVonNeumann);
            this.Controls.Add(this.buttonMoore);
            this.Controls.Add(this.checkBoxWeights);
            this.Controls.Add(this.textBoxGenerateRadius);
            this.Controls.Add(this.buttonGenerateRadius);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxHomoY);
            this.Controls.Add(this.textBoxHomoX);
            this.Controls.Add(this.buttonHomogenous);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxBoundaries);
            this.Controls.Add(this.buttonIterate);
            this.Controls.Add(this.buttonGenerateRandomly);
            this.Controls.Add(this.textBoxSeeds);
            this.Controls.Add(this.labelSeeds);
            this.Controls.Add(this.buttonSetSize);
            this.Controls.Add(this.textBoxY);
            this.Controls.Add(this.textBoxX);
            this.Controls.Add(this.labelY);
            this.Controls.Add(this.labelX);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.Button buttonSetSize;
        private System.Windows.Forms.Label labelSeeds;
        private System.Windows.Forms.TextBox textBoxSeeds;
        private System.Windows.Forms.Button buttonGenerateRandomly;
        private System.Windows.Forms.Button buttonIterate;
        private System.Windows.Forms.CheckBox checkBoxBoundaries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonHomogenous;
        private System.Windows.Forms.TextBox textBoxHomoX;
        private System.Windows.Forms.TextBox textBoxHomoY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGenerateRadius;
        private System.Windows.Forms.TextBox textBoxGenerateRadius;
        private System.Windows.Forms.CheckBox checkBoxWeights;
        private System.Windows.Forms.Button buttonMoore;
        private System.Windows.Forms.Button buttonVonNeumann;
        private System.Windows.Forms.Button buttonHexagonalLeft;
        private System.Windows.Forms.Button buttonHexagonalRight;
        private System.Windows.Forms.Button buttonHexagonalRandom;
        private System.Windows.Forms.Button buttonPentagonalRandom;
        private System.Windows.Forms.Button buttonIterateRadius;
        private System.Windows.Forms.TextBox textBoxIterateRadius;
        private System.Windows.Forms.Button buttonGoWithRadius;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Button buttonDisableRandom;
        private System.Windows.Forms.TextBox textBoxSetSeed;
        private System.Windows.Forms.Label labelCurrentSeed;
        private System.Windows.Forms.PictureBox pictureBoxSeed;
        private System.Windows.Forms.Button buttonSetSeed;
        private System.Windows.Forms.Button buttonEnergy;
        private System.Windows.Forms.Button buttonMonteCarlo;
        private System.Windows.Forms.TextBox textBoxKt;
    }
}

