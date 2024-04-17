using Dictator.Core.Models;
using System.ComponentModel;

namespace Dictator.Core.Services;

public interface IStateManagementService
{
    bool HasLoadBeenGranted(LenderCountry lenderCountry);
    void SetLoanBeenGranted(LenderCountry lenderCountry);
}

public class StateManagementService : IStateManagementService
{
    private bool _hasAmericanLoanBeenGranted;
    private bool _hasRussianLoanBeenGranted;

    public bool HasLoadBeenGranted(LenderCountry lenderCountry)
    {
        if (lenderCountry == LenderCountry.America)
        {
            return _hasAmericanLoanBeenGranted;
        }
        else if (lenderCountry == LenderCountry.Russia)
        {
            return _hasRussianLoanBeenGranted;
        }

        throw new InvalidEnumArgumentException(nameof(lenderCountry));
    }

    public void SetLoanBeenGranted(LenderCountry lenderCountry)
    {
        if(lenderCountry == LenderCountry.America)
        {
            _hasAmericanLoanBeenGranted = true;
        }
        else if(lenderCountry == LenderCountry.Russia)
        {
            _hasRussianLoanBeenGranted = true;
        }

        throw new InvalidEnumArgumentException(nameof(lenderCountry));
    }
}
