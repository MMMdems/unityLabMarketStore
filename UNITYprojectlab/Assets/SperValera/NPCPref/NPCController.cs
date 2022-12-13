using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;
    Animator NPCAnimator;
    GameObject _point;
    public float DebDistance;
    int pointNumber;
    bool isDieToReady;

    #region - Awake/Update -
    private void Awake()
    {
        NPCAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        CheckFreePoint();
    }

    void Update()
    {
        DebDistance = CheckDistanceToPoint();

        Control();
    }
    #endregion
    
    void Die()
    {
        SpawnPoint.countNPC--;
        Destroy(this.gameObject); 
    }

    void Control()
    {
        if (CheckDistanceToPoint() < 0.4f && NPCAnimator.GetInteger("animBaseInt") == 0)
        {
            if (SpawnPoint.points[pointNumber].isDiePoint && isDieToReady)
            {
                Die();
            } 
            else 
            {
                StayNPC("idel");
                StartCoroutine(CheckNextPoint());
            }
        }
    }

    void CheckFreePoint()
    {
        pointNumber = Random.Range(0, SpawnPoint.points.Count);

        if (!SpawnPoint.points[pointNumber].isDiePoint && SpawnPoint.points[pointNumber].isFree)
        {
            SpawnPoint.points[pointNumber].isFree = false;
            WalkToPoint();
        }
        else if (SpawnPoint.points[pointNumber].isDiePoint && isDieToReady) 
        { 
            WalkToPoint(); 
        } else { CheckFreePoint(); }
    }

    IEnumerator CheckNextPoint()
    {
        yield return new WaitForSeconds(ReloadTime());
        SpawnPoint.points[pointNumber].isFree = true;
        isDieToReady = true;
        CheckFreePoint();
    }

    int ReloadTime()
    {
        return Random.Range(5, 10);
    }

    float  CheckDistanceToPoint()
    {
        return Vector3.Distance(transform.position, _point.transform.position);
    }

    void WalkToPoint()
    {
        StayNPC("walk");
        _point = SpawnPoint.points[pointNumber].gameObject;
        agent.SetDestination(SpawnPoint.points[pointNumber].gameObject.transform.position);
    }

    void StayNPC(string stay)
    {
        switch (stay)
        {
            // Покой
            case "idel":
                
                transform.rotation = Quaternion.Lerp(transform.rotation, _point.transform.rotation, 90);
                
                NPCAnimator.SetInteger("animBaseInt", Random.Range(1,5));
                NPCAnimator.SetInteger("animOtherInt", 0);
            break;

            //Ходьба
            case "walk":
                NPCAnimator.SetInteger("animBaseInt", 0);
                NPCAnimator.SetInteger("animOtherInt", 1);
                break;
        }  
    }
}
