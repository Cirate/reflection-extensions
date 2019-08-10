using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NUnit.Framework;

namespace CirateSolutions.ReflectionExtensions.UnitTests
{
    public class FieldExtensionsTests
    {
	    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
	    private class Target
	    {
		    public Target(string stringField)
		    {
			    _stringField = stringField;
		    }

		    private string _stringField;
	    }

	    [TestCase(null)]
	    [TestCase("")]
	    [TestCase(" ")]
	    [TestCase("value")]
	    public void GivenFieldName_WhenItIsValid_ThenReturnsFieldValue(string expectedValue)
	    {
		    // arrange
		    var target = new Target(expectedValue);

		    // act
			var actualValue = target.GetFieldValue<string>("_stringField");

			// assert
			actualValue.Should().Be(expectedValue);
	    }
    }
}