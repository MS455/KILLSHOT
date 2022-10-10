using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class CanvasBehaviour : MonoBehaviour
{

   // public SceneAsset firstScene;
   

  //  public TextMeshProUGUI scoreText;

    public WeaponPanel mainWeaponPanel;
    public WeaponPanel secondaryWeaponPanel;

    // Start is called before the first frame update
    void Awake()
    {
        References.canvas = this;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    


   

   

    
    public void Quit()
    {
        Application.Quit();
    }

}
