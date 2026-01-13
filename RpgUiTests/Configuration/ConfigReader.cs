using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RpgFramework.Config
{
    public class ConfigReader
    {
        public static TestSettings ReadConfig()
        {
            var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/appsettings.json");

            var jsonSerializeOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true //hoofdletters/kleine letters in JSON maken niet uit.
            };

            //De chrome moet worden geconverteert naar een enum type (wat de browserType is)
            jsonSerializeOptions.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<TestSettings>(configFile, jsonSerializeOptions);
        }
    }
}
