using System.Collections.Generic;

namespace Cumulocity.MQTT.Utils
{
    public interface IIniParser
    {
        void AddSetting(string sectionName, string settingName);

        void AddSetting(string sectionName, string settingName, string settingValue);

        void DeleteSetting(string sectionName, string settingName);

        List<KeyValuePair<string, string>> EnumSection(string sectionName);

        string GetSetting(string sectionName, string settingName);

        void SaveSettings();

        void SaveSettings(string newFilePath);
    }
}