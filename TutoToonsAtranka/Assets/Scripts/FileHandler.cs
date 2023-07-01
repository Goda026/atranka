using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
using System;

public class LevelData
{
    public string[] level_data { get; set; }
}

public class Levels
{
    public LevelData[] levels { get; set; }
}


public class FileHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private string data = @"
	{
		""levels"": [
			{ ""level_data"": [ ""50"", ""50"", ""950"", ""50"", ""950"", ""950"", ""50"", ""950"" ] },
			{ ""level_data"": [ ""500"", ""80"", ""652"", ""315"", ""930"", ""400"", ""762"", ""613"", ""751"", ""889"", ""500"", ""801"", ""247"", ""882"", ""241"", ""618"", ""63"", ""414"", ""343"", ""312"" ] },
			{ ""level_data"": [ ""625"", ""151"", ""913"", ""76"", ""847"", ""382"", ""933"", ""550"", ""924"", ""675"", ""843"", ""768"", ""594"", ""835"", ""318"", ""822"", ""135"", ""748"", ""60"", ""636"", ""60"", ""486"", ""130"", ""381"", ""66"", ""96"", ""361"", ""165"", ""493"", ""123"" ] },
			{ ""level_data"": [ ""205"", ""616"", ""135"", ""633"", ""162"", ""598"", ""85"", ""589"", ""121"", ""561"", ""19"", ""543"", ""162"", ""417"", ""181"", ""252"", ""253"", ""150"", ""234"", ""51"", ""328"", ""91"", ""483"", ""103"", ""667"", ""93"", ""760"", ""51"", ""745"", ""150"", ""817"", ""264"", ""835"", ""414"", ""975"", ""540"", ""877"", ""564"", ""913"", ""590"", ""831"", ""600"", ""862"", ""630"", ""792"", ""618"", ""702"", ""897"", ""729"", ""930"", ""667"", ""931"", ""678"", ""900"", ""627"", ""816"", ""372"", ""817"", ""315"", ""900"", ""330"", ""928"", ""270"", ""933"", ""294"", ""898"" ] }
		]	
	}";

    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    //The method is used to check if the necessery data file exists in the specified directory and load it.
    //If file does not exist, it will create a new file with the same data.
    public Levels Load()
    {
        string filePath = Path.Combine(dataDirPath, dataFileName);
        Levels levelCollection = null;
        if (File.Exists(filePath))
        {
            levelCollection = LoadFromFile(filePath);
        }
        else
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(data);
                    }
                }

                levelCollection = LoadFromFile(filePath);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data from a file: " + filePath + "\n" + e);
            }
        }

        return levelCollection;
    }

    //Method used to read files data.
    private Levels LoadFromFile(string filePath)
    {
        Levels levelCollection = null;

        try
        {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            levelCollection = JsonConvert.DeserializeObject<Levels>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occurred when trying to load data from a file: " + filePath + "\n" + e);
        }

        return levelCollection;
    }
}
