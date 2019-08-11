using System;

namespace CirateSolutions.ReflectionExtensions.Exceptions
{
	public class MethodReturnTypeMismatchException : MethodNotFoundException
	{
		public MethodReturnTypeMismatchException(string methodName, Type targetType, Type expectedMethodReturnType)
			: base(
				methodName,
				targetType,
				$"Method '{methodName}' with '{expectedMethodReturnType.Name}' return type does not exist on '{targetType.Name}'")
		{
			ExpectedMethodReturnType = expectedMethodReturnType;
		}

		public Type ExpectedMethodReturnType { get; }
	}
}