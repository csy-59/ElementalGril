using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractUIManager : UIBase
{
    [SerializeField] private TextMeshProUGUI text;
    private string str;

    public override void Close()
    {
    }

    public override void Open()
    {
        text.text = str;
    }

    public void SetUI(string _text)
    {
        str = _text;
    }
}
