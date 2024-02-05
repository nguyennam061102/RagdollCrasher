using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] float velocity;
    public GameState gameState;
    public float Velocity { get => velocity; set => velocity = value; }

    protected override void Awake()
    {
        base.Awake();
        //tranh viec nguoi choi cham da diem vao man hinh
        //Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        //int maxScreenHeight = 1280;
        //float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        //if (Screen.currentResolution.height > maxScreenHeight)
        //{
        //    Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        //}
        UIManager.Ins.OpenUI<UIStart>();
        LevelManager.Ins.OnInit(SaveLoadData.Ins.DataGame.CurrenMotor, SaveLoadData.Ins.DataGame.CurrenLv);
        gameState = GameState.Start;
        AudioManager.Ins.PlayMusic(Constants.MUSIC_1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum GameState
{
    Start,
    Playing,
    Skip
}
