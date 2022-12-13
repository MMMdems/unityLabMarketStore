using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    //сеня написал
    public bool bought;
    private Vector3 startPos;
    private Quaternion startRot;
    // все, дальше не сеня
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
        //сеня написал
        startPos = transform.position;
        startRot = transform.rotation;
        // все, дальше не сеня
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

    //сеня написал
    public IEnumerator ReturnProduct()
    {
        if(!bought)
        {
            bought = true;
            yield return new WaitForSeconds(1);
            transform.SetPositionAndRotation(startPos, startRot);
            bought = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor") StartCoroutine(ReturnProduct());
    }
    // все, дальше не сеня
}
