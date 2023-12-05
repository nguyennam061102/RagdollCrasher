using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;

public class BikeController : Singleton<BikeController>
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Animation anim;
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
    Vector3 dir;
    void Start()
    {
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
            RagdollController.Ins.ChaneAnim(Constants.UPANDDOWN_0);
            //Time.timeScale = 1.5f;
        });
        spline.AddTrigger(0, 0.7, SplineTrigger.Type.Forward).AddListener(() =>
        {
            CameraManager.Ins.ChangeCam(Constants.CAM_NEAR);
            Time.timeScale = 1;
            
        });
        GetInput();
    }
    private void Update()
    {
        //dir = transform.forward;
        if (!isPress && splineFollower.followSpeed < 10)
        {
            if(splineFollower.followSpeed > 0)
            {
                splineFollower.followSpeed -= Time.deltaTime * speed;
            }
        }
    }
    void FixedUpdate()
    {
        MoveAlongSpline();
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
        }
        else
        {
            if (!isStart)
            {
                RagdollController.Ins.ChaneAnim(Constants.START);
                isStart = true;
            }
            isPress = true;
        }
    }
    void CheckPointUp()
    {
        isPress = false;
    }
    void MoveAlongSpline()
    {
        if (isPress && isMoving)
        {
            if (isAirborne)
            {
                timeNitro -= Time.deltaTime;
                if (timeNitro > 0)
                {
                    //rb.AddForce(dir * splineFollower.followSpeed * 100);
                    dir = rb.velocity.normalized;
                    dir.x = 0;
                    rb.velocity = dir * velocity;
                    velocity += speed * Time.fixedDeltaTime;
                    splineFollower.followSpeed = velocity;
                }
                else
                {
                    MovePlayer();
                }
            }
            else
            {
                rb.velocity = transform.forward * velocity;
                velocity += speed * Time.fixedDeltaTime;
                splineFollower.followSpeed = velocity;
            }
        }

        RotateWheel(_colliderF, _transformF);
        RotateWheel(_colliderB, _transformB);
    }
    void MovePlayer()
    {
        isMovePlayer = true;
        isMoving = false;

        rb.velocity = dir * velocity;
        RagdollController.Ins.OnInit(velocity, timeNitro, dir);
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
        splineFollower.follow = false;
        dir = transform.forward;
        rb.velocity = dir * splineFollower.followSpeed;
        isAirborne = true;
        Time.timeScale = 0.75f;
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
}
