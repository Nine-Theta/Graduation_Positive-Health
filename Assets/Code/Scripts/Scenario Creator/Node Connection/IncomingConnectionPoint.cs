using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class IncomingConnectionPoint : ConnectionPoint
    {
        private int _maxConnectionPoints = 5;

        private List<OutgoingConnectionPoint> _connectedStartPoints = new List<OutgoingConnectionPoint>();

        public void AddConnection(OutgoingConnectionPoint pPoint)
        {
            if (_connectedStartPoints.Count >= _maxConnectionPoints || _connectedStartPoints.Contains(pPoint))
                return;

            _connectedStartPoints.Add(pPoint);
            _isConnected = true;
        }

        public void RemoveConnection(OutgoingConnectionPoint pPoint)
        {
            _connectedStartPoints.Remove(pPoint);

            if(_connectedStartPoints.Count <= 0)
                _isConnected = false;
        }


        public override void RecalculateConnection()
        {
            if (_connectedStartPoints.Count > 0)
            {
                RecalculateAllConnections();
            }
        }

        public void RecalculateAllConnections()
        {
            for (int i = 0; i < _connectedStartPoints.Count; i++)
            {
                _connectedStartPoints[i].RecalculateConnection();
            }
        }

        public void OnDestroy()
        {
            for (int i = 0; i < _connectedStartPoints.Count; i++)
            {
                _connectedStartPoints[i].ClearConnection();
            }
        }
    }
}
