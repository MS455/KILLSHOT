using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed;
    public Rigidbody ourRigidBody;
    public NavMeshAgent navAgent;

    /* This is our parent class for all enemies - code here should be stuff they all use */

    //Protected: this function can be used by our children and us, but no-one else. We need to put this in front of everything our children might want to use.
    //We probably don't want anything to be 'private'
    //Virtual: this can be over-ridden / changed by our children - but if they don't override it, they just use our version. 'virtually all enemies use this'
    //Void: this what the function gives us back when we call it - in this case nothing

    protected void OnEnable()
    {
        References.allEnemies.Add(this);
    }

    protected void OnDisable()
    {
        References.allEnemies.Remove(this);
    }


    protected virtual void Start()
    {
        ourRigidBody = GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ChasePlayer();
    }

    protected void ChasePlayer()
    {
        if (References.thePlayer != null)
        {
            navAgent.destination = References.thePlayer.transform.position;
            /*
            Vector3 playerPosition = References.thePlayer.transform.position;
            Vector3 vectorToPlayer = playerPosition - transform.position;
            ourRigidBody.velocity = vectorToPlayer.normalized * speed;
            Vector3 playerPositionAtOurHeight = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
            transform.LookAt(playerPositionAtOurHeight);
            */
        }
    }

}
