using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region StaticComponent
    public static bool IsStartGeme, IsGameFlow, IsWinGame, IsLoseGame;
    #endregion
    public static CanvasManager instanceСanvasManager;

    [SerializeField]
    private GameObject _menuUI, _inGameUI, _wimIU, _lostUI;
    [SerializeField]
    private Text _scoreText;
    private int _pointsNamber = 0, _scoreNamber = 0, _bonusPoint;

    private void Awake()
    {
        instanceСanvasManager = this;
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {

        _scoreText.text = 0.ToString();
        if (!IsStartGeme)
        {
            _menuUI.SetActive(true);
            IsGameFlow = true;
        }
    }

    private void Update()
    {
        if (!_inGameUI.activeSelf && IsStartGeme)
        {
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
        }
        if (!_wimIU.activeSelf && IsWinGame)
        {
            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
        }
        if (!_lostUI.activeSelf && IsLoseGame)
        {
            _inGameUI.SetActive(false);
            _lostUI.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (_pointsNamber > _scoreNamber)
        {
            _scoreNamber+=5;
            _scoreText.text = _scoreNamber.ToString();
        }
    }
    //SizeBillet ==( StartSize -currentSize) sizeBilletSuperfluous == currentSizes
    public void AddPoint(float sizeBilletSuperfluous, float sizeBillet, bool noSawCut)
    {
        if (!noSawCut)
        {
            if (sizeBilletSuperfluous < LevelCharacteristicsManager.Perfect && sizeBillet < LevelCharacteristicsManager.Perfect)
            {
                _pointsNamber += LevelCharacteristicsManager.PerfectAddPoint + _bonusPoint;
                _bonusPoint += LevelCharacteristicsManager.BonusPoints;
            }
            else if (sizeBilletSuperfluous < LevelCharacteristicsManager.Fine && sizeBillet < LevelCharacteristicsManager.Fine)
            {
                _pointsNamber += LevelCharacteristicsManager.FineAddPoint;
                _bonusPoint = 0;
            }
            else
            {
                _pointsNamber += LevelCharacteristicsManager.BadlyAddPoint;
                _bonusPoint = 0;
            }
        }
        else
        {
            _bonusPoint = 0;
        }
    }
}
