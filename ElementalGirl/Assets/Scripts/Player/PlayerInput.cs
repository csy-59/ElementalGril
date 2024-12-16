using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal {  get; private set; }
    public float Vertical { get; private set; }

    public bool Jump {  get; private set; }
    public bool Interact { get; private set; }

    public bool UseSkill { get; private set; }

    public float SkillSelect { get; private set; }

    
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    public bool IsInputAvailable { get; set; }


    private void Update()
    {
        if (isActiveAndEnabled == false)
            return;

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        Jump = Input.GetKeyDown(KeyCode.Space);
        Interact = Input.GetKeyDown(KeyCode.E);

        UseSkill = Input.GetMouseButtonDown(0);
        SkillSelect = Input.GetAxis("Mouse ScrollWheel");

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }
}
