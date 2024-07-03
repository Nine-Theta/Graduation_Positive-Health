using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
namespace ScenarioEditor
{
    public abstract class ConnectionPoint : MonoBehaviour, IComparable<ConnectionPoint>
    {
        [SerializeField]
        protected AbstractNode _ownerNode;

        [ShowNonSerializedField]
        protected bool _isConnected = false;

        public bool IsConnected()
        {
            return _isConnected;
        }

        public abstract void RecalculateConnection();

        public int CompareTo(ConnectionPoint pOther)
        {
            if (pOther == null) return 1;

            int x = 0;

            x += _isConnected ? -1 : 0;
            x += pOther._isConnected ? 1 : 0;

            return x;
        }
    }
}
