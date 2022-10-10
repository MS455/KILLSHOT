using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{


    public string nextLevelName;
    public int alarmLevels;

    public List<GameObject> possibleChunkPrefabs;
    public List<GameObject> weaponPrefabs;
    public GameObject antiquePrefab;
    public GameObject guardPrefab;

    public float fractionOfPlinthsToHaveAntiques;
    public int numberOfGuardsToCreate;
    public int numberOfSpawnersToCreate;

    private void Awake()
    {
        References.levelGenerator = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //Get a random chunk type
            int randomChunkIndex = Random.Range(0, possibleChunkPrefabs.Count);
            GameObject randomChunkType = possibleChunkPrefabs[randomChunkIndex];
            Vector3 spawnPosition = transform.position + new Vector3(i * 15, 0, 0);
            Instantiate(randomChunkType, spawnPosition, Quaternion.identity);
            possibleChunkPrefabs.Remove(randomChunkType);
        }

        int numberOfThingsToPlace = References.plinths.Count;
        int numberOfAntiquesToPlace = Mathf.RoundToInt(numberOfThingsToPlace * fractionOfPlinthsToHaveAntiques);

        //For each plinth in the level
        foreach (Plinth plinth in References.plinths)
        {
            GameObject thingToCreate;

            float chanceOfAntique = numberOfAntiquesToPlace / numberOfThingsToPlace;
            if (Random.value < chanceOfAntique)
            {
                //Place an antique
                thingToCreate = antiquePrefab;
                numberOfAntiquesToPlace--;

            } else
            {
                //Place a weapon
                int randomThingIndex = Random.Range(0, weaponPrefabs.Count);
                thingToCreate = weaponPrefabs[randomThingIndex];
            }

            numberOfThingsToPlace--;

            //Instantiate one of those
            GameObject newThing = Instantiate(thingToCreate);

            //Assign it to this plinth
            plinth.AssignItem(newThing);

        }

        List<NavPoint> possibleSpots = new List<NavPoint>();
        float minDistanceFromPlayer = 12;
        foreach (NavPoint nav in References.navPoints)
        {
            //Is it far enough from the player?
            if (Vector3.Distance(nav.transform.position, References.thePlayer.transform.position) >= minDistanceFromPlayer)
            {
                possibleSpots.Add(nav);
            }
        }

        for (int i = 0; i < numberOfGuardsToCreate; i++)
        {
            if (possibleSpots.Count == 0) { break; }
            int randomIndex = Random.Range(0, possibleSpots.Count);
            NavPoint spotToSpawnAt = possibleSpots[randomIndex];
            Instantiate(guardPrefab, spotToSpawnAt.transform.position, Quaternion.identity);
            possibleSpots.Remove(spotToSpawnAt);

        }

        while (References.spawners.Count > numberOfSpawnersToCreate)
        {
            int randomIndex = Random.Range(0, References.spawners.Count);
            Destroy(References.spawners[randomIndex].gameObject);
        }

        References.alarmManager.SetUpLevel(alarmLevels);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
