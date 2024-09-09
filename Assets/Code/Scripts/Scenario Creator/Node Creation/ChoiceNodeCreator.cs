using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceNodeCreator : AbstractNodeCreator<ChoiceNode,SerializedChoice>
    {
        public override GameObject CreateNewNodeAtPosition(SerializedChoice pNode)
        {
            return CreateNewNodeAtPosition(pNode, Vector3.zero);
        }

        public override GameObject CreateNewNodeAtPosition(SerializedChoice pNode, Vector3 pPosOffset)
        {
            GameObject newNode = base.CreateNewNodeAtPosition(pNode, pPosOffset);

            newNode.GetComponent<ChoiceNode>().QuickSetValues(pNode);

            return newNode;
        }
    }
}
