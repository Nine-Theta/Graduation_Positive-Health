using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggleSpriteSwap : MonoBehaviour
{
    [SerializeField]
    private Image _toggleSprite;
    [SerializeField]
    private Image _invertedToggleSprite;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnToggle);
    }

    private void OnToggle(bool pValue)
    {
        _toggleSprite.enabled = pValue;
        _invertedToggleSprite.enabled = !pValue;
    }
}
