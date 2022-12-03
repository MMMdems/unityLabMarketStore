using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform cam;
    private void Start()
    {
        //cam = Camera.main.transform;
    }
    
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
