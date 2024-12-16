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
    }

    public override void Open()
    {
    }
}
