using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ScenarioEditor
{
    public class ResponseNodeCreator : AbstractNodeCreator<NPCResponseNode,SerializedResponse>
    {
        public override GameObject CreateNewNodeAtPosition(SerializedResponse pNode)
        {
            return CreateNewNodeAtPosition(pNode, Vector3.zero);
        }

        public override GameObject CreateNewNodeAtPosition(SerializedResponse pNode, Vector3 pPosOffset)
        {
            GameObject newNode = base.CreateNewNodeAtPosition(pNode,pPosOffset);

            newNode.GetComponent<NPCResponseNode>().QuickSetValues(pNode);

            return newNode;
        }
    }
}
