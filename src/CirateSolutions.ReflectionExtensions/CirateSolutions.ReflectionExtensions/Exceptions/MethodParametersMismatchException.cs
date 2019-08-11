using System;
using System.Linq;

namespace CirateSolutions.ReflectionExtensions.Exceptions
{
	public class MethodParametersMismatchException : MethodNotFoundException
	{
		public MethodParametersMismatchException(string methodName, Type targetType, params Type[] expectedParameterTypes)
			: base(
				methodName,
				targetType,
				message: CreateExceptionMessage(methodName, targetType, expectedParameterTypes))
		{
			ExpectedParameterTypes = expectedParameterTypes ?? new Type[0];
		}

		public Type[] ExpectedParameterTypes { get; set; }

		private static string CreateExceptionMessage(string methodName, Type targetType, Type[] expectedParameterTypes)
			=> expectedParameterTypes?.Any() ?? false
				   ? $"Method '{methodName}' with ({GetParameterTypes(expectedParameterTypes)}) parameters does not exist on '{targetType.Name}'"
				   : $"Method '{methodName}' without parameters does not exist on '{targetType.Name}'";

		private static string GetParameterTypes(Type[] expectedParameterTypes)
			=> string.Join(", ", expectedParameterTypes.Select(x => x.Name));
	}
}