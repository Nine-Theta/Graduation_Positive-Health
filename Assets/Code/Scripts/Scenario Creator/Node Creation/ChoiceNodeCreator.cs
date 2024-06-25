using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceNodeCreator : AbstractNodeCreator
    {
        [SerializeField]
        private ChoiceNode _nodeTemplate;

        private void Awake()
        {
            NodeObject = _nodeTemplate.gameObject;
        }

        public GameObject CreateChoiceNode(Vector3 pPosition, SerializedChoice pChoiceData)
        {
            GameObject newNode = CreateNewNodeAtPosition(pPosition);

            ChoiceNode choice = newNode.GetComponent<ChoiceNode>();
            choice.SetDialogue(pChoiceData.Dialogue);

            return newNode;
        }
    }
}
