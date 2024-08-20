using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

namespace ScenarioEditor
{
    public abstract class AbstractNodeCreator<NODE, SERIALIZEDNODE> : MonoBehaviour where NODE : AbstractNode where SERIALIZEDNODE : I_SerializedNode
    {
        [SerializeField]
        protected Camera MainCamera;

        [SerializeField]
        protected Canvas MainCanvas;

        [SerializeField]
        protected NODE NodeTemplate;

        protected GameObject NodeObject;

        protected virtual void Awake()
        {
            NodeObject = NodeTemplate.gameObject;
        }


        public virtual GameObject CreateNewNodeAtPosition(SERIALIZEDNODE pNode)
        {
            return CreateNewNodeAtPosition(pNode);
        }

        public virtual GameObject CreateNewNodeAtPosition(SERIALIZEDNODE pNode, Vector3 pPosOffset)
        {
            return CreateNewNodeAtPosition((Vector3)pNode.GetNodePos() + pPosOffset);
        }

        //z position will be off due to parenting to canvas, needs a fix
        public virtual GameObject CreateNewNodeAtPosition(Vector3 pPosition)
        {
            return Instantiate(NodeTemplate.gameObject, pPosition, NodeTemplate.transform.rotation, MainCanvas.transform);
        }

        public virtual void CreateNewNodeAtCameraView()
        {
            GameObject newNode = CreateNewNodeAtPosition(new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, 0));
            newNode.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, 0);
        }

    }
}
