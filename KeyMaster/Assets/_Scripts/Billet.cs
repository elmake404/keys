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
    private float _startScale;
    [HideInInspector]
    public bool IsActivation , IsProcessing;

    private void Start()
    {
        _grindstone = Grindstone.Istance;
        _startScale = transform.localScale.x;
    }

    private void LateUpdate()
    {
        if (IsActivation)
        {
            if (_grindstone.RegionGrindstone(transform.position.x + _scaleMeshBillet, out float cutSize))
            {
                if (!IsProcessing)
                {
                    ActivationEffect();
                }
                Sharpen(cutSize);
            }
            else
            {
                if (IsProcessing)
                {
                    DeactivationEffect();
                }
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
    private void ActivationEffect()
    {
        IsProcessing = true;
        _grindstone.PlaySparks();
    }
    public void DeactivationEffect()
    {
        _grindstone.StopSparks();
        IsProcessing = false;
    }
    public float GetDistance()
    {
        return _startScale - transform.localScale.x;
    }
}
