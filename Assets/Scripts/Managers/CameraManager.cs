using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera virtualCameraComp;
    private CinemachineBasicMultiChannelPerlin cinemachineNoise { get { return virtualCameraComp.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); } }

    public void Shake(float amplitude = 0.8f, float seconds = 0.1f)
    {
        IEnumerator Shake(float seconds)
        {
            cinemachineNoise.m_AmplitudeGain = amplitude;
            yield return new WaitForSeconds(seconds);
            cinemachineNoise.m_AmplitudeGain = 0;
        }

        StartCoroutine(Shake(seconds));
    }

}
