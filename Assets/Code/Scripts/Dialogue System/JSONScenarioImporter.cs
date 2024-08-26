using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONScenarioImporter : AbstractScenarioImporter
{
    [ShowNonSerializedField]
    private string _saveFolder = "/JsonScenarios/";

    [ShowNonSerializedField]
    private string _fileExtension = ".json";

    private void Awake()
    {
        _folder = _saveFolder;
        _extension =_fileExtension;
    }

    public override ScriptableExtractedDialogue ImportScenarioFromFile(string pFilepath)
    {
        Debug.Log("Import Filepath: " + pFilepath);

        //ScriptableExtractedDialogue dialogue = new ScriptableExtractedDialogue();

        string jsonFile = File.ReadAllText(pFilepath);
        ScenarioOverwriteFile.ClearData();
        JsonUtility.FromJsonOverwrite(jsonFile, ScenarioOverwriteFile);
        return ScenarioOverwriteFile;
    }
}
