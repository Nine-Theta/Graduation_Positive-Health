using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDialogueManager : MonoBehaviour
{
    protected ScriptableExtractedDialogue _scenario;

    protected Dictionary<NodeID, SerializedChoiceGroup> _groups = new Dictionary<NodeID, SerializedChoiceGroup>();
    protected Dictionary<NodeID, SerializedChoice> _choices = new Dictionary<NodeID, SerializedChoice>();
    protected Dictionary<NodeID, SerializedResponse> _responses = new Dictionary<NodeID, SerializedResponse>();

    protected NodeID _currentNode;
    protected SerializedChoice[] _recentChoices;

    protected int _currentChoiceCount;


    public virtual string GetScenarioName()
    {
        return _scenario.ScenarioName;
    }

    public virtual string GetScenarioDescription()
    {
        return _scenario.ScenarioDescription;
    }

    public virtual string GetTargetAudience()
    {
        return _scenario.TargetAudience;
    }

    public virtual string[] GetStartChoices()
    {
        _currentNode = GetStartGroup().ID;
        return GetNextChoices();

    }

    public virtual string[] GetNextChoices()
    {
        _recentChoices = GetFollowingChoices(_currentNode);

        string[] dialogues = new string[_recentChoices.Length];

        for (int i = 0; i < _recentChoices.Length; i++)
        {
            dialogues[i] = _recentChoices[i].Dialogue;
        }

        _currentChoiceCount = _recentChoices.Length - 1;

        return dialogues;
    }

    public virtual string GetNextResponse(int pChoiceMade)
    {
        if (pChoiceMade >= _recentChoices.Length)
            return null;

        SerializedResponse[] possibleResponses = GetFollowingResponses(_recentChoices[pChoiceMade].ID);

        Debug.LogWarning("TODO: Get Proper NPC response instead of first one in array");

        //Should probably be done properly
        _currentNode = GetFollowingGroup(possibleResponses[0].ID).ID;

        return possibleResponses[0].Dialogue;
    }

    private SerializedChoiceGroup GetStartGroup()
    {
        return _scenario.StarterGroup;
    }

    private SerializedChoiceGroup GetFollowingGroup(NodeID pResponseNode)
    {
        if (!_responses.ContainsKey(pResponseNode))
        {
            Debug.LogError("Response not Found, this should not happen!");
            throw new KeyNotFoundException("Response ID not found in _responses Dictionary");
        }

        NodeID groupID =  _responses[pResponseNode].GroupID;

        if (!_groups.ContainsKey(groupID))
        {
            Debug.LogError("Group not Found, this should not happen!");
            throw new KeyNotFoundException("Group ID not found in _groups Dictionary");
        }

        return _groups[groupID];
    }

    private SerializedChoice[] GetFollowingChoices(NodeID pGroupNode)
    {
        if (!_groups.ContainsKey(pGroupNode))
        {
            Debug.LogError("Group not Found, this should not happen!");
            throw new KeyNotFoundException("Group ID not found in _groups Dictionary");
        }

        NodeID[] choiceIDs = _groups[pGroupNode].ChoiceIDs;

        SerializedChoice[] choices = new SerializedChoice[choiceIDs.Length];

        for (int i = 0; i < choiceIDs.Length; i++)
        {
            if (!_choices.ContainsKey(choiceIDs[i]))
            {
                Debug.LogError("Choice not Found, this should not happen!");
                throw new KeyNotFoundException("Choice ID not found in _choices Dictionary");
            }

            choices[i] = _choices[choiceIDs[i]];
        }

        return choices;
    }

    private SerializedResponse[] GetFollowingResponses(NodeID pChoiceNode)
    {
        if (!_choices.ContainsKey(pChoiceNode))
        {
            Debug.LogError("Choice not Found, this should not happen!");
            throw new KeyNotFoundException("Choice ID not found in _choices Dictionary");
        }

        NodeID[] responseIDs = _choices[pChoiceNode].ResponseIDs;

        SerializedResponse[] responses = new SerializedResponse[responseIDs.Length];

        for (int i = 0; i < responseIDs.Length; i++)
        {
            if (!_responses.ContainsKey(responseIDs[i]))
            {
                Debug.LogError("Response not Found, this should not happen!");
                throw new KeyNotFoundException("Response ID not found in _choices Dictionary");
            }

            responses[i] = _responses[responseIDs[i]];
        }

        return responses;
    }
}
