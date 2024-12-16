using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    private InteractableObj interactableObj;
    private bool isInteractable = false;

    private void Update()
    {
        if (isInteractable == false)
            return;

        if (input.Interact == true)
        {
            interactableObj.Select();

            isInteractable = false;
            interactableObj = null;

            UIManger.Instance.CloseUI<InteractUIManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("InteractableObj"))
        {
            isInteractable = true;
            interactableObj = other.GetComponent<InteractableObj>();

            UIManger.Instance.GetUIManager<InteractUIManager>()?.SetUI(interactableObj.TriggeredStr);
            UIManger.Instance.OpenUI<InteractUIManager>();
        }
    }
}
