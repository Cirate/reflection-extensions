using System;
using System.Linq;
using System.Reflection;
using CirateSolutions.ReflectionExtensions.Exceptions;

namespace CirateSolutions.ReflectionExtensions
{
	public static class MethodExtensions
	{
		private const BindingFlags InstanceMethods = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		public static void Invoke(this object target, string methodName)
		{
			if (target is null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			var methodInfo = target.GetType().GetMethod(methodName, InstanceMethods);

			if (methodInfo is null)
			{
				throw new MethodNotFoundException(methodName, target.GetType());
			}

			if (methodInfo.ReturnType != typeof(void))
			{
				throw new MethodReturnTypeMismatchException(methodName, target.GetType(), typeof(void));
			}

			if (methodInfo.GetParameters().Any())
			{
				throw new MethodParametersMismatchException(methodName, target.GetType());
			}

			methodInfo.Invoke(target, null);
		}
	}
}