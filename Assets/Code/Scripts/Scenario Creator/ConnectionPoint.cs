using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ScenarioEditor
{
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectionPoint : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _instanceOffset;

        private Vector3 _startPos;

        private LineRenderer _lineRender;

        private bool _isConnected = false;

        public void Awake()
        {
            _lineRender = GetComponent<LineRenderer>();
            Debug.Log("parent: " + gameObject.transform.parent.name);
        }

        public void StartConnection()
        {
            if (_isConnected)
            {
                Debug.Log("Already connected, returning");
                return;
            }

            _startPos = transform.position;
        }

        public void CompleteConnection(ConnectionPoint pPoint)
        {
            GameObject newPoint = Instantiate(gameObject, (transform.position + _instanceOffset), transform.rotation, transform.parent);

            _lineRender.SetPositions(new Vector3[] { _startPos, pPoint.transform.position });
            _isConnected = true;
        }

        public void ClearConnection()
        {
            Debug.Log("test If this works properly");
            _lineRender.positionCount = 0;
            _isConnected = false;
        }
    }
}
