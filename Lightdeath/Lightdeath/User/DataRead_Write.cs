using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lightdeath
{
    /// <summary>
    /// adatok beolvasás txt fájlbol illetve irása
    /// </summary>
    public class DataRead_Write
    {
        private StreamReader sr;

        private StreamWriter sw;

        private Character_classes read;

        private string[] r;

        /// <summary>
        /// cons of data write
        /// </summary>
        /// <param name="filename">filename of cons</param>
        /// <param name="ch">char of cons</param>
        /// <param name="username">username of cons</param>
        /// <param name="passwd">passwd of cons</param>
        public DataRead_Write(string filename, Character_classes ch, string username, string passwd)
        {
            this.sw = new StreamWriter(filename, false);
            this.sw.WriteLine(username);
            this.sw.WriteLine(passwd);
            if (ch is DarkMage)
            {
                this.sw.WriteLine("DM");
            }

            this.sw.WriteLine(ch.Name);
            this.sw.WriteLine(ch.MaxHP);
            this.sw.WriteLine(ch.LVL);
            this.sw.WriteLine(ch.Exp);
            this.sw.WriteLine(ch.Expneed);
            this.sw.WriteLine(ch.MaxResource);
            this.sw.WriteLine(ch.DMG);
            this.sw.WriteLine(ch.CriticalDMG);
            this.sw.WriteLine(ch.CritRate);
            this.sw.WriteLine(ch.Def);
            this.sw.WriteLine(ch.STR);
            this.sw.WriteLine(ch.INT);
            this.sw.WriteLine(ch.VIT);
            this.sw.WriteLine(ch.WIT);
            this.sw.WriteLine(ch.Men);
            this.sw.WriteLine(ch.Statpoint);
            this.sw.WriteLine(ch.Skillpoint);

            if (ch is DarkMage)
            {
                this.sw.WriteLine((ch as DarkMage).DarkBallSkillPoint);
                this.sw.WriteLine((ch as DarkMage).LightingSkillPoint);
                this.sw.WriteLine((ch as DarkMage).IonShieldSkillPoint);
                this.sw.WriteLine((ch as DarkMage).ContractSkillPoint);
            }

            this.sw.Close();
        }

        /// <summary>
        /// cons of data read
        /// </summary>
        /// <param name="filename">filename of cons</param>
        /// <param name="pass">pass of cons</param>
        public DataRead_Write(string filename, string pass)
        {
            this.sr = new StreamReader(filename, true);
            this.r = this.Readfile();
            if ((pass + "\r").Equals(this.Passwd))
            {
                if (this.r[2] == "DM\r")
                {
                    try
                    {
                        this.read = new DarkMage();
                        this.read.Name = this.r[3];
                        this.read.MaxHP = int.Parse(this.r[4]);
                        this.read.LVL = int.Parse(this.r[5]);
                        this.read.Exp = int.Parse(this.r[6]);
                        this.read.Expneed = int.Parse(this.r[7]);
                        this.read.MaxResource = int.Parse(this.r[8]);
                        this.read.DMG = double.Parse(this.r[9]);
                        this.read.CriticalDMG = double.Parse(this.r[10]);
                        this.read.CritRate = double.Parse(this.r[11]);
                        this.read.Def = int.Parse(this.r[12]);
                        this.read.STR = int.Parse(this.r[13]);
                        this.read.INT = int.Parse(this.r[14]);
                        this.read.VIT = int.Parse(this.r[15]);
                        this.read.WIT = int.Parse(this.r[16]);
                        this.read.Men = int.Parse(this.r[17]);
                        this.read.Statpoint = int.Parse(this.r[18]);
                        this.read.Skillpoint = int.Parse(this.r[19]);
                        (this.read as DarkMage).DarkBallSkillPoint = int.Parse(this.r[20]);
                        (this.read as DarkMage).LightingSkillPoint = int.Parse(this.r[21]);
                        (this.read as DarkMage).IonShieldSkillPoint = int.Parse(this.r[22]);
                        (this.read as DarkMage).ContractSkillPoint = int.Parse(this.r[23]);
                        this.read.CriticalDMG -= (0.05 * this.read.CriticalDMG);
                        this.read.CritRate -= (0.03 * this.read.CritRate);
                        this.read.DMG -= (0.02 * this.read.DMG);
                        this.read.HP -= 3;
                        this.read.Def -= 5;
                        this.read.MaxResource -= 10;
                    }
                    catch (Exception k)
                    {
                        MessageBox.Show(k.Message);
                        Application.Current.Shutdown();
                    }
                }
            }

            this.sr.Close();
        }

        /// <summary>
        /// Gets readed character
        /// </summary>
        public Character_classes Readed
        {
            get { return this.read; }
        }

        /// <summary>
        /// Gets accname
        /// </summary>
        public string Accname
        {
            get { return this.r[0]; }
        }

        /// <summary>
        /// Gets password of accname
        /// </summary>
        public string Passwd
        {
            get { return this.r[1]; }
        }

        private string[] Readfile()
        {
            string n = this.sr.ReadToEnd();
            string[] g = n.Split('\n');
            return g;
        }
    }
}
