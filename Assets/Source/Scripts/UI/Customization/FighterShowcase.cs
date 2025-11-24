using System;
using System.Collections.Generic;
using System.Linq;
using CharacterSystem;
using R3;
using UI.Customization;
using UnityEngine;
using YG;

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
        Fighter settedFighter = YG2.saves.SettedFighter;
        int lastSettedFighterIndex = 0;
        
        _spawnedFighters = new Dictionary<Fighter, Fighter>();
        
        for (int i = 0; i < _skinViews.Length; i++)
        {
            Fighter fighter = _skinViews[i].Fighter;

            if (_spawnedFighters.ContainsKey(fighter))
                throw new ArgumentException(nameof(fighter));

            if (settedFighter == _skinViews[i].Fighter)
                lastSettedFighterIndex = i;
            
            _spawnedFighters.Add(fighter, null);
        }

        _lastIndex = lastSettedFighterIndex;
        
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