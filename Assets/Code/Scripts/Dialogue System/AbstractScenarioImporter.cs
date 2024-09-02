using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractScenarioImporter : MonoBehaviour
{
    [SerializeField]
    protected ScriptableExtractedDialogue ScenarioOverwriteFile;

    protected string _folder = ""; // example: "/JsonScenarios/"
    protected string _extension = ""; // example: ".json"

    public virtual ScriptableExtractedDialogue ImportScenarioFilePersistentDataPath(string pFilename)
    {
        //Just assuming if it does contain a dot the extension is valid
        if (!pFilename.Contains('.'))
            pFilename += _extension;

        return ImportScenarioFromFile(Application.persistentDataPath + _folder + pFilename);
    }

    public abstract ScriptableExtractedDialogue ImportScenarioFromFile(string pFilepath);

    
}
