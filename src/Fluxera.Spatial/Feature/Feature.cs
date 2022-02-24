// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A <see cref="Feature" /> object represents a spatially bounded thing.
	/// </summary>
	/// <remarks>
	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.2
	/// </remarks>
	[PublicAPI]
	public sealed class Feature
	{
		/// <summary>
		///     Creates a new instance of the <see cref="Feature" /> type.
		/// </summary>
		/// <param name="geometry"></param>
		/// <param name="identifier"></param>
		public Feature(IGeometry? geometry, string? identifier = null)
		{
			this.Geometry = geometry;
			this.Properties = new Dictionary<string, object>();
		}

		/// <summary>
		///     Gets the geometry object of the feature.
		/// </summary>
		public IGeometry? Geometry { get; }

		/// <summary>
		///     Gets the properties of the feature.
		/// </summary>
		public IDictionary<string, object> Properties { get; }

		/// <summary>
		///     Gets the commonly used identifier of the feature.
		/// </summary>
		public string? /*int*/ Identifier { get; set; }
	}
}
