using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChoiceAvailability { ALWAYS, CONDITIONAL }
public enum NPCEmotionState { NEUTRAL, SHOCKED, UPSET, SAD, IRRITATED, DISMISSIVE, COMFORTED, HAPPY }
public enum NodeType { GROUP, CHOICE, RESPONSE }

[Serializable]
public struct NodeID
{
    public static NodeID Empty;

    public NodeType Type;
    public uint NodeNumber;

    public NodeID(NodeType pNodeType, uint pNodeNumber)
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

    public SerializedChoiceGroup(NodeType pNodeType, uint pNodeNumber, NodeID[] pChoices)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        ChoiceIDs = pChoices;
    }
}

[Serializable]
public struct SerializedChoice
{
    public NodeID ID;

    public string Dialogue;
    public NodeID[] ResponseIDs;

    public ChoiceConditions Conditions;


    public SerializedChoice(NodeType pNodeType, uint pNodeNumber, string pDialogue, NodeID[] pResponses, ChoiceConditions pConditions)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        ResponseIDs = pResponses;
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

    public bool IsEnd;

    public SerializedResponse(NodeType pNodeType, uint pNodeNumber, string pDialogue, NPCEmotionState pEmotion, NodeID pGroup, bool pIsEnd = false)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        EmotionState = pEmotion;
        GroupID = pGroup;
        IsEnd = pIsEnd;
    }
}
