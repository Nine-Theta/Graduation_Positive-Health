using NaughtyAttributes;
using ScenarioEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace ScenarioEditor
{
    public class ObjectDragger : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField, Required]
        private RectTransform _objectToDrag;

        private Vector2 _oldPos;

        private Vector2 _travelDistance;

        public UnityEvent OnDragEvent = new UnityEvent();


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _oldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            _travelDistance = _objectToDrag.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Vector2 newPos = Camera.main.ScreenToWorldPoint(eventData.position);

            Vector2 delta = newPos - _oldPos;

            Debug.Log("travelDistance : " + _travelDistance);

            _travelDistance += delta;

            Vector2 objPos = _travelDistance;


            Debug.Log("objPos : " + objPos);


            if (EditorSceneSettings.Instance.IsNodeSnapEnabled)
            {
                float snapSize = EditorSceneSettings.Instance.NodeSnapSize;
                float half = snapSize * 0.5f;

                float modx = _travelDistance.x % snapSize;
                float mody = _travelDistance.y % snapSize;

                objPos.x -= modx < half ? modx : (modx - snapSize);
                objPos.y -= mody < half ? mody : (mody - snapSize);
            }

            _objectToDrag.anchoredPosition = objPos;


            Debug.Log("Drag Pos : " + objPos);
            Debug.Log("Actual Pos: " + _objectToDrag.position);

            _oldPos = newPos;

            OnDragEvent.Invoke();
        }
    }
}
