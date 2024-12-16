using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUIManger : UIBase
{
    [SerializeField] private Button restartBtn;

    private void Awake()
    {
        restartBtn.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public override void Close()
    {
    }

    public override void Open()
    {
    }

}
