namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class MultiPolygonConverter : JsonConverter<MultiPolygon>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, MultiPolygon value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("MultiPolygon");

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
		public override MultiPolygon ReadJson(JsonReader reader, Type objectType, MultiPolygon existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.TryGetValue("type", out JToken typeToken))
				{
					string type = typeToken.Value<string>();
					if(type == "MultiPolygon")
					{
						if(item.TryGetValue("coordinates", out JToken coordinatesToken))
						{
							if(coordinatesToken.Type == JTokenType.Array)
							{
								JArray coordinatesArray = (JArray)coordinatesToken;

								IList<Polygon> polygons = new List<Polygon>();
								foreach(JToken polygonToken in coordinatesArray)
								{
									JArray lineStringsArray = (JArray)polygonToken;

									IList<LineString> lineStrings = new List<LineString>();
									foreach(JToken lineStringToken in lineStringsArray)
									{
										JArray innerArray = (JArray)lineStringToken;

										IList<Position> positions = new List<Position>();
										foreach(JToken token in innerArray)
										{
											Position position = token.CreateReader().ReadPosition();
											positions.Add(position);
										}

										LineString lineString = new LineString(positions);
										lineStrings.Add(lineString);
									}

									Polygon polygon = new Polygon(lineStrings);
									polygons.Add(polygon);
								}

								return new MultiPolygon(polygons);
							}
						}
					}
				}
			}

			return MultiPolygon.Empty;
		}
	}
}
