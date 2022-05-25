using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance;

    CinemachineVirtualCamera m_vc;
    CinemachineBasicMultiChannelPerlin m_perlin;

    public void ShakeCamera(float intensity, float time)
    {
        StartCoroutine(ShakeCameraEnum(intensity, time));
    }

    private void Awake()
    {
        m_vc = GetComponent<CinemachineVirtualCamera>();
        m_perlin = m_vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Instance = this;
    }

    private IEnumerator ShakeCameraEnum(float intensity, float time)
    {
        m_perlin.m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(time);
        m_perlin.m_AmplitudeGain = 0.0f;
    }
}
