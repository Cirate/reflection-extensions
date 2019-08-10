using System;
using System.Reflection;

namespace CirateSolutions.ReflectionExtensions
{
    public static class FieldExtensions
    {
	    public static TValue GetFieldValue<TValue>(this object target, string name)
	    {
		    target = target ?? throw new ArgumentNullException(nameof(target));

		    var fieldInfo = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);

		    if (fieldInfo is null)
		    {
			    throw new Exception();
            }
		    if (fieldInfo.FieldType != typeof(TValue))
		    {
			    throw new Exception();
		    }

		    return (TValue)fieldInfo.GetValue(target);
        }
    }
}
