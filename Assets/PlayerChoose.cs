using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerChoose : MonoBehaviour
{

    public Vector2 movementInput;

    private float timeToMove = 0;
    public List<Image> characters;

    private int currentCharacter;
    private Color color;

    private void Awake()
    {
        characters = new List<Image>();
        for (int i = 0; i < GameObject.Find("Characters").GetComponent<RectTransform>().childCount; i++)
        {
            characters.Add(
                GameObject.Find("Characters").GetComponent<RectTransform>().GetChild(i).GetComponent<Image>());
        }

        currentCharacter = 0;
        
        if (GetComponent<PlayerInput>().playerIndex == 0)
        {
            color = Color.blue;
        }
        else color = Color.red;
        
        foreach (var currentChar in characters)
        {
            currentChar.color = Color.white;
        }
        characters[0].color = color;
        currentCharacter = 0;
    }

    private void Update()
    {
        timeToMove -= Time.deltaTime;
        if ((movementInput.x >= 0.7f || movementInput.x <= -0.7f) && timeToMove <= 0)
        {
            Move();
            timeToMove = 0.1f;
        }
    }

    private void Move()
    {
        if (movementInput.x < -0.7f) movementInput.x = -1; 
        else if (movementInput.x > 0.7f) movementInput.x = 1; 
        
        if (movementInput.y < -0.7f) movementInput.y = -1; 
        else if (movementInput.y > 0.7f) movementInput.y = 1; 
        
        int xInput = (int)movementInput.x;
        int toChar = xInput + currentCharacter;
        
        if (toChar >= characters.Count) toChar = 0;
        else if (toChar < 0) toChar = characters.Count - 1;
        print(xInput + " from " + GetComponent<PlayerInput>().playerIndex);
        if (characters[toChar].color != Color.white)
        {
            xInput = (int)movementInput.x * 2;
            toChar = xInput + currentCharacter;
            if (toChar >= characters.Count) toChar = 0;
            else if (toChar < 0) toChar = characters.Count - 1;
        }

        if (toChar == 0 && characters[0].color != Color.white) toChar = 1;
        
        if(characters[currentCharacter].color == color) characters[currentCharacter].color = Color.white;
        characters[toChar].color = color;
        currentCharacter = toChar;
    }

    public void OnMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
}
