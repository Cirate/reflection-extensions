using System;
using System.Diagnostics.CodeAnalysis;
using CirateSolutions.ReflectionExtensions.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CirateSolutions.ReflectionExtensions.UnitTests.MethodExtensions
{
	public class Invoke
	{
		[Test]
		public void GivenMethodName_WhenItsPublic_InvokesMethod()
		{
			// arrange
			const int expectedValue = -1;
			var target = new Target();

			// act
			target.Invoke(Target.PublicVoidMethodName);

			// assert
			target.Counter.Should().Be(expectedValue);
		}

		[Test]
		public void GivenMethodName_WhenItsPrivate_InvokesMethod()
		{
			// arrange
			const int expectedValue = 1;
			var target = new Target();

			// act
			target.Invoke(Target.PrivateVoidMethodName);

			// assert
			target.Counter.Should().Be(expectedValue);
		}

		[Test]
		[SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
		public void GivenMethodName_WhenTargetIsNull_ThrowsArgumentNullException()
		{
			// arrange
			Target target = null;

			// act
			Action act = () => target.Invoke(Target.PublicVoidMethodName);

			// assert
			act.Should().Throw<ArgumentNullException>();
			act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("target");
		}

		[Test]
		public void GivenMethodName_WhenItDoesNotExist_ThrowsMethodNotFoundException()
		{
			// arrange
			var target = new Target();

			// act
			Action act = () => target.Invoke(Target.NonExistentMethodName);

			// assert
			act.Should().Throw<MethodNotFoundException>();
			act.Should().Throw<MethodNotFoundException>().Which.MethodName.Should().Be(Target.NonExistentMethodName);
			act.Should().Throw<MethodNotFoundException>().Which.TargetType.Should().Be(typeof(Target));
		}

		[Test]
		public void GivenMethodName_WhenItsStaticMethod_ThrowsMethodNotFoundException()
		{
			// arrange
			var target = new Target();

			// act
			Action act = () => target.Invoke(Target.StaticMethodName);

			// assert
			act.Should().Throw<MethodNotFoundException>();
			act.Should().Throw<MethodNotFoundException>().Which.MethodName.Should().Be(Target.StaticMethodName);
			act.Should().Throw<MethodNotFoundException>().Which.TargetType.Should().Be(typeof(Target));
		}

		[Test]
		public void GivenMethodName_WhenItHasReturnType_ThrowsMethodReturnTypeMismatchException()
		{
			// arrange
			var target = new Target();

			// act
			Action act = () => target.Invoke(Target.WithReturnTypeMethodName);

			// assert
			act.Should().Throw<MethodReturnTypeMismatchException>();
			act.Should().Throw<MethodReturnTypeMismatchException>().Which.MethodName.Should().Be(Target.WithReturnTypeMethodName);
			act.Should().Throw<MethodReturnTypeMismatchException>().Which.TargetType.Should().Be(typeof(Target));
		}

		[Test]
		public void GivenMethodName_WhenItHasParameter_ThrowsMethodParametersMismatchException()
		{
			// arrange
			var target = new Target();

			// act
			Action act = () => target.Invoke(Target.WithParameterMethodName);

			// assert
			act.Should().Throw<MethodParametersMismatchException>();
			act.Should().Throw<MethodParametersMismatchException>().Which.MethodName.Should().Be(Target.WithParameterMethodName);
			act.Should().Throw<MethodParametersMismatchException>().Which.ExpectedParameterTypes.Should().BeEmpty();
			act.Should().Throw<MethodParametersMismatchException>().Which.TargetType.Should().Be(typeof(Target));
		}

		[SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
		[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
		private class Target
		{
			public const string PublicVoidMethodName = nameof(Decrement);
			public const string PrivateVoidMethodName = nameof(Increment);
			public const string WithReturnTypeMethodName = nameof(WithReturnType);
			public const string WithParameterMethodName = nameof(WithParameter);
			public const string StaticMethodName = nameof(StaticMethod);
			public const string NonExistentMethodName = "nonExistenMethod";

			public int Counter { get; private set; }
			public void Decrement() => Counter--;
			public string WithReturnType() => string.Empty;

			public void WithParameter(string parameter)
			{
			}

			public static void StaticMethod()
			{
			}

			private void Increment() => Counter++;
		}
	}
}