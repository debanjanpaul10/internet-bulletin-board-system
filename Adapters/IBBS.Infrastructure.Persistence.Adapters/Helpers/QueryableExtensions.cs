using System.Data.Common;
using System.Reflection;

namespace IBBS.Infrastructure.Persistence.Adapters.Helpers;

/// <summary>
/// The Queryable extensions class.
/// </summary>
internal static class QueryableExtensions
{
	/// <summary>
	/// Maps reader to object or dictionary for dynamic types like Object.
	/// </summary>
	/// <param name="reader">The data reader.</param>
	/// <param name="type">The target type.</param>
	/// <returns>The mapped object or dictionary.</returns>
	internal static object MapReaderToObjectOrDictionary(this DbDataReader reader, Type type)
	{
		// For Object type, return a dictionary with column names and values
		if (type == typeof(object))
		{
			var dictionary = new Dictionary<string, object?>();
			for (int i = 0; i < reader.FieldCount; i++)
			{
				var columnName = reader.GetName(i);
				var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
				dictionary[columnName] = value;
			}
			return dictionary;
		}

		// For specific types, use the existing extension method
		return reader.MapReaderToObject(type);
	}

	/// <summary>
	/// Maps a data reader row to an object of the specified type.
	/// </summary>
	/// <param name="reader">The data reader.</param>
	/// <param name="type">The target type.</param>
	/// <returns>The mapped object.</returns>
	private static object MapReaderToObject(this DbDataReader reader, Type type)
	{
		var instance = Activator.CreateInstance(type)!;
		var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

		for (int i = 0; i < reader.FieldCount; i++)
		{
			var columnName = reader.GetName(i);
			var property = properties.FirstOrDefault(p =>
				string.Equals(p.Name, columnName, StringComparison.OrdinalIgnoreCase));

			if (property != null && property.CanWrite && !reader.IsDBNull(i))
			{
				var value = reader.GetValue(i);
				if (value != null && value != DBNull.Value)
				{
					// Handle type conversion if needed
					if (property.PropertyType != value.GetType())
					{
						if (property.PropertyType.IsGenericType &&
							property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
						{
							var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
							value = Convert.ChangeType(value, underlyingType!);
						}
						else
						{
							value = Convert.ChangeType(value, property.PropertyType);
						}
					}
					property.SetValue(instance, value);
				}
			}
		}

		return instance;
	}
}
