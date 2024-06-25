using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class GroupNodeCreator : AbstractNodeCreator
    {
        [SerializeField]
        private ChoiceGroupNode _nodeTemplate;

        private void Awake()
        {
            NodeObject = _nodeTemplate.gameObject;
        }
    }
}
