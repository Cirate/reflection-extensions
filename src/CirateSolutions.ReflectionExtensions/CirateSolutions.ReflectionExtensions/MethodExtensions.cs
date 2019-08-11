using System;
using System.Reflection;
using CirateSolutions.ReflectionExtensions.Validators;

namespace CirateSolutions.ReflectionExtensions
{
	public static class MethodExtensions
	{
		private const BindingFlags InstanceMethods = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
		private const BindingFlags StaticMethods = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

		public static void Invoke(this object target, string methodName)
			=> target.NotNull(nameof(target))
			         .GetType()
			         .GetMethod(methodName, InstanceMethods)
			         .Validate(target.GetType(), methodName)
			         .Invoke(target, null);

		public static void InvokeStaticMethod(this object target, string methodName)
			=> target.NotNull(nameof(target))
			         .GetType()
			         .InvokeStaticMethod(methodName);

		public static void InvokeStaticMethod(this Type type, string methodName)
			=> type.NotNull(nameof(type))
			       .GetMethod(methodName, StaticMethods)
			       .Validate(type, methodName)
			       .Invoke(null, null);
	}
}