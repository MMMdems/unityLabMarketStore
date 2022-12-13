using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNPCRotatable : MonoBehaviour
{
    public List<InteractableObject> gates = new List<InteractableObject>();

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "NPC")
        {
            gates[0].hasInteract = true;
            gates[1].hasInteract = true;
            Debug.Log("Вошел");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        gates[0].hasInteract = false;
        gates[1].hasInteract = false;
        Debug.Log("Вышел");
    }
}
