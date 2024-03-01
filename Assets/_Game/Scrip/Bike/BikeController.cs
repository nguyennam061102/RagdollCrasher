using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class BikeController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] ParticleSystem flame;
    [SerializeField] ParticleSystem wind;
    [Header("--------Spline-------")]
    [SerializeField] SplineFollower splineFollower;
    [SerializeField] SplineComputer spline;
    [Header("---------Wheel-------")]
    [SerializeField] private Transform _transformF;
    [SerializeField] private Transform _transformB;
    [SerializeField] private WheelCollider _colliderF;
    [SerializeField] private WheelCollider _colliderB;
    [Header("--------Parameter----")]
    [SerializeField] float speed;
    [SerializeField] float velocity;
    [SerializeField] float timeNitro;
    [SerializeField] bool isAirborne = false;
    [SerializeField] bool isMovePlayer = false;
    [SerializeField] bool isMoving;
    [SerializeField] bool isStart;
    [SerializeField] bool isPress;
    [SerializeField] Transform aimPlayer;
    [SerializeField] RagdollController ragdollController;
    public Vector3 dir;

    public SplineComputer Spline { get => spline; set => spline = value; }
    public Transform AimPlayer { get => aimPlayer; set => aimPlayer = value; }
    public SplineFollower SplineFollower { get => splineFollower; set => splineFollower = value; }
    public RagdollController RagdollController { get => ragdollController; set => ragdollController = value; }

    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        RemoveTrigger();
        isMoving = true;
        spline.AddTrigger(0, 1, SplineTrigger.Type.Forward).AddListener(() =>
        {
            AddForce();
            SetAirBorne();
        });
        spline.AddTrigger(0, 0.8, SplineTrigger.Type.Forward).AddListener(() =>
        {
            StartCoroutine(ChangeCam());
        });
        spline.AddTrigger(0, 0.1, SplineTrigger.Type.Forward).AddListener(() =>
        {
            CameraManager.Ins.ChangeCam(Constants.CAM_FAR);
            ragdollController.ChaneAnim(Constants.STARTUP);
            wind.Play();
            Time.timeScale = 1.5f;
        });
        spline.AddTrigger(0, 0.7, SplineTrigger.Type.Forward).AddListener(() =>
        {
            CameraManager.Ins.ChangeCam(Constants.CAM_NEAR);
            Time.timeScale = 1f;

        });
        GetInput();
        smoke.gameObject.SetActive(true);
        flame.gameObject.SetActive(false);
        //AudioManager.Ins.PlaySfxLoop(Constants.SFX_IDLE_1);
    }
    public void RemoveTrigger()
    {
        if (spline.triggerGroups.Length > 0)
        {
            spline.triggerGroups = RemoveItemAt(spline.triggerGroups, 0);

        }
    }
    TriggerGroup[] RemoveItemAt(TriggerGroup[] originalArray, int index)
    {
        if (index < 0 || index >= originalArray.Length)
        {
            Debug.LogError("Invalid index to remove.");
            return originalArray;
        }

        TriggerGroup[] newArray = new TriggerGroup[originalArray.Length - 1];
        for (int i = 0, j = 0; i < originalArray.Length; i++)
        {
            if (i != index)
            {
                newArray[j] = originalArray[i];
                j++;
            }
        }

        return newArray;
    }
    private void Update()
    {
        //dir = transform.forward;
        
    }
    void FixedUpdate()
    {
        MoveAlongSpline();
        if (!isMovePlayer && isStart)
        {
            UIManager.Ins.GetUI<UIGamePlay>().SetVelocity(GameManager.Ins.Velocity);
        }
        if (!isPress && !isMovePlayer)
        {
            if(splineFollower.followSpeed < 10)
            {
                if (splineFollower.followSpeed > 0.2f)
                {
                    GameManager.Ins.Velocity -= (SaveLoadData.Ins.DataGame.EnginePow + speed) * Time.fixedDeltaTime;
                    splineFollower.followSpeed = GameManager.Ins.Velocity;
                    rb.velocity = transform.forward * GameManager.Ins.Velocity;
                }
            }
        }
    }

    void GetInput()
    {
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => {
            CheckPointDown();
        });
        HandleInput.Ins.EventTrigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => {
            CheckPointUp();
        });
        HandleInput.Ins.EventTrigger.triggers.Add(entryUp);
    }
    void CheckPointDown()
    {
        if (isAirborne)
        {
            if (!isMovePlayer)
            {
                MovePlayer();
            }
            if (timeNitro > 0)
            {
                AudioManager.Ins.PlaySfxLoop(Constants.SFX_BOOST_LOOP);
            }
        }
        else
        {
            if (!isStart)
            {
                Debug.Log("Level " + SaveLoadData.Ins.DataGame.CurrenLv);
                CameraManager.Ins.ChaneAim(this.transform);
                ragdollController.ChaneAnim(Constants.START);
                isStart = true;
                UIManager.Ins.GetUI<UIStart>().OpenNewUI<UIGamePlay>();
                if(SaveLoadData.Ins.DataGame.CurrenMotor != MotorType.Philotes)
                {
                    AudioManager.Ins.PlaySfxLoop(Constants.SFX_RUN_1);
                }
                else
                {
                    AudioManager.Ins.PlaySfxLoop(Constants.SFX_XEDAP);
                }
            }
            isPress = true;
        }
    }
    void CheckPointUp()
    {
        isPress = false;
        if (isAirborne)
        {
            AudioManager.Ins.StopSfx(Constants.SFX_BOOST_LOOP);
        }
    }
    void MoveAlongSpline()
    {
        if (isPress && isMoving)
        {
            if (isAirborne)
            {
                smoke.gameObject.SetActive(false);
                flame.gameObject.SetActive(true);
                timeNitro -= Time.deltaTime;
                UIManager.Ins.GetUI<UIGamePlay>().SetSlider(timeNitro);
                if (timeNitro > 0)
                {
                    AudioManager.Ins.PlaySfxLoop(Constants.SFX_BOOST_LOOP);
                    //dir = rb.velocity.normalized;
                    //rb.AddForce(dir * splineFollower.followSaveLoadData.Ins.DataGame.EnginePow * 100);
                    rb.velocity = dir * GameManager.Ins.Velocity;
                    GameManager.Ins.Velocity += (SaveLoadData.Ins.DataGame.EnginePow + speed) * Time.fixedDeltaTime;
                    //splineFollower.followSpeed = GameManager.Ins.Velocity;
                }
                else
                {
                    AudioManager.Ins.StopSfx(Constants.SFX_BOOST_LOOP);
                    MovePlayer();
                }
            }
            else
            {
                //rb.velocity = transform.forward * GameManager.Ins.Velocity;
                GameManager.Ins.Velocity += (SaveLoadData.Ins.DataGame.EnginePow + speed) * Time.fixedDeltaTime;
                splineFollower.followSpeed = GameManager.Ins.Velocity;
            }
        }
        else if (isMoving && GameManager.Ins.Velocity >= 10 && !isAirborne)
        {
            rb.velocity = transform.forward * GameManager.Ins.Velocity;
            GameManager.Ins.Velocity += (SaveLoadData.Ins.DataGame.EnginePow + speed) * Time.fixedDeltaTime;
            splineFollower.followSpeed = GameManager.Ins.Velocity;
        }

        RotateWheel(_colliderF, _transformF);
        RotateWheel(_colliderB, _transformB);
    }
    void MovePlayer()
    {
        
        smoke.gameObject.SetActive(true);
        flame.gameObject.SetActive(false);
        isMovePlayer = true;
        isMoving = false;
        ragdollController.OnInit(timeNitro);
        dir.y = 0;
        rb.velocity = dir * GameManager.Ins.Velocity;
        this.enabled = false;
    }

    private void RotateWheel(WheelCollider coll, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        coll.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }

    public void AddForce()
    {
        ragdollController.StartPos = transform.position;
        ragdollController.IsAirborn = true;
        splineFollower.follow = false;
        dir = transform.forward;
        rb.velocity = dir * splineFollower.followSpeed;
        isAirborne = true;
        Time.timeScale = 1.25f;
    }
    IEnumerator ChangeCam()
    {
        //yield return new WaitForSeconds(3f);
        CameraManager.Ins.ChangeCam(Constants.CAM_RIGHT);
        yield return new WaitForSeconds(2f);
        //CameraManager.Ins.ChangeCam(Constants.CAM_LEFT);
        //yield return new WaitForSeconds(2f);
        CameraManager.Ins.ChangeCam(Constants.CAM_NEAR);
    }
    public void SetAirBorne()
    {
        isAirborne = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player") && !other.CompareTag("Path") && !isMovePlayer && GameManager.Ins.gameState != GameState.Skip)
        {
            Debug.Log(other.name);
             MovePlayer();
        }
    }
}
