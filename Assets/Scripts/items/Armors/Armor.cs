using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public Dictionary<string, int> Stats = new Dictionary<string, int>()
    {
        {"HP", 0},
        {"Defense", 0},
        {"Energy", 0},

    };
}
