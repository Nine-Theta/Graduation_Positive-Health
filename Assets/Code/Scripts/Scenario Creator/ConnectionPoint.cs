using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
namespace ScenarioEditor
{
    [RequireComponent(typeof(LineRenderer))]
    public class ConnectionPoint : MonoBehaviour, IComparable<ConnectionPoint>
    {
        [SerializeField]
        private AbstractNode _ownerNode;


        [SerializeField]
        private Vector3 _instanceOffset;

        [SerializeField]
        private bool _isStartPoint = false;
        [SerializeField, ShowIf("_isStartPoint")]
        private CustomVerticalLayoutGroup _startNodeVerticalLayout;


        [ShowNonSerializedField]
        private ConnectionPoint _connectedNode;
        [ShowNonSerializedField]
        private bool _isConnected = false;

        private LineRenderer _lineRender;


        public void Start()
        {
            _lineRender = GetComponent<LineRenderer>();
            _lineRender.positionCount = 0;
            Debug.Log("parent: " + gameObject.transform.parent.name);

            if (_startNodeVerticalLayout != null)
                _startNodeVerticalLayout.OnSetLayoutVertical.AddListener(RecalculateConnection);
        }

        public bool IsConnected()
        {
            return _isConnected;
        }

        public void MakeConnection(ConnectionPoint pPoint)
        {
            Debug.Log("Attempting to Connected: " + this + " to: " + pPoint);

            if (_isConnected)
            {
                Debug.LogWarning("Already connected, Clearing existing connection");
                ClearConnection();
            }

            _connectedNode = pPoint;
            RecalculateConnection();
            _isConnected = true;

            Debug.Log("Are we connected? " + this.IsConnected());

            TryDuplicate();
        }

        public void ClearConnection()
        {
            Debug.Log("test If this works properly");
            _lineRender.positionCount = 0;
            _connectedNode = null;
            _isConnected = false;
        }

        [Button]
        public void TryDuplicate()
        {
            if (!_isStartPoint || _ownerNode.HasMaxConnections()) return;

            GameObject newPoint = Instantiate(gameObject, (transform.position + _instanceOffset), transform.rotation, transform.parent);
            _ownerNode.AddStartPoint(newPoint.GetComponent<ConnectionPoint>());
        }

        [Button]
        public void RecalculateConnection()
        {
            if (_connectedNode != null)
            {
                _lineRender.positionCount = 2;
                _lineRender.SetPositions(new Vector3[] { transform.position, _connectedNode.transform.position });
            }
        }

        public int CompareTo(ConnectionPoint pOther)
        {
            if (pOther == null) return 1;

            int x = 0;

            x += _isConnected ? -1 : 0;
            x += pOther._isConnected ? 1 : 0;

            return x;
        }

        public void OnDestroy()
        {
            if (_startNodeVerticalLayout != null)
                _startNodeVerticalLayout.OnSetLayoutVertical.RemoveListener(RecalculateConnection);
        }
    }
}
