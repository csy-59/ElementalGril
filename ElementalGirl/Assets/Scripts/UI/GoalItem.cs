using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalItem : MonoBehaviour
{
    [SerializeField] private Image preview;
    [SerializeField] private TextMeshProUGUI textCount;

    public void SetItem(InteractableObj interactableObj)
    {
        preview.sprite = interactableObj.PreviewSprite;
    }

    public void UpdateItem(InteractableObj interactableObj)
    {
        textCount.text = (interactableObj.isSet ? 0 : 1).ToString();
    }
}
