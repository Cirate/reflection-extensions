using System;
using System.Reflection;
using CirateSolutions.ReflectionExtensions.Exceptions;
using JetBrains.Annotations;

namespace CirateSolutions.ReflectionExtensions.Validators
{
	internal static class FieldInfoValidationExtensions
	{
		[AssertionMethod]
		internal static FieldInfo Validate(
			[AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this FieldInfo fieldInfo,
			Type targetType,
			string fieldName,
			Type expectedFieldType)
		{
			if (fieldInfo is null)
			{
				throw new FieldNotFoundException(fieldName, targetType);
			}

			if (fieldInfo.FieldType != expectedFieldType)
			{
				throw new FieldTypeMismatchException(fieldName, targetType, expectedFieldType);
			}

			return fieldInfo;
		}
    }
}