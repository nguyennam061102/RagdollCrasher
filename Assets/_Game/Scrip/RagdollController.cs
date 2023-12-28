using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject charRagdoll;
    [SerializeField] GameObject charAnim;
    [SerializeField] Animation anim;
    [SerializeField] Animation animRagdoll;
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
    Vector3 dir;

    public Vector3 StartPos { get => startPos; set => startPos = value; }
    public bool IsAirborn { get => isAirborn; set => isAirborn = value; }
    public Vector3 EndPos { get => endPos; set => endPos = value; }

    private void Start()
    {
        isMoving = false;
    }
    private void Update()
    {
        //dir = transform.forward;  
    }
    private void FixedUpdate()
    {
        if (isAirborn)
        {
            UIManager.Ins.GetUI<UIGamePlay>().SetDistance(Vector3.Distance(rbRagdoll.transform.position, startPos));
        }
        if (isMoving)
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
            timeNitro -= Time.deltaTime;
            if (timeNitro > 0)
            {
                rb.velocity = dir * GameManager.Ins.Velocity;
                GameManager.Ins.Velocity += speed * Time.fixedDeltaTime * 0.2f;
            }
        }
    }
    public void OnInit(float timeNitro)
    {
        ChaneAnim(Constants.JETPACKSTART);
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(-15, 0, 0);
        isMoving = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        this.timeNitro = timeNitro;
        dir = transform.forward;
        rb.velocity = dir * GameManager.Ins.Velocity;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        StartCoroutine(JetpackAnim());
    }
    IEnumerator JetpackAnim()
    {
        yield return new WaitForSeconds(0.15f);
        ChaneAnim(Constants.JETPACKLOOP);
        Time.timeScale = 1f;

    }
    public void ChaneAnim(string name)
    {
        if (currenAnim != name)
        {
            currenAnim = name;
            anim.Play(currenAnim);
        }
    }
    public void SetRagdoll()
    {
        charAnim.SetActive(false);
        charRagdoll.SetActive(true);
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
        yield return new WaitForSeconds(3);
        UIManager.Ins.GetUI<UIGamePlay>().OpenNewUI<UIEnd>();
        UIManager.Ins.GetUI<UIEnd>().SetEnd(Vector3.Distance(rbRagdoll.transform.position, startPos), Vector3.Distance(endPos, startPos));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isMoving)
        {
            SetRagdoll();
            //rb.velocity = Vector3.zero;
            rbRagdoll.transform.SetParent(null);
            isActiveRagdoll = true;
            rbRagdoll.velocity = dir * GameManager.Ins.Velocity;
            Debug.Log(GameManager.Ins.Velocity);
            GameManager.Ins.Velocity = rbRagdoll.velocity.magnitude;
            UIManager.Ins.GetUI<UIGamePlay>().SetVelocity(GameManager.Ins.Velocity);
            CameraManager.Ins.ChangeCam(Constants.CAM_ROTATE);
            rb.isKinematic = true;
            StartCoroutine(SetEndPanel());
        }
        if (CompareTag("Win"))
        {
            SaveLoadData.Ins.DataGame.CurrenLv++;
            SaveLoadData.Ins.Save();
        }
    }
}
