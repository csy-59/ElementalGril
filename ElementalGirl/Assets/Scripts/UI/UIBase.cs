using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    [field: SerializeField] public bool IsFullScrean { get; private set; }

    /// <summary>
    /// ui의 setActive가 true가 된 후 호출됨
    /// </summary>
    public abstract void Open();
    /// <summary>
    /// 호출 후 ui의 setActive가 false됨
    /// </summary>
    public abstract void Close();
}
