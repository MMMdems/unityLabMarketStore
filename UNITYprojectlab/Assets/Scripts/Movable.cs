using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class Movable : MonoBehaviour
{
    [Header("Movable parameters")]
    [SerializeField] private float _animTime = 0.6f;
    [SerializeField]private Vector3 _closePos, _openPos;
    
    private float _timer = 0, _kTime = 1f;

    private InteractableObject _interactableObject;
    
    public bool Opened { get; private set; } = false;

    private void Start()
    {
        _interactableObject = GetComponent<InteractableObject>();
        _closePos = transform.position;
        _openPos = new Vector3(_closePos.x + _openPos.x, _closePos.y + _openPos.y, _closePos.z + _openPos.z);
    }

    private void LateUpdate()
    {
        if (_interactableObject.hasInteract && !Opened)
        {
            _timer += Time.deltaTime;
            if (_timer > _animTime) { _timer = _animTime; }
            _kTime = _timer / _animTime;
            transform.position = Vector3.Slerp(_closePos,_openPos,_kTime);
            if (_kTime == 1f)
            {
                Opened = true;
                _timer = 0;
            }
        }
        else if (!_interactableObject.hasInteract && Opened)
        {
            _timer += Time.deltaTime;
            if (_timer > _animTime) { _timer = _animTime; }
            _kTime = _timer / _animTime;
            transform.position = Vector3.Slerp(_openPos,_closePos,_kTime);
            if (_kTime == 1f)
            {
                Opened = false;
                _timer = 0;
            }
        }
    }
}
