using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    [Header("GameTime")]
    [SerializeField] private float gameTime;
    private float elapsedTime = 0f;
    private int second = 0;
    public UnityEvent<int> OnSecondChanged { get; private set; } = new UnityEvent<int>();

    [Header("GameStart&End")]
    [field: SerializeField] public Transform StartPosition;
    [SerializeField] private MapClearSencer stageEndSencer;
    [field: SerializeField] public List<InteractableObj> KeyList { get; private set; }

    [field: SerializeField] public AudioClip BGM { get; private set; }

    private PlayerHP hp;
    private PlayerInput input;

    public void Init()
    {
        elapsedTime = 0f;

        stageEndSencer.OnStageSence.RemoveListener(OnStageClear);
        stageEndSencer.OnStageSence.AddListener(OnStageClear);

        GameObject player = GameManager.Instance.Player;
        hp = player.GetComponent<PlayerHP>();
        input = player.GetComponent<PlayerInput>();

        foreach(var key in KeyList)
        {
            key.OnSelect.RemoveListener(OnInteractObj);
            key.OnSelect.AddListener(OnInteractObj);
        }
    }

    public void StartStage()
    {
        SetStage();

        StartCoroutine(CoSetTimer());
    }

    private void SetStage()
    {
        ResetPlayerPosition();

        hp.OnPlayerDeath.RemoveAllListeners();
        hp.OnPlayerDeath.AddListener(OnGameOver);

        hp.OnPlayerFall.RemoveAllListeners();
        hp.OnPlayerFall.AddListener(ResetPlayerPosition);

        foreach(var key in KeyList)
        {
            key.ResetObj();
        }

        input.IsInputAvailable = true;

        Time.timeScale = 1f;

        UIManger.Instance.GetUIManager<MapUIManager>().ConnectUI(this);
        UIManger.Instance.OpenUI<MapUIManager>();
    }

    private IEnumerator CoSetTimer()
    {
        second = -1;
        elapsedTime = 0f;

        while (true)
        {
            int newSecond = (int)elapsedTime;
            if (newSecond != second)
            {
                OnSecondChanged?.Invoke((int)gameTime - newSecond);
                second = newSecond;
            }

            elapsedTime += Time.deltaTime;

            if (elapsedTime >= gameTime)
            {
                break;
            }


            yield return null;
        }

        OnGameOver();
    }

    private void OnStageClear(Transform _player)
    {
        StopAllCoroutines();

        if(CheckIfGameClear(_player))
        {
            OnGameClear();
        }
    }

    private bool CheckIfGameClear(Transform _player)
    {
        bool isClear = true;
        foreach (var key in KeyList)
        {
            if (key.isSet == false)
            {
                isClear = false;
                break;
            }
        }

        return isClear;
    }

    private void OnGameClear()
    {
        StopAllCoroutines();
        input.IsInputAvailable = false;
        Time.timeScale = 0f;

        DisconnectUI();

        UIManger.Instance.OpenUI<MapClearUIManager>();
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
        input.IsInputAvailable = false;
        Time.timeScale = 0f;

        DisconnectUI();

        UIManger.Instance.OpenUI<MapOverUIManager>();
    }

    private void DisconnectUI()
    {
        UIManger.Instance.GetUIManager<MapUIManager>().DisConnectUI(this);
        UIManger.Instance.CloseUI<MapUIManager>();
    }

    public void ResetPlayerPosition()
    {
        Transform player = GameManager.Instance.Player.transform;

        player.position = StartPosition.position;
        player.rotation = StartPosition.rotation;
    }

    private void OnInteractObj(InteractableObj obj)
    {
        var index = KeyList.FindIndex(_ =>  _ == obj);

        if(index >= 0 && index < KeyList.Count)
        {
            UIManger.Instance.GetUIManager<MapUIManager>().UpdateQuestList(obj, index);
        }
    }
}
