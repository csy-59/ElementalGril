using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    [SerializeField] private Transform skillPoint;

    [SerializeField] private SkillBase[] skills;
    SkillBase currentSkill = null;
    private int currentSkillIndex = -1;
    private bool isSkillSelected = false;

    private void Awake()
    {
        foreach (var skill in skills)
        {
            skill.Init(skillPoint);
        }
    }

    private void Start()
    {
        // 항상 첫번째 스킬로 세팅
        SetSkill(0);
    }

    private void Update()
    {
        SelectSkill();
        UseSkill();

        SetPreview();
    }

    private void SelectSkill()
    {
        if (input.SkillSelect == 0)
            return;

        bool isUp = input.SkillSelect > 0;
        int newSkillIndex = (currentSkillIndex + (isUp ? 1 : -1)) / skills.Length;
        newSkillIndex = Mathf.Max(newSkillIndex, 0);

        SetSkill(newSkillIndex);
    }

    private void UseSkill()
    {
        if (isSkillSelected == false)
            return;

        if (input.UseSkill == false)
            return;

        if (currentSkill.IsCoolTime == true)
            return;

        currentSkill.UseSkill();
    }

    private void SetPreview()
    {
        if (isSkillSelected == false)
            return;

        currentSkill.SetOrbInPosition();
    }

    private void SetSkill(int _newSkillIndex)
    {
        if (currentSkill != null)
        {
            currentSkill.SkillDeselected();
        }

        currentSkillIndex = _newSkillIndex;
        currentSkill = skills[currentSkillIndex];
        currentSkill.SkillSelected();
        isSkillSelected = true;
    }
}
