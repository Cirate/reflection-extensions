using System;
using System.Reflection;
using CirateSolutions.ReflectionExtensions.Exceptions;

namespace CirateSolutions.ReflectionExtensions
{
	public static class FieldExtensions
	{
		private const BindingFlags InstanceFields = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		public static TValue GetFieldValue<TValue>(this object target, string fieldName)
		{
			if (target is null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			var fieldInfo = target.GetType()
			                      .GetField(fieldName, InstanceFields);

			if (fieldInfo is null)
			{
				throw new FieldNotFoundException(fieldName, target.GetType());
			}

			if (fieldInfo.FieldType != typeof(TValue))
			{
				throw new FieldTypeMismatchException(fieldName, target.GetType(), typeof(TValue));
			}

			return (TValue) fieldInfo.GetValue(target);
		}
	}
}