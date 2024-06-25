using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ResponseNodeCreator : AbstractNodeCreator
    {
        [SerializeField]
        private NPCResponseNode _nodeTemplate;

        private void Awake()
        {
            NodeObject = _nodeTemplate.gameObject;
        }
    }
}
