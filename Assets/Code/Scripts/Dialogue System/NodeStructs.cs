using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum ChoiceAvailability { ALWAYS, CONDITIONAL }
public enum NPCEmotionState { NEUTRAL, SHOCKED, UPSET, SAD, IRRITATED, DISMISSIVE, COMFORTED, HAPPY }
public enum NodeType { GROUP, CHOICE, RESPONSE }

[Serializable]
public struct NodeID
{
    public static NodeID Empty;

    public NodeType Type;
    public int NodeNumber;

    public NodeID(NodeType pNodeType, int pNodeNumber)
    {
        Type = pNodeType;
        NodeNumber = pNodeNumber;
    }

    public string GetIDString()
    {
        return Type.ToString() + NodeNumber.ToString();
    }
}

public struct ChoiceConditions
{
    public static ChoiceConditions Empty = new ChoiceConditions(-1, 0, -1, 0);

    public int UnlockIndex;
    public int UnlockThreshold;

    public int ModifiedIndex;
    public int VarModifier;

    public ChoiceConditions(int pUnlockVarIndex, int pUnlockThreshold, int ModifiedVarIndex, int pVarModifier)
    {
        UnlockIndex = pUnlockVarIndex;
        UnlockThreshold = pUnlockThreshold;
        ModifiedIndex = ModifiedVarIndex;
        VarModifier = pVarModifier;
    }
}

[Serializable]
public struct SerializedChoiceGroup
{
    public NodeID ID;

    public NodeID[] ChoiceIDs;

    public Vector2 NodePos;

    public SerializedChoiceGroup(NodeType pNodeType, int pNodeNumber, NodeID[] pChoices, Vector2 pPosition)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        ChoiceIDs = pChoices;
        NodePos = pPosition;
    }
}

[Serializable]
public struct SerializedChoice
{
    public NodeID ID;

    public string Dialogue;
    public NodeID[] ResponseIDs;

    public Vector2 NodePos;

    public ChoiceConditions Conditions;


    public SerializedChoice(NodeType pNodeType, int pNodeNumber, string pDialogue, NodeID[] pResponses, Vector2 pPosition, ChoiceConditions pConditions)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        ResponseIDs = pResponses;
        NodePos = pPosition;
        Conditions = pConditions;
    }

    public ChoiceAvailability GetChoiceAvailability()
    {
        if (Conditions.UnlockIndex < 0)
            return ChoiceAvailability.ALWAYS;
        else
            return ChoiceAvailability.CONDITIONAL;
    }
}

[Serializable]
public struct SerializedResponse
{
    public NodeID ID;

    public string Dialogue;

    public NPCEmotionState EmotionState;

    public NodeID GroupID;

    public Vector2 NodePos;

    public bool IsEnd;

    public SerializedResponse(NodeType pNodeType, int pNodeNumber, string pDialogue, NPCEmotionState pEmotion, NodeID pGroup, Vector2 pPosition, bool pIsEnd = false)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        EmotionState = pEmotion;
        GroupID = pGroup;
        NodePos = pPosition;
        IsEnd = pIsEnd;
    }
}
