using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanBehaviour {
    Action Eat();
    Action Purchase();
    Action Attack();
    Action Rest();
}
