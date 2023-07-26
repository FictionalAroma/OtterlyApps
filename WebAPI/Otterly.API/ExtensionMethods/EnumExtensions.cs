using System;
using System.ComponentModel;
using System.Reflection;

namespace Otterly.API.ExtensionMethods;

public static class EnumExtensions
{
	public static string GetDescription(this Enum value)
	{            
		FieldInfo field = value.GetType().GetField(value.ToString());

		DescriptionAttribute attribute
			= Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
				  as DescriptionAttribute;

		return attribute == null ? value.ToString() : attribute.Description;
	}

	public static T GetValueFromDescription<T>(string description) where T : Enum
	{
		foreach(var field in typeof(T).GetFields())
		{
			if (Attribute.GetCustomAttribute(field,
											 typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
			{
				if (attribute.Description == description)
					return (T)field.GetValue(null);
			}
			else
			{
				if (field.Name == description)
					return (T)field.GetValue(null);
			}
		}

		throw new ArgumentException("Not found.", nameof(description));
		// Or return default(T);
	}
}