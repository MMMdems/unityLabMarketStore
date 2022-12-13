using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAreaActivator40000ExtendedEditionDLC : MonoBehaviour
{
    public GameObject area;
    void Start()
    {
        StartCoroutine(ActivateFromInactive());
    }

    IEnumerator ActivateFromInactive()
    {
        yield return new WaitForSeconds(1);
        area.SetActive(true);
    }
}
