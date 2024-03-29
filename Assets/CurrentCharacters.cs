using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentCharacters : MonoBehaviour
{
    public bool isChosing = true;
    public List<InputDevice> inputs;

    public GameObject playerPrefab;

    private void Start()
    {
        if (isChosing)
        {
            inputs = new List<InputDevice>();
        }
    }

    private void FixedUpdate()
    {
        DontDestroyOnLoad(this);
        if (!isChosing)
        {
            print("ischosing false, inpsCount: " + inputs.Count);
            foreach (InputDevice inp in inputs)
            {
                GameObject.Find("PIM").GetComponent<PlayerInputManager>().JoinPlayer(0, 0, "", inp);
                print("joined");
            }

            gameObject.SetActive(false);
            return;
        }
        else
        {
            print("new inputs" + SceneManager.GetActiveScene().buildIndex);
            GameObject.Find("PlayButton").GetComponent<Button>().onClick.AddListener(delegate
            {
                isChosing = false;
                SceneManager.LoadScene(1);
            });
        }
    }

    public void OnConnect(PlayerInput context)
    {
        print("added");
        InputDevice device = new InputDevice();
        device = context.devices[0];
        inputs.Add(device);
    }

    public void OnDisconnect(PlayerInput context)
    {
        print("removed");
        InputDevice device = new InputDevice();
        device = context.devices[0];
        inputs.Remove(device);
    }

}
