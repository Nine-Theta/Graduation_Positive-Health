using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct NodeID
{
    public static NodeID Empty;

    public char NodeType;
    public uint NodeNumber;

    public NodeID(char pNodeType, uint pNodeNumber)
    {
        NodeType = pNodeType;
        NodeNumber = pNodeNumber;
    }

    public string GetIDString()
    {
        return NodeType + NodeNumber.ToString();
    }
}

    [Serializable]
public struct SerializedChoiceGroup
{
    public NodeID ID;

    public NodeID[] ChoiceIDs;

    public SerializedChoiceGroup(char pNodeType, uint pNodeNumber, NodeID[] pChoices)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        ChoiceIDs = pChoices;
    }

    public NodeID[] GetChoiceIDs()
    {
        return ChoiceIDs;
    }
}

[Serializable]
public struct SerializedChoice
{
    public NodeID ID;

    public string Dialogue;
    public NodeID[] ResponseIDs;

    public SerializedChoice(char pNodeType, uint pNodeNumber, string pDialogue, NodeID[] pResponses)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        ResponseIDs = pResponses;
    }
    public NodeID[] GetResponses()
    {
        return ResponseIDs;
    }
}

[Serializable]
public struct SerializedResponse
{
    public NodeID ID;

    public string Dialogue;
    public NodeID GroupID;

    public bool IsEnd;

    public SerializedResponse(char pNodeType, uint pNodeNumber, string pDialogue, NodeID pGroup, bool pIsEnd = false)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        GroupID = pGroup;
        IsEnd = pIsEnd;
    }

    public NodeID GetGroup()
    {
        return GroupID;
    }
}
