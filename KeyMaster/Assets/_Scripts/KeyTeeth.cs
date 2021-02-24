using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTeeth : MonoBehaviour
{
    [SerializeField]
    private Billet _billetSuperfluous, _billet;
    [SerializeField]
    private Key _key;

    public bool IsProcessingBillet 
    { get { return _billet.IsProcessing || _billetSuperfluous.IsProcessing; } }
    public void ProngParameters(float sizeBilletSuperfluous)
    {
        float sizeBillet = (_billetSuperfluous.transform.localScale.x + _billet.transform.localScale.x) - sizeBilletSuperfluous;

        _billet.transform.localScale = new Vector3(sizeBillet, _billet.transform.localScale.y, _billet.transform.localScale.z);
        _billetSuperfluous.transform.localScale = new Vector3(sizeBilletSuperfluous, _billetSuperfluous.transform.localScale.y, _billetSuperfluous.transform.localScale.z);
        _billetSuperfluous.transform.localPosition = new Vector3(-sizeBillet, _billetSuperfluous.transform.localPosition.y, _billetSuperfluous.transform.localPosition.z);
    }
    public void ActivationBillets()
    {
        _billetSuperfluous.IsActivation = true;
        _billet.IsActivation = true;
    }
    public void DeactivationBillets()
    {
        _billetSuperfluous.IsActivation = false;
        _billet.IsActivation = false;

        _billetSuperfluous.IsProcessing = false;
        _billet.IsProcessing = false;

        Grindstone.Istance.StopSparks();
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
