using NaughtyAttributes;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    [Serializable]
    public class DialogueExtractor : MonoBehaviour
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

            _nodeIDs.Clear();
            _groupChoices.Clear();
            _choiceResponses.Clear();
            _responseGroups.Clear();

            _nodeIDs.Add(_description.StartingNode.GetNodeID());

            ChoiceNode[] choices = _description.StartingNode.GetChoices().ToArray();

            _groupChoices.Add(_description.StartingNode, choices);

            extract.groups.Add(new SerializedChoiceGroup('A',1,_description.StartingNode.GetChoices()));

            foreach (ChoiceNode choice in choices)
            {
                extract.choices.Add(choice);
                AddResponsesRecursive(choice);
            }

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
                extract.choices.Add(choice);
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
