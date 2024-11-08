using Dictator.Core.Models;
using System.ComponentModel;

namespace Dictator.Core.Services;

public interface IStateManagementService
{
    bool HasLoanBeenGranted(LenderCountry lenderCountry);
    void SetLoanHasBeenGranted(LenderCountry lenderCountry);
}

public class StateManagementService : IStateManagementService
{
    private bool _hasAmericanLoanBeenGranted;
    private bool _hasRussianLoanBeenGranted;

    public bool HasLoanBeenGranted(LenderCountry lenderCountry)
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

    public void SetLoanHasBeenGranted(LenderCountry lenderCountry)
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
