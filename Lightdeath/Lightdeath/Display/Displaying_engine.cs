using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Lightdeath
{
    /// <summary>
    /// display of project
    /// </summary>
    public class Displaying_engine : FrameworkElement
    {
        ////private variables//
        #region
        private static Random rand;

        private Maps map;       

        ////for UI//
        private Geometry health;

        ////for UI//
        private Geometry monsterhealth;

        ////for UI//
        private Geometry mana;

        ////for UI//
        private Geometry expbar;

        ////for UI//
        private double offwhite;

        ////for UI//
        private double offred;

        ////for UI//
        private Geometry firstskillicon;      

        ////for UI//
        private Geometry secskillicon;

        ////for UI//
        private Geometry thirdskillicon;

        ////for UI//
        private Geometry fourthskillicon;

        ////for skills//
        private Vector dirvectorskill;

        ////for monsters//
        private Vector dirvectormonster;

        ////for UI//
        private Geometry exit;

        ////for UI//
        private Geometry menu;

        ////for UI//
        private Geometry save;

        ////for UI//
        private Geometry continues;

        ////for UI//
        private Geometry win;

        private bool spacenyomva;

        ////for UI//
        private Geometry deffeat;

        private Point mouseposition;

        private bool mousedown;       

        private bool ionshieldactive;      

        private int spawntime = 0;

        private int potionspawntime = 0;

        private bool criticalhit;

        private MainWindow main;

        private int manarecorver = 0;

        private StartGameScreen startgame;

        private DispatcherTimer timer;       

        private Vector skilldirvector;

        private Vector mousepoint;

        private Tower tower;

        private int gametime = 0;

        private Character_window cw;

        private Skill_window kw;       

        #endregion

        /// <summary>
        /// constructor of display
        /// </summary>
        /// <param name="map">map of game</param>
        /// <param name="main">main window</param>
        /// <param name="startgame">the startgame</param>
        public Displaying_engine(Maps map, MainWindow main, StartGameScreen startgame)
        {
            this.map = map;           
            this.main = main;
            this.startgame = startgame;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timer.Tick += Timer_Tick;
            cw = new Character_window(main.Vml.User.Chars, timer);
            kw = new Skill_window(main.Vml.User.Chars as DarkMage, timer);       
            spacenyomva = false;
            main.Closing += Main_Closing;
            main.WindowStyle = WindowStyle.ToolWindow;
            main.KeyDown += Main_KeyDown;
            main.MouseMove += Main_MouseMove;
            main.MouseUp += Main_MouseUp;
            main.MouseDown += Main_MouseDown;
            main.KeyUp += Main_KeyUp;
            main.PreviewMouseMove += Main_PreviewMouseMove;
            this.MouseMove += Displaying_engine_MouseMove;
        }

        /// <summary>
        /// Gets or sets the timer
        /// </summary>
        public DispatcherTimer Timer
        {
            get { return timer; }
            set { timer = value; }
        }      

        /// <summary>
        /// Gets or sets the game map
        /// </summary>
        public Maps Map
        {
            get { return map; }
            set { map = value; }
        }
        ////initalize game//
        #region
        /// <summary>
        /// initalize game
        /// </summary>
        public void Initalize()
        {
            try
            {
                this.Width = map.Width;
                this.Height = map.Height;                
                this.health = new EllipseGeometry(new Point((map.Width / 2) - 250, map.Height - 95), 50, 50);
                this.mana = new EllipseGeometry(new Point((map.Width / 2) + 230, map.Height - 95), 50, 50);
                this.firstskillicon = new RectangleGeometry(new Rect((map.Width / 2) - 180, map.Height - 120, 50, 50));
                this.secskillicon = new RectangleGeometry(new Rect((map.Width / 2) - 83, map.Height - 120, 50, 50));
                this.thirdskillicon = new RectangleGeometry(new Rect((map.Width / 2) + 15, map.Height - 120, 50, 50));
                this.fourthskillicon = new RectangleGeometry(new Rect((map.Width / 2) + 110, map.Height - 120, 50, 50));               
                timer.Start();
                mousedown = false;
                dirvectorskill = new Vector();
                dirvectormonster = new Vector();
                rand = new Random();
                map.Player.Damagedealt = 0;
                map.Player.Damagetaken = 0;
                map.Player.Killedmons = 0;            
                gametime = 0;
                tower = new Tower(map.Player, map);

                ////map backfround//
                if (map is Firstmap)
                {
                    main.Background = new ImageBrush(new BitmapImage(new Uri(@"images\firstmapback.PNG", UriKind.Relative)));
                }
                else if (map is Secondmap)
                {
                    main.Background = new ImageBrush(new BitmapImage(new Uri(@"images\Secondmapback.PNG", UriKind.Relative)));
                }
                else if (map is Thirdmap)
                {
                    main.Background = new ImageBrush(new BitmapImage(new Uri(@"images\Thirdmapback.PNG", UriKind.Relative)));
                }

                foreach (Monsters mon in map.Monsters)
                {
                    if (!(mon is Tower))
                    {
                        map.Removeelement.Add(mon);
                    }
                }

                foreach (Skills skill in map.Skill)
                {
                    map.Removeelement.Add(skill);
                }

                foreach (object item in map.Removeelement)
                {
                    map.Monsters.Remove((item as Monsters));
                    map.Skill.Remove((item as Skills));
                    map.Potions.Remove((item as Potion));
                }

                map.Player.Actpoint = new Point(60, 350);
                (map.Player as DarkMage).Refresh();
            }
            catch (Exception k)
            {
                using (StreamWriter sw = new StreamWriter("hibaaaaaaaaaa.txt"))
                {
                    sw.WriteLine(k.Message);
                }
            }
        }
        #endregion
        ////draw geometrys//
        #region
        /// <summary>
        /// the renderer
        /// </summary>
        /// <param name="drawingContext">the drawingcontext</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            ////draw player//
            if (map.Player != null)
            {
                drawingContext.DrawGeometry(map.Player.Image, null, map.Player.Geometry);
            }

            ////draw monsters//
            if (map.Monsters != null)
            {
                foreach (Monsters mons in map.Monsters)
                {
                    drawingContext.DrawGeometry(mons.Image, null, mons.Geometry);
                }
            }

            ////draw skills//
            if (map.Skill != null)
            {
                foreach (Skills skill in map.Skill)
                {
                    if (skill is Ionshield)
                    {
                        Pen pen = new Pen(Brushes.Aqua, 10);
                        drawingContext.DrawGeometry(null, pen, skill.Geometry);
                    }
                    else
                    {
                        drawingContext.DrawGeometry(skill.Image, null, skill.Geometry);
                    }
                }
            }

            ////draw potions//
            if (map.Potions != null)
            {
                foreach (Potion pot in map.Potions)
                {
                    drawingContext.DrawGeometry(pot.Image, null, pot.Geometry);
                }
            }

            ////Health display//
            if (health != null)
            {
                this.offwhite = 1 - ((double)map.Player.HP / (double)map.Player.MaxHP);
                this.offred = 1 - ((double)map.Player.HP / (double)map.Player.MaxHP) + 0.01;
                GradientStopCollection gsc = new GradientStopCollection();
                GradientStop gs1 = new GradientStop((Color)ColorConverter.ConvertFromString("#FFFF0000"), offred);
                GradientStop gs2 = new GradientStop((Color)ColorConverter.ConvertFromString("#FFFFFF"), offwhite);
                gsc.Add(gs1);
                gsc.Add(gs2);
                LinearGradientBrush lgb = new LinearGradientBrush(gsc, new Point(0.5, 0), new Point(0.5, 1));
                Pen pen = new Pen(Brushes.Black, 2);
                drawingContext.DrawGeometry(lgb, pen, health);
                FormattedText text = new FormattedText(map.Player.HP + "/" + map.Player.MaxHP, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                drawingContext.DrawText(text, new Point((map.Width / 2) - 275, map.Height - 105));
            }

            ////mana display//
            if (mana != null)
            {
                this.offwhite = 1 - ((double)map.Player.Resource / (double)map.Player.MaxResource);
                this.offred = 1 - ((double)map.Player.Resource / (double)map.Player.MaxResource) + 0.01;
                GradientStopCollection gsc = new GradientStopCollection();
                GradientStop gs1 = new GradientStop((Color)ColorConverter.ConvertFromString("#FF002EFF"), offred);
                GradientStop gs2 = new GradientStop((Color)ColorConverter.ConvertFromString("#FFFFFF"), offwhite);
                gsc.Add(gs1);
                gsc.Add(gs2);
                LinearGradientBrush lgb = new LinearGradientBrush(gsc, new Point(0.5, 0), new Point(0.5, 1));
                Pen pen = new Pen(Brushes.Black, 2);
                drawingContext.DrawGeometry(lgb, pen, mana);
                FormattedText text = new FormattedText(map.Player.Resource + "/" + map.Player.MaxResource, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                drawingContext.DrawText(text, new Point((map.Width / 2) + 210, map.Height - 105));
            }

            ////expbar display//
            this.expbar = new RectangleGeometry(new Rect((map.Width / 2) - 190, (map.Height - 145), (((double)map.Player.Exp / (double)map.Player.Expneed) * 360), 10));
            if (expbar != null)
            {
                Pen pen = new Pen(Brushes.White, 2);
                drawingContext.DrawGeometry(Brushes.GreenYellow, pen, expbar);
                FormattedText text = new FormattedText(map.Player.Exp + "/" + map.Player.Expneed, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Green);
                drawingContext.DrawText(text, new Point((map.Width / 2) - 30, map.Height - 150));
            }

            ////DarkBall skill icon//
            if (firstskillicon != null && (map.Player as DarkMage).DarkBallSkillPoint != 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\DarkBall_skillicon.png", UriKind.Relative)));
                drawingContext.PushOpacity(1);
                drawingContext.DrawGeometry(ib, pen, firstskillicon);
            }
            else if (firstskillicon != null && (map.Player as DarkMage).DarkBallSkillPoint == 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\padlock.png", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, firstskillicon);
            }

            ////Lighting skill icon//
            if (secskillicon != null && (map.Player as DarkMage).LightingSkillPoint != 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\Lightingskill_icon.PNG", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, secskillicon);
            }
            else if (secskillicon != null && (map.Player as DarkMage).LightingSkillPoint == 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\padlock.PNG", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, secskillicon);
            }

            ////Ion Shiled skill icon//
            if (thirdskillicon != null && (map.Player as DarkMage).IonShieldSkillPoint != 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\Ionshield_icon.PNG", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, thirdskillicon);
            }
            else if (thirdskillicon != null && (map.Player as DarkMage).IonShieldSkillPoint == 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\padlock.PNG", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, thirdskillicon);
            }

            ////Contract skill icon
            if (fourthskillicon != null && (map.Player as DarkMage).ContractSkillPoint != 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\Contractskill.png", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, fourthskillicon);
            }
            else if (fourthskillicon != null && (map.Player as DarkMage).ContractSkillPoint == 0)
            {
                Pen pen = new Pen(Brushes.Black, 2);
                ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(@"images\padlock.PNG", UriKind.Relative)));
                drawingContext.DrawGeometry(ib, pen, fourthskillicon);
            }

            ////monster health bar//
            if (mouseposition != null)
            {
                foreach (Monsters mon in map.Monsters)
                {
                    if (mon.Geometry.Bounds.Contains(mouseposition))
                    {
                        GeometryGroup gg = new GeometryGroup();
                        FormattedText text = new FormattedText(mon.Name, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Green);
                        gg.Children.Add(text.BuildGeometry(new Point((map.Width / 2) - 30, 10)));
                        gg.Children.Add(new RectangleGeometry(new Rect((map.Width / 2) - 60, 40, (((double)mon.HP / (double)mon.MaxHP) * 100), 10)));
                        this.monsterhealth = gg;
                        drawingContext.DrawGeometry(Brushes.Red, null, monsterhealth);
                    }
                }
            }

            ////menu exit button//
            if (exit != null)
            {
                if (exit.Bounds.Contains(mouseposition))
                {
                    drawingContext.DrawGeometry(Brushes.Lavender, null, exit);
                    FormattedText text = new FormattedText("Exit", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 15, (Height / 2) + 65));
                }
                else
                {
                    drawingContext.DrawGeometry(Brushes.Gray, null, exit);
                    FormattedText text = new FormattedText("Exit", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 15, (Height / 2) + 65));
                }
            }

            ////menu continue button//
            if (menu != null)
            {              
                if (menu.Bounds.Contains(mouseposition))
                {
                    drawingContext.DrawGeometry(Brushes.Lavender, null, menu);
                    FormattedText text = new FormattedText("Back to Menu", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 45, (Height / 2) - 5));
                }
                else
                {
                    drawingContext.DrawGeometry(Brushes.Gray, null, menu);
                    FormattedText text = new FormattedText("Back to Menu", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 45, (Height / 2) - 5));
                }
            }

            ////menu save button//
            if (save != null)
            {
                if (save.Bounds.Contains(mouseposition))
                {
                    drawingContext.DrawGeometry(Brushes.Lavender, null, save);
                    FormattedText text = new FormattedText("Save", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 17, (Height / 2) - 75));
                }
                else
                {
                    drawingContext.DrawGeometry(Brushes.Gray, null, save);
                    FormattedText text = new FormattedText("Save", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 17, (Height / 2) - 75));
                }
            }

            ////menu continue button//
            if (continues != null)
            {               
                if (continues.Bounds.Contains(mouseposition))
                {
                    drawingContext.DrawGeometry(Brushes.Lavender, null, continues);
                    FormattedText text = new FormattedText("Continue", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 30, (Height / 2) - 145));
                }
                else
                {
                    drawingContext.DrawGeometry(Brushes.Gray, null, continues);
                    FormattedText text = new FormattedText("Continue", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                    drawingContext.DrawText(text, new Point((Width / 2) - 30, (Height / 2) - 145));
                }
            }

            ////win button//
            if (win != null)
            {
                drawingContext.DrawGeometry(Brushes.Gray, null, win);
                FormattedText text = new FormattedText("You WIN!!", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 145, Brushes.Green);
                FormattedText textok = new FormattedText("OK", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                drawingContext.DrawText(text, new Point((Width / 2) - 310, (Height / 2) - 180));
                drawingContext.DrawText(textok, new Point((Width / 2) - 10, (Height / 2) + 16));
            }

            ////deffeat button//
            if (deffeat != null)
            {
                drawingContext.DrawGeometry(Brushes.Gray, null, deffeat);
                FormattedText text = new FormattedText("You DEFFEAT!!", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 145, Brushes.Red);
                FormattedText textok = new FormattedText("OK", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Black);
                drawingContext.DrawText(text, new Point((Width / 2) - 480, (Height / 2) - 180));
                drawingContext.DrawText(textok, new Point((Width / 2) - 10, (Height / 2) + 16));
            }

            ////info display//
            if (map.Player.Skillpoint > 0 || map.Player.Statpoint > 0)
            {
                FormattedText info = new FormattedText("Press button 's' to go the Skills, Press button 'k' to go the Character Stats", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 15, Brushes.Azure);
                drawingContext.DrawText(info, new Point((Width / 2) - 240, 50));
            }            
        }
        #endregion
        ////timer ticks//
        #region
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                spawntime += 10;
                potionspawntime += 10;
                manarecorver += 20;
                if (gametime <= int.MaxValue)
                {
                    gametime += 5;
                }                            

                ////mana recorver//
                if (manarecorver % 100 == 0)
                {
                    if (map.Player.Resource != map.Player.MaxResource)
                    {
                        map.Player.Resource++;
                    }

                    manarecorver = 0;
                }

                ////boss spawn//
                if (!tower.Alive && Map is Firstmap && (map as Firstmap).Boss == null)
                {
                    (map as Firstmap).Boss = new Naratos(map.Player, tower.Actpoint.X, tower.Actpoint.Y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (!tower.Alive && map is Secondmap && (map as Secondmap).Boss == null)
                {
                    (map as Secondmap).Boss = new Mariel(map.Player, tower.Actpoint.X, tower.Actpoint.Y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (!tower.Alive && map is Thirdmap && (map as Thirdmap).Boss == null)
                {
                    (map as Thirdmap).Boss = new DeathKing(map.Player, tower.Actpoint.X, tower.Actpoint.Y, map, dirvectormonster.X, dirvectormonster.Y);
                }

                ////the boss is alive or no
                if (!tower.Alive && Map is Firstmap && !(map as Firstmap).Boss.Alive && map.Monsters.Count == 0)
                {
                    (map as Firstmap).Boss = null;
                    win = new RectangleGeometry(new Rect((map.Width / 2) - 50, (map.Height / 2), 100, 50));
                    timer.Stop();
                }
                else if (!tower.Alive && map is Secondmap && !(map as Secondmap).Boss.Alive && map.Monsters.Count == 0)
                {
                    (map as Secondmap).Boss = null;
                    win = new RectangleGeometry(new Rect((map.Width / 2) - 50, (map.Height / 2), 100, 50));
                    timer.Stop();
                }
                else if (!tower.Alive && map is Thirdmap && !(map as Thirdmap).Boss.Alive && map.Monsters.Count == 0)
                {
                    (map as Thirdmap).Boss = null;
                    win = new RectangleGeometry(new Rect((map.Width / 2) - 50, (map.Height / 2), 100, 50));
                    timer.Stop();
                }

                foreach (Potion pot in map.Potions)
                {
                    if (pot is PotionHP && (pot as PotionHP).Removable)
                    {
                        map.Removeelement.Add(pot);
                    }

                    if (pot is PotionMP && (pot as PotionMP).Removable)
                    {
                        map.Removeelement.Add(pot);
                    }
                }

                ////player dead then go back main menu//
                if (!map.Player.Alive)
                {
                    timer.Stop();
                    deffeat = new RectangleGeometry(new Rect((map.Width / 2) - 50, (map.Height / 2), 100, 50));
                }

                ////player move//
                if (mousedown)
                {
                    double angle = Math.Atan2(mouseposition.Y - map.Player.Actpoint.Y, mouseposition.X - map.Player.Actpoint.X);
                    Transform t = new TranslateTransform((2 * Math.Cos(angle)), (2 * Math.Sin(angle)));
                    map.Player.Actpoint = new Point(map.Player.Actpoint.X + (2 * Math.Cos(angle)), map.Player.Actpoint.Y + (2 * Math.Sin(angle)));
                    map.Player.Move(t);
                    foreach (Potion pot in map.Potions)
                    {
                        if (map.Player.Geometry.Bounds.IntersectsWith(pot.Geometry.Bounds))
                        {
                            if (pot is PotionHP)
                            {
                                (pot as PotionHP).RecorverHP();
                                map.Removeelement.Add(pot);
                            }
                            else
                            {
                                (pot as PotionMP).RecorverMP();
                                map.Removeelement.Add(pot);
                            }
                        }
                    }
                }

                ////spawn monster//
                if (gametime < 10000)
                {
                    if (spawntime % 1000 == 0 && map.Monsters.Contains(tower))
                    {
                        int x = rand.Next(0 + 15, (int)Width - 40);
                        int y = rand.Next(0 + 15, (int)Height - 40);
                        Portal portal = new Portal(map, x, y);
                        if (!(map is Thirdmap))
                        {
                            Spawnmonster(rand.Next(0, 2), x, y);
                        }
                        else
                        {
                            Spawnmonster(rand.Next(0, 4), x, y);
                        }

                        spawntime = 0;
                    }
                }
                else if (gametime > 10000 && gametime < 20000)
                {
                    if (spawntime % 500 == 0 && map.Monsters.Contains(tower))
                    {
                        int x = rand.Next(0 + 15, (int)Width - 40);
                        int y = rand.Next(0 + 15, (int)Height - 40);
                        Portal portal = new Portal(map, x, y);
                        if (!(map is Thirdmap))
                        {
                            Spawnmonster(rand.Next(0, 2), x, y);
                        }
                        else
                        {
                            Spawnmonster(rand.Next(0, 4), x, y);
                        }

                        spawntime = 0;
                    }
                }
                else if (gametime > 20000)
                {
                    if (spawntime % 250 == 0 && map.Monsters.Contains(tower))
                    {
                        int x = rand.Next(0 + 15, (int)Width - 40);
                        int y = rand.Next(0 + 15, (int)Height - 40);
                        Portal portal = new Portal(map, x, y);
                        if (!(map is Thirdmap))
                        {
                            Spawnmonster(rand.Next(0, 2), x, y);
                        }
                        else
                        {
                            Spawnmonster(rand.Next(0, 4), x, y);
                        }

                        spawntime = 0;
                    }
                }

                ////spawn potion//
                if (potionspawntime == 2000)
                {
                    Spawnpotion(rand.Next(0, 2), rand.Next(0 + 15, (int)Width - 40), rand.Next(0 + 15, (int)Height - 40));
                    potionspawntime = 0;
                }

                ////for skill move//
                foreach (Skills skill in map.Skill)
                {
                    ////ha kimegy a pályárol skill akkor törlési listába kerül//
                    if (skill.Geometry.Bounds.Right < 0)
                    {
                        map.Removeelement.Add(skill);
                    }
                    else if (skill.Geometry.Bounds.Left > map.Width)
                    {
                        map.Removeelement.Add(skill);
                    }

                    if (skill.Geometry.Bounds.Bottom < 0)
                    {
                        map.Removeelement.Add(skill);
                    }
                    else if (skill.Geometry.Bounds.Top > map.Height)
                    {
                        map.Removeelement.Add(skill);
                    }

                    ////adot skillt mozgat//
                    if ((skill as Darkball) != null && (skill as Darkball).Crashee == false && (skill as Darkball).Removeable == false)
                    {
                        (skill as Darkball).Move();
                    }
                    else if ((skill as Darkball) != null && (skill as Darkball).Removeable)
                    {
                        map.Removeelement.Add(skill);
                    }

                    ////skill mozgatás//
                    if ((skill as Lighting) != null && !(skill as Lighting).Removeable)
                    {
                        (skill as Lighting).Move();
                    }
                    else if ((skill as Lighting) != null && (skill as Lighting).Removeable)
                    {
                        map.Removeelement.Add(skill);
                    }

                    ////a shield skill a karival egyutt mozogjon//
                    if ((skill as Ionshield) != null && mousedown && !((skill as Ionshield).Removeable))
                    {
                        double angle = Math.Atan2(mouseposition.Y - map.Player.Actpoint.Y, mouseposition.X - map.Player.Actpoint.X);
                        Transform t = new TranslateTransform((2 * Math.Cos(angle)), (2 * Math.Sin(angle)));
                        skill.Actpoint = new Point(skill.Actpoint.X + (2 * Math.Cos(angle)), skill.Actpoint.Y + (2 * Math.Sin(angle)));
                        (skill as Ionshield).Move(t);
                    }
                    else if ((skill as Ionshield) != null && ((skill as Ionshield).Removeable))
                    {
                        map.Removeelement.Add(skill);
                    }

                    if ((skill as Contract) != null && (skill as Contract).Removeable)
                    {
                        map.Removeelement.Add(skill);
                    }

                    if ((skill as Basic_attack) != null)
                    {
                        (skill as Basic_attack).Move();
                    }

                    if (skill is Portal  && (skill as Portal).Removeable)
                    {
                        map.Removeelement.Add(skill);
                    }

                    ////ez a dark ball terulet sebzésnek kell//
                    foreach (Monsters mons in map.Monsters)
                    {
                        if ((skill as Darkball) != null && (skill as Darkball).Crash(mons.Geometry.Bounds))
                        {
                            (skill as Darkball).Crashee = true;
                            foreach (Monsters monster in map.Indistance(150, skill.Actpoint.X, skill.Actpoint.Y))
                            {
                                int denom = (map.Player as DarkMage).Critdenominator();
                                int cou = rand.Next(1, denom + 1);

                                ////crit hit//
                                if (cou <= (map.Player as DarkMage).Counter())
                                {
                                    criticalhit = true;
                                }
                                else
                                {
                                    criticalhit = false;
                                }

                                if ((skill as Darkball).Removeable == true)
                                {
                                    if (criticalhit)
                                    {
                                        monster.Getdmg((int)(skill.DMG * map.Player.CriticalDMG));
                                    }
                                    else
                                    {
                                        monster.Getdmg(skill.DMG);
                                    }

                                    if (!monster.Alive)
                                    {
                                        map.Removeelement.Add(monster);
                                    }
                                }
                            }
                            break;
                        }
                    }

                    ////basic attack betlál a szörnybe akkor sebezze//
                    foreach (Monsters mon in map.Monsters)
                    {
                        if ((skill as Basic_attack) != null && (skill as Basic_attack).Crash(mon.Geometry.Bounds))
                        {
                            int denom = (map.Player as DarkMage).Critdenominator();
                            int cou = rand.Next(1, denom + 1);

                            ////crit hit//
                            if (cou <= (map.Player as DarkMage).Counter())
                            {
                                criticalhit = true;
                            }
                            else
                            {
                                criticalhit = false;
                            }

                            if (criticalhit)
                            {
                                mon.Getdmg((int)(skill.DMG * map.Player.CriticalDMG));
                            }
                            else
                            {
                                mon.Getdmg(skill.DMG);
                            }

                            map.Removeelement.Add(skill);
                            if (!mon.Alive)
                            {
                                map.Removeelement.Add(mon);
                            }
                        }
                    }                    
                }

                ////for monsters move//
                foreach (Monsters mons in map.Monsters)
                {
                    if (!(mons is Tower))
                    {
                        double angle = Math.Atan2(map.Player.Actpoint.Y - mons.Actpoint.Y, map.Player.Actpoint.X - mons.Actpoint.X);
                        dirvectormonster.X = Math.Cos(angle);
                        dirvectormonster.Y = Math.Sin(angle);
                        mons.DirX = dirvectormonster.X;
                        mons.DirY = dirvectormonster.Y;
                        if (!((map.Player as DarkMage).Crash(mons.Geometry.Bounds)))
                        {
                            mons.Monsmove(new TranslateTransform(mons.DirX, mons.DirY));
                        }
                        else if ((map.Player as DarkMage).Crash(mons.Geometry.Bounds))
                        {
                            if (ionshieldactive)
                            {
                                mons.Hit(map.Player, (int)(mons.DMG * (0.8 - ((map.Player as DarkMage).IonShieldSkillPoint * 0.05))));
                            }
                            else
                            {
                                mons.Hit(map.Player, mons.DMG);
                            }
                        }
                    }
                }

                ////for remove element//
                foreach (object element in map.Removeelement)
                {
                    map.Skill.Remove(element as Skills);
                    map.Monsters.Remove(element as Monsters);
                    map.Potions.Remove(element as Potion);
                }

                InvalidateVisual();
            }
            catch (Exception k)
            {
                using (StreamWriter sw = new StreamWriter("hibaaaaa.txt"))
                {
                    sw.WriteLine(k.Message);
                }
            }
        }
        #endregion
        ////keyp up-down mouse up-move-down event//
        #region
        private void Main_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ////eger gombot felengedi //
            if (main.Content == this)
            {
                mousedown = false;           
            }
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            ////hogy folyamatosan nyomva a space ne lojen sok basic attackot//
            if (main.Content == this)
            {
                if (e.Key == Key.Space)
                {
                    spacenyomva = false;
                }
            }
        }

        ////mouse aktuális pont számolás illetve irányvektorok//
        private void Displaying_engine_MouseMove(object sender, MouseEventArgs e)
        {
            if (main.Content == this)
            {
                Vector mousepoint = new Vector(e.GetPosition(this).X, e.GetPosition(this).Y);
                dirvectorskill.X = mousepoint.X - map.Player.Actpoint.X;
                dirvectorskill.Y = mousepoint.Y - map.Player.Actpoint.Y;
                mouseposition = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);                
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// billentyu lenyomás
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the event</param>
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (main.Content == this)
            {
                ////basic attack//
                if (e.Key == Key.Space && spacenyomva == false && deffeat == null && win == null && exit == null && kw.Visibility != Visibility.Visible && cw.Visibility != Visibility.Visible)
                {
                    skilldirvector = new Vector(dirvectorskill.X, dirvectorskill.Y);
                    double angle = Math.Atan2(skilldirvector.Y, skilldirvector.X);
                    Basic_attack ba = new Basic_attack((int)map.Player.DMG, map.Player.Actpoint.X + 30, map.Player.Actpoint.Y - 25, map, Math.Cos(angle), Math.Sin(angle));
                    spacenyomva = true;
                }

                ////Darkball skill//
                if (e.Key == Key.W && (map.Player as DarkMage).DarkBallSkillPoint != 0 && deffeat == null && win == null && exit == null && kw.Visibility != Visibility.Visible && cw.Visibility != Visibility.Visible)
                {
                    skilldirvector = new Vector(dirvectorskill.X, dirvectorskill.Y);
                    double angle = Math.Atan2(skilldirvector.Y, skilldirvector.X);
                    if (map.Player.Resource > 0 && map.Player.Resource - 10 >= 0)
                    {
                        Darkball db = new Darkball((int)(map.Player.DMG * (1.8 + ((1 / 5) * (map.Player as DarkMage).LightingSkillPoint))), map.Player.Actpoint.X + 30, map.Player.Actpoint.Y - 25, map, (8 * Math.Cos(angle)), (8 * Math.Sin(angle)));
                        map.Player.Resource -= db.Cost;
                    }
                }

                ////lighting skill//
                if (e.Key == Key.E && (map.Player as DarkMage).LightingSkillPoint != 0 && deffeat == null && win == null && exit == null && kw.Visibility != Visibility.Visible && cw.Visibility != Visibility.Visible)
                {
                    skilldirvector = new Vector(dirvectorskill.X, dirvectorskill.Y);
                    double angle = Math.Atan2(skilldirvector.Y, skilldirvector.X);
                    if (map.Player.Resource > 0 && map.Player.Resource - 15 >= 0)
                    {
                        Lighting db = new Lighting((int)(map.Player.DMG * (2 + ((1 / 5) * (map.Player as DarkMage).LightingSkillPoint))), map.Player.Actpoint.X + 30, map.Player.Actpoint.Y - 25, map, (12 * Math.Cos(angle)), (12 * Math.Sin(angle)), (map.Player as DarkMage));
                        map.Player.Resource -= db.Cost;
                    }
                }

                ////ionshield skill//
                if (e.Key == Key.R && (map.Player as DarkMage).IonShieldSkillPoint != 0 && deffeat == null && win == null && exit == null && kw.Visibility != Visibility.Visible && cw.Visibility != Visibility.Visible)
                {
                    if (map.Player.Resource > 0 && map.Player.Resource - 100 >= 0)
                    {
                        Ionshield io = new Ionshield(map, map.Player.Actpoint.X, map.Player.Actpoint.Y);
                        map.Player.Resource -= io.Cost;
                        ionshieldactive = true;
                    }
                }

                ////contract skill//
                if (e.Key == Key.T && (map.Player as DarkMage).ContractSkillPoint != 0 && deffeat == null && win == null && exit == null && kw.Visibility != Visibility.Visible && cw.Visibility != Visibility.Visible)
                {
                    if (map.Player.Resource > 0 && map.Player.Resource - 25 >= 0)
                    {
                        Contract c = new Contract(map, mouseposition.X, mouseposition.Y, 150 + ((map.Player as DarkMage).ContractSkillPoint * 15), 150 + ((map.Player as DarkMage).ContractSkillPoint * 15), 150 + ((map.Player as DarkMage).ContractSkillPoint * 15));
                        map.Player.Resource -= c.Cost;
                        c.Cont();                       
                    }
                }

                ////stat window//
                if (e.Key == Key.K && deffeat == null && win == null && kw.Visibility != Visibility.Visible && menu == null)
                {
                    cw.Show();
                    timer.Stop();                  
                }

                ////skill sindow//
                if (e.Key == Key.S && deffeat == null && win == null && cw.Visibility != Visibility.Visible && menu == null)
                {
                    kw.Visibility = Visibility.Visible;
                    timer.Stop();                  
                }

                ////menu//
                if (e.Key == Key.Escape && deffeat == null && win == null && kw.Visibility != Visibility.Visible && cw.Visibility != Visibility.Visible)
                {
                    timer.Stop();
                    exit = new RectangleGeometry(new Rect((Width / 2) - 50, (Height / 2) + 50, 100, 50));
                    menu = new RectangleGeometry(new Rect((Width / 2) - 50, (Height / 2) - 20, 100, 50));
                    save = new RectangleGeometry(new Rect((Width / 2) - 50, (Height / 2) - 90, 100, 50));
                    continues = new RectangleGeometry(new Rect((Width / 2) - 50, (Height / 2) - 160, 100, 50));
                }

                InvalidateVisual();
            }
        }

        private void Main_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ////hogy a main window contetjébe a framework utod van e//
            if (main.Content == this)
            {
                mousedown = true;
                mousepoint = new Vector(e.GetPosition(this).X, e.GetPosition(this).Y);
                dirvectorskill.X = mousepoint.X - map.Player.Actpoint.X;
                dirvectorskill.Y = mousepoint.Y - map.Player.Actpoint.Y;

                ////menu exitre kattva leáll a prog és elmenti a karit//
                if (exit != null && exit.Bounds.Contains(mouseposition.X, mouseposition.Y) && mousedown)
                {
                    main.Vml.User.Save();
                    Application.Current.Shutdown();
                }

                ////főmenübe lép//
                if (menu != null && menu.Bounds.Contains(mouseposition.X, mouseposition.Y) && mousedown)
                {
                    main.Content = startgame.Start;
                    menu = null;
                    exit = null;
                    save = null;
                    continues = null;
                }

                ////elmenti a karit//
                if (save != null && save.Bounds.Contains(mouseposition.X, mouseposition.Y) && mousedown)
                {
                    main.Vml.User.Save();
                    menu = null;
                    exit = null;
                    save = null;
                    continues = null;
                    timer.Start();
                }

                ////folytatás//
                if (continues != null && continues.Bounds.Contains(mouseposition.X, mouseposition.Y) && mousedown)
                {                    
                    menu = null;
                    exit = null;
                    save = null;
                    continues = null;
                    timer.Start();
                }

                ////ha nyer megáll minden ha ok-éára kattint főmenübe dobja//
                if (win != null && win.Bounds.Contains(mouseposition.X, mouseposition.Y) && mousedown)
                {
                    win = null;
                    main.Content = startgame.Start;
                }

                ////ha veszt megáll minden ha ok-éára kattint főmenübe dobja//
                if (deffeat != null && deffeat.Bounds.Contains(mouseposition.X, mouseposition.Y) && mousedown)
                {
                    deffeat = null;
                    main.Content = startgame.Start;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            ////mouse aktuális pont számolás illetve irányvektorok//
            if (main.Content == this)
            {
                Vector mousepoint = new Vector(e.GetPosition(this).X, e.GetPosition(this).Y);
                dirvectorskill.X = mousepoint.X - (map.Player.Actpoint.X + 30);
                dirvectorskill.Y = mousepoint.Y - (map.Player.Actpoint.Y - 25);
                mouseposition = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);
                if (exit != null)
                {
                    InvalidateVisual();
                }       
            }
            else
            {
                e.Handled = true;
            }           
        }

        private void Main_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ////mouse aktuális pont számolás illetve irányvektorok//
            if (main.Content == this)
            {
                Vector mousepoint = new Vector(e.GetPosition(this).X, e.GetPosition(this).Y);
                dirvectorskill.X = mousepoint.X - map.Player.Actpoint.X + 30;
                dirvectorskill.Y = mousepoint.Y - map.Player.Actpoint.Y - 25;
                mouseposition = new Point(e.GetPosition(this).X, e.GetPosition(this).Y);
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion
        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        ////spawn monsters and potions//
        #region
        /// <summary>
        /// spawn monster
        /// </summary>
        /// <param name="random">random num</param>
        /// <param name="x">x numb</param>
        /// <param name="y">y numb</param>
        private void Spawnmonster(int random, int x, int y)
        {  
            ////monster spawnolo a pálya függvényébe//          
            if (map is Firstmap)
            {
                if (random == 0)
                {
                    Skeleton skeleton = new Skeleton(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (random == 1)
                {
                    HellDog dog = new HellDog(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
            }
            else if (map is Secondmap)
            {
                if (random == 0)
                {
                    Ghoul skeleton = new Ghoul(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (random == 1)
                {
                    FireLord dog = new FireLord(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
            }
            else if (map is Thirdmap)
            {
                if (random == 0)
                {
                    Skeleton skeleton = new Skeleton(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (random == 1)
                {
                    HellDog dog = new HellDog(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (random == 2)
                {
                    Ghoul skeleton = new Ghoul(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
                else if (random == 3)
                {
                    FireLord dog = new FireLord(map.Player, x, y, map, dirvectormonster.X, dirvectormonster.Y);
                }
            }
        }

        private void Spawnpotion(int random, int x, int y)
        {     
            ////potiont spawnol random helyen a pályán//       
            if (random == 0)
            {
                PotionHP pot = new PotionHP(map.Player, map, x, y);
            }
            else if (random == 1)
            {
                PotionMP pot = new PotionMP(map.Player, map, x, y);
            }
        }
        #endregion
    }
}
