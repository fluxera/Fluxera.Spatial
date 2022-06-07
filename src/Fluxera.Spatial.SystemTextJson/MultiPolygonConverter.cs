namespace Fluxera.Spatial.SystemTextJson
{
	using System;
	using System.Collections.Generic;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class MultiPolygonConverter : JsonConverter<MultiPolygon>
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, MultiPolygon value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteStringValue("MultiPolygon");

			writer.WritePropertyName("coordinates");
			writer.WriteStartArray();

			foreach(Polygon polygon in value.Coordinates)
			{
				writer.WriteStartArray();

				foreach(LineString lineString in polygon.Coordinates)
				{
					writer.WriteStartArray();

					foreach(Position position in lineString.Coordinates)
					{
						writer.WritePosition(position);
					}

					writer.WriteEndArray();
				}

				writer.WriteEndArray();
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override MultiPolygon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.StartObject)
			{
				string type = string.Empty;
				IList<Polygon> polygons = new List<Polygon>();

				while(reader.Read() && reader.TokenType != JsonTokenType.EndObject)
				{
					string propertyName = reader.GetString();

					reader.Read();
					if(propertyName == "type")
					{
						type = reader.GetString();
					}
					else if(propertyName == "coordinates")
					{
						while(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
						{
							IList<LineString> lineStrings = new List<LineString>();

							while(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
							{
								IList<Position> positions = new List<Position>();

								while(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
								{
									Position position = reader.GetPosition();
									positions.Add(position);
								}

								lineStrings.Add(new LineString(positions));
							}

							polygons.Add(new Polygon(lineStrings));
						}
					}
					else
					{
						break;
					}
				}

				if(type == "MultiPolygon")
				{
					return new MultiPolygon(polygons);
				}
			}

			return MultiPolygon.Empty;
		}
	}
}
