using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IBoundary
{
    public bool IsObstacle
    {
        get { return true; }
    }
}