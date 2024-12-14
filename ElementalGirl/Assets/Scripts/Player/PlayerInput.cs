using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal {  get; private set; }
    public float Vertical { get; private set; }

    public bool Jump {  get; private set; }
    public bool Inventory { get; private set; }
    public bool Interact { get; private set; }

    public bool UseSkill { get; private set; }

    public float SkillSelect { get; private set; }

    
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }


    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        Jump = Input.GetKeyDown(KeyCode.Space);
        Inventory = Input.GetKeyDown(KeyCode.E);
        Interact = Input.GetKeyDown(KeyCode.F);

        UseSkill = Input.GetMouseButtonDown(0);
        SkillSelect = Input.GetAxis("Mouse ScrollWheel");

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }
}
