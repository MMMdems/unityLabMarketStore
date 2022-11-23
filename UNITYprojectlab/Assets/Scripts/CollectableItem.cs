using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Rigidbody))]
public class CollectableItem : MonoBehaviour
{
    private InteractableObject _interactableObject;
    
    [Header("Hand Parameters")] 
    [SerializeField] private Transform hand;
    [SerializeField] private KeyCode DropKey;
    
    [Header("Physics parameters")]
    [SerializeField] private float dropForce = 2;
    private Rigidbody _rb;

    [Header("Animation parameters")]
    [SerializeField] private float animTime = 0.3f;
    private float _timer = 0, _kTime = 1f;
    private Vector3 _startPos;

    public bool InHand = false;

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        _rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        
        if (!InHand && !_interactableObject.hasInteract && _startPos != transform.position) { _startPos = transform.position; }
        
        if (_interactableObject.hasInteract && !InHand )
        {
            //_interactableObject.hasInteract = false;
            transform.rotation = Quaternion.LookRotation(hand.forward);
            
            SwitchRigidbody(false);
            
            _timer += Time.deltaTime;
            if (_timer > animTime) { _timer = animTime; }
            _kTime = _timer / animTime;
            transform.position = Vector3.Slerp(_startPos, hand.position, _kTime);
            if (_kTime == 1f)
            {
                transform.parent = hand;
                InHand = true;
                _timer = 0;
            }
        }
        
        if (Input.GetKeyDown(DropKey) && _interactableObject.hasInteract && InHand)
        {
            _interactableObject.hasInteract = false;
        }

        else if (!_interactableObject.hasInteract && InHand)
        {
            SwitchRigidbody(true);
            
            InHand = false;
            transform.parent = null;
            _rb.AddForce(hand.forward * dropForce, ForceMode.Impulse);
        }
        
    }

    private void SwitchRigidbody(bool isEnabled)
    {
        if (isEnabled)
        {
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }
        else if (!isEnabled)
        {
            _rb.useGravity = false;
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
