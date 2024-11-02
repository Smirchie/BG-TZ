using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public static UnityEvent<int> OnItemPickUp = new();
    public static UnityEvent<Transform> OnCheckPointActivate = new();
    public static UnityEvent<int> OnMultiplierSet = new();
    [SerializeField] private List<PlayerStatus> _playerStatuses;
    [SerializeField] private SkinnedMeshRenderer _playerMeshRenderer;
    [SerializeField] private TextMeshProUGUI _moneyTMP;
    [SerializeField] private Image _currentStatusBar;
    [SerializeField] private TextMeshProUGUI _statusTMP;
    [SerializeField] private List<MoneyPopup> _popups;
    [SerializeField] private Animator _playerAnimator;
    public static int CurrentPlayerStatusIndex { get; private set; }
    private Transform _spawnPoint;
    private int _money;
    private int _moneyMultiplier = 1;

    private int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            if (Money > 0)
            {
                SetStatusBarScale();
                if (IsMoneyLowerThanCurrentStatus())
                {
                    SetStatusAt(CurrentPlayerStatusIndex - 1);
                }
                else if (IsMoneyHigherThanCurrentStatus())
                {
                    SetStatusAt(CurrentPlayerStatusIndex + 1);
                }
                _moneyTMP.text = Money.ToString();
            }
            else
            {
                GameManager.OnGameOver.Invoke(false);
            }
        }
    }
    private void SetStatusBarScale()
    {
        if(Money<=150)
        {
            _currentStatusBar.rectTransform.localScale = new Vector3(Money / 150f, _currentStatusBar.rectTransform.localScale.y, _currentStatusBar.rectTransform.localScale.z);
            _currentStatusBar.rectTransform.localPosition = new Vector3(Money - 145, 0f, 0f);
        }
    }
    private void SetStatusAt(int _newStatusIndex)
    {
        CurrentPlayerStatusIndex = _newStatusIndex;
        PlayerStatus newPlayerStatus = _playerStatuses[_newStatusIndex];
        _playerMeshRenderer.sharedMesh = newPlayerStatus._mesh;
        _currentStatusBar.color = newPlayerStatus._statusBarColor;
        _statusTMP.color = newPlayerStatus._statusBarColor;
        _statusTMP.text = newPlayerStatus._name.ToUpper();
        _playerAnimator.SetTrigger("Spin");
    }

    private bool IsMoneyHigherThanCurrentStatus()
    {
        if (Money > _playerStatuses[CurrentPlayerStatusIndex]._maxMoney && CurrentPlayerStatusIndex < _playerStatuses.Count - 1)
        {
            return true;
        }
        return false;
    }

    private bool IsMoneyLowerThanCurrentStatus()
    {
        if (Money < _playerStatuses[CurrentPlayerStatusIndex]._minMoney)
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        OnItemPickUp.AddListener(ChangeMoney);
        OnCheckPointActivate.AddListener(SetSpawnPoint);
        SetStatusesMinMoney();
        SetStatusAt(CurrentPlayerStatusIndex);
        Money = 40;
        OnMultiplierSet.AddListener(SetMultiplier);
    }

    private void SetMultiplier(int _multiplier)
    {
        _moneyMultiplier = _multiplier;
    }
    private void SetStatusesMinMoney()
    {
        _playerStatuses[0]._minMoney = 0;
        for (int i = 1; i < _playerStatuses.Count; i++)
        {
            _playerStatuses[i]._minMoney = _playerStatuses[i - 1]._maxMoney + 1;
        }
    }

    private void ChangeMoney(int _moneyDelta)
    {
        Money += _moneyDelta;
        if(_moneyDelta>=0)
        {
            _popups[0].OnMoneyChanged.Invoke(_moneyDelta);
        }
        else
        {
            _popups[1].OnMoneyChanged.Invoke(_moneyDelta);
        }
    }

    private void SetSpawnPoint(Transform _transform)
    {
        _spawnPoint = _transform;
    }

    [Serializable]
    private class PlayerStatus
    {
        [HideInInspector] public int _minMoney;
        public int _maxMoney;
        public string _name;
        public Mesh _mesh;
        public Color _statusBarColor;
    }
}