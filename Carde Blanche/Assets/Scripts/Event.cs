using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event {
    private int id;
    public int Id { get { return id; } set { id = value; } }

    private string name;
    public string Name { get { return name; } set { name = value; } }

    /// <summary>
    /// Description of the event
    /// </summary>
    private string desc;
    public string Desc { get { return desc; } set { desc = value; } }


    /// <summary>
    /// The type that the event will be. Tech or Nature
    /// </summary>
    private string affinity;
    public string Affinity { get { return affinity; } set { affinity = value; } }
    public Event(int id, string name, string description, string affinity)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = description;
        this.Affinity = affinity;
    }
 };

