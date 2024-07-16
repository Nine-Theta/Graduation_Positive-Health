using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScenarioEditor
{
    public class ChoiceNode : GenericNode<ChoiceGroupNode, NPCResponseNode>
    {
        [HorizontalLine(color: EColor.Yellow)]

        [SerializeField]
        private ChoiceGroupNode _choiceGroup;

        [SerializeField]
        private string _choiceDialogue;
        [SerializeField]
        private TMP_InputField _dialogueField;

        [SerializeField]
        private ChoiceAvailability _availability = ChoiceAvailability.ALWAYS;

        [SerializeField]
        private List<NPCResponseNode> _linkedResponses = new List<NPCResponseNode>();

        [SerializeField, Foldout("Modifiable Variables")]
        private int _unlockIndex = -1;
        [SerializeField, Foldout("Modifiable Variables")]
        private int _unlockTreshold = 0;

        [SerializeField, Foldout("Modifiable Variables")]
        private int _conditionIndex = -1;
        [SerializeField, Foldout("Modifiable Variables")]
        private int _conditionModifier = 0;

        public string GetDialogue()
        {
            return _choiceDialogue;
        }

        public void SetDialogue(string pDialogue)
        {
            _choiceDialogue = pDialogue;
            _dialogueField.text = pDialogue;
        }

        public bool IsChoiceConditional
        {
            get { return _availability != ChoiceAvailability.ALWAYS; }

            set { _availability = value ? ChoiceAvailability.CONDITIONAL : ChoiceAvailability.ALWAYS; }
        }

        public ChoiceAvailability GetChoiceAvailability()
        {
            return _availability;
        }

        public int ConditionThreshold
        {
            get { return _unlockTreshold; }
            set { _unlockTreshold = value; }
        }

        public int ConditionModifier
        {
            get { return _conditionModifier; }
            set { _conditionModifier = value; }
        }

        public void QuickSetValues(SerializedChoice pSerializedData)
        {
            SetDialogue(pSerializedData.Dialogue);
        }

        /*
        #region Node linking
        public List<NPCResponseNode> GetResponses()
        {
            return _linkedResponses;
        }

        public void LinkChildNode(NPCResponseNode pResponse)
        {
            base.LinkChildNode(pResponse);
        }

        public void LinkResponse(NPCResponseNode pResponse)
        {
            if (_linkedResponses.Contains(pResponse))
                return;

            _linkedResponses.Add(pResponse);
            pResponse.LinkChoice(this);
        }

        public void UnlinkResponse(NPCResponseNode pResponse)
        {
            if (!_linkedResponses.Contains(pResponse))
                return;

            _linkedResponses.Remove(pResponse);
            pResponse.UnlinkChoice(this);
        }


        public ChoiceGroupNode GetChoiceGroup()
        {
            return _choiceGroup;
        }

        public void SetChoiceGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (_choiceGroup == pChoiceGroup) return;

            if (_choiceGroup != null)
            {
                _choiceGroup.RemoveChoice(this);
            }

            _choiceGroup = pChoiceGroup;

            if (pChoiceGroup == null)
                return;

            _choiceGroup.AddChoice(this);

        }
        #endregion
        */
    }
}
