using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : EnemyBehaviour
{

    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    public Light myLight;
    public float turnSpeed;
    public WeaponBehaviour myWeapon;
    public float reactionTime;
    float secondsSeeingPlayer;

    /* This is specifically guard behaviour - anything all enemies do will be handled by our parent, enemybehaviour */

    //Protected: we have to give it the same 'access' type as we did in the parent - just use protected 
    //Override: we know our parent has a version of this function, and we're deciding to over-ride it - use our version INSTEAD
    //base.Start() -- run our parent's version of this
    
    protected override void Start()
    {
        base.Start();
        alerted = false;
        GoToRandomNavPoint();
        secondsSeeingPlayer = 0;
    }

    void GoToRandomNavPoint()
    {
        //When we give Random.Range float numbers, they can go all the way up to the max
        //But when we give it integers, it will never choose the max
            int randomNavPointIndex = Random.Range(0, References.navPoints.Count);
            navAgent.destination = References.navPoints[randomNavPointIndex].transform.position;

    }


    protected bool CanSeePlayer()
    {

        if (References.thePlayer == null)
        {
            return false;
        }

        Vector3 playerPosition = References.thePlayer.transform.position;
        Vector3 vectorToPlayer = playerPosition - transform.position;

        if (Physics.Raycast(
            transform.position,
            vectorToPlayer, 
            vectorToPlayer.magnitude, 
            References.wallsLayer
        ))
        {
            //The ray DID hit a wall BEFORE it hit the player - we CAN'T see the player
            return false;
        } else
        {
            //The ray hit no walls before it reached the player
            return true;
        }

    }

    public void KnockoutAttempt()
    {
        if (References.alarmManager.AlarmHasSounded() == false)
        {
            GetComponent<HealthSystem>().KillMe();
            References.alarmManager.RaiseAlertLevel();
        }
    }

    protected Vector3 PlayerPosition()
    {
        return References.thePlayer.transform.position;
    }

   
    protected override void Update()
    {

        if (References.alarmManager.AlarmHasSounded())
        {
            alerted = true;
        }

        if (References.thePlayer != null)
        {
            Vector3 vectorToPlayer = PlayerPosition() - transform.position;
            myLight.color = Color.white;

            if (alerted)
            {
                myLight.color = Color.red;
                ChasePlayer();
                if (CanSeePlayer())
                {
                    secondsSeeingPlayer += Time.deltaTime;
                    //Look at player even before we're ready to fire, to warn them
                    transform.LookAt(PlayerPosition());
                    if (secondsSeeingPlayer >= reactionTime)
                    {
                        myWeapon.Fire(PlayerPosition());
                    }
                }
                else
                {
                    //Not seeing player
                    secondsSeeingPlayer = 0;
                }
            }
            else
            {

                if (navAgent.remainingDistance < 0.5f)
                {
                    GoToRandomNavPoint();
                }

                //ourRigidBody.velocity = transform.forward * speed;

                //Checking if we can see the player
                if (Vector3.Distance(transform.position, PlayerPosition()) <= visionRange)
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)
                    {
                        //Raycast(Starting point, direction, distance to check, layermask that only includes things we care about hitting)
                        //This returns true IF we hit something on that layer, when we shoot a laser in that direction for that distance
                        if (Physics.Raycast(transform.position, vectorToPlayer, vectorToPlayer.magnitude, References.wallsLayer) == false)
                        {
                            //First time we see the player
                            alerted = true;
                            References.alarmManager.SoundTheAlarm();
                        }
                    }
                }

            }
        }

    }

}
