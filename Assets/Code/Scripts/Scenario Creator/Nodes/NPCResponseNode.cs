using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScenarioEditor
{
    public class NPCResponseNode : GenericNode<ChoiceNode, ChoiceGroupNode>
    {
        [HorizontalLine(color: EColor.Violet)]

        [SerializeField]
        private string _responseDialogue;
        [SerializeField]
        private TMP_InputField _dialogueField;

        [SerializeField]
        private NPCEmotionState _emotionState;

        [SerializeField]
        private List<ChoiceNode> _linkingChoices = new List<ChoiceNode>();
        [SerializeField]
        private ChoiceGroupNode _linkedChoiceGroup;

        public string GetDialogue()
        {
            return _responseDialogue;
        }
        public void SetDialogue(string pDialogue)
        {
            _responseDialogue = pDialogue;
            _dialogueField.text = _responseDialogue;
        }

        public NPCEmotionState GetEmotionState()
        {
            return _emotionState;
        }

        public void SetEmotionState(NPCEmotionState pEmotionState)
        {
            _emotionState = pEmotionState;
        }

        public void QuickSetValues(SerializedResponse pSerializedData)
        {
            SetDialogue(pSerializedData.Dialogue);
            SetEmotionState(pSerializedData.EmotionState);
        }

        /*
        #region Node linking

        public ChoiceGroupNode GetChoiceGroup()
        {
            return _linkedChoiceGroup;
        }

        public void SetChoiceGroup(ChoiceGroupNode pChoiceGroup)
        {
            if (pChoiceGroup == _linkedChoiceGroup) return;

            if (_linkedChoiceGroup != null)
            {
                _linkedChoiceGroup.UnlinkResponse(this);
            }

            _linkedChoiceGroup = pChoiceGroup;

            if (pChoiceGroup == null)
                return;

            pChoiceGroup.LinkResponse(this);
        }

        public List<ChoiceNode> GetChoices()
        {
            return _linkingChoices;
        }

        public void LinkChoice(ChoiceNode pChoice)
        {
            if (_linkingChoices.Contains(pChoice))
                return;

            _linkingChoices.Add(pChoice);
            pChoice.LinkResponse(this);
        }

        public void UnlinkChoice(ChoiceNode pChoice)
        {
            if (!_linkingChoices.Contains(pChoice))
                return;

            _linkingChoices.Remove(pChoice);
            pChoice.UnlinkResponse(this);
        }
        #endregion
        */

    }
}
