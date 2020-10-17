using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType
    {
        Simple,
        Competent,
        Master
    }

    public bool inFear;
    public bool foundPlayer;
    public EnemyType type;
    public Transform playerPosition = null;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        //if (type != EnemyType.Master) playerPosition = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(type == EnemyType.Master)
        {
            agent.SetDestination(playerPosition.position);
        }else if(type == EnemyType.Competent)
        {
            
        }
        else
        {
            SimpleMonsterUpdate();
        }
        
    }

    void SimpleMonsterUpdate()
    {
        //RaycastHit hit;
        //Physics.Raycast(transform.localPosition, Vector3.forward * 10, out hit);

        //if(hit.collider.tag == "Player")
        //{
        //    print("found player");
        //    playerPosition = hit.collider.gameObject.transform;
        //    agent.SetDestination(playerPosition.position);
        //}


        //Vector3 relative = transform.InverseTransformDirection(playerPosition.position);
        //print(relative);
        //if(relative.z < 10 && relative.z > -10)
        //{
        //    print("found player");
        //    agent.SetDestination(playerPosition.position);
        //}

        //float tanAngleNum = playerPosition.localPosition.y - transform.localPosition.y;
        //float tanAngeDen = playerPosition.localPosition.x - transform.localPosition.x;

        //float tanAngle = t

        float cosAngle = Vector3.Dot((playerPosition.localPosition - transform.localPosition).normalized, transform.forward);

        float degree = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

        if(degree < 15)
        {
            agent.SetDestination(-playerPosition.position);
        }

        print(agent.pathPending);
    }
}
