using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOverUIManager : UIBase
{
    [SerializeField] private Button restartBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(RestartMap);
    }

    private void RestartMap()
    {
        GameManager.Instance.RestartCurrentMap();
    }

    public override void Close()
    {
    }

    public override void Open()
    {
    }
}
