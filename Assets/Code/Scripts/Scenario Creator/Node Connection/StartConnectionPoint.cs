using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    [RequireComponent(typeof(LineRenderer))]
    public class StartConnectionPoint : ConnectionPoint
    {
        [SerializeField]
        private CustomVerticalLayoutGroup _startNodeVerticalLayout;

        [SerializeField]
        protected Vector3 _instanceOffset;

        private EndConnectionPoint _connectedEndPoint;

        private LineRenderer _lineRender;

        public void OnEnable()
        {
            if (_startNodeVerticalLayout != null)
                _startNodeVerticalLayout.OnSetLayoutVertical.AddListener(RecalculateConnection);

            _lineRender = GetComponent<LineRenderer>();
            _lineRender.positionCount = 0;
        }

        public void MakeConnection(EndConnectionPoint pPoint)
        {
            Debug.Log("Attempting to Connected: " + this + " to: " + pPoint);

            if (_isConnected)
            {
                Debug.LogWarning("Already connected, Clearing existing connection");
                ClearConnection();
            }

            _connectedEndPoint = pPoint;
            _isConnected = true;

            _connectedEndPoint.AddConnection(this);

            TryDuplicate();
        }

        public override void RecalculateConnection()
        {
            Debug.Log("is linerenderer null? "+ (_lineRender == null));

            if (_connectedEndPoint != null)
            {
                _lineRender.positionCount = 2;
                _lineRender.SetPositions(new Vector3[] { transform.position, _connectedEndPoint.transform.position });
            }
        }

        [Button]
        public void TryDuplicate()
        {
            if (_ownerNode.HasMaxConnections()) return;

            GameObject newPointObj = Instantiate(gameObject, (transform.position + _instanceOffset), transform.rotation, transform.parent);
            StartConnectionPoint startPoint = newPointObj.GetComponent<StartConnectionPoint>();

            startPoint.ClearConnection();
            _ownerNode.AddStartPoint(startPoint);
        }

        public void ClearConnection()
        {
            if (!_isConnected)
                return;

            _lineRender.positionCount = 0;
            _connectedEndPoint.RemoveConnection(this);
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
