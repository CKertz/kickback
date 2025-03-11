using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public float timeToTrigger = 2f; 
    private float elapsedTime = 0f;
    private int waveCount = 1;
    
    [SerializeField]
    private GameObject enemyPrefab;

    private Dictionary<int, int> waveCounter = new Dictionary<int, int>
    {
        { 1, 10 },
        { 2, 20 },
        { 3, 30 }
    };

    void Start()
    {
        
    }


    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= timeToTrigger)
        {
            SpawnEnemies(waveCount);
            waveCount++;
            elapsedTime = 0f; 
        }
    }

    void SpawnEnemies(int waveCount)
    {
        Debug.Log("spawning " + waveCounter[waveCount].ToString() + " enemies");
        for ( int i = 0; i < waveCounter[waveCount]; i++ )
        {
            Vector3 spawnPosition = new Vector3 ( 0, 0, 0 );
            var gameobject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("spawned Y pos: " + gameobject.transform.position.y);

        }

        var dirtObject = GameObject.Find("dirt");
        if ( dirtObject != null )
        {
            Vector3 position = dirtObject.transform.position;
            Debug.Log("dirt Y pos: " + position.y);
        }
    }
}
