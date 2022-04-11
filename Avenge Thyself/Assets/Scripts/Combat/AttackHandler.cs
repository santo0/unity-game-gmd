using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public enum Orientation
    {
        Right, Left
    }
    private Attack[] attacks;

    // Start is called before the first frame update
    void Start()
    {
        attacks = GetComponentsInChildren<Attack>();
    }


    void ChangeOrientation(Orientation orientation)
    {
        switch (orientation)
        {
            default:
                break;
            case Orientation.Left:
                break;
            case Orientation.Right:
                break;
        }
    }
}
