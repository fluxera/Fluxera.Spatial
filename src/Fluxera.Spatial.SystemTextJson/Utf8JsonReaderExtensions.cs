namespace Fluxera.Spatial.SystemTextJson
{
	using System.Collections.Generic;
	using System.Text.Json;

	internal static class Utf8JsonReaderExtensions
	{
		public static Position GetPosition(this ref Utf8JsonReader reader)
		{
			if(reader.TokenType == JsonTokenType.StartArray)
			{
				IList<double> values = new List<double>();

				while(reader.Read() && (reader.TokenType != JsonTokenType.EndArray))
				{
					if(reader.TokenType == JsonTokenType.Number)
					{
						double value = reader.GetDouble();
						values.Add(value);
					}
				}

				if(values.Count >= 2)
				{
					double longitude = values[0];
					double latitude = values[1];
					double? altitude = null;

					if(values.Count >= 3)
					{
						altitude = values[2];
					}

					return new Position(longitude, latitude, altitude);
				}
			}

			return Position.Empty;
		}
	}
}
