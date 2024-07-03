using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class GroupNodeCreator : AbstractNodeCreator
    {
        [SerializeField]
        private ChoiceGroupNode _nodeTemplate;

        private void Awake()
        {
            NodeObject = _nodeTemplate.gameObject;
        }

        public GameObject CreateGroupNode(Vector3 pPosition, SerializedChoiceGroup pGroupData)
        {
            GameObject newNode = CreateNewNodeAtPosition(pPosition);

            ChoiceGroupNode group = newNode.GetComponent<ChoiceGroupNode>();

            return newNode;
        }
    }
}
