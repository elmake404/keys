using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billet : MonoBehaviour
{
    private Grindstone _grindstone;
    [SerializeField]
    private Key _key;

    private void Start()
    {
        _grindstone = Grindstone.Istance;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _grindstone.gameObject)
        {
            Sharpen();
        }
    }

    private void Sharpen()
    {
        if (transform.localScale.x > 0)
        {
            Vector3 ScaleBillet = transform.localScale;

            ScaleBillet.x -= _key.KeyFeedSpeed;

            transform.localScale = ScaleBillet;
        }
        else
        {
            _key.WorkpieceWornOut();
        }
    }
}
