using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    [RequireComponent(typeof(LineRenderer))]
    public class OutgoingConnectionPoint : ConnectionPoint
    {
        [SerializeField]
        private CustomVerticalLayoutGroup _startNodeVerticalLayout;

        [SerializeField]
        protected Vector3 _instanceOffset;

        private IncomingConnectionPoint _connectedInPoint;

        private LineRenderer _lineRender;

        public void OnEnable()
        {
            if (_startNodeVerticalLayout != null)
                _startNodeVerticalLayout.OnSetLayoutVertical.AddListener(RecalculateConnection);

            _lineRender = GetComponent<LineRenderer>();
            _lineRender.positionCount = 0;
        }

        public IncomingConnectionPoint GetConnectedPoint()
        {
            return _connectedInPoint;
        }

        public void MakeConnection(IncomingConnectionPoint pPoint)
        {
            //Debug.Log("Attempting to Connected: " + this + " to: " + pPoint);

            if (_isConnected)
            {
                Debug.LogWarning("Already connected, Clearing existing connection");
                ClearConnection();
            }

            _connectedInPoint = pPoint;
            _isConnected = true;

            _connectedInPoint.MakeConnection(this);

            TryDuplicate();
        }

        public override void RecalculateConnection()
        {
            //Debug.Log("is linerenderer null? "+ (_lineRender == null));

            if (_connectedInPoint != null)
            {
                _lineRender.positionCount = 2;
                _lineRender.SetPositions(new Vector3[] { transform.position, _connectedInPoint.transform.position });
            }
        }

        [Button]
        public void TryDuplicate()
        {
            if (_ownerNode.HasMaxConnections()) return;

            GameObject newPointObj = Instantiate(gameObject, (transform.position + _instanceOffset), transform.rotation, transform.parent);
            OutgoingConnectionPoint outPoint = newPointObj.GetComponent<OutgoingConnectionPoint>();

            outPoint.ClearConnection();
            _ownerNode.AddOutgoingPoint(outPoint);
        }

        public void ClearConnection()
        {
            if (!_isConnected)
                return;

            _lineRender.positionCount = 0;
            _connectedInPoint.RemoveConnection(this);
            _connectedInPoint = null;
            _isConnected = false;
        }

        public void OnDestroy()
        {
            if (_startNodeVerticalLayout != null)
                _startNodeVerticalLayout.OnSetLayoutVertical.RemoveListener(RecalculateConnection);

            ClearConnection();
        }
    }
}
