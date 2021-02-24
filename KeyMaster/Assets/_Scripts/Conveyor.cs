using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private Key[] _keys;
    [SerializeField]
    private Key _keyPrefab;

    private int _namberKey;
    [SerializeField]
    private int _quantityKeys;
    [SerializeField]
    private float _speedTransitionAnotherLink, _distanceBetweenKeys;

    private void Start()
    {
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
                transform.position = Vector3.MoveTowards(transform.position, PosLink, _speedTransitionAnotherLink);
                if (Mathf.Abs(transform.position.z - LocaLinkPosition().z)<0.01)
                {
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
        if (_namberKey<_keys.Length-1)
        {
            _namberKey++;

        }
        else
        {
            Debug.Log("Game Win");
        }
    }
    private void SpawnKey()
    {
        _keys = new Key[_quantityKeys];
        Vector3 spawnPosition = transform.position;
        for (int i = 0; i < _keys.Length; i++)
        {
            _keys[i] = Instantiate(_keyPrefab,spawnPosition,transform.rotation);
            _keys[i].Initialization(this);
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
