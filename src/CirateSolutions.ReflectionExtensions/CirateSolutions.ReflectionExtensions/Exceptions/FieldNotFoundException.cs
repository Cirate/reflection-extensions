using System;

namespace CirateSolutions.ReflectionExtensions.Exceptions
{
	public class FieldNotFoundException : Exception
	{
		public FieldNotFoundException(string fieldName, Type targetType)
			: base(message: $"Field '{fieldName}' does not exist on '{targetType.Name}'")
		{
			FieldName = fieldName;
			TargetType = targetType;
		}

		protected FieldNotFoundException(string fieldName, Type targetType, string message)
			: base(message)
		{
			FieldName = fieldName;
			TargetType = targetType;
		}

		public string FieldName { get; }
		public Type TargetType { get; }
	}
}