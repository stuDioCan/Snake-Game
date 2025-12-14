namespace SnakeGame
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox picGameBoard;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelUI;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnSnakeColor;
        private System.Windows.Forms.Button btnBgColor;
        private System.Windows.Forms.Button btnResetColors;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblFoodType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picGameBoard = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panelUI = new System.Windows.Forms.Panel();
            this.lblFoodType = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnResetColors = new System.Windows.Forms.Button();
            this.btnBgColor = new System.Windows.Forms.Button();
            this.btnSnakeColor = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picGameBoard)).BeginInit();
            this.panelUI.SuspendLayout();
            this.SuspendLayout();
            // 
            // picGameBoard
            // 
            this.picGameBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.picGameBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picGameBoard.Location = new System.Drawing.Point(12, 120);
            this.picGameBoard.Name = "picGameBoard";
            this.picGameBoard.Size = new System.Drawing.Size(600, 600);
            this.picGameBoard.TabIndex = 0;
            this.picGameBoard.TabStop = false;
            this.picGameBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.picGameBoard_Paint);
            this.picGameBoard.Click += new System.EventHandler(this.picGameBoard_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 150;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.White;
            this.lblScore.Location = new System.Drawing.Point(15, 45);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(80, 19);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "SCORE: 0";
            this.lblScore.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblHighScore.ForeColor = System.Drawing.Color.White;
            this.lblHighScore.Location = new System.Drawing.Point(15, 70);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(70, 19);
            this.lblHighScore.TabIndex = 1;
            this.lblHighScore.Text = "HIGH: 0";
            this.lblHighScore.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Gold;
            this.lblStatus.Location = new System.Drawing.Point(150, 30);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(190, 19);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "PRESS START TO PLAY!";
            this.lblStatus.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnStart.FlatAppearance.BorderSize = 2;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(500, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 30);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnPause.Enabled = false;
            this.btnPause.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnPause.FlatAppearance.BorderSize = 2;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnPause.ForeColor = System.Drawing.Color.White;
            this.btnPause.Location = new System.Drawing.Point(500, 50);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 30);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnExit.FlatAppearance.BorderSize = 2;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(610, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 30);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelUI
            // 
            this.panelUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(0)))), ((int)(((byte)(130)))));
            this.panelUI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelUI.Controls.Add(this.lblFoodType);
            this.panelUI.Controls.Add(this.btnBack);
            this.panelUI.Controls.Add(this.btnResetColors);
            this.panelUI.Controls.Add(this.btnBgColor);
            this.panelUI.Controls.Add(this.btnSnakeColor);
            this.panelUI.Controls.Add(this.btnSettings);
            this.panelUI.Controls.Add(this.lblSpeed);
            this.panelUI.Controls.Add(this.lblLevel);
            this.panelUI.Controls.Add(this.lblTitle);
            this.panelUI.Controls.Add(this.lblScore);
            this.panelUI.Controls.Add(this.lblHighScore);
            this.panelUI.Controls.Add(this.btnExit);
            this.panelUI.Controls.Add(this.lblStatus);
            this.panelUI.Controls.Add(this.btnPause);
            this.panelUI.Controls.Add(this.btnStart);
            this.panelUI.Location = new System.Drawing.Point(12, 12);
            this.panelUI.Name = "panelUI";
            this.panelUI.Size = new System.Drawing.Size(724, 102);
            this.panelUI.TabIndex = 1;
            this.panelUI.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // lblFoodType
            // 
            this.lblFoodType.AutoSize = true;
            this.lblFoodType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblFoodType.ForeColor = System.Drawing.Color.White;
            this.lblFoodType.Location = new System.Drawing.Point(280, 80);
            this.lblFoodType.Name = "lblFoodType";
            this.lblFoodType.Size = new System.Drawing.Size(85, 18);
            this.lblFoodType.TabIndex = 13;
            this.lblFoodType.Text = "FOOD: APPLE";
            this.lblFoodType.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.Cyan;
            this.btnBack.FlatAppearance.BorderSize = 2;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(610, 65);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnResetColors
            // 
            this.btnResetColors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnResetColors.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnResetColors.FlatAppearance.BorderSize = 2;
            this.btnResetColors.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnResetColors.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnResetColors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetColors.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnResetColors.ForeColor = System.Drawing.Color.White;
            this.btnResetColors.Location = new System.Drawing.Point(500, 65);
            this.btnResetColors.Name = "btnResetColors";
            this.btnResetColors.Size = new System.Drawing.Size(100, 30);
            this.btnResetColors.TabIndex = 11;
            this.btnResetColors.Text = "DEFAULTS";
            this.btnResetColors.UseVisualStyleBackColor = false;
            this.btnResetColors.Visible = false;
            this.btnResetColors.Click += new System.EventHandler(this.btnResetColors_Click);
            // 
            // btnBgColor
            // 
            this.btnBgColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnBgColor.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnBgColor.FlatAppearance.BorderSize = 2;
            this.btnBgColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnBgColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnBgColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBgColor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnBgColor.ForeColor = System.Drawing.Color.White;
            this.btnBgColor.Location = new System.Drawing.Point(390, 65);
            this.btnBgColor.Name = "btnBgColor";
            this.btnBgColor.Size = new System.Drawing.Size(100, 30);
            this.btnBgColor.TabIndex = 10;
            this.btnBgColor.Text = "BG COLOR";
            this.btnBgColor.UseVisualStyleBackColor = false;
            this.btnBgColor.Visible = false;
            this.btnBgColor.Click += new System.EventHandler(this.btnBgColor_Click);
            // 
            // btnSnakeColor
            // 
            this.btnSnakeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnSnakeColor.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnSnakeColor.FlatAppearance.BorderSize = 2;
            this.btnSnakeColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnSnakeColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnSnakeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnakeColor.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSnakeColor.ForeColor = System.Drawing.Color.White;
            this.btnSnakeColor.Location = new System.Drawing.Point(280, 65);
            this.btnSnakeColor.Name = "btnSnakeColor";
            this.btnSnakeColor.Size = new System.Drawing.Size(100, 30);
            this.btnSnakeColor.TabIndex = 9;
            this.btnSnakeColor.Text = "SNAKE COLOR";
            this.btnSnakeColor.UseVisualStyleBackColor = false;
            this.btnSnakeColor.Visible = false;
            this.btnSnakeColor.Click += new System.EventHandler(this.btnSnakeColor_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226)))));
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.Gold;
            this.btnSettings.FlatAppearance.BorderSize = 2;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(80)))), ((int)(((byte)(240)))));
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(63)))), ((int)(((byte)(236)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(610, 50);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(100, 30);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "SETTINGS";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblSpeed.ForeColor = System.Drawing.Color.White;
            this.lblSpeed.Location = new System.Drawing.Point(150, 80);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(106, 18);
            this.lblSpeed.TabIndex = 7;
            this.lblSpeed.Text = "SPEED: NORMAL";
            this.lblSpeed.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblLevel.ForeColor = System.Drawing.Color.White;
            this.lblLevel.Location = new System.Drawing.Point(150, 55);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(69, 18);
            this.lblLevel.TabIndex = 6;
            this.lblLevel.Text = "LEVEL: 1";
            this.lblLevel.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(222, 29);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "90's PIXEL SNAKE";
            this.lblTitle.Click += new System.EventHandler(this.panelUI_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(112)))));
            this.ClientSize = new System.Drawing.Size(748, 732);
            this.Controls.Add(this.panelUI);
            this.Controls.Add(this.picGameBoard);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "90\'s Pixel Snake Game - Meme Sounds & Food Art";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picGameBoard)).EndInit();
            this.panelUI.ResumeLayout(false);
            this.panelUI.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}