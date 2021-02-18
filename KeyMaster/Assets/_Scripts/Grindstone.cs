using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grindstone : MonoBehaviour
{
    public static Grindstone Istance;

    private float _regionRight
    { get { return transform.position.x - transform.localScale.x / 2; } }
    private float _regionLeft
    { get { return transform.position.x + transform.localScale.x / 2; } }
    private void Awake()
    {
        Istance = this;
    }

    public bool RegionGrindstone(float PositionX, out float sawCutSize)
    {
        if (transform.position.x - PositionX > 0)
        {
            sawCutSize = Mathf.Abs(_regionRight - PositionX);
        }
        else
        {
            sawCutSize = Mathf.Abs( PositionX - _regionLeft);
        }

        return (_regionRight < PositionX && PositionX < _regionLeft);
    }
}
