using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

  

    public float speed; //'float' is short for floating point number, which is basically just a normal number

    public WeaponBehaviour mainWeapon;
    public WeaponBehaviour secondaryWeapon;

    public int score;

    private void Awake()
    {
        References.thePlayer = this;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    
    }

  
    void Update()
    {

   
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);

     
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

      
        if (mainWeapon != null && Input.GetButton("Fire1"))
        {
           
            mainWeapon.Fire(cursorPosition);
        }


     
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchWeapons();
        }

        if (Input.GetButtonDown("Use"))
        {

       
            Useable nearestUseableSoFar = null;
            float nearestDistance = 2; 
            foreach (Useable thisUseable in References.useables)
            {
               
                float thisDistance = Vector3.Distance(transform.position, thisUseable.transform.position);
               
                if (thisDistance <= nearestDistance)
                {
                   
                    nearestUseableSoFar = thisUseable;
                    nearestDistance = thisDistance;
                }
            }

            if (nearestUseableSoFar != null)
            {
                nearestUseableSoFar.Use();
            }

        }

    }

    public void PickUpWeapon(WeaponBehaviour weapon)
    {
        if (mainWeapon == null)
        {
          
            SetAsMainWeapon(weapon);
        } else
        {
          
            if (secondaryWeapon == null)
            {
               
                SetAsSecondaryWeapon(weapon);
            } else
            {
              
                mainWeapon.Drop();
              
                SetAsMainWeapon(weapon);

            }
        }
    }

    void SetAsMainWeapon(WeaponBehaviour weapon)
    {
        mainWeapon = weapon;
        References.canvas.mainWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(true);
    }
    void SetAsSecondaryWeapon(WeaponBehaviour weapon)
    {
        secondaryWeapon = weapon;
        References.canvas.secondaryWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(false);
    }

    private void SwitchWeapons()
    {

        if (mainWeapon != null && secondaryWeapon != null)
        {
            WeaponBehaviour oldMainWeapon = mainWeapon;
            SetAsMainWeapon(secondaryWeapon);
            SetAsSecondaryWeapon(oldMainWeapon);
        }
   
    }

}
