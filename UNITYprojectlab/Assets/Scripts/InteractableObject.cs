using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    public string objectName;
    public float objectPrice = 1f;
    
    public enum TypeInteract
    {
        Rotatable, Collectable, Movable
    }

    public TypeInteract typeInteract;
    
    public Rotatable rotatable;
    public CollectableItem collectable;
    public Movable movable;
    
    public bool hasInteract = false;

    private Outline _outline;
    void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.OutlineWidth = 0;

        if (typeInteract == TypeInteract.Rotatable)
        {
            rotatable = GetComponent<Rotatable>();
        }

        else if (typeInteract == TypeInteract.Collectable)
        {
            collectable = GetComponent<CollectableItem>();
        }
        else if (typeInteract == TypeInteract.Movable)
        {
            movable = GetComponent<Movable>();
        }
    }

    public void OutlineOn()
    {
        _outline.OutlineWidth = 4;
    }

    public void OutlineOff()
    {
        _outline.OutlineWidth = 0;
    }
}
