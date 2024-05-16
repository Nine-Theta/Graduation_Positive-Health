using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONDialogueManager : AbstractDialogueManager
{   
    public void ImportDialogue(string pPath)
    {
        JsonUtility.FromJsonOverwrite(pPath, _scenario);
    }
}
