using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationManager : PersistantSingleton<LocalizationManager>
{
    public string LangID;
    public string ActiveApplicationLang { get; private set; } = "English";

    private Dictionary<string, List<LangData>> loadedLanguages = new Dictionary<string, List<LangData>>();

    private Dictionary<string, string> cachedData = new Dictionary<string, string>();

    public UnityEvent<string> OnLangChanged = new UnityEvent<string>();

    protected override void Awake()
    {
        base.Awake();
        var ExternalLangDirectory = Environment.CurrentDirectory + "/Langs";
        if (!Directory.Exists(ExternalLangDirectory))
        {
            Directory.CreateDirectory(ExternalLangDirectory);
        }

        if (Directory.GetFiles(ExternalLangDirectory).Length > 0)
        {
            foreach (var langFile in Directory.GetFiles(ExternalLangDirectory))
            {
                LoadLangFile(langFile);
            }
        }

        var systemLang = Application.systemLanguage.ToString();
        if (!loadedLanguages.ContainsKey(systemLang))
        {
            systemLang = "English";
        }
        ChangeLang(systemLang);
    }

    /// <summary>
    /// Load's a json language file into the memory
    /// </summary>
    /// <param name="filePath">The file to load</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public void LoadLangFile(string filePath)
    {
        JObject LoadedObject = JObject.Parse(File.ReadAllText(filePath));

        string sl = LoadedObject.GetValue("lang").Value<string>();
        List<LangData> enteryList = new List<LangData>();

        if (!loadedLanguages.ContainsKey(sl))
        {
            foreach (var data in LoadedObject.Properties())
            {
                if (data.Name == "entry")
                {
                    foreach (var entry in data.Children<JObject>().Properties())
                    {
                        enteryList.Add(new LangData()
                        {
                            Id = entry.Name,
                            langEntery = new LanguageEntry(entry.Value.ToString())
                        });
                    }
                }

                if (data.Name == "MultiDataEntery")
                {
                    foreach (var upgradeChildEntry in data.Children<JObject>().Properties())
                    {
                        var id = upgradeChildEntry.Name;
                        var name = "";
                        var description = "";


                        foreach (var uceChildrendData in JObject.Parse(upgradeChildEntry.Value.ToString()).Properties())
                        {
                            if (uceChildrendData.Name == "name")
                            {
                                name = uceChildrendData.Value.ToString();
                            }

                            if (uceChildrendData.Name == "description")
                            {
                                description = uceChildrendData.Value.ToString();
                            }
                        }

                        enteryList.Add(new LangData()
                        {
                            Id = id,
                            MultiDataLang = new MultiDataLanguageEntry(name, description)
                        });
                    }
                }
            }

            loadedLanguages.Add(sl, enteryList);
        }
    }

    /// <summary>
    /// Get's a language entry in the active languages jason file
    /// </summary>
    /// <param name="id">The id of the requested entry</param>
    /// <returns>The found entry</returns>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public string GetEntry(string id)
    {
        try
        {
            if (cachedData.ContainsKey(id))
            {
                return cachedData[id];
            }

            foreach (var entry in ActiveLangEntryList())
            {
                if (entry.Id.Equals(id))
                {
                    cachedData.Add(id, entry.langEntery.Value);
                    return entry.langEntery.Value;
                }
            }
        }
        catch (Exception)
        {

        }

        return id.ToUpper();
    }

    /// <summary>
    /// Get's the MultiData entry in the active languages json file
    /// </summary>
    /// <param name="id">The id of the entry</param>
    /// <returns>Th found entry</returns>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public MultiDataLanguageEntry GetMultiDataEntry(string id)
    {
        try
        {
            foreach (var entry in ActiveLangEntryList())
            {
                if (entry.Id.Equals(id))
                {
                    return entry.MultiDataLang;
                }
            }
        }
        catch (Exception)
        {

        }

        return new MultiDataLanguageEntry(id.ToUpper());
    }

    /// <summary>
    /// Get's the load lang data out of the memory for the systems active language
    /// </summary>
    /// <returns>A list of the lang data if it's found</returns>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public List<LangData> ActiveLangEntryList()
    {
        foreach (var loadedLangs in loadedLanguages)
        {
            if (loadedLangs.Key == ActiveApplicationLang)
            {
                return loadedLangs.Value;
            }
        }
        return null;
    }

    /// <summary>
    /// Change's the currently active language
    /// </summary>
    /// <param name="langID">The id of the new language</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public void ChangeLang(string langID)
    {
        if (!ActiveApplicationLang.Equals(langID))
        {
            if (loadedLanguages.ContainsKey(langID))
            {
                InvokeLangChangeEvents(langID);
            }
            else
            {
                InvokeLangChangeEvents("English");
            }
        }
    }

    /// <summary>
    /// Invokes All of the events and function needed for a lang change
    /// </summary>
    /// <remarks>Helper function for <see cref="ChangeLang(string)"/></remarks>
    /// <param name="langID">The lang to change to</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    private void InvokeLangChangeEvents(string langID)
    {
        cachedData.Clear();
        ActiveApplicationLang = langID;
        OnLangChanged.Invoke(langID);
    }

    [Serializable]
    public struct LanguageEntry
    {
        public string Value;

        public LanguageEntry(string value)
        {
            Value = value;
        }
    }

    [Serializable]
    public struct MultiDataLanguageEntry
    {
        public string name;
        public string description;

        public MultiDataLanguageEntry(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public MultiDataLanguageEntry(string data)
        {
            name = data;
            description = data;
        }
    }

    [Serializable]
    public struct LangData
    {
        public string Id;
        public LanguageEntry langEntery;
        public MultiDataLanguageEntry MultiDataLang;
    }
}


