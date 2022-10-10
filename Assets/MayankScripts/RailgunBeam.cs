using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunBeam : BulletBehaviour
{

    public LineRenderer myBeam;

    void Start()
    {
      
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, References.maxDistanceInALevel, References.wallsLayer);
        float distanceToWall = hitInfo.distance;

        float beamThickness = 0.3f;
        RaycastHit[] listOfHitInfo = Physics.SphereCastAll(transform.position, beamThickness, transform.forward, distanceToWall, References.enemiesLayer);
        foreach (RaycastHit enemyHitInfo in listOfHitInfo) {
            HealthSystem theirHealthSystem = enemyHitInfo.collider.GetComponentInParent<HealthSystem>();
            if (theirHealthSystem != null)
            {
                theirHealthSystem.TakeDamage(damage);
            }
        }

        myBeam.SetPosition(0, transform.position);
        myBeam.SetPosition(1, hitInfo.point);


    }

    protected override void Update()
    {
        base.Update();
      
        myBeam.endColor = Color.Lerp(myBeam.endColor, Color.clear, 0.05f);
    }

}
