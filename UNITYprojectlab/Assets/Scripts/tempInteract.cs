using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempInteract : MonoBehaviour
{
    private RaycastHit _hit;
    [SerializeField] private float interactDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        InteractionWithDialogueWindow();
    }

    //это сеня написал
    private void InteractionWithDialogueWindow()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, interactDistance))
        {
            var answer = _hit.transform.gameObject.GetComponent<Answer>();
            if (answer)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    answer.SetScenario();
                }
            }
            var cashier = _hit.transform.gameObject.GetComponent<Cashier>();
            if (cashier)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    cashier.StartScenario(1);
                }
            }
        }
    }
}
