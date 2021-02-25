using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCharacteristics : MonoBehaviour
{
    [SerializeField]
    private List<LevelKeyCharacteristics> _levelKeyCharacteristics;
    void Awake()
    {
        LevelCharacteristicsManager.Initialize(_levelKeyCharacteristics);
    }

}
[System.Serializable]
public struct LevelKeyCharacteristics
{
    public List<KeyCharacteristics> keyCharacteristics;
}

public static class LevelCharacteristicsManager
{
    [SerializeField]
    public static List<LevelKeyCharacteristics> LevelKeyCharacteristics;

    public static void Initialize(List<LevelKeyCharacteristics> sample)
    {
        LevelKeyCharacteristics = sample;
    }

}

