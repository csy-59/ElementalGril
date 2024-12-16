using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    [field: SerializeField] public bool IsFullScrean { get; private set; }

    /// <summary>
    /// ui�� setActive�� true�� �� �� ȣ���
    /// </summary>
    public abstract void Open();
    /// <summary>
    /// ȣ�� �� ui�� setActive�� false��
    /// </summary>
    public abstract void Close();
}
