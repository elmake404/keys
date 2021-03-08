using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivation : MonoBehaviour
{
    [SerializeField]
    private float _timeLetteringPromotion = 0.5f;
    private float _timerLetteringPromotion;

    private void Awake()
    {
        _timerLetteringPromotion = _timeLetteringPromotion;
    }
    private void FixedUpdate()
    {
        if (_timerLetteringPromotion<=0)
        {
            _timerLetteringPromotion = _timeLetteringPromotion;
            gameObject.SetActive(false);
        }
        else
        {
            _timerLetteringPromotion -= Time.deltaTime;
        }
    }
}
