﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictator.Core.Services
{
    public class NewsService : INewsService
    {
        private News[] news;

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

        public News[] GetUnusedNews()
        {
            News[] unusedNews = news.Where(x => !x.HasBeenUsed).ToArray();

            return unusedNews;
        }

        /// <summary>
        ///     Determines if a random news event should happen in the current month.
        /// </summary>
        /// <returns><c>true</c> if a random news event should happen in the current month; otherwise, <c>false</c>.</returns>
        public bool ShouldNewsHappen()
        {
            Random random = new Random();
            int number = random.Next(0, 2);

            if (number == 0)
            {
                return true;
            }

            return false;
        }

        public bool DoesUnusedNewsExist()
        {
            News[] unusedNews = GetUnusedNews();

            if (unusedNews.Any())
            {
                return true;
            }

            return false;
        }

        public News SelectRandomUnusedNews()
        {
            News[] unusedNews = GetUnusedNews();

            if (unusedNews.Any())
            {
                var rand = new Random();
                var randomUnusedNews = unusedNews.ElementAt(rand.Next(unusedNews.Count()));

                randomUnusedNews.HasBeenUsed = true;

                return randomUnusedNews;
            }

            throw new InvalidOperationException("There are unused news items in the collection.");
        }
    }
}
