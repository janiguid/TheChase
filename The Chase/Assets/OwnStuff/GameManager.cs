using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private PickUp[] ups;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ups = FindObjectsOfType<PickUp>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3  GetRandomPoint()
    {
        int rand = Random.Range(0, ups.Length);

        return ups[rand].transform.position;
    }
}
