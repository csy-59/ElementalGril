using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : InteractableObj
{
    [SerializeField] private Transform cristal;

    public override void Select()
    {
        cristal.gameObject.SetActive(false);

        base.Select();
    }

    public override void ResetObj()
    {
        cristal.gameObject.SetActive(true);

        base.ResetObj();
    }
}
