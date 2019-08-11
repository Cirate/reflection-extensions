using System.Reflection;
using CirateSolutions.ReflectionExtensions.Validators;

namespace CirateSolutions.ReflectionExtensions
{
	public static class FieldExtensions
	{
		private const BindingFlags InstanceFields = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

		public static TValue GetFieldValue<TValue>(this object target, string fieldName)
			=> (TValue) target.NotNull(nameof(target))
			                  .GetType()
			                  .GetField(fieldName, InstanceFields)
			                  .Validate(target.GetType(), fieldName, typeof(TValue))
			                  .GetValue(target);
	}
}