using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
namespace ScenarioEditor
{
    [Serializable, CreateAssetMenu(fileName = "textdiag", menuName = "script/testextract")]
    public class ScriptableExtractedDialogue : ScriptableObject
    {
        public List<SerializedChoiceGroup> groups = new List<SerializedChoiceGroup>();
        public List<SerializedChoice> choices = new List<SerializedChoice>();
        public List<SerializedResponse> responses = new List<SerializedResponse>();



    }
}
