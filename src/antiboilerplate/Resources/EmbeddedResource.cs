using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Antiboilerplate.Disposables;
using Antiboilerplate.Functional;

namespace Antiboilerplate.Resources
{
    public static class EmbeddedResource
    {
        /// <summary>
        /// Reads an embedded resource as a string.
        /// </summary>
        /// <param name="resourceName">The fully qualified  name for the embedded resource</param>
        /// <param name="assembly">The assembly where the resource is</param>
        /// <returns></returns>
        public static Task<string> Read(string resourceName, Assembly assembly) =>
            Disposable.Using(
                () => assembly.GetManifestResourceStream(resourceName),
                stream => new StreamReader(stream),
                reader => reader.ReadToEndAsync());

        /// <summary>
        /// Reads an embedded resource as a string.
        /// </summary>
        /// <typeparam name="T">A type which full name denotes the name of the embedded resource, without the '.json' at the end.</typeparam>
        /// <returns></returns>
        public static Task<string> ReadJson<T>() =>
            typeof(T).Map(t => Read($"{t.FullName}.json", t.Assembly));

        /// <summary>
        /// Reads an embedded resource as an XDocument.
        /// </summary>
        /// <typeparam name="T">A type which full name denotes the name of the embedded resource, without the '.json' at the end.</typeparam>
        /// <returns></returns>
        public static Task<XDocument> ReadXml<T>()
            => Read<T, XDocument>(XDocument.Load, "xml");

        /// <summary>
        /// Reads an embedded resource as a Json file and deserializes it.
        /// </summary>
        /// <typeparam name="T">A type which full name denotes the name of the embedded resource, without the '.json' at the end.</typeparam>
        /// <typeparam name="TOut">The type to deserialize to.</typeparam>
        /// <returns></returns>
        public static Task<TOut> ReadAndDeserializeJson<T, TOut>()
            where TOut : class
            => Read<T,TOut>(
            s => new DataContractJsonSerializer(typeof(TOut)).ReadObject(s) as TOut, "json");
        
        private static Task<TOut> Read<T,TOut>(Func<Stream, TOut> func, string fileType)
            where TOut:class
        {
            var o = typeof(T).Map(t => Disposable.Using(
                () => t.Assembly.GetManifestResourceStream($"{t.FullName}.{fileType}"),
                func));

            return Task.FromResult(o);
        }
    }
}
