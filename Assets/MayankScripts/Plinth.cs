using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plinth : MonoBehaviour
{

    Useable myUseable;
    public TextMeshProUGUI myLabel;
    public Transform spotForItem;

    public GameObject cage;

    public float secondsToLock;

    private void OnEnable()
    {
        References.plinths.Add(this);
    }

    private void OnDisable()
    {
        References.plinths.Remove(this);
    }


    public void AssignItem(GameObject item)
    {
  
        if (myUseable == null)
        {
       
        }
 

    }

    private void Update()
    {

        if (myUseable != null && myUseable.enabled == false)
        {
           
            myUseable = null;
        }

        if (secondsToLock > 0 && References.alarmManager.AlarmHasSounded())
        {
            secondsToLock -= Time.deltaTime;
            if (secondsToLock <= 0)
            {
                cage.SetActive(true);
                myLabel.text = "ALARM";
              
                if (myUseable != null && myUseable.enabled)
                {
                    Destroy(myUseable.gameObject);
                }
            }
        }
    }

}
