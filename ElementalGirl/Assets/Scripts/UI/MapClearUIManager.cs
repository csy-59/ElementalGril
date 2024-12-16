using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapClearUIManager : UIBase
{
    [SerializeField] private Button nextBtn;

    private void Awake()
    {
        nextBtn.onClick.AddListener(OnClickRestart);
    }

    private void OnClickRestart()
    {
        GameManager.Instance.StartNextMap();
    }

    public override void Close()
    {
    }

    public override void Open()
    {
    }
}
