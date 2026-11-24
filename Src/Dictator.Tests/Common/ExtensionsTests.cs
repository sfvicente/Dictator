using Dictator.Common.Extensions;

namespace Dictator.Core.Tests.Common;

[TestFixture]
public class ExtensionsTests
{
    [Test]
    public void IsEmpty_WithEmptyArray_ReturnsTrue()
    {
        int[] empty = [];

        Assert.IsTrue(empty.IsEmpty());
        Assert.IsFalse(empty.IsNotEmpty());
    }

    [Test]
    public void IsNotEmpty_WithPopulatedArray_ReturnsTrue()
    {
        int[] values = [1, 2, 3];

        Assert.IsTrue(values.IsNotEmpty());
        Assert.IsFalse(values.IsEmpty());
    }

    [Test]
    public void AsReadOnly_WrapsArrayInReadOnlyCollection()
    {
        string[] items = ["Army", "Peasants"];

        var readOnly = items.AsReadOnly();

        Assert.AreEqual(2, readOnly.Count);
        Assert.AreEqual("Army", readOnly[0]);
    }

    [Test]
    public void Abs_ReturnsAbsoluteIntegerValue()
    {
        Assert.AreEqual(42, (-42).Abs());
        Assert.AreEqual(42, 42.Abs());
        Assert.AreEqual(0, 0.Abs());
    }
}
