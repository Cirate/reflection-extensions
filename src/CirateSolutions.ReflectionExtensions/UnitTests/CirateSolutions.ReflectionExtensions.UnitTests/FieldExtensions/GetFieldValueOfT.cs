using System;
using System.Diagnostics.CodeAnalysis;
using CirateSolutions.ReflectionExtensions.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CirateSolutions.ReflectionExtensions.UnitTests.FieldExtensions
{
	public class GetFieldValueOfT
	{
		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase("value")]
		public void GivenFieldName_WhenItsPrivate_ReturnsFieldValue(string expectedValue)
		{
			// arrange
			var target = new Target(expectedValue);

			// act
			var actualValue = target.GetFieldValue<string>(Target.PrivateStringFieldName);

			// assert
			actualValue.Should().Be(expectedValue);
		}

		[TestCase(-1)]
		[TestCase(0)]
		[TestCase(1)]
		public void GivenFieldName_WhenItsPublic_ReturnsFieldValue(int expectedValue)
		{
			// arrange
			var target = new Target(expectedValue);

			// act
			var actualValue = target.GetFieldValue<int>(Target.PublicIntFieldName);

			// assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void GivenFieldName_WhenItDoesNotExist_ThrowsFieldNotFoundException()
		{
			// arrange
			var target = new Target();

			// act
			Action act = () => target.GetFieldValue<string>(Target.NonExistentFieldName);

			// assert
			act.Should().Throw<FieldNotFoundException>();
			act.Should().Throw<FieldNotFoundException>().Which.FieldName.Should().Be(Target.NonExistentFieldName);
			act.Should().Throw<FieldNotFoundException>().Which.TargetType.Should().Be(typeof(Target));
		}

		[Test]
		public void GivenFieldName_WhenItExistsButHasDifferentType_FieldTypeMismatchException()
		{
			// arrange
			var target = new Target();

			// act
			Action act = () => target.GetFieldValue<string>(Target.PublicIntFieldName);

			// assert
			act.Should().Throw<FieldTypeMismatchException>();
			act.Should().Throw<FieldTypeMismatchException>().Which.FieldName.Should().Be(Target.PublicIntFieldName);
			act.Should().Throw<FieldTypeMismatchException>().Which.TargetType.Should().Be(typeof(Target));
			act.Should().Throw<FieldTypeMismatchException>().Which.ExpectedFieldType.Should().Be(typeof(string));
		}

		[Test]
		[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
		public void GivenFieldName_WhenTargetIsNull_ThrowsArgumentNullException()
		{
			// arrange
			Target target = null;

			// act
			Action act = () => target.GetFieldValue<string>(Target.PrivateStringFieldName);

			// assert
			act.Should().Throw<ArgumentNullException>();
			act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("target");
		}

		[SuppressMessage("ReSharper", "NotAccessedField.Local")]
		[SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
		private class Target
		{
			public const string PrivateStringFieldName = nameof(_privateStringField);
			public const string PublicIntFieldName = nameof(PublicIntField);
			public const string NonExistentFieldName = "nonExistentField";

			private string _privateStringField;
			public int PublicIntField;

			public Target()
			{
			}

			public Target(int publicIntField)
			{
				PublicIntField = publicIntField;
			}

			public Target(string privateStringField)
			{
				_privateStringField = privateStringField;
			}
		}
	}
}