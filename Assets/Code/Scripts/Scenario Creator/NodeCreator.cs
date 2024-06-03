using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{

    public class NodeCreator : MonoBehaviour
    {
        [SerializeField,Required]
        private Camera _mainCamera;

        [SerializeField]
        private ChoiceGroupNode _groupNodeTemplate;
        [SerializeField]
        private ChoiceNode _choiceNodeTemplate;
        [SerializeField]
        private NPCResponseNode _responseNodeTemplate;

        private GameObject _groupNodeObject;
        private GameObject _choiceNodeObject;
        private GameObject _responseNodeObject;

        private void Start()
        {
            _groupNodeObject = _groupNodeTemplate.gameObject;
            _choiceNodeObject = _choiceNodeTemplate.gameObject;
            _responseNodeObject = _responseNodeTemplate.gameObject;
        }

        public void createNewGroupNode()
        {
            Instantiate(_groupNodeObject, new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, 0), _groupNodeObject.transform.rotation, _groupNodeObject.transform.parent);
        }

        public void createNewChoiceNode()
        {
            Instantiate(_choiceNodeObject, new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, 0), _choiceNodeObject.transform.rotation, _choiceNodeObject.transform.parent);
        }

        public void createNewResponseNode()
        {
            Instantiate(_responseNodeObject, new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, 0), _responseNodeObject.transform.rotation, _responseNodeObject.transform.parent);
        }
    }
}
