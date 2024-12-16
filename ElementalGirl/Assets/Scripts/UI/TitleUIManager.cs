using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : UIBase
{
    [SerializeField] private Button gameStartBtn;
    [SerializeField] private Button howToPlayBtn;

    [SerializeField] private Transform howToPlayPanel;
    [SerializeField] private Button exitBtn;

    private void Awake()
    {
        gameStartBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.GameStart();
        });

        howToPlayBtn.onClick.AddListener(() =>
        {
            howToPlayPanel.gameObject.SetActive(true);
        });

        exitBtn.onClick.AddListener(() =>
        {
            howToPlayPanel.gameObject.SetActive(false);
        });
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
