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

        [ShowNonSerializedField]
        private ScriptableExtractedDialogue _scenario;


        private Dictionary<NodeID, GameObject> _nodeDictionary = new Dictionary<NodeID, GameObject>();


        public void ImportScenarioFromFile(string pFilename)
        {
            _scenarioFilename = pFilename;
            GetExtractedDialogue();
            FillStartNode();
            CreateAllNodes();
            LinkAllNodes();
        }

        public void SwitchImporter(AbstractScenarioImporter pImporter)
        {
            _importer = pImporter;
        }

        [Button]
        public void LoadSpecifiedScenarioFile()
        {
            GetExtractedDialogue();
            FillStartNode();
            CreateAllNodes();
            LinkAllNodes();
        }

        [Button]
        public void GetExtractedDialogue()
        {
            _nodeDictionary.Clear();
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
                //Debug.Log("NodeID: " + _scenario.groups[i].GetID().GetIDString());
                _nodeDictionary.Add(_scenario.groups[i].GetID(), _groupCreator.CreateNewNodeAtPosition(_scenario.groups[i]));
            }

            for (int i = 0; i < _scenario.choices.Count; i++)
            {
                _nodeDictionary.Add(_scenario.choices[i].GetID(), _choiceCreator.CreateNewNodeAtPosition(_scenario.choices[i]));
            }

            for (int i = 0; i < _scenario.responses.Count; i++)
            {
                _nodeDictionary.Add(_scenario.responses[i].GetID(), _responseCreator.CreateNewNodeAtPosition(_scenario.responses[i]));
            }
        }

        [Button]
        public void LinkAllNodes()
        {
            for (int i = 0; i < _scenario.StarterGroup.GetChildNodeIDs().Length; i++)
            {
                ChoiceGroupNode group = _scenarioStartNode.GetComponent<ChoiceGroupNode>();
                ChoiceNode choice = _nodeDictionary[_scenario.StarterGroup.GetChildNodeIDs()[i]].GetComponent<ChoiceNode>();
                group.LinkChildNode(choice);
            }

            for (int i = 0; i < _scenario.groups.Count; i++)
            {
                for (int j = 0; j < _scenario.groups[i].GetChildNodeIDs().Length; j++)
                {
                    ChoiceGroupNode group = _nodeDictionary[_scenario.groups[i].GetID()].GetComponent<ChoiceGroupNode>();
                    ChoiceNode choice = _nodeDictionary[_scenario.groups[i].GetChildNodeIDs()[j]].GetComponent<ChoiceNode>();
                    group.LinkChildNode(choice);
                }
            }

            for (int i = 0; i < _scenario.choices.Count; i++)
            {
                for (int j = 0; j < _scenario.choices[i].GetChildNodeIDs().Length; j++)
                {
                    ChoiceNode choice = _nodeDictionary[_scenario.choices[i].GetID()].GetComponent<ChoiceNode>();
                    NPCResponseNode response = _nodeDictionary[_scenario.choices[i].GetChildNodeIDs()[j]].GetComponent<NPCResponseNode>();

                    choice.LinkChildNode(response);
                }
            }

            //Debug.Log("responses Count: "+_scenario.responses.Count);

            for (int i = 0; i < _scenario.responses.Count; i++)
            {
                if (_scenario.responses[i].IsEnd) continue;

                NPCResponseNode response = _nodeDictionary[_scenario.responses[i].GetID()].GetComponent<NPCResponseNode>();
                ChoiceGroupNode group = _nodeDictionary[_scenario.responses[i].GetChildNodeIDs()[0]].GetComponent<ChoiceGroupNode>();

                response.LinkChildNode(group);
                //Debug.Log("Linking: "+ _scenario.responses[i].GetID().GetIDString() + " to: " + _scenario.responses[i].GetChildNodeIDs()[0].GetIDString());
            }
        }
    }
}
