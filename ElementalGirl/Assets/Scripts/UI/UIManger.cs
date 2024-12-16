using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoSingleton<UIManger>
{
    [SerializeField] private UIBase[] uiList;
    [SerializeField] private Dictionary<Type, UIBase> uiDict = new Dictionary<Type, UIBase>();

    private UIBase currentBaseUI;
    [SerializeField] private List<UIBase> coverUIStack = new List<UIBase>();

    private void Awake()
    {
        foreach (var ui in uiList)
        {
            uiDict[ui.GetType()] = ui;
        }
    }

    public T GetUIManager<T>() where T : UIBase
    {
        if (uiDict.ContainsKey(typeof(T)) == false)
            return null;

        return uiDict[typeof(T)] as T;
    }

    public void OpenUI<T>() where T : UIBase
    {
        T ui = GetUIManager<T>();
        if(ui.IsFullScrean == false)
        {
            coverUIStack.Add(ui);
            ui.gameObject.SetActive(true);
            ui.Open();
        }
        else
        {
            if(currentBaseUI != null)
            {
                currentBaseUI.Close();
                currentBaseUI.gameObject.SetActive(false);
            }

            currentBaseUI = ui;
            currentBaseUI.gameObject.SetActive(true);
            currentBaseUI.Open();
        }
    }

    public void CloseUI<T>() where T : UIBase
    {
        T ui = GetUIManager<T>();
        if (ui.IsFullScrean == false)
        {
            int uiBase = coverUIStack.FindIndex(_ => _ == ui);
            if (uiBase < 0)
                return;

            coverUIStack.Remove(ui);

            ui.Close();
            ui.gameObject.SetActive(false);
        }
        else
        {
            if (currentBaseUI == ui)
                return;

            currentBaseUI.Close();
            currentBaseUI.gameObject.SetActive(false);
            currentBaseUI = null;
        }
    }

    public void CloseAllUI()
    {
        for (int i = coverUIStack.Count - 1; i >= 0; --i)
        {
            var ui = coverUIStack[i];
            ui.Close();
            ui.gameObject.SetActive(false);

            coverUIStack.Remove(ui);
        }

        if (currentBaseUI == null)
            return;

        currentBaseUI.Close();
        currentBaseUI.gameObject.SetActive(false);
        currentBaseUI = null;
    }
}
