using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class DialogueExtractor : MonoBehaviour
    {
        [SerializeField, ReadOnly]
        private ScenarioDescription _description;

        private List<NodeID> _nodeIDs = new List<NodeID>();

        private Dictionary<ChoiceGroupNode, ChoiceNode[]> _groupChoices = new Dictionary<ChoiceGroupNode, ChoiceNode[]>();
        private Dictionary<ChoiceNode, NPCResponseNode[]> _choiceResponses = new Dictionary<ChoiceNode, NPCResponseNode[]>();
        private Dictionary<NPCResponseNode, ChoiceGroupNode> _responseGroups = new Dictionary<NPCResponseNode, ChoiceGroupNode>();

        public void GetDialogue()
        {
            if (_description == null) return;

            _nodeIDs.Clear();
            _groupChoices.Clear();
            _choiceResponses.Clear();
            _responseGroups.Clear();

            _nodeIDs.Add(_description.StartingNode.GetNodeID());

            ChoiceNode[] choices = _description.StartingNode.GetChoices().ToArray();

            _groupChoices.Add(_description.StartingNode, choices);

            foreach (ChoiceNode choice in choices)
            {
                AddResponsesRecursive(choice);
            }

            Debug.Log("All Nodes Processed!");
        }

        private void AddChoicesRecursive(ChoiceGroupNode pGroup)
        {
            ChoiceNode[] choices = pGroup.GetChoices().ToArray();

            if (_groupChoices.ContainsKey(pGroup))
                Debug.LogError("Duplicate Group Detected!");

            _groupChoices.Add(pGroup, choices);


            foreach (ChoiceNode choice in choices)
            {
                AddResponsesRecursive(choice);
            }
        }

        private void AddResponsesRecursive(ChoiceNode pChoice)
        {
            NPCResponseNode[] responses = pChoice.GetResponses().ToArray();

            if(_choiceResponses.ContainsKey(pChoice))
                Debug.LogError("Duplicate Choice Detected!");

            _choiceResponses.Add(pChoice, responses);

            foreach (NPCResponseNode response in responses)
            {
                AddChoiceGroupRecursive(response);
            }
        }

        private void AddChoiceGroupRecursive(NPCResponseNode pResponse)
        {
            ChoiceGroupNode group = pResponse.GetChoiceGroup();

            if (_responseGroups.ContainsKey(pResponse))
                Debug.LogError("Duplicate Response Detected!");

            if (group != null)
            {
                _responseGroups.Add(pResponse, group);

                AddChoicesRecursive(group);
            }
        }

    }
}
