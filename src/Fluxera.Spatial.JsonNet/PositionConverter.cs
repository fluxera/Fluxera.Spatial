namespace Fluxera.Spatial.JsonNet
{
	using System;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	internal sealed class PositionConverter : JsonConverter<Position>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, Position value, JsonSerializer serializer)
		{
			writer.WriteStartArray();

			writer.WriteValue(value.Longitude);
			writer.WriteValue(value.Latitude);
			if(value.Altitude.HasValue)
			{
				writer.WriteValue(value.Altitude);
			}

			writer.WriteEndArray();
		}

		/// <inheritdoc />
		public override Position ReadJson(JsonReader reader, Type objectType, Position existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			while(reader.Read())
			{
				if(reader.TokenType == JsonToken.StartArray)
				{
					JArray array = JArray.Load(reader);

					double[]? values = array.ToObject<double[]>();
					if((values != null) && (values.Length >= 2))
					{
						double longitude = values[0];
						double latitude = values[1];
						double? altitude = null;

						if(values.Length >= 3)
						{
							altitude = values[2];
						}

						return new Position(longitude, latitude, altitude);
					}
				}
			}

			return Position.Empty;
		}
	}
}
