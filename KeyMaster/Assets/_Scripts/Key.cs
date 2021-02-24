using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private List<KeyTeeth> _keyTeethList;
    private Vector3 _startPosKey;
    private Conveyor _conveyor;


    [SerializeField]
    private float _keyFeedSpeed, _keyFeedSpeedProcessing;
    [SerializeField]
    [Range(0,0.9f)]
    private float _minDifferenceBetweenTeeth;
    private int _numberLink = 0;
    private bool _isKeyInPosition,_nextKey;

    private void Update()
    {
        if (_isKeyInPosition)
        {
            //transform.Translate(Vector3.left * _keyFeedSpeed * Time.deltaTime);

            if (Input.GetMouseButton(0))
            {
                transform.Translate(Vector3.left * GetSpeed() * Time.deltaTime);
            }
            else if (Input.GetMouseButtonUp(0) /*&& _keyTeethList[_numberLink].WastedAwaySuperfluous()*/)
            {
                _conveyor.WorkpieceChange();
            }
        }
    }
    //private IEnumerator GoToAnotherLink()
    //{
    //    _isKeyInPosition = false;
    //    _keyTeethList[_numberLink].DeactivationBillets();

    //    if (_numberLink < _keyTeethList.Count - 1)
    //    {
    //        _numberLink++;
    //    }
    //    else
    //    {
    //        yield return null;
    //    }

    //    Vector3 PosLink = transform.position;

    //    while (true)
    //    {
    //        if (transform.position.x != _startPosKey.x)
    //        {
    //            PosLink.x = _startPosKey.x;
    //        }
    //        //else if (transform.position.z != LocaLinkPosition().z)
    //        //{
    //        //    PosLink.z = LocaLinkPosition().z;
    //        //}
    //        else
    //        {
    //            break;
    //        }

    //        transform.position = Vector3.MoveTowards(transform.position, PosLink, _speedTransitionAnotherLink);
    //        yield return new WaitForSeconds(0.02f);
    //    }
    //    _isKeyInPosition = true;
    //    _keyTeethList[_numberLink].ActivationBillets();
    //}
    private void SettingTheSizeOfTheTeeth(float SizeTeeht)
    {
        float SizeOldTeeth = 1;
        for (int i = 0; i < _keyTeethList.Count; i++)
        {
            while (true)
            {
                float SizeTeeth = Random.Range(0.2f, 1.8f);
                if (Mathf.Abs(SizeOldTeeth - SizeTeeth) > _minDifferenceBetweenTeeth)
                {
                    SizeOldTeeth = SizeTeeth;
                    break;
                }
            }
            _keyTeethList[i].ProngParameters(SizeOldTeeth, SizeTeeht);
        }
    }
    private float GetSpeed()
    {
        return _keyTeethList[_numberLink].IsProcessingBillet ? _keyFeedSpeedProcessing : _keyFeedSpeed ;
    }
    public void Initialization(Conveyor conveyor,int namberTeeht,float size)
    {
        _conveyor = conveyor;
        SettingTheSizeOfTheTeeth(size);
        _startPosKey = transform.position;
        namberTeeht = namberTeeht <= 0 ? 5 : namberTeeht;
        namberTeeht = namberTeeht >= _keyTeethList.Count ? _keyTeethList.Count - 1 : namberTeeht;

        int numberOfUnclaimedTeeth = _keyTeethList.Count - namberTeeht;
        for (int i = 0; i < numberOfUnclaimedTeeth; i++)
        {
            _keyTeethList[i].enabled = false;
            _keyTeethList[i].transform.parent.gameObject.SetActive(false);
        }
        _numberLink = numberOfUnclaimedTeeth;
    }
    public bool GetStartPosKey(float speed)
    {
        Vector3 PosLink = transform.position;
        if (Mathf.Abs(transform.position.x - _startPosKey.x) >= 0.01f)
        {
            PosLink.x = _startPosKey.x;
            transform.position = Vector3.MoveTowards(transform.position, PosLink, speed);
            return false;
        }
        else
        {
            if (_nextKey)
                _conveyor.NextKey();
                return true;
        }

    }
    public void ActivationKey()
    {
        _keyTeethList[_numberLink].ActivationBillets();
        _isKeyInPosition = true;
    }
    public void DeactivationOldTeeth()
    {
        _isKeyInPosition = false;
        _keyTeethList[_numberLink].DeactivationBillets();

        if (_numberLink < _keyTeethList.Count - 1)
        {
            _numberLink++;
        }
        else
        {
            _nextKey = true;
        }
    }
    public void ActivationNewTeeth()
    {
        _isKeyInPosition = true;
        _keyTeethList[_numberLink].ActivationBillets();
    }
    public void WorkpieceWornOut()
    {
        _conveyor.WorkpieceChange();
    }
    public KeyTeeth LinkInWork()
    {
        return _keyTeethList[_numberLink];
    }
}
