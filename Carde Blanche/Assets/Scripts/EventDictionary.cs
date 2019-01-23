using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class EventDictionary{

    Dictionary<int, Event> events = new Dictionary<int, Event>();
    public Dictionary<int, Event> Events{ get { return events; } }

    // Use this for initialization
    public EventDictionary () {
        ReadFile("Assets/Externals/events.txt");   
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
        reader.Close();      
    }
}
