using System;
using UnityEngine;
public class SaveLoadData : Singleton<SaveLoadData>
{
    [SerializeField] DataGame dataGame;

    public DataGame DataGame { get => dataGame; set => dataGame = value; }
    protected override void Awake()
    {
        base.Awake();
        if (!ES3.KeyExists("Datagame", "Datagame"))
        {
            Save();
        }
        else
        {
            dataGame = Load();
        }
    }

    void Save()
    {
        ES3.Save("DataGame",dataGame,"Datagame");
    }
    DataGame Load()
    {
        return ES3.Load<DataGame>("Datagame", dataGame);
    }
    
}
[Serializable]
public struct DataGame
{
    [SerializeField] private float enginePow;
    [SerializeField] private float jetpackPow;
    [SerializeField] private float coinReward;
    [SerializeField] private int coin;
    [SerializeField] private int lv;
    [SerializeField] private float maxPathMove;

    public float EnginePow { get => enginePow; set => enginePow = value; }
    public float JetpackPow { get => jetpackPow; set => jetpackPow = value; }
    public float CoinReward { get => coinReward; set => coinReward = value; }
    public int Coin { get => coin; set => coin = value; }
    public int Lv { get => lv; set => lv = value; }
    public float MaxPathMove { get => maxPathMove; set => maxPathMove = value; }
}