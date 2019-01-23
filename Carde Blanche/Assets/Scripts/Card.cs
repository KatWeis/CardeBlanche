using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int id;
    public int Id { get { return id; } set { id = value; } }

    private string name;
    public string Name { get { return name; } set { name = value; } }

    public Card(int id, string name)
    {
        this.Id = id;
        this.Name = name;
    }
};
