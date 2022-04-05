using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMessenger : MonoBehaviour
{
    [SerializeField]
    private Image _background;
    public Image Background {
        get { return _background; }
    }

    [SerializeField]
    private Text _text;

    public string Message{
        get { return _text.text; }
        set { _text.text = value; }
    }
}
