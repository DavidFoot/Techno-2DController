using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;


public class Sword : MonoBehaviour, ICollectable
{
    public void Collect(PlayerController _playerController)
    {
        _playerController.getSword();
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            // Attack ? 
        }
    }

}
