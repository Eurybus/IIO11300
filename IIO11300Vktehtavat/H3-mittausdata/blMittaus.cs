﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO113
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
    }
}