using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions
{
    public static Directions none = new Directions(new Vector3(0, 0, 0));
    public static Directions left = new Directions(new Vector3(-1, 0, 0));
    public static Directions right = new Directions(new Vector3(1, 0, 0));

    public Vector3 vector;

    private Directions(Vector3 vector)
    {
        this.vector = vector;
    }
}
