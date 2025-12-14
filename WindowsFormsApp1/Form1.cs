using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.IO;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        // ==================== GAME VARIABLES ====================
        private List<Point> snake = new List<Point>();
        private Point food = new Point();
        private Direction currentDirection = Direction.Right;
        private Direction nextDirection = Direction.Right;
        private int score = 0;
        private int highScore = 0;
        private int gridSize = 20;
        private int gameSpeed = 150;
        private bool gameRunning = false;
        private bool gamePaused = false;
        private Random random = new Random();
        private int level = 1;
        private int foodsEaten = 0;
        private FoodType currentFoodType = FoodType.Apple;

        // ==================== SOUND LIBRARY ====================
        private Dictionary<string, SoundPlayer> soundLibrary = new Dictionary<string, SoundPlayer>();

        // ==================== 90's PIXELATED COLOR SCHEME ====================
        private Color gridColor = Color.FromArgb(100, 100, 150);
        private Color snakeHeadColor = Color.FromArgb(255, 105, 180); // Hot Pink
        private Color snakeBodyColor = Color.FromArgb(255, 20, 147); // Deep Pink
        private Color backgroundColor = Color.FromArgb(25, 25, 112); // Midnight Blue
        private Color textColor = Color.FromArgb(255, 255, 255);
        private Color buttonColor = Color.FromArgb(138, 43, 226);
        private Color panelColor = Color.FromArgb(75, 0, 130);
        private Color highlightColor = Color.FromArgb(255, 215, 0);

        // Food colors
        private readonly Dictionary<FoodType, Color> foodColors = new Dictionary<FoodType, Color>
        {
            { FoodType.Apple, Color.Red },
            { FoodType.Pizza, Color.FromArgb(255, 165, 0) },
            { FoodType.Burger, Color.FromArgb(139, 69, 19) },
            { FoodType.Carrot, Color.FromArgb(255, 140, 0) },
            { FoodType.Kiwi, Color.FromArgb(154, 205, 50) },
            { FoodType.Cherry, Color.FromArgb(220, 20, 60) },
            { FoodType.Banana, Color.Yellow },
            { FoodType.Grapes, Color.Purple }
        };

        // ==================== ENUMS ====================
        private enum GameState { Start, Playing, Paused, GameOver, Settings }
        private GameState currentState = GameState.Start;

        private enum Direction { Up, Down, Left, Right }

        private enum FoodType
        {
            Apple,      // +10 points (Normal)
            Pizza,      // +30 points (Bonus)
            Burger,     // Slows down (-20 speed for 5 sec)
            Carrot,     // +15 points (Healthy)
            Kiwi,       // Speed boost (-30 speed for 4 sec)
            Cherry,     // +25 points (Sweet)
            Banana,     // +20 points (Energy)
            Grapes      // +40 points (Rare)
        }

        // ==================== FOOD PIXEL ART ====================
        private Dictionary<FoodType, bool[,]> foodPixelArt = new Dictionary<FoodType, bool[,]>();

        public Form1()
        {

            InitializeComponent();

            // Initialize food pixel art
            InitializeFoodPixelArt();

            // Enable key preview
            this.KeyPreview = true;

            // Load WAV sounds
            LoadSounds();

            InitializeGame();
            SetupFormStyle();

            this.Focus();
            this.ActiveControl = null;
        }

        // ==================== SOUND SYSTEM ====================
        private void LoadSounds()
        {
            try
            {
                // Load WAV files from Sounds folder
                string soundsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds");

                if (Directory.Exists(soundsFolder))
                {
                    // Check if WAV files exist
                    string[] requiredSounds = { "eat.wav", "gameover.wav", "levelup.wav", "bonus.wav", "speed.wav", "slow.wav" };
                    bool allSoundsExist = true;

                    foreach (string soundFile in requiredSounds)
                    {
                        string soundPath = Path.Combine(soundsFolder, soundFile);
                        if (!File.Exists(soundPath))
                        {
                            allSoundsExist = false;
                            break;
                        }
                    }

                    if (allSoundsExist)
                    {
                        // Load all sounds
                        soundLibrary["eat"] = new SoundPlayer(Path.Combine(soundsFolder, "eat.wav"));
                        soundLibrary["gameover"] = new SoundPlayer(Path.Combine(soundsFolder, "gameover.wav"));
                        soundLibrary["levelup"] = new SoundPlayer(Path.Combine(soundsFolder, "levelup.wav"));
                        soundLibrary["bonus"] = new SoundPlayer(Path.Combine(soundsFolder, "bonus.wav"));
                        soundLibrary["speed"] = new SoundPlayer(Path.Combine(soundsFolder, "speed.wav"));
                        soundLibrary["slow"] = new SoundPlayer(Path.Combine(soundsFolder, "slow.wav"));

                        // Pre-load sounds
                        foreach (var sound in soundLibrary.Values)
                        {
                            sound.LoadAsync();
                        }

                        Console.WriteLine("✅ WAV sounds loaded successfully!");
                    }
                    else
                    {
                        // Create folder and generate sounds
                        Directory.CreateDirectory(soundsFolder);
                        WavGenerator.CreateMemeWavFiles();
                        LoadSounds(); // Try loading again
                    }
                }
                else
                {
                    // Create folder and generate sounds
                    Directory.CreateDirectory(soundsFolder);
                    WavGenerator.CreateMemeWavFiles();
                    LoadSounds(); // Try loading again
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sound loading error: {ex.Message}\nUsing meme beep sounds.", "Sound Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine($"Sound error: {ex.Message}");
            }
        }

        private void PlaySound(string soundName)
        {
            if (soundLibrary.ContainsKey(soundName))
            {
                try
                {
                    soundLibrary[soundName].Play();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error playing {soundName}: {ex.Message}");
                    // Fallback to meme sounds
                }
            }

            // Fallback to meme sounds if WAV fails
            PlayMemeSound(soundName);
        }

        private void PlayMemeSound(string soundType)
        {
            try
            {
                // Play different pitched beeps for meme-like sounds
                switch (soundType)
                {
                    case "eat":
                        Console.Beep(800, 150); // Nom nom sound
                        System.Threading.Thread.Sleep(50);
                        Console.Beep(900, 100);
                        break;

                    case "gameover":
                        Console.Beep(200, 300); // Sad trombone-like
                        System.Threading.Thread.Sleep(100);
                        Console.Beep(150, 400);
                        System.Threading.Thread.Sleep(100);
                        Console.Beep(100, 500);
                        break;

                    case "levelup":
                        Console.Beep(523, 150); // Mario coin sound
                        Console.Beep(659, 150);
                        Console.Beep(784, 150);
                        break;

                    case "bonus":
                        Console.Beep(784, 100); // Exciting ding
                        Console.Beep(880, 100);
                        Console.Beep(988, 200);
                        break;

                    case "speed":
                        Console.Beep(1000, 100); // Zoom sound
                        System.Threading.Thread.Sleep(50);
                        Console.Beep(1200, 100);
                        System.Threading.Thread.Sleep(50);
                        Console.Beep(1400, 100);
                        break;

                    case "slow":
                        Console.Beep(400, 200); // Slow motion sound
                        System.Threading.Thread.Sleep(150);
                        Console.Beep(350, 200);
                        System.Threading.Thread.Sleep(150);
                        Console.Beep(300, 200);
                        break;
                }
            }
            catch
            {
                // Ultimate fallback to system sounds
                switch (soundType)
                {
                    case "eat": System.Media.SystemSounds.Beep.Play(); break;
                    case "gameover": System.Media.SystemSounds.Hand.Play(); break;
                    case "levelup": System.Media.SystemSounds.Asterisk.Play(); break;
                    case "bonus": System.Media.SystemSounds.Question.Play(); break;
                    case "speed": System.Media.SystemSounds.Beep.Play(); break;
                    case "slow": System.Media.SystemSounds.Exclamation.Play(); break;
                }
            }
        }

        // ==================== INITIALIZATION ====================
        private void InitializeFoodPixelArt()
        {
            // Apple (5x5 pixel art)
            foodPixelArt[FoodType.Apple] = new bool[5, 5]
            {
                { false, false, true, false, false },
                { false, true, true, true, false },
                { true, true, true, true, true },
                { false, true, true, true, false },
                { false, false, true, false, false }
            };

            // Pizza (5x5 pixel art - triangle slice)
            foodPixelArt[FoodType.Pizza] = new bool[5, 5]
            {
                { true, true, true, true, true },
                { false, true, true, true, false },
                { false, false, true, false, false },
                { false, false, false, false, false },
                { false, false, false, false, false }
            };

            // Burger (5x5 pixel art)
            foodPixelArt[FoodType.Burger] = new bool[5, 5]
            {
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, true, true, true, true },
                { true, false, false, false, true },
                { true, true, true, true, true }
            };

            // Carrot (5x5 pixel art)
            foodPixelArt[FoodType.Carrot] = new bool[5, 5]
            {
                { false, false, true, false, false },
                { false, true, true, true, false },
                { true, true, true, true, true },
                { false, true, true, true, false },
                { false, false, true, false, false }
            };

            // Kiwi (5x5 pixel art)
            foodPixelArt[FoodType.Kiwi] = new bool[5, 5]
            {
                { false, true, true, true, false },
                { true, false, true, false, true },
                { true, true, true, true, true },
                { true, false, true, false, true },
                { false, true, true, true, false }
            };

            // Cherry (5x5 pixel art - two circles)
            foodPixelArt[FoodType.Cherry] = new bool[5, 5]
            {
                { false, true, true, false, false },
                { true, true, true, true, false },
                { true, true, true, true, false },
                { false, true, true, false, true },
                { false, false, false, true, true }
            };

            // Banana (5x5 pixel art - curved shape)
            foodPixelArt[FoodType.Banana] = new bool[5, 5]
            {
                { false, false, true, true, false },
                { false, true, true, true, true },
                { true, true, true, true, false },
                { false, true, true, false, false },
                { false, false, false, false, false }
            };

            // Grapes (5x5 pixel art - cluster)
            foodPixelArt[FoodType.Grapes] = new bool[5, 5]
            {
                { false, true, true, false, false },
                { true, true, true, true, false },
                { true, true, true, true, true },
                { false, true, true, true, false },
                { false, false, true, false, false }
            };
        }


        private void InitializeGame()
        {
            // Reset snake
            snake.Clear();
            int startX = 10;
            int startY = 10;
            for (int i = 0; i < 3; i++)
            {
                snake.Add(new Point(startX - i, startY));
            }

            // Generate first food
            GenerateFood();

            // Reset game variables
            score = 0;
            level = 1;
            foodsEaten = 0;
            gameSpeed = 150;
            currentFoodType = FoodType.Apple;

            // Reset direction
            currentDirection = Direction.Right;
            nextDirection = Direction.Right;

            // Update UI
            lblScore.Text = "SCORE: 0";
            lblHighScore.Text = "HIGH: " + highScore;
            lblLevel.Text = "LEVEL: 1";
            lblSpeed.Text = "SPEED: NORMAL";
            lblFoodType.Text = "FOOD: APPLE";

            // Setup game area
            picGameBoard.Size = new Size(30 * gridSize, 30 * gridSize);


            currentState = GameState.Start;
            gameRunning = false;
            gamePaused = false;
            gameTimer.Enabled = false;
            gameTimer.Interval = gameSpeed;



            btnStart.Text = "START GAME";
            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";
            btnSettings.Enabled = true;
            lblStatus.Text = "PRESS START TO PLAY!";
            lblStatus.ForeColor = highlightColor;

            btnStart.Text = "START GAME";
            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";
            btnSettings.Enabled = true;
            lblStatus.Text = "PRESS START TO PLAY!";
            lblStatus.ForeColor = highlightColor;

            this.Focus();
            this.ActiveControl = null;
        }

        private void SetupFormStyle()
        {
            this.BackColor = backgroundColor;
            this.ForeColor = textColor;
            this.Font = new Font("Arial", 10, FontStyle.Bold);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            Apply90sStyleToButtons();
        }

        private void Apply90sStyleToButtons()
        {
            foreach (Control control in panelUI.Controls)
            {
                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = highlightColor;
                    button.FlatAppearance.BorderSize = 2;
                    button.BackColor = buttonColor;
                    button.ForeColor = Color.White;
                    button.Font = new Font("Arial", 9, FontStyle.Bold);
                }
            }
        }

        private void GenerateFood()
        {
            bool foodOnSnake;

            // Random food type with probabilities
            int foodTypeRoll = random.Next(100);
            if (foodTypeRoll < 40) // 40% Apple
                currentFoodType = FoodType.Apple;
            else if (foodTypeRoll < 55) // 15% Pizza
                currentFoodType = FoodType.Pizza;
            else if (foodTypeRoll < 65) // 10% Burger
                currentFoodType = FoodType.Burger;
            else if (foodTypeRoll < 75) // 10% Carrot
                currentFoodType = FoodType.Carrot;
            else if (foodTypeRoll < 82) // 7% Kiwi
                currentFoodType = FoodType.Kiwi;
            else if (foodTypeRoll < 88) // 6% Cherry
                currentFoodType = FoodType.Cherry;
            else if (foodTypeRoll < 95) // 7% Banana
                currentFoodType = FoodType.Banana;
            else // 5% Grapes (Rare)
                currentFoodType = FoodType.Grapes;

            // Update food type label
            lblFoodType.Text = "FOOD: " + currentFoodType.ToString().ToUpper();

            do
            {
                foodOnSnake = false;
                int maxX = picGameBoard.Width / gridSize - 1;
                int maxY = picGameBoard.Height / gridSize - 1;
                food = new Point(random.Next(0, maxX), random.Next(0, maxY));

                foreach (Point segment in snake)
                {
                    if (segment.X == food.X && segment.Y == food.Y)
                    {
                        foodOnSnake = true;
                        break;
                    }
                }
            } while (foodOnSnake);
        }

        // ==================== GAME LOOP ====================
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!gameRunning || gamePaused || currentState != GameState.Playing)
                return;

            MoveSnake();
            CheckCollisions();
            picGameBoard.Invalidate();
        }

        private void MoveSnake()
        {
            Point head = snake[0];
            Point newHead = head;

            currentDirection = nextDirection;

            switch (currentDirection)
            {
                case Direction.Up: newHead.Y--; break;
                case Direction.Down: newHead.Y++; break;
                case Direction.Left: newHead.X--; break;
                case Direction.Right: newHead.X++; break;
            }

            snake.Insert(0, newHead);

            if (newHead.X == food.X && newHead.Y == food.Y)
            {
                foodsEaten++;

                // Handle different food types with WAV sounds
                switch (currentFoodType)
                {
                    case FoodType.Apple:
                        score += 10;
                        PlaySound("eat");
                        break;

                    case FoodType.Pizza:
                        score += 30;
                        PlaySound("bonus");
                        break;

                    case FoodType.Burger:
                        score += 5;
                        PlaySound("slow");
                        gameTimer.Interval = Math.Min(300, gameTimer.Interval + 40);
                        lblSpeed.Text = "SPEED: SLOW (BURGER!)";
                        // Reset after 5 seconds
                        Timer slowTimer = new Timer();
                        slowTimer.Interval = 5000;
                        slowTimer.Tick += (s, ev) =>
                        {
                            gameTimer.Interval = gameSpeed;
                            lblSpeed.Text = "SPEED: NORMAL";
                            slowTimer.Stop();
                        };
                        slowTimer.Start();
                        break;

                    case FoodType.Carrot:
                        score += 15;
                        PlaySound("eat");
                        break;

                    case FoodType.Kiwi:
                        score += 20;
                        PlaySound("speed");
                        gameTimer.Interval = Math.Max(50, gameTimer.Interval - 30);
                        lblSpeed.Text = "SPEED: FAST (KIWI POWER!)";
                        // Reset after 4 seconds
                        Timer fastTimer = new Timer();
                        fastTimer.Interval = 4000;
                        fastTimer.Tick += (s, ev) =>
                        {
                            gameTimer.Interval = gameSpeed;
                            lblSpeed.Text = "SPEED: NORMAL";
                            fastTimer.Stop();
                        };
                        fastTimer.Start();
                        break;

                    case FoodType.Cherry:
                        score += 25;
                        PlaySound("bonus");
                        break;

                    case FoodType.Banana:
                        score += 20;
                        PlaySound("eat");
                        break;

                    case FoodType.Grapes:
                        score += 40;
                        PlaySound("levelup");
                        break;
                }

                lblScore.Text = "SCORE: " + score;

                if (score > highScore)
                {
                    highScore = score;
                    lblHighScore.Text = "HIGH: " + highScore;
                }

                GenerateFood();

                // Level up every 8 foods
                if (foodsEaten % 8 == 0 && currentFoodType != FoodType.Grapes)
                {
                    level++;
                    gameSpeed = Math.Max(50, gameSpeed - 15);
                    gameTimer.Interval = gameSpeed;
                    lblLevel.Text = "LEVEL: " + level;
                    lblSpeed.Text = "SPEED: LEVEL " + level;
                    PlaySound("levelup");
                }
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void CheckCollisions()
        {
            Point head = snake[0];

            if (head.X < 0 || head.X >= picGameBoard.Width / gridSize ||
                head.Y < 0 || head.Y >= picGameBoard.Height / gridSize)
            {
                GameOver();
                return;
            }

            for (int i = 1; i < snake.Count; i++)
            {
                if (head.X == snake[i].X && head.Y == snake[i].Y)
                {
                    GameOver();
                    return;
                }
            }
        }

        private void GameOver()
        {
            gameRunning = false;
            gameTimer.Enabled = false;
            currentState = GameState.GameOver;

            btnStart.Text = "PLAY AGAIN";
            btnPause.Enabled = false;
            btnPause.Text = "PAUSE";
            lblStatus.Text = "GAME OVER! SCORE: " + score;
            lblStatus.ForeColor = Color.Red;

            PlaySound("gameover");
            this.Focus();
            this.ActiveControl = null;
        }

        // ==================== KEYBOARD CONTROLS ====================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (gameRunning && !gamePaused && currentState == GameState.Playing)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        if (currentDirection != Direction.Down)
                        {
                            nextDirection = Direction.Up;
                            return true;
                        }
                        break;
                    case Keys.Down:
                        if (currentDirection != Direction.Up)
                        {
                            nextDirection = Direction.Down;
                            return true;
                        }
                        break;
                    case Keys.Left:
                        if (currentDirection != Direction.Right)
                        {
                            nextDirection = Direction.Left;
                            return true;
                        }
                        break;
                    case Keys.Right:
                        if (currentDirection != Direction.Left)
                        {
                            nextDirection = Direction.Right;
                            return true;
                        }
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||
                e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameRunning && !gamePaused && currentState == GameState.Playing)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                    case Keys.W:
                        if (currentDirection != Direction.Down)
                            nextDirection = Direction.Up;
                        e.Handled = true;
                        break;
                    case Keys.Down:
                    case Keys.S:
                        if (currentDirection != Direction.Up)
                            nextDirection = Direction.Down;
                        e.Handled = true;
                        break;
                    case Keys.Left:
                    case Keys.A:
                        if (currentDirection != Direction.Right)
                            nextDirection = Direction.Left;
                        e.Handled = true;
                        break;
                    case Keys.Right:
                    case Keys.D:
                        if (currentDirection != Direction.Left)
                            nextDirection = Direction.Right;
                        e.Handled = true;
                        break;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (currentState == GameState.Playing || currentState == GameState.Paused)
                        TogglePause();
                    e.Handled = true;
                    break;
                case Keys.R:
                    InitializeGame();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    if (currentState == GameState.Playing)
                        TogglePause();
                    else if (currentState == GameState.Paused)
                        TogglePause();
                    else if (currentState == GameState.Settings)
                        ShowGameScreen();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    if (currentState == GameState.Start || currentState == GameState.GameOver)
                        btnStart_Click(null, null);
                    e.Handled = true;
                    break;
                case Keys.F1:
                    ShowSettings();
                    e.Handled = true;
                    break;
            }
        }

        // ==================== DRAWING ====================
        private void picGameBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;

            g.Clear(backgroundColor);

            // Draw grid
            using (Pen gridPen = new Pen(gridColor, 1))
            {
                for (int x = 0; x <= picGameBoard.Width / gridSize; x++)
                {
                    g.DrawLine(gridPen, x * gridSize, 0, x * gridSize, picGameBoard.Height);
                }

                for (int y = 0; y <= picGameBoard.Height / gridSize; y++)
                {
                    g.DrawLine(gridPen, 0, y * gridSize, picGameBoard.Width, y * gridSize);
                }
            }

            // Draw snake
            for (int i = 0; i < snake.Count; i++)
            {
                Point segment = snake[i];
                Rectangle rect = new Rectangle(
                    segment.X * gridSize + 2,
                    segment.Y * gridSize + 2,
                    gridSize - 4,
                    gridSize - 4
                );

                if (i == 0)
                {
                    using (SolidBrush headBrush = new SolidBrush(snakeHeadColor))
                    {
                        g.FillRectangle(headBrush, rect);
                    }

                    // Eyes
                    using (SolidBrush eyeBrush = new SolidBrush(Color.White))
                    {
                        g.FillRectangle(eyeBrush, rect.X + 4, rect.Y + 4, 3, 3);
                        g.FillRectangle(eyeBrush, rect.X + rect.Width - 7, rect.Y + 4, 3, 3);
                    }

                    // Smile
                    using (Pen mouthPen = new Pen(Color.Black, 2))
                    {
                        g.DrawLine(mouthPen,
                            rect.X + 5,
                            rect.Y + rect.Height - 5,
                            rect.X + rect.Width - 5,
                            rect.Y + rect.Height - 5);
                    }
                }
                else
                {
                    int darken = i * 2;
                    Color bodyColor = Color.FromArgb(
                        Math.Max(0, snakeBodyColor.R - darken),
                        Math.Max(0, snakeBodyColor.G - darken),
                        Math.Max(0, snakeBodyColor.B - darken)
                    );

                    using (SolidBrush bodyBrush = new SolidBrush(bodyColor))
                    {
                        g.FillRectangle(bodyBrush, rect);
                    }
                }
            }

            // Draw food with pixel art
            DrawFoodPixelArt(g, currentFoodType, food.X, food.Y);

            // Draw game state messages
            if (currentState == GameState.Start)
            {
                DrawCenteredText(g, "90's PIXEL SNAKE!\n\n" +
                    "ARROW KEYS: Move\n" +
                    "SPACE: Pause/Resume\n" +
                    "ENTER: Start Game\n" +
                    "F1: Settings\n" +
                    "ESC: Menu\n\n" +
                    "FOOD TYPES:\n" +
                    "🍎 Apple: +10 pts\n" +
                    "🍕 Pizza: +30 pts\n" +
                    "🍔 Burger: Slows down\n" +
                    "🥕 Carrot: +15 pts\n" +
                    "🥝 Kiwi: Speed boost!\n" +
                    "🍒 Cherry: +25 pts\n" +
                    "🍌 Banana: +20 pts\n" +
                    "🍇 Grapes: +40 pts!",
                    new Font("Arial", 10, FontStyle.Bold), highlightColor);
            }
            else if (currentState == GameState.Paused)
            {
                DrawCenteredText(g, "GAME PAUSED\n\nSPACE: Resume\nR: Restart\nESC: Menu",
                    new Font("Arial", 14, FontStyle.Bold), Color.Cyan);
            }
            else if (currentState == GameState.GameOver)
            {
                DrawCenteredText(g, $"GAME OVER!\n\nFINAL SCORE: {score}\nLEVEL: {level}\nFOOD EATEN: {foodsEaten}\n\nENTER: Play Again",
                    new Font("Arial", 14, FontStyle.Bold), Color.Red);
            }
            else if (currentState == GameState.Settings)
            {
                DrawCenteredText(g, "SETTINGS MENU\n\nUse buttons above to:\n• Change snake color\n• Change background\n• Reset to defaults\n\nESC: Back to Game",
                    new Font("Arial", 12, FontStyle.Bold), Color.White);
            }
        }

        private void DrawFoodPixelArt(Graphics g, FoodType foodType, int gridX, int gridY)
        {
            bool[,] pixels = foodPixelArt[foodType];
            Color foodColor = foodColors[foodType];
            int pixelSize = gridSize / 5;
            int startX = gridX * gridSize + (gridSize - pixelSize * 5) / 2;
            int startY = gridY * gridSize + (gridSize - pixelSize * 5) / 2;

            using (SolidBrush pixelBrush = new SolidBrush(foodColor))
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (pixels[x, y])
                        {
                            Rectangle pixelRect = new Rectangle(
                                startX + x * pixelSize,
                                startY + y * pixelSize,
                                pixelSize,
                                pixelSize
                            );
                            g.FillRectangle(pixelBrush, pixelRect);

                            // Add pixel highlight
                            if (x > 0 && y > 0 && pixels[x - 1, y] && pixels[x, y - 1])
                            {
                                using (SolidBrush highlightBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 255)))
                                {
                                    g.FillRectangle(highlightBrush,
                                        pixelRect.X + pixelSize / 2,
                                        pixelRect.Y + pixelSize / 2,
                                        pixelSize / 2,
                                        pixelSize / 2);
                                }
                            }
                        }
                    }
                }
            }

            // Draw food name below
            string foodName = foodType.ToString();
            using (Font nameFont = new Font("Arial", 8, FontStyle.Bold))
            using (SolidBrush nameBrush = new SolidBrush(Color.White))
            {
                SizeF nameSize = g.MeasureString(foodName, nameFont);
                g.DrawString(foodName, nameFont, nameBrush,
                    gridX * gridSize + (gridSize - nameSize.Width) / 2,
                    gridY * gridSize + gridSize + 2);
            }
        }

        private void DrawCenteredText(Graphics g, string text, Font font, Color color)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            using (SolidBrush brush = new SolidBrush(color))
            {
                g.DrawString(text, font, brush,
                    new RectangleF(0, 0, picGameBoard.Width, picGameBoard.Height),
                    format);
            }
        }

        // ==================== BUTTON CONTROLS ====================
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (currentState == GameState.Start || currentState == GameState.GameOver)
            {
                StartNewGame();
            }
            else if (currentState == GameState.Playing)
            {
                var result = MessageBox.Show("Restart current game?", "Restart",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StartNewGame();
                }
            }
        }

        private void StartNewGame()
        {
            InitializeGame();
            currentState = GameState.Playing;
            gameRunning = true;
            gamePaused = false;
            gameTimer.Enabled = true;
            gameTimer.Interval = gameSpeed;

            btnStart.Text = "RESTART";
            btnPause.Enabled = true;
            btnPause.Text = "PAUSE";
            btnSettings.Enabled = true;
            lblStatus.Text = "GOOD LUCK! USE ARROW KEYS";
            lblStatus.ForeColor = highlightColor;

            this.Focus();
            this.ActiveControl = null;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            TogglePause();
        }

        private void TogglePause()
        {
            if (currentState != GameState.Playing && currentState != GameState.Paused)
                return;

            gamePaused = !gamePaused;

            if (gamePaused)
            {
                currentState = GameState.Paused;
                gameTimer.Enabled = false;
                btnPause.Text = "RESUME";
                lblStatus.Text = "PAUSED";
                lblStatus.ForeColor = Color.Cyan;
            }
            else
            {
                currentState = GameState.Playing;
                gameTimer.Enabled = true;
                btnPause.Text = "PAUSE";
                lblStatus.Text = "PLAYING";
                lblStatus.ForeColor = highlightColor;
            }

            picGameBoard.Invalidate();
            this.Focus();
            this.ActiveControl = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Exit game?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void ShowSettings()
        {
            currentState = GameState.Settings;
            gameTimer.Enabled = false;
            lblStatus.Text = "SETTINGS MENU";
            lblStatus.ForeColor = Color.Cyan;

            btnSnakeColor.Visible = true;
            btnBgColor.Visible = true;
            btnResetColors.Visible = true;
            btnBack.Visible = true;

            picGameBoard.Invalidate();
            this.Focus();
        }

        private void ShowGameScreen()
        {
            currentState = GameState.Playing;

            btnSnakeColor.Visible = false;
            btnBgColor.Visible = false;
            btnResetColors.Visible = false;
            btnBack.Visible = false;

            lblStatus.Text = "PLAYING";
            lblStatus.ForeColor = highlightColor;

            if (!gamePaused)
                gameTimer.Enabled = true;

            picGameBoard.Invalidate();
            this.Focus();
        }

        // ==================== SETTINGS CONTROLS ====================
        private void btnSnakeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = snakeHeadColor;
            colorDialog.CustomColors = new int[]
            {
                Color.HotPink.ToArgb(),
                Color.Magenta.ToArgb(),
                Color.Purple.ToArgb(),
                Color.Fuchsia.ToArgb(),
                Color.DeepPink.ToArgb()
            };

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                snakeHeadColor = colorDialog.Color;
                snakeBodyColor = Color.FromArgb(
                    Math.Max(0, snakeHeadColor.R - 30),
                    Math.Max(0, snakeHeadColor.G - 30),
                    Math.Max(0, snakeHeadColor.B - 30)
                );
                picGameBoard.Invalidate();
                PlaySound("eat");
            }
        }

        private void btnBgColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = backgroundColor;
            colorDialog.CustomColors = new int[]
            {
                Color.MidnightBlue.ToArgb(),
                Color.Navy.ToArgb(),
                Color.DarkBlue.ToArgb(),
                Color.MediumBlue.ToArgb(),
                Color.DarkSlateBlue.ToArgb()
            };

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                backgroundColor = colorDialog.Color;
                gridColor = Color.FromArgb(
                    Math.Min(255, backgroundColor.R + 50),
                    Math.Min(255, backgroundColor.G + 50),
                    Math.Min(255, backgroundColor.B + 50)
                );
                this.BackColor = backgroundColor;
                panelUI.BackColor = Color.FromArgb(
                    Math.Min(255, backgroundColor.R + 20),
                    Math.Min(255, backgroundColor.G + 20),
                    Math.Min(255, backgroundColor.B + 20)
                );
                picGameBoard.Invalidate();
                PlaySound("eat");
            }
        }

        private void btnResetColors_Click(object sender, EventArgs e)
        {
            snakeHeadColor = Color.FromArgb(255, 105, 180);
            snakeBodyColor = Color.FromArgb(255, 20, 147);
            backgroundColor = Color.FromArgb(25, 25, 112);
            gridColor = Color.FromArgb(100, 100, 150);

            this.BackColor = backgroundColor;
            panelUI.BackColor = Color.FromArgb(75, 0, 130);

            Apply90sStyleToButtons();
            picGameBoard.Invalidate();
            PlaySound("levelup");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ShowGameScreen();
        }

        // ==================== FOCUS MANAGEMENT ====================
        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Focus();
            this.ActiveControl = null;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.ActiveControl = null;
        }

        private void picGameBoard_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.ActiveControl = null;
        }

        private void panelUI_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.ActiveControl = null;
        }
    }
}