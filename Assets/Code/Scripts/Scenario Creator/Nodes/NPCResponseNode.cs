using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ScenarioEditor
{
    public class NPCResponseNode : GenericNode<ChoiceNode, ChoiceGroupNode>, I_QuickSetNode<SerializedResponse>
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
            Debug.Log("QuickSetValues for ResponseNode");
            SetDialogue(pSerializedData.Dialogue);
            SetEmotionState(pSerializedData.EmotionState);
        }
    }
}
