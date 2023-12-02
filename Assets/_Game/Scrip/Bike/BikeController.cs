using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections;

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
    Vector3 dir;
    void Start()
    {
        isMoving = true;
        spline.AddTrigger(0, 1, SplineTrigger.Type.Forward).AddListener(() =>
        {
            AddForce();
        });
        spline.AddTrigger(0, 1, SplineTrigger.Type.Forward).AddListener(() =>
        {
            SetAirBorne();
        });
        spline.AddTrigger(0, 0.06, SplineTrigger.Type.Forward).AddListener(() =>
        {
            CameraManager.Ins.ChangeCam(Constants.CAM_FAR);
            RagdollController.Ins.ChaneAnim(Constants.UPANDDOWN_0);
        });
        spline.AddTrigger(0, 0.8, SplineTrigger.Type.Forward).AddListener(() =>
        {
            CameraManager.Ins.ChangeCam(Constants.CAM_NEAR);
        });
    }
    private void Update()
    {
        if (isAirborne)
        {
            if(Input.GetMouseButtonDown(0) && !isMovePlayer)
            {
                MovePlayer();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !isStart)
            {
                isStart = true;
                RagdollController.Ins.ChaneAnim(Constants.START);
            }
        }
    }
    void FixedUpdate()
    {
        MoveAlongSpline();
    }

    void MoveAlongSpline()
    {
        dir = transform.forward;
        if (!isMoving) return;
        if (Input.GetMouseButton(0))
        {
            if (isAirborne)
            {
                timeNitro -= Time.deltaTime;
                if (timeNitro > 0)
                {
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
                rb.velocity = dir * velocity;
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
        RagdollController.Ins.transform.SetParent(null);
        RagdollController.Ins.OnInit(velocity, timeNitro);
        RagdollController.Ins.ChaneAnim(Constants.JETPACKSTART);
        //rb.velocity = dir * velocity;
    }
    void StopAlongSpline()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (isAirborne)
            {
                timeNitro -= Time.deltaTime;
                if (timeNitro > 0)
                {
                    //Vector3 dir = transform.forward;
                    //rb.velocity = dir * splineFollower.followSpeed;
                    splineFollower.followSpeed += Time.fixedDeltaTime;
                    RotateWheel(_colliderF, _transformF);
                    RotateWheel(_colliderB, _transformB);
                }
            }
            else
            {
                //Vector3 dir = transform.forward;
                //rb.velocity = dir * splineFollower.followSpeed;
                splineFollower.followSpeed += speed * Time.fixedDeltaTime;
                RotateWheel(_colliderF, _transformF);
                RotateWheel(_colliderB, _transformB);
            }
        }
    }

    private void RotateWheel(WheelCollider coll, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        coll.GetWorldPose(out position, out rotation);

        transform.rotation = rotation;
        transform.position = position;
    }
    void LiftOff()
    {
        isAirborne = true;
    }

    void HandleAirborne()
    {

    }
    public bool isRunRb;

    public void AddForce()
    {
        isRunRb = true;
        splineFollower.follow = false;
        Vector3 dir = transform.forward;
        rb.velocity = dir * splineFollower.followSpeed;
        isAirborne = true;
        StartCoroutine(ChangeCam());
    }
    IEnumerator ChangeCam()
    {
        yield return new WaitForSeconds(3f);
        CameraManager.Ins.ChangeCam(Constants.CAM_RIGHT);
        yield return new WaitForSeconds(3f);
        //CameraManager.Ins.ChangeCam(Constants.CAM_LEFT);
        //yield return new WaitForSeconds(2f);
        CameraManager.Ins.ChangeCam(Constants.CAM_NEAR);
    }
    public void SetAirBorne()
    {
        isAirborne = true;
    }
}
