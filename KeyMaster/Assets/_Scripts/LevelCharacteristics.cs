using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCharacteristics : MonoBehaviour
{
    [SerializeField]
    private float _perfect, _fine;
    [SerializeField]
    private int _perfectAddPoint, _fineAddPoint, _badlyAddPoint, _bonusPoints;

    [SerializeField]
    private List<LevelKeyCharacteristics> _levelKeyCharacteristics;
    void Awake()
    {
        LevelCharacteristicsManager.Initialize(_levelKeyCharacteristics, _perfect, _fine, _perfectAddPoint, _fineAddPoint, _badlyAddPoint,_bonusPoints);
    }

}
[System.Serializable]
public struct LevelKeyCharacteristics
{
    public List<KeyCharacteristics> keyCharacteristics;
}

public static class LevelCharacteristicsManager
{
    public static List<LevelKeyCharacteristics> LevelKeyCharacteristics;
    public static float Perfect, Fine;
    public static int PerfectAddPoint, FineAddPoint, BadlyAddPoint, BonusPoints;

    public static void Initialize(List<LevelKeyCharacteristics> sample,float perfect,float fine,
       int perfectAddPoint, int fineAddPoint, int badlyAddPoint, int bonusPoints)
    {
        LevelKeyCharacteristics = sample;
        Perfect = perfect;
        Fine = fine;
        PerfectAddPoint = perfectAddPoint;
        FineAddPoint = fineAddPoint;
        BadlyAddPoint = badlyAddPoint;
        BonusPoints = bonusPoints;
    }

}

