using NaughtyAttributes;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ScenarioEditor
{
    public class ScenarioExtractor : MonoBehaviour
    {
        [SerializeField]
        private ScenarioDescription _description;

        [SerializeField] private List<NodeID> _nodeIDs = new List<NodeID>();

        private Dictionary<ChoiceGroupNode, ChoiceNode[]> _groupChoices = new Dictionary<ChoiceGroupNode, ChoiceNode[]>();
        private Dictionary<ChoiceNode, NPCResponseNode[]> _choiceResponses = new Dictionary<ChoiceNode, NPCResponseNode[]>();
        private Dictionary<NPCResponseNode, ChoiceGroupNode> _responseGroups = new Dictionary<NPCResponseNode, ChoiceGroupNode>();

        [SerializeField]
        private ScriptableExtractedDialogue extract;

        [Button]
        public void GetDialogue()
        {
            if (_description == null) return;

            /*
            _nodeIDs.Clear();
            _groupChoices.Clear();
            _choiceResponses.Clear();
            _responseGroups.Clear();

            _nodeIDs.Add(_description.StartingNode.GetNodeID());

            //ChoiceNode[] choices = _description.StartingNode.GetChoices().ToArray();

            _groupChoices.Add(_description.StartingNode, choices);*/

            extract.groups.Clear();
            extract.choices.Clear();
            extract.responses.Clear();

            extract.ScenarioName = _description.ScenarioName;
            extract.ScenarioDescription = _description.Description;
            extract.TargetAudience = _description.TargetAudience;

            SerializeNodesRecursively(_description.StartingNode);
            
            /*foreach (ChoiceNode choice in choices)
            {
                extract.choices.Add(choice);
                AddResponsesRecursive(choice);
            }*/

            Debug.Log("All Nodes Processed!");
            ExportAsJSON();
        }

        private void AddChoicesRecursive(ChoiceGroupNode pGroup)
        {
            ChoiceNode[] choices = pGroup.GetChoices().ToArray();

            if (_groupChoices.ContainsKey(pGroup))
                Debug.LogError("Duplicate Group Detected!");

            _groupChoices.Add(pGroup, choices);

            foreach (ChoiceNode choice in choices)
            {
                //extract.choices.Add(choice);
                AddResponsesRecursive(choice);
            }
        }

        private void AddResponsesRecursive(ChoiceNode pChoice)
        {
            NPCResponseNode[] responses = pChoice.GetResponses().ToArray();

            if (_choiceResponses.ContainsKey(pChoice))
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

        private void SerializeNodesRecursively(ChoiceGroupNode pGroup)
        {
            extract.StarterGroup = new SerializedChoiceGroup(NodeType.GROUP, extract.groups.Count, AddSerializedChoicesRecursive(pGroup.GetChoices().ToArray()));
        }

        private NodeID AddSerializedChoiceGroupRecursive(ChoiceGroupNode pGroup)
        {
            extract.groups.Add(new SerializedChoiceGroup(NodeType.GROUP, extract.groups.Count, AddSerializedChoicesRecursive(pGroup.GetChoices().ToArray())));
            return extract.groups.Last().ID;
        }

        private NodeID[] AddSerializedChoicesRecursive(ChoiceNode[] pChoices)
        {
            NodeID[] serializedChoices = new NodeID[pChoices.Length];

            for (int i = 0; i < pChoices.Length; i++)
            {
                extract.choices.Add(new SerializedChoice(NodeType.CHOICE, extract.choices.Count, pChoices[i].GetDialogue(), AddSerializedResponsesRecursive(pChoices[i].GetResponses().ToArray()),ChoiceConditions.Empty));

                serializedChoices[i] = extract.choices.Last().ID;
            }

            return serializedChoices;
        }

        private NodeID[] AddSerializedResponsesRecursive(NPCResponseNode[] pResponses)
        {
            NodeID[] serializedResponses = new NodeID[pResponses.Length];

            for (int i = 0; i < pResponses.Length; i++)
            {
                ChoiceGroupNode group = pResponses[i].GetChoiceGroup();

                if (group != null)
                {
                    extract.responses.Add(new SerializedResponse(NodeType.RESPONSE, extract.responses.Count, pResponses[i].GetDialogue(), NPCEmotionState.NEUTRAL, AddSerializedChoiceGroupRecursive(group)));
                    
                }
                else
                {
                    extract.responses.Add(new SerializedResponse(NodeType.RESPONSE, extract.responses.Count, pResponses[i].GetDialogue(), NPCEmotionState.NEUTRAL, NodeID.Empty, true));
                }

                serializedResponses[i] = extract.responses.Last().ID;
            }

            return serializedResponses;            
        }


        private void ExportAsJSON()
        {
            FileStream fileStream = new FileStream(Application.dataPath + "/JsonExportTest/Test.json", FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(JsonUtility.ToJson(extract, true));
            }

        }
    }
}
