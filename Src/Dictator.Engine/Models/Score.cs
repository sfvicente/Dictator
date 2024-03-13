namespace Dictator.Core.Models
{
    /// <summary>
    ///     Aggregates all elements requires to provide a score to a specific game.
    /// </summary>
    public class Score
    {
        /// <summary>
        ///     Gets or sets the combined popularity of the individual groups.
        /// </summary>
        public int TotalPopularity { get; set; }

        /// <summary>
        ///     Gets or sets the total months that the player has been in office.
        /// </summary>
        public int MonthsInOffice { get; set; }

        /// <summary>
        ///     Gets or sets the number of points awarded by the months of being in office.
        /// </summary>
        public int PointsForMonthsInOffice { get; set; }

        /// <summary>
        ///     Gets or sets the number of points awarded for ending the game alive.
        /// </summary>
        public int PointsForStayingAlive { get; set; }

        /// <summary>
        ///     Gets or sets the amount of money stolen from the treasury to the Swissb bank account.
        /// </summary>
        public int MoneyGrabbed { get; set; }

        /// <summary>
        ///     Gets or sets the number of points awarded by the amount of money stolen from the treasury to the Swissb bank account.
        /// </summary>
        public int PointsForMoneyGrabbing { get; set; }

        /// <summary>
        ///     Gets or sets the total score for the game.
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        ///     Gets or sets the current highest score of all the games played.
        /// </summary>
        public int HighestScore { get; set; }
    }
}
