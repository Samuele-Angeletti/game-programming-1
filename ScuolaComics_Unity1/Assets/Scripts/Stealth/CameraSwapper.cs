using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapper : MonoBehaviour
{
    GameObject _target;
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void OpenCamera(CinemachineVirtualCamera virtualCamera)
    {
        virtualCamera.gameObject.SetActive(true);
        virtualCamera.LookAt = _target.transform;
    }

    public void CloseCamera(CinemachineVirtualCamera virtualCamera)
    {
        virtualCamera.LookAt = null;
        virtualCamera.gameObject.SetActive(false);
    }
}
