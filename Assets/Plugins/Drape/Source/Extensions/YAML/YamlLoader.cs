/**
 * Yaml loader uses .net YamlDotNet package.
 * 
 * Prequisites
 * Source code: https://github.com/aaubry/YamlDotNet
 * Binaries: https://ci.appveyor.com/project/aaubry/yamldotnet/build/artifacts
 * 
 * Additional sources
 * Yaml 1.1 spec: http://yaml.org/spec/1.1/
 * Yaml JS Parser: http://nodeca.github.io/js-yaml/
 */

using UnityEngine;

using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using YamlDotNet.Serialization;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Drape.YAML
{
	public class YamlLoader
	{
		private YamlStream stream;
		private Deserializer deserializer;

		public YamlLoader()
		{
			stream = new YamlStream();

			deserializer = new DeserializerBuilder()
				.WithNamingConvention(new HyphenatedNamingConvention())
				.IgnoreUnmatchedProperties()
				.Build();
		}		

		public List<TType> FromString<TType>(string data)
		{
			List<TType> list = new List<TType>();
			StringReader input = new StringReader(data);
			Parser parser = new Parser(input);

			// Consume the stream start event "manually"
			parser.Expect<StreamStart>();

			while (parser.Accept<DocumentStart>()) {
				list.Add(deserializer.Deserialize<TType>(parser));
			}

			return list;
		}
	}

}
