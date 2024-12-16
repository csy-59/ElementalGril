using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapClearSencer : MonoBehaviour
{
    [SerializeField] private Collider sencer;

    public UnityEvent<Transform> OnStageSence { get; private set; } = new UnityEvent<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPlayerSenced(other.gameObject);
        }
    }

    private void OnPlayerSenced(GameObject _player)
    {
        print("PlayerSenced");

        Transform player = _player.transform;
        while(player != null)
        {
            if(player.TryGetComponent<PlayerHP>(out var _))
            {
                break;
            }

            player = player.parent;
        }

        OnStageSence?.Invoke(player);
    }
}
