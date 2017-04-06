using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanBehaviour {
    void Eat();
    void Purchase(Item item);
    void Attack();
    void Rest();
}
