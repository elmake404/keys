﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct KeyCharacteristics
{
    public int ModelNamber;
    public int NaberTeeht;
    [Range(0.1f, 1f)]
    public float SizeTeeth;
}

public class Conveyor : MonoBehaviour
{
    //[SerializeField]
    private KeyCharacteristics[] _quantityKeys;

    private Key[] _keys;
    [SerializeField]
    private Key _keyPrefab;

    private int _namberKey;
    [SerializeField]
    private float _speedTransitionAnotherLink, _speedTransitionAnotherKey, _distanceBetweenKeys;
    private bool _isNewKey;

    private void Start()
    {
        _quantityKeys = LevelCharacteristicsManager.LevelKeyCharacteristics[0].keyCharacteristics.ToArray();
        SpawnKey();
    }
    private IEnumerator GoToAnotherLink()
    {
        _keys[_namberKey].DeactivationOldTeeth();
        Vector3 PosLink = transform.position;

        while (true)
        {
            if (_keys[_namberKey].GetStartPosKey(_speedTransitionAnotherLink))
            {
                PosLink.z = LocaLinkPosition().z;
                if (!_isNewKey) transform.position = Vector3.MoveTowards(transform.position, PosLink, _speedTransitionAnotherLink);
                else transform.position = Vector3.MoveTowards(transform.position, PosLink, _speedTransitionAnotherKey);

                if (Mathf.Abs(transform.position.z - LocaLinkPosition().z) < 0.01)
                {
                    _isNewKey = false;

                    break;
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
        _keys[_namberKey].ActivationNewTeeth();
    }
    private Vector3 LocaLinkPosition()
    {
        return (transform.position - _keys[_namberKey].LinkInWork().transform.InverseTransformPoint(Grindstone.Istance.transform.position));
    }
    public void NextKey()
    {
        if (_namberKey < _keys.Length - 1)
        {
            _namberKey++;
            _isNewKey = true;
        }
        else
        {
            Debug.Log("Game Win");
        }
    }
    private void SpawnKey()
    {
        _keys = new Key[_quantityKeys.Length];

        Vector3 spawnPosition = transform.position;

        for (int i = 0; i < _keys.Length; i++)
        {
            _keys[i] = Instantiate(_keyPrefab, spawnPosition, transform.rotation);
            _keys[i].Initialization(this, _quantityKeys[i].NaberTeeht, _quantityKeys[i].SizeTeeth, _quantityKeys[i].ModelNamber);
            _keys[i].transform.SetParent(transform);
            spawnPosition.z += _distanceBetweenKeys;
        }
        _keys[0].ActivationKey();

        transform.position = new Vector3(transform.position.x, Grindstone.Istance.transform.position.y, LocaLinkPosition().z);
    }
    public void WorkpieceChange()
    {
        StartCoroutine(GoToAnotherLink());
    }
}
