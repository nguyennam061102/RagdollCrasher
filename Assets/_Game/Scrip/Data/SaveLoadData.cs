using System;
using System.Collections.Generic;
using UnityEngine;


public interface IObserver
{
    void OnNotify();
}

public class SaveLoadData : Singleton<SaveLoadData>
{
    [SerializeField] public DataGame DataGame;

    //public DataGame DataGame { get => dataGame; set => dataGame = value; }
    protected override void Awake()
    {
        base.Awake();
        if (!ES3.KeyExists("DataGame", "DataGame"))
        {
            Save();
        }
        else
        {
            DataGame = LoadData();
        }
    }

    public void Save()
    {
        ES3.Save("DataGame",DataGame,"DataGame");
    }
    DataGame LoadData()
    {
        return ES3.Load<DataGame>("DataGame", "DataGame");
    }
    private void OnApplicationPause(bool pause)
    {
        Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
[Serializable]
public class DataGame
{
    [SerializeField] private float enginePow;
    [SerializeField] private float jetpackPow;
    [SerializeField] private float coinReward;
    [SerializeField] private int enginePowCoinUp;
    [SerializeField] private int jetpackPowCoinUp;
    [SerializeField] private int coinRewardCoinUp;
    [SerializeField] private int enginePowLv;
    [SerializeField] private int jetpackLv;
    [SerializeField] private int coinRewardLv;
    [SerializeField] public int coin;
    [SerializeField] private int lv;
    [SerializeField] private int currenLv;
    [SerializeField] private MotorType currenMotor;
    [SerializeField] private float maxPathMove;
    [SerializeField] private int vibrateData;

    public float EnginePow { get => enginePow; set => enginePow = value; }
    public float JetpackPow { get => jetpackPow; set => jetpackPow = value; }
    public float CoinReward { get => coinReward; set => coinReward = value; }
    public int Coin 
    { 
        get => coin; 
        set {
            coin = value;
            //UIManager.Ins.GetUI<UIStart>().SetGold();
            NotifyObservers();
        }  
    }
    public int Lv 
    { 
        get => lv;
        set 
        {
            lv = value;
            NotifyObservers();
        }  
    }
    public float MaxPathMove { get => maxPathMove; set => maxPathMove = value; }
    public int CurrenLv { get => currenLv; set => currenLv = value; }
    public MotorType CurrenMotor { get => currenMotor; set => currenMotor = value; }
    public int EnginePowCoinUp { get => enginePowCoinUp; set => enginePowCoinUp = value; }
    public int JetpackPowCoinUp { get => jetpackPowCoinUp; set => jetpackPowCoinUp = value; }
    public int CoinRewardCoinUp { get => coinRewardCoinUp; set => coinRewardCoinUp = value; }
    public int EnginePowLv { get => enginePowLv; set => enginePowLv = value; }
    public int JetpackLv { get => jetpackLv; set => jetpackLv = value; }
    public int CoinRewardLv { get => coinRewardLv; set => coinRewardLv = value; }
    public int VibrateData { get => vibrateData; set => vibrateData = value; }

    [Header("IObserver")]
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify();
        }
    }
}