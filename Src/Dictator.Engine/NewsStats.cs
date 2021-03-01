﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core
{
    public class NewsStats : INewsStats
    {
        private News[] news;

        public News CurrentNews { get; set; }

        public void Initialise()
        {
            news = new News[]
            {
                new News(0, 0, "MMMMMIMM", "MMMQMI", " PRESIDENT LOSES S.POLICE FILES "),
                new News(0, 0, "MMMMMMMM", "LMMVMM", " CUBANS ARM and TRAIN GUERILLAS "),
                new News(0, 0, "MMMMMMMM", "IMMOMN", "ACCIDENT. ARMY BARRACKS BLOWS UP"),
                new News(0, 0, "MMMMMMMM", "MMJMKM", "   BANANA PRICES FALL by 98%    "),
                new News(0, 0, "MMMMMMMM", "MMOMIM", "  MAJOR EARTHQUAKE in LEFTOTO   "),
                new News(0, 0, "MMMMMMMM", "MILKMM", "A PLAGUE SWEEPS through PEASANTS"),
            };
        }

        public News[] GetNews()
        {
            return (News[])news.Clone();
        }
    }
}
