using System;
using System.Linq;
using System.Reflection;
using CirateSolutions.ReflectionExtensions.Exceptions;
using JetBrains.Annotations;

namespace CirateSolutions.ReflectionExtensions.Validators
{
	internal static class MethodInfoValidationExtensions
	{
		[AssertionMethod]
		internal static MethodInfo Validate(
			[AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
			this MethodInfo methodInfo,
			Type targetType,
			string methodName)
		{
			if (methodInfo is null)
			{
				throw new MethodNotFoundException(methodName, targetType);
			}

			if (methodInfo.ReturnType != typeof(void))
			{
				throw new MethodReturnTypeMismatchException(methodName, targetType, typeof(void));
			}

			if (methodInfo.GetParameters().Any())
			{
				throw new MethodParametersMismatchException(methodName, targetType);
			}

			return methodInfo;
		}
	}
}