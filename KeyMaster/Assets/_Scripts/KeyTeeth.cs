using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTeeth : MonoBehaviour
{
    [SerializeField]
    private Billet _billetSuperfluous, _billet;
    [SerializeField]
    private Key _key;

    public void ActivationBillets()
    {
        _billetSuperfluous.IsActivation = true;
        _billet.IsActivation = true;
    }
    public void DeactivationBillets()
    {
        _billetSuperfluous.IsActivation = false;
        _billet.IsActivation = false;
    }
    public void Erased(Billet billet)
    {
        if (billet == _billetSuperfluous)
        {
            _billetSuperfluous.gameObject.SetActive(false);
        }
        else if (billet == _billet)
        {
            _key.WorkpieceWornOut();
        }
    }
    public bool WastedAwaySuperfluous()
    {
        return _billetSuperfluous.transform.localScale.x <= 0;
    }
}
