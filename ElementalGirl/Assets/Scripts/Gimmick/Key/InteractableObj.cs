using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObj : MonoBehaviour
{
    [SerializeField] private Collider col;

    [field: SerializeField] public string TriggeredStr { get; private set; } = "Take";
    [field: SerializeField] public Sprite PreviewSprite { get; private set; }

    public bool isSet { get; private set; } = false;

    public UnityEvent<InteractableObj> OnSelect { get; private set; } = new UnityEvent<InteractableObj>();

    public virtual void Select()
    {
        isSet = true;
        col.enabled = false;
        OnSelect?.Invoke(this);
    }

    public virtual void ResetObj()
    {
        isSet = false;
        col.enabled = true;
    }
}
