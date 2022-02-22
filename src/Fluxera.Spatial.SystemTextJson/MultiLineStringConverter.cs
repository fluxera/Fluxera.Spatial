﻿namespace Fluxera.Spatial.SystemTextJson
{
	using System;
	using System.Collections.Generic;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class MultiLineStringConverter : JsonConverter<MultiLineString>
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, MultiLineString value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteStringValue("MultiLineString");

			writer.WritePropertyName("coordinates");
			writer.WriteStartArray();

			foreach(LineString lineString in value.Coordinates)
			{
				writer.WriteStartArray();

				foreach(Position position in lineString.Coordinates)
				{
					writer.WritePosition(position);
				}

				writer.WriteEndArray();
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override MultiLineString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.StartObject)
			{
				string type = string.Empty;
				IList<LineString> lineStrings = new List<LineString>();

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
							IList<Position> positions = new List<Position>();

							while(reader.Read() && (reader.TokenType != JsonTokenType.EndArray))
							{
								Position position = reader.GetPosition();
								positions.Add(position);
							}

							lineStrings.Add(new LineString(positions));
						}
					}
					else
					{
						break;
					}
				}

				if(type == "MultiLineString")
				{
					return new MultiLineString(lineStrings);
				}
			}

			return MultiLineString.Empty;
		}
	}
}
