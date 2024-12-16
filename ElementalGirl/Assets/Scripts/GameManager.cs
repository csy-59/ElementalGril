using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public GameObject Player { get; private set; }
    private PlayerInput pInput;
    private PlayerHP pHp;

    [SerializeField] private MapManager[] maps;
    private int nextStageIndex = 0;
    private int currentStageIndex = 0;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip opening;
    [SerializeField] private AudioClip ending;

    protected override void Init()
    {
        base.Init();

        pInput = Player.GetComponent<PlayerInput>();
        pHp = Player.GetComponent<PlayerHP>();

        foreach (var map in maps)
        {
            map.Init();
        }

        PlayBGM(opening);
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

        pHp.FullHealth();

        maps[nextStageIndex].StartStage();
        PlayBGM(maps[nextStageIndex].BGM);
        currentStageIndex = nextStageIndex;
        nextStageIndex += 1;
    }

    public void RestartCurrentMap()
    {
        pHp.FullHealth();

        UIManger.Instance.CloseAllUI();
        maps[currentStageIndex].StartStage();
        PlayBGM(maps[currentStageIndex].BGM);
    }

    private void GameClear()
    {
        UIManger.Instance.CloseAllUI();
        UIManger.Instance.OpenUI<EndingUIManger>();
        PlayBGM(ending);
    }

    private void PlayBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
