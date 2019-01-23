using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class EventDictionary : MonoBehaviour {
    Dictionary<int, Event> events = new Dictionary<int, Event>();
    // Use this for initialization
    void Start () {
        Debug.Log("Run");
        ReadFile("Assets/Externals/events.txt");
        
        Debug.Log(events[0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ReadFile(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        int id;
        string name;
        string description;
        string affinity;
        while (!reader.EndOfStream)
        {
            id = Convert.ToInt32(reader.ReadLine());
            affinity = reader.ReadLine();
            name = reader.ReadLine();
            description = reader.ReadLine();
            
            events.Add(id, new Event(id, name, description,affinity));
            reader.ReadLine();
        }
    }
}
