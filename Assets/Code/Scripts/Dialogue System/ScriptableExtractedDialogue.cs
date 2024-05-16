using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable, CreateAssetMenu(fileName = "textdiag", menuName = "script/testextract")]
public class ScriptableExtractedDialogue : ScriptableObject
{
    public string ScenarioName = "";
    public string ScenarioDescription = "";

    public string TargetAudience = "";

    public int[] ChoiceVariables = new int[0];

    public SerializedChoiceGroup StarterGroup = new SerializedChoiceGroup();

    public List<SerializedChoiceGroup> groups = new List<SerializedChoiceGroup>();
    public List<SerializedChoice> choices = new List<SerializedChoice>();
    public List<SerializedResponse> responses = new List<SerializedResponse>();

}
