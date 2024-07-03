using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ResponseNodeCreator : AbstractNodeCreator
    {
        [SerializeField]
        private NPCResponseNode _nodeTemplate;

        private void Awake()
        {
            NodeObject = _nodeTemplate.gameObject;
        }

        public GameObject CreateResponseNode(Vector3 pPosition, SerializedResponse pResponseData)
        {
            GameObject newNode = CreateNewNodeAtPosition(pPosition);

            NPCResponseNode response = newNode.GetComponent<NPCResponseNode>();
            response.SetDialogue(pResponseData.Dialogue);

            return newNode;
        }
    }
}
