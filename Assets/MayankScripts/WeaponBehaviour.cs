using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float accuracy;
    public float secondsBetweenShots;
    public float numberOfProjectiles;
    float secondsSinceLastShot;
    public AudioSource audioSource;
    public float kickAmount;
    public int ammo;


    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        secondsSinceLastShot += Time.deltaTime;

    }

    public void BePickedUpByPlayer()
    {
 
        transform.position = References.thePlayer.transform.position;
        transform.rotation = References.thePlayer.transform.rotation;
   
        transform.SetParent(References.thePlayer.transform);

        References.alarmManager.RaiseAlertLevel();
        References.thePlayer.PickUpWeapon(this);
    }

    public void Fire(Vector3 targetPosition)
    {
        if (secondsSinceLastShot >= secondsBetweenShots && ammo > 0)
        {
         
            References.alarmManager.SoundTheAlarm();
            audioSource.Play();
      

            for (
                int iterationCount = 0;
                iterationCount < numberOfProjectiles; 
                iterationCount++ 
            )
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
              
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;
                Vector3 inaccuratePosition = targetPosition;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);
                newBullet.transform.LookAt(inaccuratePosition);
                secondsSinceLastShot = 0;
              
            }

            ammo--;

        }

    }

    public void Drop()
    {
        transform.parent = null;
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        GetComponent<Useable>().enabled = true;
    }

}
