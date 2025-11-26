using System;
using System.Collections.Generic;
using CharacterSystem;
using R3;
using UI.Customization;
using Unity.VisualScripting;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class FighterShowcase : MonoBehaviour
{
    private readonly Subject<SkinView> _skinChanged = new();

    [SerializeField] private SkinView[] _skinViews;
    [SerializeField] private Transform _spawnPoint;

    private Fighter _lastSpawnedFighter;
    private int _lastIndex;
    private Dictionary<Fighter, Fighter> _spawnedFighters;

    public Subject<SkinView> SkinChanged => _skinChanged;

    public void Initialize()
    {
        _spawnedFighters = new Dictionary<Fighter, Fighter>();

        for (int i = 0; i < _skinViews.Length; i++)
        {
            Fighter fighter = _skinViews[i].Fighter;

            if (_spawnedFighters.ContainsKey(fighter))
                throw new ArgumentException(nameof(fighter));

            _spawnedFighters.Add(fighter, null);
        }

        if (YG2.saves.IsGuidePassed == false)
            _lastIndex = 0;
        else
            _lastIndex = Random.Range(0, _skinViews.Length);

        Show();
    }

    public void ShowNext()
    {
        _lastIndex++;

        if (_lastIndex >= _skinViews.Length)
            _lastIndex = 0;

        Show();
    }

    public void ShowPrevious()
    {
        _lastIndex--;

        if (_lastIndex < 0)
            _lastIndex = _skinViews.Length - 1;

        Show();
    }

    private void Show()
    {
        Fighter requiredSkin = _skinViews[_lastIndex].Fighter;
        Fighter spawnedSkin;

        if (_spawnedFighters[requiredSkin] == null)
        {
            spawnedSkin = Instantiate(requiredSkin, _spawnPoint.position, _spawnPoint.rotation);
            _spawnedFighters[requiredSkin] = spawnedSkin;
        }
        else
        {
            spawnedSkin = _spawnedFighters[requiredSkin];
        }

        _lastSpawnedFighter?.gameObject.SetActive(false);
        _lastSpawnedFighter = spawnedSkin;
        _lastSpawnedFighter.gameObject.SetActive(true);

        _skinChanged.OnNext(_skinViews[_lastIndex]);
    }
}