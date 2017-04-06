using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Food : Item {

    public int HungerValue { get; private set; }

    public Food() {
        Name = "Food";
        Cost = 50;
        HungerValue = 5;
    }
}

