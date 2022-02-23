namespace Fluxera.Spatial.SystemTextJson
{
	using System;
	using System.Collections.Generic;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class LineStringConverter : JsonConverter<LineString>
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, LineString value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteStringValue("LineString");

			writer.WritePropertyName("coordinates");
			writer.WriteStartArray();

			foreach(Position position in value.Coordinates)
			{
				writer.WritePosition(position);
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override LineString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.StartObject)
			{
				string type = string.Empty;
				IList<Position> positions = new List<Position>();

				while(reader.Read() && (reader.TokenType != JsonTokenType.EndObject))
				{
					string propertyName = reader.GetString()!;

					reader.Read();
					if(propertyName == "type")
					{
						type = reader.GetString()!;
					}
					else if(propertyName == "coordinates")
					{
						while(reader.Read() && (reader.TokenType != JsonTokenType.EndArray))
						{
							Position position = reader.GetPosition();
							positions.Add(position);
						}
					}
					else
					{
						break;
					}
				}

				if(type == "LineString")
				{
					return new LineString(positions);
				}
			}

			return LineString.Empty;
		}
	}
}
