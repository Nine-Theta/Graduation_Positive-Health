using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractScenarioImporter : MonoBehaviour
{
    [SerializeField]
    protected ScriptableExtractedDialogue ScenarioOverwriteFile;

    private string _savePath = "/JsonScenarios/";

    public virtual ScriptableExtractedDialogue ImportScenarioFilePersistentDataPath(string pFilename)
    {
        return ImportScenarioFromFile(Application.persistentDataPath + _savePath + pFilename);
    }

    public abstract ScriptableExtractedDialogue ImportScenarioFromFile(string pFilepath);

    
}
