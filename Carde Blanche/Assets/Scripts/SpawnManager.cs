using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject[] spawnList;
    private int objectCount;

    private GameObject[] spawnPoints;

    [Range(0.01f, 1f)]
    public float spawnChance;
	// Use this for initialization
	void Start () {
        objectCount = spawnList.Length;

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].transform.position = spawnPoints[i].transform.position.normalized * 5.001f;
            float chance = Random.Range(0.01f, 1f);
            if(chance <= spawnChance)
            {
                int rand = Random.Range(0, objectCount);
                GameObject temp = (GameObject)Instantiate(spawnList[rand], spawnPoints[i].transform.position, Quaternion.identity);
                temp.transform.LookAt(Vector3.zero);
                temp.transform.Rotate(transform.right, -90);
                temp.transform.Rotate(transform.up, Random.Range(0f, 359f));
            }
        }
	}
}
