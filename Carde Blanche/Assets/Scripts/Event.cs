using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event {
    int Id { get { return Id; } set { Id = value; } }
    string Name { get { return Name; } set { Name = value; } }
    string Desc { get { return Desc; } set { Desc = value; } }
    string Affinity { get { return Affinity; } set { Affinity = value; } }
    public Event(int id, string name, string description, string affinity)
    {
        this.Id = id;
        this.Name = name;
        this.Desc = description;
        this.Affinity = affinity;
    }
 };

