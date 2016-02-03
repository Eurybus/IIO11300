using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class MittausData
    {
        private string kello;

        public string Kello
        {
            get { return kello; }
            set { kello = value; }
        }
        private string mittaus;

        public string Mittaus
        {
            get { return mittaus; }
            set { mittaus = value; }
        }

        public string DataForm
        {
            get { return kello + ";" + mittaus; }
        }
        #region CONSTRUCTORS
        //luokalle tehdään kaksi konstruktoria
        public MittausData()
        {
            kello = "0:00";
            mittaus = "empty";
        }
        public MittausData(string klo, string mdata)
        {
            this.kello = klo;
            this.mittaus = mdata;
        }
        #endregion
        //ylikirjoitetaan ToString
        override public string ToString()
        {
            return kello + "=" + mittaus;
            //return base.ToString();
        }
        #region METHODS
        public static void SaveDataToFile(List<MittausData> data, string file)
        {
            //kirjoitetaan data tiedostoon, jos tiedosto on olemassa liitetään data olemassa olevaan
            try
            {
                //Tutkitaan onko tiedosto olemassa
                if (!System.IO.File.Exists(file))
                {
                    //luodaan uusi
                    using (StreamWriter sw = File.CreateText(file))
                    {
                        //Käydään kokoelma läpi ja kirjoitetaan kukin mittausdata omalle riville
                        foreach (var item in data)
                        {
                            sw.WriteLine(item.DataForm);
                        }
                    }
                }
                else
                {
                    //Lisätään olemassaolevaan tiedostoon
                    using (StreamWriter sw = File.AppendText(file))
                    {
                        foreach (var item in data)
                        {
                            sw.WriteLine(item.DataForm);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<MittausData> ReadDataFromFile(string filepath)
        {
            //Luetaan käyttäjän antamasta tiedostosta tekstireivejä
            //ja muuutetaan ne mittausdataksi
            
            try
            {
                if (File.Exists(filepath))
                {
                    using (StreamReader sr = File.OpenText(filepath))
                    {
                        //Luetaan tiedostoa rivi kerrallaan
                        MittausData md;
                        List<MittausData> luetut = new List<MittausData>();
                        string rivi = "";
                        while((rivi = sr.ReadLine()) != null)
                        {
                            //tutkitaan löytyykö sovittu erotinmerkki ; --> etupuolella on kellonaika ja jälkeen mittausarvo
                            if ((rivi.Length > 3) && rivi.Contains(";"))
                            {
                                string[] split = rivi.Split(new char[] { ';' });
                                //luodaan tekstinpätkistä olio
                                md = new MittausData(split[0], split[1]);
                                luetut.Add(md);
                            }
                        }
                        //Palautetaan
                        return luetut;
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        #endregion
    }
}
