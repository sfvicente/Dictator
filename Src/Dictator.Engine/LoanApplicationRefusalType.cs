using System;
using System.Collections.Generic;
using System.Text;

namespace Dictator.Core
{
    /// <summary>
    ///     Specifies the type of a loan application refusal.
    /// </summary>
    public enum LoanApplicationRefusalType
    {
        None = 0,
        TooEarly = 1,
        AlreadyUsed = 2,
        NotPopularEnough = 3
    }
}
