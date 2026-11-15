using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour, InputSystem_Actions.INPCActions
{
	private InputSystem_Actions _inputAction;
	public List<string> textNPC = new List<string>();
    public GameObject bubbleText;
    public bool imInside;
	public int currentText = 0;
	public TextMeshPro text;

    void Awake() 
    {
		_inputAction = new InputSystem_Actions();
		_inputAction.NPC.SetCallbacks(this);
	}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		text.text = textNPC[currentText];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnEnable()
	{
		_inputAction.Enable();
	}

	void OnDisable()
	{
		_inputAction.Disable();
	}

	public void OnTalk(InputAction.CallbackContext context) 
    {
        if(context.started && currentText < textNPC.Count)
		{
			currentText++;
			text.text = textNPC[currentText];
		}
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        bubbleText.SetActive(true);
        imInside = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		bubbleText.SetActive(false);
        imInside = false;
		currentText = 0;
		text.text = textNPC[currentText];
	}
}
