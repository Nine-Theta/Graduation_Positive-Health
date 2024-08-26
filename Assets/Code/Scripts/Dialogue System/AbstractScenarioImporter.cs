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
        return ImportScenarioFromFile(Application.persistentDataPath + _folder + pFilename + _extension);
    }

    public abstract ScriptableExtractedDialogue ImportScenarioFromFile(string pFilepath);

    
}
