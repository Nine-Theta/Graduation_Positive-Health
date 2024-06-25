using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceNodeCreator : AbstractNodeCreator
    {
        [SerializeField]
        private ChoiceNode _nodeTemplate;

        private void Awake()
        {
            NodeObject = _nodeTemplate.gameObject;
        }
    }
}
