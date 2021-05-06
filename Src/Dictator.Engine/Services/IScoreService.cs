using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core.Services
{
    public interface IScoreService
    {
        public Score GetCurrentScore();
        public int GetCurrentHighscore();
    }
}
