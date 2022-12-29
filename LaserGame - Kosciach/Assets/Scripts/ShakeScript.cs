using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class ShakeScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] CinemachineBasicMultiChannelPerlin _noise;

    private void Awake()
    {
        _noise = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float shakeStrength, float shakeDuration)
    {
        _noise.m_AmplitudeGain = shakeStrength;
        StartCoroutine(StopShake(shakeDuration));
    }


    IEnumerator StopShake(float shakeDuration)
    {
        yield return new WaitForSeconds(shakeDuration);
        _noise.m_AmplitudeGain = 0;
    }
}
