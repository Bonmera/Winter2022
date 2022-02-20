using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform pathStart;
    [SerializeField]
    private Transform pathEnd;
    [SerializeField]
    private float patrolSpeed = 2f;
    [SerializeField]
    private float chaseSpeed = 6f;
    [SerializeField]
    private float stoppingDistance = 0.1f;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float sensorRadius = 3;
    [SerializeField]
    private Transform sensorPosition;
    [SerializeField]
    private LayerMask layerMask;

    private Transform startPosition;
    public AIDestinationSetter aIDestinationSetter;
    public AIPath aIPath;
    private bool isPatrolling = true;
    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform;
        aIDestinationSetter.target = pathStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrolling)
        {
            aIPath.maxSpeed = patrolSpeed;
            if (Vector2.Distance(transform.position, pathStart.position) < stoppingDistance)
            {
                coroutine =  StartCoroutine(ChangeTarget(pathEnd,1f));
            }
            else if (Vector2.Distance(transform.position, pathEnd.position) < stoppingDistance)
            {
                coroutine =  StartCoroutine(ChangeTarget(pathStart,1f));
            }

            Vector2 direction = ((Vector2)(player.position - sensorPosition.position)).normalized;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)sensorPosition.position, direction, sensorRadius, ~layerMask);
        //    Debug.DrawRay((Vector2)sensorPosition.position, direction * sensorRadius, Color.red);
            
            if (hit.collider && hit.collider.gameObject.tag == "Player")
            {
                isPatrolling = false;
                StopCoroutine(coroutine);
                aIDestinationSetter.target = player;
                aIPath.maxSpeed = chaseSpeed;
            }
        }else if (!isPatrolling)
        {
            Vector2 direction = ((Vector2)(player.position - sensorPosition.position)).normalized;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)sensorPosition.position, direction, sensorRadius, ~layerMask);
        //    Debug.DrawRay((Vector2)sensorPosition.position, direction * sensorRadius, Color.red);
            if (hit.collider && hit.collider.gameObject.tag == "Player")
            {
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(MaintainChase(5f));
                aIPath.maxSpeed = chaseSpeed;
            }
        }

        if (Vector2.Distance(transform.position, player.position) <= 1f )
        {
            CaughtPlayer();
        }

    }

    public void CaughtPlayer()
    {
        Debug.Log("Caught Player");
    }

    IEnumerator MaintainChase(float time)
    {
        yield return new WaitForSeconds(time);
        aIDestinationSetter.target = pathStart;
        isPatrolling = true;
    }
    
    IEnumerator ChangeTarget(Transform target,float time = 1f) {
        yield return new WaitForSeconds(time);
        aIDestinationSetter.target = target;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,1f);
      //  Gizmos.DrawRay(sensorPosition.position, direction*sensorRadius);
    }

}
