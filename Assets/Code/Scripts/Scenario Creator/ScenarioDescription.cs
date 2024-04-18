using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScenarioEditor
{
    public class ScenarioDescription : MonoBehaviour
    {
        private string _targetAudience;
        private string _description;

        private ChoiceGroupNode _startingNode;

        public string TargetAudience
        {
            get { return _targetAudience; }
            set { _targetAudience = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public ChoiceGroupNode StartingNode
        {
            get { return _startingNode; }
            set { _startingNode = value; }
        }
    }
}
