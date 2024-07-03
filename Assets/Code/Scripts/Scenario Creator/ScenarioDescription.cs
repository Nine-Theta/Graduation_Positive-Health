using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScenarioEditor
{
    [Serializable]
    public class ScenarioDescription : MonoBehaviour
    {    
        [SerializeField]
        private string _name = "Test Scenario";
        [SerializeField]
        private TMP_InputField _nameField;

        [SerializeField]
        private string _targetAudience;
        [SerializeField]
        private TMP_InputField _targetAudienceField;

        [SerializeField]
        private string _description = "f";
        [SerializeField]
        private TMP_InputField _descriptionField;


        [SerializeField]
        private ChoiceGroupNode _startingNode;

        public string TargetAudience
        {
            get { return _targetAudience; }
            set
            {
                _targetAudience = value;
                _targetAudienceField.text = value;
            }
        }

        public string ScenarioName
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameField.text = value;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                _descriptionField.text = value;
            }
        }

        public ChoiceGroupNode StartingNode
        {
            get { return _startingNode; }
            set { _startingNode = value; }
        }
    }
}
