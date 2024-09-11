using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class EditorSceneSettings : MonoBehaviour
    {
        private static EditorSceneSettings _instance = null;

        public static EditorSceneSettings Instance
        {
            get { return _instance; }
        }

        [SerializeField]
        private bool _nodeSnap = false;

        [SerializeField]
        private float _snapSize = 1f;

        public bool IsNodeSnapEnabled { get { return _nodeSnap; } }

        public float NodeSnapSize { get { return _snapSize; } }

        private void Awake()
        {
            if (_instance == null || _instance == this)
                _instance = this;
            else
                Destroy(this);
        }

        public void SetEnableNodeSnap(bool pEnabled)
        {
            _nodeSnap = pEnabled;
        }

        public void SetSnapSize(float pSize)
        {
            _snapSize = pSize;
        }
    }
}
