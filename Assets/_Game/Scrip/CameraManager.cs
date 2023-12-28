using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class CamPos
{
    public string name;
    public CinemachineVirtualCamera virtualCamera;
}
public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] List<CamPos> lisCamera;
    [SerializeField] CamPos curentCam;
    [SerializeField] ParticleSystem speedLine;

    private void Start()
    {
        ChangeCam(Constants.CAM_START);
        ChaneAim();
    }
    public void ChangeCam(string name)
    {
        if (name == Constants.CAM_FAR)
        {
            speedLine.Play();
        }
        else
        {
            speedLine.Stop();
        }
        CamPos targetCam = lisCamera.Find(camera => camera.name == name);
        if(curentCam.name != targetCam.name)
        {
            curentCam.virtualCamera.gameObject.SetActive(false);
            targetCam.virtualCamera.gameObject.SetActive(true);
            curentCam = targetCam;
        }
    }
    public void ChaneAim()
    {
        foreach (var cam in lisCamera)
        {
            cam.virtualCamera.Follow = LevelManager.Ins.AimPlayer;
            cam.virtualCamera.LookAt = LevelManager.Ins.AimPlayer;
        }
    }
}
