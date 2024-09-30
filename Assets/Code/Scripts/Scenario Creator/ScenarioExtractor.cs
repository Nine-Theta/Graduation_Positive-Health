using NaughtyAttributes;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ScenarioEditor
{
    public class ScenarioExtractor : MonoBehaviour
    {
        [ShowNonSerializedField]
        private string _savePath = "/JsonScenarios/";

        [SerializeField]
        private ScenarioDescription _description;

        private Dictionary<AbstractNode, NodeID> _discoveredNodes = new Dictionary<AbstractNode, NodeID>();

        private int _groupCounter = 0;
        private int _choiceCounter = 0;
        private int _responseCounter = 0;

        [SerializeField]
        private ScriptableExtractedDialogue extract;

        [Button]
        public void GetDialogue()
        {
            if (_description == null) return;

            extract.ClearData();

            extract.ScenarioName = _description.ScenarioName;
            extract.ScenarioDescription = _description.Description;
            extract.TargetAudience = _description.TargetAudience;

            SerializeNodesRecursively(_description.StartingNode);

            Debug.Log("All Nodes Processed!");
            ExportAsJSON();
        }

        private void SerializeNodesRecursively(ChoiceGroupNode pGroup)
        {
            extract.StarterGroup = new SerializedChoiceGroup(NodeType.GROUP, _groupCounter++, AddSerializedChoicesRecursive(pGroup.GetChildNodes()), new Vector2(0, 0));
        }

        private NodeID AddSerializedChoiceGroupRecursive(ChoiceGroupNode pGroup)
        {
            if (_discoveredNodes.ContainsKey(pGroup))
            {
                return _discoveredNodes[pGroup];
            }

            NodeID id = new NodeID(NodeType.GROUP, _groupCounter++);

            _discoveredNodes.Add(pGroup, id);

            extract.groups.Add(new SerializedChoiceGroup(id, AddSerializedChoicesRecursive(pGroup.GetChildNodes()), pGroup.GetNodePosition()));

            return id;
        }

        private NodeID[] AddSerializedChoicesRecursive(ChoiceNode[] pChoices)
        {
            NodeID[] serializedChoices = new NodeID[pChoices.Length];

            for (int i = 0; i < pChoices.Length; i++)
            {
                ChoiceNode choice = pChoices[i];

                if (_discoveredNodes.ContainsKey(choice))
                {
                    serializedChoices[i] = _discoveredNodes[choice];
                    continue;
                }

                NodeID id = new NodeID(NodeType.CHOICE, _choiceCounter++);

                _discoveredNodes.Add(choice, id);

                extract.choices.Add(new SerializedChoice(id, AddSerializedResponsesRecursive(choice.GetChildNodes()), choice.GetNodePosition(), choice.GetDialogue(), ChoiceConditions.Empty));

                serializedChoices[i] = id;
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

                if (_discoveredNodes.ContainsKey(response))
                {
                    serializedResponses[i] = _discoveredNodes[response];
                    continue;
                }

                NodeID id = new NodeID(NodeType.RESPONSE, _responseCounter++);

                _discoveredNodes.Add(response, id);

                if (group != null)
                {
                    extract.responses.Add(new SerializedResponse(id, AddSerializedChoiceGroupRecursive(group), response.GetNodePosition(), response.GetDialogue(), NPCEmotionState.NEUTRAL));
                }
                else
                {
                    extract.responses.Add(new SerializedResponse(id, NodeID.Empty, response.GetNodePosition(), response.GetDialogue(), NPCEmotionState.NEUTRAL, true));
                }

                serializedResponses[i] = id;
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
