using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

namespace ScenarioEditor
{
    public abstract class AbstractNodeCreator : MonoBehaviour
    {
        [SerializeField]
        protected Camera MainCamera;

        [SerializeField]
        protected Canvas MainCanvas;


        protected GameObject NodeObject;

        //z position will be off due to parenting to canvas, needs a fix
        public virtual GameObject CreateNewNodeAtPosition(Vector3 pPosition)
        {
            return Instantiate(NodeObject, pPosition, NodeObject.transform.rotation, MainCanvas.transform);
        }

        public virtual void CreateNewNodeAtCameraView()
        {
            GameObject newNode = CreateNewNodeAtPosition(new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, 0));
            newNode.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, 0);
        }

    }
}
