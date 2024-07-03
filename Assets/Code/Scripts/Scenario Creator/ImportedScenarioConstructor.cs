using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ImportedScenarioConstructor : MonoBehaviour
    {
        [SerializeField]
        private AbstractScenarioImporter _importer;
        [SerializeField]
        private string _scenarioFilename;

        [SerializeField]
        private GameObject _scenarioStartNode;

        [SerializeField]
        private GroupNodeCreator _groupCreator;
        [SerializeField]
        private ChoiceNodeCreator _choiceCreator;
        [SerializeField]
        private ResponseNodeCreator _responseCreator;


        private ScriptableExtractedDialogue _scenario;


        private Dictionary<NodeID, GameObject> _nodeDictionary = new Dictionary<NodeID, GameObject>();

        [Button]
        public void JustDoItAll()
        {
            GetExtractedDialogue();
            FillStartNode();
            CreateAllNodes();
            LinkAllNodes();
        }

        [Button]
        public void GetExtractedDialogue()
        {
            _scenario = _importer.ImportScenarioFilePersistentDataPath(_scenarioFilename);
        }

        [Button]
        public void FillStartNode()
        {
            ScenarioDescription desc = _scenarioStartNode.GetComponent<ScenarioDescription>();
            desc.ScenarioName = _scenario.ScenarioName;
            desc.TargetAudience = _scenario.TargetAudience;
            desc.Description = _scenario.ScenarioDescription;
        }

        [Button]
        public void CreateAllNodes()
        {
            for (int i = 0; i < _scenario.groups.Count; i++)
            {
                _nodeDictionary.Add(_scenario.groups[i].ID, _groupCreator.CreateGroupNode(Vector3.zero, _scenario.groups[i]));
            }

            for (int i = 0; i < _scenario.choices.Count; i++)
            {
                _nodeDictionary.Add(_scenario.choices[i].ID, _choiceCreator.CreateChoiceNode(Vector3.zero, _scenario.choices[i]));
            }

            for (int i = 0; i < _scenario.responses.Count; i++)
            {
                _nodeDictionary.Add(_scenario.responses[i].ID, _responseCreator.CreateResponseNode(Vector3.zero, _scenario.responses[i]));
            }
        }

        [Button]
        public void LinkAllNodes()
        {
            for (int i = 0; i < _scenario.StarterGroup.ChoiceIDs.Length; i++)
            {
                ChoiceGroupNode group = _scenarioStartNode.GetComponent<ChoiceGroupNode>();
                ChoiceNode choice = _nodeDictionary[_scenario.StarterGroup.ChoiceIDs[i]].GetComponent<ChoiceNode>();
                group.AddChoice(choice);
                group.GetLastStartPoint().MakeConnection(choice.GetEndPoint());
            }

            for (int i = 0; i < _scenario.groups.Count; i++)
            {
                for (int j = 0; j < _scenario.groups[i].ChoiceIDs.Length; j++)
                {
                    ChoiceGroupNode group = _nodeDictionary[_scenario.groups[i].ID].GetComponent<ChoiceGroupNode>();
                    ChoiceNode choice = _nodeDictionary[_scenario.groups[i].ChoiceIDs[j]].GetComponent<ChoiceNode>();
                    group.AddChoice(choice);
                    group.GetLastStartPoint().MakeConnection(choice.GetEndPoint());
                }
            }

            for (int i = 0; i < _scenario.choices.Count; i++)
            {
                for (int j = 0; j < _scenario.choices[i].ResponseIDs.Length; j++)
                {
                    ChoiceNode choice = _nodeDictionary[_scenario.choices[i].ID].GetComponent<ChoiceNode>();
                    NPCResponseNode response = _nodeDictionary[_scenario.choices[i].ResponseIDs[j]].GetComponent<NPCResponseNode>();

                    choice.LinkResponse(response);
                    choice.GetLastStartPoint().MakeConnection(response.GetEndPoint());
                }
            }

            for (int i = 0; i < _scenario.responses.Count; i++)
            {
                NPCResponseNode response = _nodeDictionary[_scenario.responses[i].ID].GetComponent<NPCResponseNode>();
                ChoiceGroupNode group = _nodeDictionary[_scenario.responses[i].GroupID].GetComponent<ChoiceGroupNode>();

                response.SetChoiceGroup(group);
                response.GetLastStartPoint().MakeConnection(group.GetEndPoint());
            }
        }
    }
}
