using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References 
{

    public static PlayerBehaviour thePlayer;
    public static CanvasBehaviour canvas;
    public static List<EnemySpawner> spawners = new List<EnemySpawner>();
    public static List<EnemyBehaviour> allEnemies = new List<EnemyBehaviour>();
    public static List<Useable> useables = new List<Useable>();
    public static List<Plinth> plinths = new List<Plinth>();
    public static LevelManager levelManager;
    public static AlarmManager alarmManager;
    public static LevelGenerator levelGenerator;


    public static Persistent essentials;



    public static List<NavPoint> navPoints = new List<NavPoint>();

    public static float maxDistanceInALevel = 1000;

    public static LayerMask wallsLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemiesLayer = LayerMask.GetMask("Enemies");

}
