namespace Fluxera.Spatial.SystemTextJson
{
	using System;
	using System.Collections.Generic;
	using System.Text.Json;
	using System.Text.Json.Nodes;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class GeometryCollectionConverter : JsonConverter<GeometryCollection>
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, GeometryCollection value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteStringValue("GeometryCollection");

			writer.WritePropertyName("geometries");

			writer.WriteStartArray();
			foreach(IGeometry geometry in value.Geometries)
			{
				if(geometry is GeometryCollection)
				{
					throw new ArgumentOutOfRangeException();
				}

				JsonSerializer.Serialize(writer, geometry, geometry.GetType(), options);
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override GeometryCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.StartObject)
			{
				string type = string.Empty;
				IList<IGeometry> geometries = new List<IGeometry>();

				while(reader.Read() && reader.TokenType != JsonTokenType.EndObject)
				{
					string propertyName = reader.GetString();

					reader.Read();
					if(propertyName == "type")
					{
						type = reader.GetString();
					}
					else if(propertyName == "geometries")
					{
						while(reader.Read() && reader.TokenType != JsonTokenType.EndArray)
						{
							JsonObject jObject = JsonSerializer.Deserialize<JsonObject>(ref reader, options);
							string geometryTypeName = jObject["type"].GetValue<string>();

							Type geometryType = geometryTypeName switch
							{
								"Point" => typeof(Point),
								"MultiPoint" => typeof(MultiPoint),
								"LineString" => typeof(LineString),
								"MultiLineString" => typeof(MultiLineString),
								"Polygon" => typeof(Polygon),
								"MultiPolygon" => typeof(MultiPolygon),
								_ => throw new ArgumentOutOfRangeException()
							};

							object geometry = jObject.Deserialize(geometryType, options);
							geometries.Add((IGeometry)geometry);
						}
					}
					else
					{
						break;
					}
				}

				if(type == "GeometryCollection")
				{
					return new GeometryCollection(geometries);
				}
			}

			return GeometryCollection.Empty;
		}
	}
}
