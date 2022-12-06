using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetProductArea : MonoBehaviour
{
    [SerializeField] private Cashier _cashier;

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<InteractableObject>();
        if(interactable)
        {
            _cashier.AddProduct(interactable);
        }
    }
}
