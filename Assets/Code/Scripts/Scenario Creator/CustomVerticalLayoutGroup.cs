using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomVerticalLayoutGroup : VerticalLayoutGroup
{
    public UnityEvent OnCalculateLayoutInputHorizontal = new UnityEvent();
    public UnityEvent OnCalculateLayoutInputVertical = new UnityEvent();
    public UnityEvent OnSetLayoutHorizontal = new UnityEvent();
    public UnityEvent OnSetLayoutVertical = new UnityEvent();

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        OnCalculateLayoutInputHorizontal.Invoke();
    }
    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputVertical();
        OnCalculateLayoutInputVertical.Invoke();
    }

    public override void SetLayoutHorizontal()
    {
        base.SetLayoutHorizontal();
        OnSetLayoutHorizontal.Invoke();
    }

    public override void SetLayoutVertical()
    {
        base.SetLayoutVertical();
        OnSetLayoutVertical.Invoke();
    }
}
