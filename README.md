# Fluxera.Spatial
A libary that provides spatial types based on GeoJSON defined in [RFC 7946](https://datatracker.ietf.org/doc/html/rfc7946).

## Usage

Just use the provied structs to define your classes that need gep-spatial informations in it.

```C#
public sealed class Restaurant
{
	public string Name { get; set; }

	public Point Location { get; set; }
}
```

## Supported GeoJSON objects

This libary privide an inclomple implementation of [RFC 7946](https://datatracker.ietf.org/doc/html/rfc7946) at the moment.
The basic geometries are supported.

- ```Point```
- ```MultiPoint```
- ```LineString```
- ```MultiLineString```
- ```Polygon```
- ```MultiPolygon```
- ```GeometryCollection```

Please refer to the documentation of [RFC 7946](https://datatracker.ietf.org/doc/html/rfc7946) for details.

## Serialization Support

At the moment serialization support is available for:

- [Newtonsoft.Json (JSON.NET)](https://github.com/JamesNK/Newtonsoft.Json)
- [LiteDB](https://github.com/mbdavid/LiteDB)
- [MongoDB](https://github.com/mongodb/mongo-csharp-driver)
- [System.Text.Json](https://github.com/dotnet/corefx/tree/master/src/System.Text.Json)

The serializers make shure that the objects are serialized in the GeoJSON format and in case of
databases also stored im the correct format to support indexing and geo queries.

### Newtonsoft.Json (JSON.NET)

To support the GeoJSON objects in JSON.NET use the ```UseSpatial``` extension method on the ```JsonSerializerSettings```.

```c#
JsonSerializerSettings settings = new JsonSerializerSettings();

// Use the serialization support for GeoJSON objects.
settings.UseSpatial();

JsonConvert.DefaultSettings = () => settings;
```

### LiteDB

To support the GeoJSON objects in LiteDB use the ```UseSpatial``` extension method on the global ```BsonMapper```.

```C#
BsonMapper.Global.UseSpatial();
```

### MongoDB

To support the GeoJSON objects in MongoDB use the ```UseSpatial``` extension method on a ```ConventionPack```.

```C#
ConventionPack pack = new ConventionPack();
pack.UseSpatial();
ConventionRegistry.Register("ConventionPack", pack, t => true);
```

### System.Text.Json

To support the GeoJSON objects in System.Text.Json use the ```UseSpatial``` extension method on the ```JsonSerializerOptions```.

```C#
private JsonSerializerOptions options;

this.options = new JsonSerializerOptions
{
	WriteIndented = false
};
this.options.UseSpatial();

JsonSerializer.Serialize(Point.Empty, this.options);
```

## References

[RFC 7946](https://datatracker.ietf.org/doc/html/rfc7946)

## Future

We plan to add support for EFCore and OData and to add the missing pieces of the RFC Specification.
