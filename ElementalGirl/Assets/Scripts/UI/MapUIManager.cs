using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapUIManager : UIBase
{
    [SerializeField] private Image hpImage;
    [SerializeField] private TextMeshProUGUI time;
    
    [SerializeField] private Transform goalTrans;
    [SerializeField] private GameObject goalItem;
    private List<GoalItem> goalList = new List<GoalItem>();

    private PlayerHP hp;
    private int maxHp;

    private void Awake()
    {
        hp = GameManager.Instance.Player.GetComponent<PlayerHP>();

        hp.OnPlayerHit.RemoveListener(SetHPUI);
        hp.OnPlayerHit.AddListener(SetHPUI);

        maxHp = hp.MaxHp;
    }

    public void ConnectUI(MapManager _manager)
    {
        _manager.OnSecondChanged.AddListener(SetSecond);

        SetQuestList(_manager.KeyList);
    }

    private void SetQuestList(List<InteractableObj> interactableObjs)
    {
        for (int i = goalList.Count - 1; i >= 0; --i)
        {
            Destroy(goalList[i].gameObject);
        }

        foreach (var item in interactableObjs)
        {
            GameObject go = Instantiate(goalItem, goalTrans);
            GoalItem itemScript = go.GetComponent<GoalItem>();
            itemScript.SetItem(item);
            goalList.Add(itemScript);
        }
    }

    public void UpdateQuestList(InteractableObj obj, int index)
    {
        goalList[index].UpdateItem(obj);
    }

    private void SetSecond(int second)
    {
        time.text = second.ToString();
    }

    private void SetHPUI(int _hp)
    {
        hpImage.fillAmount = _hp / maxHp;
    }

    public override void Close()
    {
    }

    public override void Open()
    {
    }

    public void DisConnectUI(MapManager _manager)
    {
        _manager.OnSecondChanged.RemoveListener(SetSecond);
    }
}
