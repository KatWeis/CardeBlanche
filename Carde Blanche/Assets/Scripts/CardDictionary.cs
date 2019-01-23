using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class CardDictionary
{
    Dictionary<int, Card> cards = new Dictionary<int, Card>();
    public Dictionary<int, Card> Cards { get { return cards; } }


    // Use this for initialization
    public CardDictionary()
    {
        ReadFile("Assets/Externals/cards.txt");
    }


    private void ReadFile(string filePath)
    {
        StreamReader reader = new StreamReader(filePath);
        int id;
        string name;
        while (!reader.EndOfStream)
        {
            id = Convert.ToInt32(reader.ReadLine());
            name = reader.ReadLine();

            cards.Add(id, new Card(id, name));
            reader.ReadLine();
        }
        reader.Close();
    }
}
