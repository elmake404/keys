using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grindstone : MonoBehaviour
{
    public static Grindstone Istance;

    [SerializeField]
    private ParticleSystem _sparksPS;
    [SerializeField]
    private Transform _grindstoneMesh;

    [SerializeField]
    private float _speedRotation;

    private float _regionRight
    { get { return transform.position.x - transform.localScale.x / 2; } }
    private float _regionLeft
    { get { return transform.position.x + transform.localScale.x / 2; } }
    private void Awake()
    {
        Istance = this;
        StartCoroutine(RotationGrindstone());
    }
    private IEnumerator RotationGrindstone()
    {
        while (true)
        {
            _grindstoneMesh.Rotate(Vector3.forward * _speedRotation);
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void PlaySparks()
    {
        _sparksPS.Play();
    }
    public void StopSparks()
    {
        _sparksPS.Stop();
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
