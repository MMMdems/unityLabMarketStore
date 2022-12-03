using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class PlayerInteractions : MonoBehaviour
{
    private InteractableObject _interactable;
    private InteractableObject _prevInteractable;
    private RaycastHit _hit;
    
    
    [SerializeField] private GameObject objectInfo;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPrice;
    
    [SerializeField] private float interactDistance = 3f;
    
    //[SerializeField] private KeyCode interactKey;
    
    [Header("VR Input")]
    public GameObject rightController; public SteamVR_Behaviour_Pose rightControllerPose;
    public GameObject leftController; public SteamVR_Behaviour_Pose leftControllerPose;
    [SerializeField] private SteamVR_Action_Boolean buttonGrabPinch = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    [SerializeField] private SteamVR_Action_Boolean buttonGrabGrip = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    [SerializeField] private bool pressed = false;

    [Header("Liner")] 
    [SerializeField] private GameObject pLinePrefab;
    [SerializeField] private GameObject pLine;

    private void Awake()
    {
        pLine = Instantiate(pLinePrefab, rightController.transform.position, rightController.transform.rotation) as GameObject;
        pLine.transform.SetParent(rightController.transform);
        pressed = false;
    }

    private void Update()
    {
        Interaction();
    }

    private void Interaction()
    {
        Ray ray = new Ray(rightController.transform.position, rightController.transform.forward);
        
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
            if (buttonGrabPinch.GetStateDown(rightControllerPose.inputSource) && pressed == false )
            {
                if (_interactable.hasInteract && _interactable.rotatable.Opened)
                {
                    _interactable.hasInteract = false;
                }
                else if (!_interactable.hasInteract && !_interactable.rotatable.Opened)
                {
                    _interactable.hasInteract = true;
                }
                pressed = true;
            }
        }
        
        if (_interactable.typeInteract == InteractableObject.TypeInteract.Movable)
        {
            if (buttonGrabPinch.GetStateDown(rightControllerPose.inputSource) && pressed == false )
            {
                if (_interactable.hasInteract && _interactable.movable.Opened)
                {
                    _interactable.hasInteract = false;
                }
                else if (!_interactable.hasInteract && !_interactable.movable.Opened)
                {
                    _interactable.hasInteract = true;
                }
                pressed = true;
            }
        }

        else if (_interactable.typeInteract == InteractableObject.TypeInteract.Collectable)
        {
            
        }
        
        if (buttonGrabPinch.GetStateUp(rightControllerPose.inputSource))
        {
            pressed = false;
        }
    }
    
    private void EnableCurrentOutline()
    {
        print("outlined " + _interactable.name);

        if (_interactable.typeInteract == InteractableObject.TypeInteract.Collectable)
        {
            
            objectInfo.SetActive(true);
            textName.text = _interactable.objectName;
            textPrice.text = $"{_interactable.objectPrice} P";
            
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
