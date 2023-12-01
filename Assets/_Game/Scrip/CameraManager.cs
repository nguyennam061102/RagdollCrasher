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

    private void Start()
    {
        ChangeCam(Constants.CAM_START);
    }
    public void ChangeCam(string name)
    {
        CamPos targetCam = lisCamera.Find(camera => camera.name == name);
        if(curentCam.name != targetCam.name)
        {
            curentCam.virtualCamera.gameObject.SetActive(false);
            targetCam.virtualCamera.gameObject.SetActive(true);
            curentCam = targetCam;
        }
    }
}
