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
        [ShowNonSerializedField]
        private string _savePath = "/JsonScenarios/";

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

            extract.groups.Clear();
            extract.choices.Clear();
            extract.responses.Clear();

            extract.ScenarioName = _description.ScenarioName;
            extract.ScenarioDescription = _description.Description;
            extract.TargetAudience = _description.TargetAudience;

            SerializeNodesRecursively(_description.StartingNode);

            Debug.Log("All Nodes Processed!");
            ExportAsJSON();
        }

        private void SerializeNodesRecursively(ChoiceGroupNode pGroup)
        {
            extract.StarterGroup = new SerializedChoiceGroup(NodeType.GROUP, extract.groups.Count, AddSerializedChoicesRecursive(pGroup.GetChildNodes()), new Vector2(0,0));
        }

        private NodeID AddSerializedChoiceGroupRecursive(ChoiceGroupNode pGroup)
        {
            extract.groups.Add(new SerializedChoiceGroup(NodeType.GROUP, extract.groups.Count, AddSerializedChoicesRecursive(pGroup.GetChildNodes()), pGroup.GetNodePosition()));
            return extract.groups.Last().ID;
        }

        private NodeID[] AddSerializedChoicesRecursive(ChoiceNode[] pChoices)
        {
            NodeID[] serializedChoices = new NodeID[pChoices.Length];

            for (int i = 0; i < pChoices.Length; i++)
            {
                ChoiceNode choice = pChoices[i];

                extract.choices.Add(new SerializedChoice(NodeType.CHOICE, extract.choices.Count, choice.GetDialogue(), AddSerializedResponsesRecursive(choice.GetChildNodes()), choice.GetNodePosition(), ChoiceConditions.Empty));

                serializedChoices[i] = extract.choices.Last().ID;
            }

            return serializedChoices;
        }

        private NodeID[] AddSerializedResponsesRecursive(NPCResponseNode[] pResponses)
        {
            NodeID[] serializedResponses = new NodeID[pResponses.Length];

            for (int i = 0; i < pResponses.Length; i++)
            {

                NPCResponseNode response = pResponses[i];
                ChoiceGroupNode group = response.GetChildNodes().Length == 0 ? null : response.GetChildNodes()[0];

                if (group != null)
                {
                    extract.responses.Add(new SerializedResponse(NodeType.RESPONSE, extract.responses.Count, response.GetDialogue(), NPCEmotionState.NEUTRAL, AddSerializedChoiceGroupRecursive(group), response.GetNodePosition()));
                }
                else
                {
                    extract.responses.Add(new SerializedResponse(NodeType.RESPONSE, extract.responses.Count, response.GetDialogue(), NPCEmotionState.NEUTRAL, NodeID.Empty, response.GetNodePosition(), true));
                }

                serializedResponses[i] = extract.responses.Last().ID;
            }

            return serializedResponses;            
        }


        private void ExportAsJSON()
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + _savePath);
            FileStream fileStream = new FileStream(Application.persistentDataPath + _savePath + extract.ScenarioName + ".json", FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(JsonUtility.ToJson(extract, true));
            }
        }
    }
}
