using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{


    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        References.navPoints.Add(this);
    }

    private void OnDisable()
    {
        References.navPoints.Remove(this);
    }


    void Update()
    {
        
    }
}
