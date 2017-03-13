using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX, gridY;

    public int gCost; // weight of node next to current node (10 horizontal, vertical and 14 diagonally)
    public int hCost; // expected weight to target position
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost { // summed up weight
        get {
            return gCost + hCost;
        }
    }
}
