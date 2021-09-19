using Dictator.ConsoleInterface.Common;

namespace Dictator.ConsoleInterface.Revolution
{
    /// <summary>
    ///     Represents the dialog that is displayed when the player crushes the revolution
    ///     and is given a choice to punish the revolutionaries.
    /// </summary>
    public interface IRevolutionCrushedDialog
    {
        public DialogResult Show();
    }
}
