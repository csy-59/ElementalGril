using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public GameObject Player { get; private set; }
    private PlayerInput pInput;

    [SerializeField] private MapManager[] maps;
    private int nextStageIndex = 0;
    private int currentStageIndex = 0;

    protected override void Init()
    {
        base.Init();

        pInput = Player.GetComponent<PlayerInput>();

        foreach (var map in maps)
        {
            map.Init();
        }
    }

    private void Start()
    {
        pInput.IsInputAvailable = false;
        UIManger.Instance.OpenUI<TitleUIManager>();
    }

    public void GameStart()
    {
        nextStageIndex = 0;
        StartNextMap();
    }

    public void StartNextMap()
    {
        if (nextStageIndex < 0)
            return;

        if(nextStageIndex >= maps.Length)
        {
            GameClear();
            return;
        }

        UIManger.Instance.CloseAllUI();

        maps[nextStageIndex].StartStage();
        currentStageIndex = nextStageIndex;
        nextStageIndex += 1;
    }

    public void RestartCurrentMap()
    {
        UIManger.Instance.CloseAllUI();
        maps[currentStageIndex].StartStage();
    }

    private void GameClear()
    {
        UIManger.Instance.CloseAllUI();
        UIManger.Instance.OpenUI<EndingUIManger>();
    }
}
