using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class CameraShake : MonoBehaviour
    {
        public static CameraShake Instance { get; private set; }

        private CinemachineVirtualCamera cinemachineVirtualCamera;
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
        private float shakeTimer;
        private float shakeTimerTotal;
        private float startingIntesnity;
        private float frequency;

        private void Awake()
        {
            Instance = this;
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera(float intensity, float frequency, float time)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            startingIntesnity = intensity;
            shakeTimerTotal = time;
            shakeTimer = time;
            this.frequency = frequency;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntesnity, 0f, 1 - (shakeTimer / shakeTimerTotal));
                cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
            }
        }
    }
}
