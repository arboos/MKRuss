using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    private PlayerInputManager inputManager;
    private InputDevice gamepadDevice;
    private InputDevice keyboardDevice;

    private void Awake()
    {
        if(Input.GetKeyDown(KeyCode.H)) AddPlayers();
    }

    private void AddPlayers()
    {
        inputManager = GameObject.FindObjectOfType<PlayerInputManager>();
        inputManager.JoinPlayer(0, 0, "Main", gamepadDevice);
        inputManager.JoinPlayer(1, 1, "Main", keyboardDevice);
    }
    
    private void Update()
    {
        InputSystem.onDeviceChange += InputSystemOnonDeviceChange;
        print(InputSystem.devices);
    }

    private void InputSystemOnonDeviceChange(InputDevice arg1, InputDeviceChange arg2)
    {
        inputManager.JoinPlayer(0, 0, "Main", arg1);
        print(1);
    }
}
