using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectDragger : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField,Required]
    private Transform _objectToDrag;

    private Vector2 _oldPos;

    public UnityEvent OnDragEvent = new UnityEvent();
    

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        _oldPos = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 newPos = Camera.main.ScreenToWorldPoint(eventData.position);

        Vector3 delta = newPos - _oldPos;

        _objectToDrag.position += delta;

        _oldPos = newPos;

        OnDragEvent.Invoke();
    }
}
