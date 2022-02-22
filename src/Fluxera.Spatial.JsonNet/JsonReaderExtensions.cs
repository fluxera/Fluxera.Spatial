namespace Fluxera.Spatial.JsonNet
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	internal static class JsonReaderExtensions
	{
		public static Position ReadPosition(this JsonReader reader)
		{
			while(reader.Read())
			{
				if(reader.TokenType == JsonToken.StartArray)
				{
					JArray coordinatesArray = JArray.Load(reader);

					double[]? values = coordinatesArray.ToObject<double[]>();
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
