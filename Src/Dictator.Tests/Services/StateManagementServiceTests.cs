using System.ComponentModel;
using Dictator.Core.Models;
using Dictator.Core.Services;

namespace Dictator.Core.Tests.Services;

[TestFixture]
public class StateManagementServiceTests
{
    private StateManagementService _stateManagementService;

    [SetUp]
    public void SetUp()
    {
        _stateManagementService = new StateManagementService();
    }

    [Test]
    public void HasLoanBeenGranted_InitiallyReturnsFalseForBothCountries()
    {
        Assert.IsFalse(_stateManagementService.HasLoanBeenGranted(LenderCountry.America));
        Assert.IsFalse(_stateManagementService.HasLoanBeenGranted(LenderCountry.Russia));
    }

    [Test]
    public void SetLoanHasBeenGranted_MarksSpecificCountryAsGranted()
    {
        _stateManagementService.SetLoanHasBeenGranted(LenderCountry.America);

        Assert.IsTrue(_stateManagementService.HasLoanBeenGranted(LenderCountry.America));
        Assert.IsFalse(_stateManagementService.HasLoanBeenGranted(LenderCountry.Russia));

        _stateManagementService.SetLoanHasBeenGranted(LenderCountry.Russia);

        Assert.IsTrue(_stateManagementService.HasLoanBeenGranted(LenderCountry.Russia));
    }

    [Test]
    public void Initialise_ResetsGrantedLoansToFalse()
    {
        _stateManagementService.SetLoanHasBeenGranted(LenderCountry.America);
        _stateManagementService.SetLoanHasBeenGranted(LenderCountry.Russia);

        _stateManagementService.Initialise();

        Assert.IsFalse(_stateManagementService.HasLoanBeenGranted(LenderCountry.America));
        Assert.IsFalse(_stateManagementService.HasLoanBeenGranted(LenderCountry.Russia));
    }

    [Test]
    public void HasLoanBeenGranted_WithInvalidCountry_ThrowsInvalidEnumArgumentException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
            _stateManagementService.HasLoanBeenGranted((LenderCountry)99));
    }

    [Test]
    public void SetLoanHasBeenGranted_WithInvalidCountry_ThrowsInvalidEnumArgumentException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
            _stateManagementService.SetLoanHasBeenGranted((LenderCountry)99));
    }
}
