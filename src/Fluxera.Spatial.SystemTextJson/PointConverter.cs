namespace Fluxera.Spatial.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class PointConverter : JsonConverter<Point>
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteStringValue("Point");

			writer.WritePropertyName("coordinates");
			writer.WritePosition(value.Coordinates);

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.StartObject)
			{
				string type = string.Empty;
				Position position = Position.Empty;

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
						position = reader.GetPosition();
					}
					else
					{
						break;
					}
				}

				if(type == "Point")
				{
					return new Point(position);
				}
			}

			return Point.Empty;
		}
	}
}
