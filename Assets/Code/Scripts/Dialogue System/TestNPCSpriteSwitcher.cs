using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[Serializable]
public class TestEmotionSpritePair
{
    [SerializeField]
    private NPCEmotionState _emotion;
    [SerializeField]
    private Sprite _sprite;

    public NPCEmotionState EmotionState { get { return _emotion; } }
    public Sprite Sprite { get { return _sprite; } }
}

public class TestNPCSpriteSwitcher : MonoBehaviour
{
    [SerializeField]
    private Image _npcSprite;

    [SerializeField]  
    private List<TestEmotionSpritePair> _sprites = new List<TestEmotionSpritePair>();

    [SerializeField]
    private SerializedDictionary<NPCEmotionState, Sprite> _npcEmotionSprites = new SerializedDictionary<NPCEmotionState, Sprite> ();

    private void Start()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            _npcEmotionSprites.TryAdd(_sprites[i].EmotionState, _sprites[i].Sprite);
        }
    }

    public void SwitchSprite(NPCEmotionState pEmotion)
    {
        if (_npcEmotionSprites.ContainsKey(pEmotion))
        {
            _npcSprite.sprite = _npcEmotionSprites[pEmotion];
        }
    }
}
