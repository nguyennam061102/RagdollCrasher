using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RagdollController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody[] ragdollRigidbodies;
    [SerializeField] Collider col;
    [SerializeField] Rigidbody rbRagdoll;
    [SerializeField] string currenAnim;
    [SerializeField] bool isMoving;
    [SerializeField] bool isActiveRagdoll;
    [SerializeField] bool isAirborn;
    [SerializeField] float speed;
    [SerializeField] float velocity;
    [SerializeField] float timeNitro;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] GameObject FlameJetpack;
    Vector3 dir;
    private bool isPress;

    public Vector3 StartPos { get => startPos; set => startPos = value; }
    public bool IsAirborn { get => isAirborn; set => isAirborn = value; }
    public Vector3 EndPos { get => endPos; set => endPos = value; }

    private void Start()
    {
        isMoving = false;
        GetInput();
    }
    private void Update()
    {
        //dir = transform.forward;  
    }
    private void FixedUpdate()
    {
        if (isAirborn && GameManager.Ins.gameState != GameState.Skip)
        {
            UIManager.Ins.GetUI<UIGamePlay>().SetDistance(Vector3.Distance(rbRagdoll.transform.position, startPos));
        }
        if (isMoving && GameManager.Ins.gameState != GameState.Skip)
        {
            if (!isActiveRagdoll)
            {
                GameManager.Ins.Velocity = rb.velocity.magnitude;
                UIManager.Ins.GetUI<UIGamePlay>().SetVelocity(GameManager.Ins.Velocity);
            }
            else
            {
                GameManager.Ins.Velocity = rbRagdoll.velocity.magnitude;
                UIManager.Ins.GetUI<UIGamePlay>().SetVelocity(GameManager.Ins.Velocity);
            }
            MoveWithhJetpack();
            //timeNitro -= Time.deltaTime;
            //UIManager.Ins.GetUI<UIGamePlay>().SetSlider(timeNitro);
            //if (timeNitro > 0)
            //{
            //    rb.velocity = dir * GameManager.Ins.Velocity;
            //    GameManager.Ins.Velocity += speed * Time.fixedDeltaTime * 0.2f;
            //    FlameJetpack.SetActive(true);
            //}
            //else
            //{
            //    FlameJetpack.SetActive(false);
            //}
        }
    }
    public void OnInit(float timeNitro)
    {
        ChaneAnim(Constants.AIR);
        transform.SetParent(null);
        GetComponent<Collider>().isTrigger = false;
        transform.rotation = Quaternion.Euler(-15, 0, 0);
        isMoving = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        this.timeNitro = timeNitro;
        dir = transform.forward;
        rb.velocity = dir * GameManager.Ins.Velocity;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }
    void GetInput()
    {
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) =>
        {
            CheckPointDown();
        });
        HandleInput.Ins.EventTrigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) =>
        {
            CheckPointUp();
        });
        HandleInput.Ins.EventTrigger.triggers.Add(entryUp);
    }

    private void CheckPointUp()
    {
        isPress = false;
        AudioManager.Ins.StopSfx(Constants.SFX_BOOST_LOOP);
        
    }


    private void CheckPointDown()
    {
        isPress = true;
    }
    void MoveWithhJetpack()
    {
        if (isPress)
        {

            timeNitro -= Time.deltaTime;
            UIManager.Ins.GetUI<UIGamePlay>().SetSlider(timeNitro);
            if (timeNitro > 0)
            {
                AudioManager.Ins.PlaySfxLoop(Constants.SFX_BOOST_LOOP);
                rb.velocity += Vector3.up * SaveLoadData.Ins.DataGame.JetpackPow * 0.1f;
                GameManager.Ins.Velocity += SaveLoadData.Ins.DataGame.JetpackPow * Time.fixedDeltaTime * 0.1f;
                FlameJetpack.SetActive(true);
            }
            else
            {
                AudioManager.Ins.StopSfx(Constants.SFX_BOOST_LOOP);
                FlameJetpack.SetActive(false);
            }

        }
        else
        {
            FlameJetpack.SetActive(false);
        }
    }
    public void ChaneAnim(string name)
    {
        if (currenAnim != name)
        {
            currenAnim = name;
            anim.SetTrigger(currenAnim);
        }
    }
    [Button]
    public void SetStateRagdoll(bool state)
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        anim.enabled = state;
    }
    IEnumerator SetEndPanel()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Ins.GetUI<UIGamePlay>().OpenNewUI<UIEnd>();
        UIManager.Ins.GetUI<UIEnd>().SetEnd(Vector3.Distance(rbRagdoll.transform.position, startPos), Vector3.Distance(endPos, startPos));
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (isMoving && GameManager.Ins.gameState != GameState.Skip)
    //    {
    //        SetStateRagdoll(false);
    //        rbRagdoll.transform.SetParent(null);
    //        isActiveRagdoll = true;
    //        rbRagdoll.velocity = dir * (GameManager.Ins.Velocity + 10f);
    //        Debug.Log(GameManager.Ins.Velocity);
    //        GameManager.Ins.Velocity = rbRagdoll.velocity.magnitude;
    //        UIManager.Ins.GetUI<UIGamePlay>().SetVelocity(GameManager.Ins.Velocity);
    //        CameraManager.Ins.ChangeCam(Constants.CAM_ROTATE);
    //        rb.isKinematic = true;
    //        StartCoroutine(SetEndPanel());
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (!UIManager.Ins.IsOpened<UIEnd>())
        {
            AudioManager.Ins.PlaySfx(Constants.SFX_VAR);
            AudioManager.Ins.PlaySfx(Constants.SFX_FALL);

        }
        GetComponent<Collider>().isTrigger = true;
        SetStateRagdoll(false);
        rbRagdoll.transform.SetParent(null);
        isActiveRagdoll = true;
        rbRagdoll.velocity = dir * (GameManager.Ins.Velocity + 10f);
        Debug.Log(GameManager.Ins.Velocity);
        GameManager.Ins.Velocity = rbRagdoll.velocity.magnitude;
        UIManager.Ins.GetUI<UIGamePlay>().SetVelocity(GameManager.Ins.Velocity);
        CameraManager.Ins.ChangeCam(Constants.CAM_ROTATE);
        rb.isKinematic = true;
        StartCoroutine(SetEndPanel());
    }
    [Button]
    public void SetSkip()
    {
        GameManager.Ins.gameState = GameState.Skip;
        CameraManager.Ins.ChangeCam(Constants.CAM_ROTATE);
        LevelManager.Ins.AimPlayer.SetParent(null);
        LevelManager.Ins.CurrentMotor.RemoveTrigger();
        if (isAirborn)
        {

            UIManager.Ins.GetUI<UIGamePlay>().OpenNewUI<UIEnd>();
            UIManager.Ins.GetUI<UIEnd>().SetEnd(Vector3.Distance(rbRagdoll.transform.position, startPos), Vector3.Distance(endPos, startPos));
            LevelManager.Ins.CurrentMotor.RagdollController.enabled = false;
            LevelManager.Ins.CurrentMotor.enabled = false;
        }
        else
        {
            UIManager.Ins.GetUI<UIGamePlay>().OpenNewUI<UIEnd>();
            UIManager.Ins.GetUI<UIEnd>().SetEnd(0f, 0f);
            LevelManager.Ins.CurrentMotor.RagdollController.enabled = false;
            LevelManager.Ins.CurrentMotor.enabled = false;
        }

    }
}
