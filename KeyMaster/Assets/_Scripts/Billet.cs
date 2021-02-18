using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billet : MonoBehaviour
{
    private Grindstone _grindstone;
    [SerializeField]
    private Transform _meshBillet;
    [SerializeField]
    private KeyTeeth _keyTeeth;
    private float _scaleMeshBillet
    { get { return _meshBillet.localScale.x * transform.localScale.x; } }
    [HideInInspector]
    public bool IsActivation;

    private void Start()
    {
        _grindstone = Grindstone.Istance;
    }

    private void LateUpdate()
    {
        if (IsActivation)
        {
            if (_grindstone.RegionGrindstone(transform.position.x + _scaleMeshBillet, out float cutSize))
            {
                Sharpen(cutSize);
            }
        }
    }
    private void Sharpen(float sawCutSize)
    {
        Vector3 ScaleBillet = transform.localScale;

        ScaleBillet.x -= sawCutSize;

        transform.localScale = ScaleBillet;

        if (transform.localScale.x < 0)
        {
            ScaleBillet.x = 0;

            transform.localScale = ScaleBillet;

            _keyTeeth.Erased(this);
        }
    }
}
