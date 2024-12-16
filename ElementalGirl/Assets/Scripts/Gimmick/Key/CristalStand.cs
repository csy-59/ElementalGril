using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalStand : InteractableObj
{
    [SerializeField] private Transform cristal;

    public override void Select()
    {
        cristal.gameObject.SetActive(true);

        base.Select();
    }

    public override void ResetObj()
    {
        cristal.gameObject.SetActive(false);

        base.ResetObj();
    }
}
