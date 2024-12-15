using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private float dissapearTime = 0.5f;
    private WaitForSeconds wfDissapear;

    private void Awake()
    {
        wfDissapear = new WaitForSeconds(dissapearTime);

        model.SetActive(true);
        fireEffect.SetActive(false);
    }

    public void SetFire() => StartCoroutine(CoSetOnFire());

    private IEnumerator CoSetOnFire()
    {
        fireEffect.SetActive(true);
        yield return wfDissapear;
        
        model.SetActive(false);
        yield return wfDissapear;

        gameObject.SetActive(false);
    }
}
