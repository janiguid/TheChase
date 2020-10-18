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

    public bool hasDestination;
    public Vector3 currDestination; 

    [SerializeField] private bool spotted;
    // Start is called before the first frame update
    void Start()
    {
        hasDestination = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPosition.gameObject.activeSelf)
        {
            if (type == EnemyType.Master)
            {
                agent.SetDestination(playerPosition.position);
            }
            else if (type == EnemyType.Competent)
            {

            }
            else
            {
                SimpleMonsterUpdate();
            }
        }


        if(foundPlayer == false)
        {
            Patrol();
        }

        if (agent.remainingDistance < 0.5f)
        {

            hasDestination = false;
            Patrol();
        }
    }

    void SimpleMonsterUpdate()
    {

        float cosAngle = Vector3.Dot((playerPosition.localPosition - transform.localPosition).normalized, transform.forward);

        float degree = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

        if(degree < 15)
        {
            agent.SetDestination(playerPosition.position);
            foundPlayer = true;
        }
        else
        {
            foundPlayer = false;

            Patrol();
        }

    }

    void Patrol()
    {
        if (hasDestination == false)
        {

            currDestination = GameManager.Instance.GetRandomPoint();
            agent.SetDestination(currDestination);
            hasDestination = true;
        }
    }

    public void MarkAsSpotted()
    {
        spotted = true;
        StartCoroutine(SpottedTimer());
    }

    IEnumerator SpottedTimer()
    {
        yield return new WaitForSeconds(5);
        spotted = false;
    }

    public bool HasBeenSpotted()
    {
        return spotted;
    }
}
