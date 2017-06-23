using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class EntityStats {
    BaseEntity entity;

    // float because percentage
    float health = 100f;
    public float Health {
        get { return health; }
        set {
            health = value;
            if (health > 100f) {
                health = 100f;
            }
            if (health < 0f) {
                health = 0f;
            }
        }
    }

    int hunger = 100;
    public int Hunger {
        get { return hunger; }
        set {
            hunger = value;
            if (hunger > 100) {
                hunger = 100;
            }
            if (hunger < 0) {
                hunger = 0;
            }
        }
    }

    double money;
    public double Money {
        get;
        set;
    } 

    public EntityStats() {
        Money = 0;
        Hunger = 0;
        Health = 100;
    }
}

