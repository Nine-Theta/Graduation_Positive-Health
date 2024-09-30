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

    public void ClearData()
    {
        ScenarioName = "";
        ScenarioDescription = "";
        TargetAudience = "";

        ChoiceVariables = new int[0];

        StarterGroup = new SerializedChoiceGroup();

        groups.Clear();
        choices.Clear();
        responses.Clear();
    }
    public bool SearchGroupByID(NodeID pID, out SerializedChoiceGroup outGroup)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            if (groups[i].ID == pID)
            {
                outGroup = groups[i];
                return true;
            }
        }
        outGroup = new SerializedChoiceGroup();
        return false;
    }


    public bool SearchChoiceByID(NodeID pID, out SerializedChoice outChoice)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            if (choices[i].ID == pID)
            {
                outChoice = choices[i];
                return true;
            }
        }
        outChoice = new SerializedChoice();
        return false;
    }
    public bool SearchResponseByID(NodeID pID, out SerializedResponse outResponse)
    {
        for (int i = 0; i < responses.Count; i++)
        {
            if (responses[i].ID == pID)
            {
                outResponse = responses[i];
                return true;
            }
        }
        outResponse = new SerializedResponse();
        return false;
    }
}
