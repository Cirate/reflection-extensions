using System;

namespace CirateSolutions.ReflectionExtensions.Exceptions
{
	public class FieldTypeMismatchException : FieldNotFoundException
	{
		public FieldTypeMismatchException(string fieldName, Type targetType, Type expectedFieldType)
			: base(
				fieldName,
				targetType,
				$"Field '{fieldName}' of '{expectedFieldType.Name}' type does not exist on '{targetType.Name}'")
		{
			ExpectedFieldType = expectedFieldType;
		}

		public Type ExpectedFieldType { get; }
	}
}