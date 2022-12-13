using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SpawnPoint : MonoBehaviour
{
    public GameObject[] NPCTypes;

    public static List<PointStayNPC> points = new List<PointStayNPC>();
    
    public int countNPCLimit;
    public int spawnNPCTimer;
    public int debSize;
    public int debSpawnSize;

    public static int countNPC = 0;
    bool isReload;

    #region - Awake/Update -
    private void Awake()
    {
        isReload = true;
        CreateArrPoints();
    }

    void Update()
    {
        debSpawnSize = countNPC;
        debSize = points.Count;
        CreateNPC();
    }
    #endregion

    #region - CreateNPC -
    void CreateNPC()
    {
        if (countNPC < points.Count && countNPC < countNPCLimit && isReload)
        { 
            Instantiate(NPCTypes[Random.Range(0, NPCTypes.Length-1)], transform);
            isReload = false;
            countNPC++;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(spawnNPCTimer);
        isReload = true;
    }
    #endregion

    void CreateArrPoints()
    {
        points = FindObjectsOfType<PointStayNPC>().ToList();  
    }

    //public void Destoy(GameObject obj)
    //{
    //    Destoy(obj);
    //}
}
