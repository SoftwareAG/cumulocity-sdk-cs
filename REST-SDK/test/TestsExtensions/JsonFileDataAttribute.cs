using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace XUnitTestsExtensions
{
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly IList<(string, Type)> fileMap = new List<(string, Type)>();

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
        /// <param name="type">Type of object to deserialize into</param>
        public JsonFileDataAttribute(string filePath, Type type)
        {
            fileMap.Add((filePath, type));
        }

        /// <summary>
        /// Load data from a JSON file as the data source for a theory. Types must mach paths.
        /// </summary>
        /// <param name="paths">IList of strings with file paths</param>
        /// <param name="types">IList of Types to deserialize into</param>
        public JsonFileDataAttribute(string[] paths, Type[] types)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                fileMap.Add((paths[i], types[i]));
            }
        }

        /// <inheritDoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }
            var resultSet = new List<object[]>();
            var result = new object[fileMap.Count];
            for (int i = 0; i < fileMap.Count; i++)
            {
                result[i] = ReadAndMapFile(fileMap[i].Item1, fileMap[i].Item2);
            }
            resultSet.Add(result);

            return resultSet;
        }

        private object ReadAndMapFile(string filePath, Type type)
        {
            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(filePath)
                ? filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(path)) { throw new ArgumentException($"Could not find file at path: {path}"); }

            // Load the file
            var fileData = File.ReadAllText(path);

            return typeof(JsonConvert).GetMethod("DeserializeObject", 1, new Type[] { typeof(string) }).MakeGenericMethod(type).Invoke(null, new[] { fileData });
        }
    }
}