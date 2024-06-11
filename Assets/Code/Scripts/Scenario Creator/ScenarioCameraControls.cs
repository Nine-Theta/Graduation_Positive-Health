using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScenarioEditor
{
    public class ScenarioCameraControls : MonoBehaviour
    {
        [SerializeField, Required]
        private Camera _mainCamera;
        [SerializeField]
        private float _camMovementMult = 0.1f;
        [SerializeField]
        private float _camZoomMult = 1f;

        [SerializeField, MinMaxSlider(1f, 500f)]
        private Vector2 _camSizeLimit = new Vector2(1f, 200f);

        private float _cameraStartingDepth;

        public void Start()
        {
            _cameraStartingDepth = _mainCamera.transform.position.z;
        }

        public void OnMove(InputAction.CallbackContext pContext)
        {
            Vector3 movement = -pContext.ReadValue<Vector2>() * _camMovementMult * Time.deltaTime * _mainCamera.orthographicSize;

            _mainCamera.transform.position += movement;
        }

        public void OnZoom(InputAction.CallbackContext pContext)
        {
            float zoom = pContext.ReadValue<float>() * Time.deltaTime * _camZoomMult;
                        
            if (_mainCamera.orthographicSize - zoom < _camSizeLimit.x) //ugly if-else chain
                _mainCamera.orthographicSize = _camSizeLimit.x;
            else if (_mainCamera.orthographicSize - zoom > _camSizeLimit.y)
                _mainCamera.orthographicSize = _camSizeLimit.y;
            else
                _mainCamera.orthographicSize -= zoom;
        }

        public void OnClear(InputAction.CallbackContext pContext)
        {
            Debug.Log("called Clear");

        }

        public void OnResetView()
        {
            _mainCamera.transform.position = new Vector3(0, 0, _cameraStartingDepth);
        }
    }
}
