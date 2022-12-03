using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRPlayerInteractions : MonoBehaviour
{
    private InteractableObject _interactable;
    private InteractableObject _prevInteractable;
    private RaycastHit _hit;
    
    
    [SerializeField] private GameObject objectInfo;
    [SerializeField] private Text textName;
    [SerializeField] private Text textPrice;

    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private KeyCode interactKey;

    private void Update()
    {
        Interaction();
    }

    private void Interaction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out _hit, interactDistance) && _hit.collider.TryGetComponent(out _interactable))
        {
            if (_interactable != _prevInteractable)
            {
                DisablePrevOutline();
                EnableCurrentOutline();
            }

            CheckTypeInteractable();
            
        }
        else if (_prevInteractable != null) { DisablePrevOutline(); }
    }

    private void CheckTypeInteractable()
    {
        if (_interactable.typeInteract == InteractableObject.TypeInteract.Rotatable)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (_interactable.hasInteract && _interactable.rotatable.Opened)
                {
                    _interactable.hasInteract = false;
                }
                else if (!_interactable.hasInteract && !_interactable.rotatable.Opened)
                {
                    _interactable.hasInteract = true;
                }
            }
        }
        
        if (_interactable.typeInteract == InteractableObject.TypeInteract.Movable)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (_interactable.hasInteract && _interactable.movable.Opened)
                {
                    _interactable.hasInteract = false;
                }
                else if (!_interactable.hasInteract && !_interactable.movable.Opened)
                {
                    _interactable.hasInteract = true;
                }
            }
        }

        else if (_interactable.typeInteract == InteractableObject.TypeInteract.Collectable)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (!_interactable.hasInteract && !_interactable.collectable.InHand)
                {
                    _interactable.hasInteract = true;
                }
            }
        }
    }
    
    private void EnableCurrentOutline()
    {
        print("outlined " + _interactable.name);

        if (_interactable.typeInteract == InteractableObject.TypeInteract.Collectable)
        {
            objectInfo.SetActive(true);
            textName.text = _interactable.objectName;
            textPrice.text = $"{_interactable.objectPrice} ₽";
        }

        _interactable.OutlineOn();
        _prevInteractable = _interactable;
    }

    private void DisablePrevOutline()
    {
        print("disoutlined " + _prevInteractable);
        
        objectInfo.SetActive(false);
        textName.text = "";
        textPrice.text = "";

        if (_prevInteractable != null) _prevInteractable.OutlineOff();
        _prevInteractable = null;
    }
}
