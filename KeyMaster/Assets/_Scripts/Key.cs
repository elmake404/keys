using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private List<KeyTeeth> _keyTeethList;
    private Vector3 _startPosKey;


    [SerializeField]
    private float _keyFeedSpeed;
    //public float KeyFeedSpeed
    //{ get { return _keyFeedSpeed * Time.deltaTime; } }

    [SerializeField]
    private float _speedTransitionAnotherLink;
    [SerializeField]
    [Range(0,0.9f)]
    private float _minDifferenceBetweenTeeth;
    private int _numberLink = 0;
    private bool _isKeyInPosition;

    private void Start()
    {
        SettingTheSizeOfTheTeeth();
        transform.position = new Vector3(transform.position.x, Grindstone.Istance.transform.position.y, LocaLinkPosition().z);
        _keyTeethList[_numberLink].ActivationBillets();
        _isKeyInPosition = true;
        _startPosKey = transform.position;
    }

    private void Update()
    {
        if (_isKeyInPosition)
        {
            //transform.Translate(Vector3.left * _keyFeedSpeed * Time.deltaTime);

            if (Input.GetMouseButton(0))
            {
                transform.Translate(Vector3.left * _keyFeedSpeed * Time.deltaTime);
            }
            else if (Input.GetMouseButtonUp(0) /*&& _keyTeethList[_numberLink].WastedAwaySuperfluous()*/)
            {
                StartCoroutine(GoToAnotherLink());
            }
        }
    }
    private IEnumerator GoToAnotherLink()
    {
        _isKeyInPosition = false;
        _keyTeethList[_numberLink].DeactivationBillets();

        if (_numberLink < _keyTeethList.Count - 1)
        {
            _numberLink++;
        }
        else
        {
            yield return null;
        }

        Vector3 PosLink = transform.position;

        while (true)
        {
            if (transform.position.x != _startPosKey.x)
            {
                PosLink.x = _startPosKey.x;
            }
            else if (transform.position.z != LocaLinkPosition().z)
            {
                PosLink.z = LocaLinkPosition().z;
            }
            else
            {
                break;
            }

            transform.position = Vector3.MoveTowards(transform.position, PosLink, _speedTransitionAnotherLink);
            yield return new WaitForSeconds(0.02f);
        }
        _isKeyInPosition = true;
        _keyTeethList[_numberLink].ActivationBillets();

    }
    private void SettingTheSizeOfTheTeeth()
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
            _keyTeethList[i].ProngParameters(SizeOldTeeth);
        }
    }
    private Vector3 LocaLinkPosition()
    {
        return (transform.position - _keyTeethList[_numberLink].transform.InverseTransformPoint(Grindstone.Istance.transform.position));

    }
    public void WorkpieceWornOut()
    {
        StartCoroutine(GoToAnotherLink());
    }
}
