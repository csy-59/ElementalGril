using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObj : MonoBehaviour
{
    [field: SerializeField] public string TriggeredStr { get; private set; } = "Take";
    [field: SerializeField] public Sprite PreviewSprite { get; private set; }

    public bool isSet {  get; private set; }

    public UnityEvent<InteractableObj> OnSelect { get; private set; } = new UnityEvent<InteractableObj>();

    public virtual void Select()
    {
        isSet = true;
        gameObject.SetActive(false);

        OnSelect?.Invoke(this);
    }

    public void ResetObj()
    {
        isSet = false;
        gameObject.SetActive(true);
    }
}
