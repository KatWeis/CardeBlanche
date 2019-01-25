using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject[] spawnList;
    private int objectCount;

    private GameObject[] spawnPoints;
	// Use this for initialization
	void Start () {
        objectCount = spawnList.Length;

        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            int rand = Random.Range(0, objectCount);
            GameObject temp = (GameObject)Instantiate(spawnList[rand], spawnPoints[i].transform.position, Quaternion.identity);
            temp.transform.LookAt(Vector3.zero);
            temp.transform.Rotate(transform.right, -90);
        }
	}
}
