using System;
using UnityEngine;
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

public interface I_SerializedNode
{
    public NodeID GetID();
    public NodeID[] GetChildNodeIDs();
    public Vector2 GetNodePos();
}

[Serializable]
public struct SerializedChoiceGroup : I_SerializedNode
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

    public NodeID GetID() { return ID; }
    public NodeID[] GetChildNodeIDs() { return ChoiceIDs; }
    public Vector2 GetNodePos() { return NodePos; }
}

[Serializable]
public struct SerializedChoice : I_SerializedNode
{
    public NodeID ID;
    public NodeID[] ResponseIDs;
    public Vector2 NodePos;

    public string Dialogue;
    public ChoiceConditions Conditions;


    public SerializedChoice(NodeType pNodeType, int pNodeNumber, NodeID[] pResponses, Vector2 pPosition, string pDialogue, ChoiceConditions pConditions)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        Dialogue = pDialogue;
        ResponseIDs = pResponses;
        NodePos = pPosition;
        Conditions = pConditions;
    }
    public NodeID GetID() { return ID; }
    public NodeID[] GetChildNodeIDs() { return ResponseIDs; }
    public Vector2 GetNodePos() { return NodePos; }

    public ChoiceAvailability GetChoiceAvailability()
    {
        if (Conditions.UnlockIndex < 0)
            return ChoiceAvailability.ALWAYS;
        else
            return ChoiceAvailability.CONDITIONAL;
    }
}

[Serializable]
public struct SerializedResponse : I_SerializedNode
{
    public NodeID ID;
    public NodeID GroupID;
    public Vector2 NodePos;

    public string Dialogue;
    public NPCEmotionState EmotionState;
    public bool IsEnd;

    public SerializedResponse(NodeType pNodeType, int pNodeNumber, NodeID pGroup, Vector2 pPosition, string pDialogue, NPCEmotionState pEmotion, bool pIsEnd = false)
    {
        ID = new NodeID(pNodeType, pNodeNumber);
        GroupID = pGroup;
        NodePos = pPosition;

        Dialogue = pDialogue;
        EmotionState = pEmotion;
        IsEnd = pIsEnd;
    }
    public NodeID GetID() { return ID; }
    public NodeID[] GetChildNodeIDs() { return new NodeID[] { GroupID }; }
    public Vector2 GetNodePos() { return NodePos; }
}
