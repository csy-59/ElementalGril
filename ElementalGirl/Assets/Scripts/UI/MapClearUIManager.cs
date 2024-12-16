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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Open()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
