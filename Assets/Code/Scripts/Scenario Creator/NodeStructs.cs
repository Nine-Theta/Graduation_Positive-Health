using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SerializedChoiceGroup
{
    public char NodeType;
    public uint NodeNumber;
    public SerializedChoice[] Choices;

    public SerializedChoiceGroup(char pNodeType, uint pNodeNumber, SerializedChoice[] pChoices)
    {
        NodeType = pNodeType;
        NodeNumber = pNodeNumber;
        Choices = pChoices;
    }

    public string GetIDString()
    {
        return NodeType + NodeNumber.ToString();
    }

    public SerializedChoice[] GetChoices()
    {
        return Choices;
    }
}

[Serializable]
public struct SerializedChoice
{
    public char NodeType;
    public uint NodeNumber;
    public string Dialogue;
    public SerializedResponse[] Responses;

    public SerializedChoice(char pNodeType, uint pNodeNumber, string pDialogue, SerializedResponse[] pResponses)
    {
        NodeType = pNodeType;
        NodeNumber = pNodeNumber;
        Dialogue = pDialogue;
        Responses = pResponses;
    }

    public string GetIDString()
    {
        return NodeType + NodeNumber.ToString();
    }

    public SerializedResponse[] GetResponses()
    {
        return Responses;
    }
}

[Serializable]
public struct SerializedResponse
{
    public char NodeType;
    public uint NodeNumber;
    public string Dialogue;
    public SerializedChoiceGroup Group;

    public SerializedResponse(char pNodeType, uint pNodeNumber, string pDialogue, SerializedChoiceGroup pGroup)
    {
        NodeType = pNodeType;
        NodeNumber = pNodeNumber;
        Dialogue = pDialogue;
        Group = pGroup;
    }

    public string GetIDString()
    {
        return NodeType + NodeNumber.ToString();
    }

    public SerializedChoiceGroup GetGroup()
    {
        return Group;
    }
}
