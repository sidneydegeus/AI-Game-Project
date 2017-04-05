using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHumanBehaviour {
    Action Eat();
    Action Attack();
    Action Rest();
}
