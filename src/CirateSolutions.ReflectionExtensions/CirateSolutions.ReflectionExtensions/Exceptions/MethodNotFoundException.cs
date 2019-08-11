using System;

namespace CirateSolutions.ReflectionExtensions.Exceptions
{
	public class MethodNotFoundException : Exception
	{
		public MethodNotFoundException(string methodName, Type targetType)
			: base(message: $"Method '{methodName}' does not exist on '{targetType.Name}'")
		{
			MethodName = methodName;
			TargetType = targetType;
		}

		protected MethodNotFoundException(string methodName, Type targetType, string message)
			: base(message)
		{
			MethodName = methodName;
			TargetType = targetType;
		}

		public string MethodName { get; }
		public Type TargetType { get; }
	}
}