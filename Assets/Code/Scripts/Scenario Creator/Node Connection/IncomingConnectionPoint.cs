using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class IncomingConnectionPoint : ConnectionPoint
    {
        private int _maxConnections = 5;

        private List<OutgoingConnectionPoint> _connectedOutPoints = new List<OutgoingConnectionPoint>();

        public void MakeConnection(OutgoingConnectionPoint pPoint)
        {
            if (_connectedOutPoints.Count >= _maxConnections || _connectedOutPoints.Contains(pPoint))
                return;

            _connectedOutPoints.Add(pPoint);
            _isConnected = true;
        }

        public void RemoveConnection(OutgoingConnectionPoint pPoint)
        {
            _connectedOutPoints.Remove(pPoint);

            if(_connectedOutPoints.Count <= 0)
                _isConnected = false;
        }


        public override void RecalculateConnection()
        {
            if (_connectedOutPoints.Count > 0)
            {
                RecalculateAllConnections();
            }
        }

        public void RecalculateAllConnections()
        {
            for (int i = 0; i < _connectedOutPoints.Count; i++)
            {
                _connectedOutPoints[i].RecalculateConnection();
            }
        }

        public void OnDestroy()
        {
            for (int i = 0; i < _connectedOutPoints.Count; i++)
            {
                _connectedOutPoints[i].ClearConnection();
            }
        }
    }
}
