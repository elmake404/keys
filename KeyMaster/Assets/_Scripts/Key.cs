using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private List<Billet> _billetsList;
    private Vector3 _startPosKey;


    [SerializeField]
    private float _keyFeedSpeed; public float KeyFeedSpeed
    { get { return _keyFeedSpeed * Time.deltaTime; } }

    [SerializeField]
    private float _speedTransitionAnotherLink;
    private int _numberLink = 0;
    private bool _isKeyInPosition;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, Grindstone.Istance.transform.position.y, LocaLinkPosition().z);
        _billetsList[_numberLink].IsActivation= true;
        _isKeyInPosition = true;
        _startPosKey = transform.position;
    }

    private void Update()
    {
        if (_isKeyInPosition)
        {
            transform.Translate(Vector3.left * _keyFeedSpeed * Time.deltaTime);

            if (Input.GetMouseButton(0))
            {
                transform.Translate(Vector3.left * _keyFeedSpeed * Time.deltaTime);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine(GoToAnotherLink());
            }
        }
    }
    private IEnumerator GoToAnotherLink()
    {
        _isKeyInPosition=false;
        _billetsList[_numberLink].IsActivation = false;

        if (_numberLink < _billetsList.Count-1)
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
        _billetsList[_numberLink].IsActivation = true;

    }
    private Vector3 LocaLinkPosition()
    {
        return (transform.position - _billetsList[_numberLink].transform.InverseTransformPoint(Grindstone.Istance.transform.position));

    }
    public void WorkpieceWornOut()
    {
        StartCoroutine(GoToAnotherLink());
    }
}
