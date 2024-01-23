using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum MotorType
{
    Atami,
    Borrelly,
    Emma,
    Makhai,
    Massalia,
    Melpomene,
    Nausikaa,
    Patientia,
    Philotes,
    Suomi,
    Taiowa
}
public class LevelManager : SerializedMonoBehaviour
{
#region SingLeton
    private static LevelManager instance;
    public static LevelManager Ins { get => instance; }
    public Transform AimPlayer { get => aimPlayer; set => aimPlayer = value; }
    public BikeController CurrentMotor { get => currentMotor; set => currentMotor = value; }

    [SerializeField] private bool needDontDestroy = false;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (LevelManager)this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (needDontDestroy) DontDestroyOnLoad(gameObject);
        SetUp();
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    protected virtual void SetUp()
    {

    }
#endregion

    [SerializeField] Dictionary<MotorType, BikeController> motors;
    [SerializeField] Dictionary<int, Map> maps;
    [SerializeField] Transform aimPlayer;
    [SerializeField] Map currentMap;
    [SerializeField] BikeController currentMotor;
    public void OnInit(MotorType motorType, int lv)
    {
        currentMap = Instantiate(maps[lv]);
        currentMotor = Instantiate(motors[motorType], currentMap.StartPoint.position,Quaternion.identity );
        currentMotor.Spline = currentMap.Spline;
        currentMotor.SplineFollower.spline = currentMap.Spline;
        currentMotor.RagdollController.EndPos = currentMap.EndPoint.position;
        aimPlayer = currentMotor.AimPlayer;
        RenderSettings.skybox = currentMap.Skybox;
    }
    public void ChaneMap()
    {
        Destroy(currentMap.gameObject);
        currentMap = null;
        currentMap = Instantiate(maps[SaveLoadData.Ins.DataGame.CurrenLv]);
        currentMotor.Spline = currentMap.Spline;
        currentMotor.SplineFollower.spline = currentMap.Spline;
        currentMotor.RagdollController.EndPos = currentMap.EndPoint.position;
        currentMotor.OnInit();
        RenderSettings.skybox = currentMap.Skybox;
    }
    public void ChaneMotor()
    {
        Destroy(currentMotor.gameObject);
        currentMotor = null;
        currentMotor = Instantiate(motors[SaveLoadData.Ins.DataGame.CurrenMotor], currentMap.StartPoint.position, Quaternion.identity);
        currentMotor.Spline = currentMap.Spline;
        currentMotor.SplineFollower.spline = currentMap.Spline;
        currentMotor.RagdollController.EndPos = currentMap.EndPoint.position;
        aimPlayer = currentMotor.AimPlayer;
        CameraManager.Ins.ChaneAim(aimPlayer);
    }
}
