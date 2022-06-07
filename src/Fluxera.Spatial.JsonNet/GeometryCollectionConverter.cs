namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class GeometryCollectionConverter : JsonConverter<GeometryCollection>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, GeometryCollection value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("GeometryCollection");

			writer.WritePropertyName("geometries");

			writer.WriteStartArray();
			foreach(IGeometry geometry in value.Geometries)
			{
				if(geometry is GeometryCollection)
				{
					throw new ArgumentOutOfRangeException();
				}

				serializer.Serialize(writer, geometry, geometry.GetType());
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override GeometryCollection ReadJson(JsonReader reader, Type objectType, GeometryCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.ContainsKey("type"))
				{
					string type = item["type"].Value<string>();
					if(type == "GeometryCollection")
					{
						if(item.ContainsKey("geometries"))
						{
							JToken geometriesToken = item["geometries"];
							if(geometriesToken.Type == JTokenType.Array)
							{
								JArray geometriesArray = (JArray)geometriesToken;

								IList<IGeometry> geometries = new List<IGeometry>();
								foreach(JToken token in geometriesArray)
								{
									string geometryTypeName = token["type"].Value<string>();

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

									object geometry = serializer.Deserialize(token.CreateReader(), geometryType);
									geometries.Add((IGeometry)geometry);
								}

								return new GeometryCollection(geometries);
							}
						}
					}
				}
			}

			return GeometryCollection.Empty;
		}
	}
}
