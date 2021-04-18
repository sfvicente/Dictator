using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IGroupStats groupStats;

        public ScoreService(IGroupStats groupStats)
        {
            this.groupStats = groupStats;
        }

        public Score GetCurrentScore()
        {
            Score score = new Score()
            {
                TotalPopularity = groupStats.GetTotalPopularity(),
                // TODO: Complement with remaining data
            };

            return score;
        }
    }
}
