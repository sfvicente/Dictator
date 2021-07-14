using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IScoreService
    {
        public Score GetCurrentScore();
        public int GetCurrentHighscore();

        /// <summary>
        ///     Records the current score as the new high score if it is greater than the previous score.
        /// </summary>
        public void SaveHighScore();
    }
}
