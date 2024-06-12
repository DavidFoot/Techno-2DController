using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Key : MonoBehaviour, ICollectable
{
    [SerializeField] GameObject m_focusCinemachine;
    [SerializeField] CinemachineVirtualCamera m_cinemachineVirtualCamera;
    private float m_timer;
    private bool getCollected = false;

    public void Collect(PlayerController _player)
    {
        
        m_cinemachineVirtualCamera.gameObject.SetActive(true);
        m_focusCinemachine.GetComponent<SpriteAnimation>().AnimateOnce();
        m_timer = 0f;
        getCollected = true;
    }
    private void Update()
    {
        if (getCollected)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= 3f)
            {
                m_cinemachineVirtualCamera.gameObject.SetActive(false);
                getCollected = false;
                gameObject.SetActive(false);
            }
        }

    }
}
