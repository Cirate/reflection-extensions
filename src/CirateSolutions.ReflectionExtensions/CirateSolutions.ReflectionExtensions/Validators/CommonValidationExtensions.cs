using System;
using JetBrains.Annotations;

namespace CirateSolutions.ReflectionExtensions.Validators
{
	internal static class CommonValidationExtensions
	{
		[AssertionMethod]
		internal static T NotNull<T>(
			[AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
			this T target,
			string paramName)
			where T : class
		{
			if (target is null)
			{
				throw new ArgumentNullException(paramName);
			}

			return target;
		}
	}
}