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
    private Text _scoreText, _scoreTextWin, _levelNamberLevel,_textGood, _textAmazing;

    private int _pointsNamber = 0, _scoreNamber = 0, _bonusPoint;
    [SerializeField]
    private float _timeLetteringPromotion;

    private void Awake()
    {
        instanceСanvasManager = this;
        IsWinGame = false;
        IsLoseGame = false;
    }
    private void Start()
    {
        _scoreText.text = 0.ToString();

        if (PlayerPrefs.GetInt("Level") <= 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        if (!IsStartGeme)
        {
            FacebookManager.Instance.GameStart();
            _menuUI.SetActive(true);
        }
        else
        {
            FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));
            IsGameFlow = true;
        }
    }

    private void Update()
    {
        if (!_inGameUI.activeSelf && IsStartGeme && IsGameFlow)
        {
            _menuUI.SetActive(false);
            _inGameUI.SetActive(true);
        }
        if (!_wimIU.activeSelf && IsWinGame)
        {
            IsGameFlow = false;

            _levelNamberLevel.text = "Level " + PlayerPrefs.GetInt("Level").ToString();
            FacebookManager.Instance.LevelWin(PlayerPrefs.GetInt("Level"));

            PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

            _scoreTextWin.text = _pointsNamber.ToString();
            _inGameUI.SetActive(false);
            _wimIU.SetActive(true);
            FacebookManager.Instance.LevelWin(PlayerPrefs.GetInt("Level"));
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
            _scoreNamber += 5;
            _scoreText.text = _scoreNamber.ToString();
        }
    }
    private IEnumerator Incentive(bool Amazing)
    {
        _textAmazing.gameObject.SetActive(false);
        _textGood.gameObject.SetActive(false);

        GameObject incentive = Amazing ? _textAmazing.gameObject : _textGood.gameObject;

        incentive.SetActive(true);
        yield return new WaitForSeconds(_timeLetteringPromotion);
        incentive.SetActive(false);
    }
    public void AddPoint(float sizeBilletSuperfluous, float sizeBillet, bool noSawCut)
    {
        if (!noSawCut)
        {
            if (sizeBilletSuperfluous < LevelCharacteristicsManager.Perfect && sizeBillet < LevelCharacteristicsManager.Perfect)
            {
                _pointsNamber += LevelCharacteristicsManager.PerfectAddPoint + _bonusPoint;
                _bonusPoint += LevelCharacteristicsManager.BonusPoints;
                _textAmazing.text = "Amazing! \r\n Score +" + _bonusPoint;
                StartCoroutine(Incentive(true));
            }
            else if (sizeBilletSuperfluous < LevelCharacteristicsManager.Fine && sizeBillet < LevelCharacteristicsManager.Fine)
            {
                _pointsNamber += LevelCharacteristicsManager.FineAddPoint;
                _bonusPoint = 0;

                StartCoroutine(Incentive(false));
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
