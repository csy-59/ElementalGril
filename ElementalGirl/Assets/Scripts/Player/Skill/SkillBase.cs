using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] private GameObject skillPreview;

    [field: SerializeField]
    public float SkillCoolTime { get; private set; } = 0.5f;

    public bool IsCoolTime { get; private set; } = false;

    private Vector3 skillPreviewOffset;

    protected virtual void Awake()
    {
        skillPreviewOffset = skillPreview.transform.localPosition;
    }

    public abstract void Init(Transform _parent);

    public virtual void UseSkill()
    {
        IsCoolTime = true;

        SkillLogic();
        StartCoroutine(CoSetCoolTime());
    }

    protected abstract void SkillLogic();

    private IEnumerator CoSetCoolTime()
    {
        float elspaedTime = 0f;
        while(true)
        {
            elspaedTime += Time.deltaTime;
            if (elspaedTime >= SkillCoolTime)
                break;
            yield return null;
        }

        IsCoolTime = false;
    }

    public virtual void SkillSelected()
    {
        ShowOrb(true);
    }

    public virtual void SkillDeselected()
    {
        ShowOrb(false); 
    }

    private void ShowOrb(bool _isShow)
    {
        skillPreview.SetActive(_isShow);
    }

    public void SetOrbInPosition()
    {
        skillPreview.transform.localPosition = skillPreviewOffset;
    }
}
