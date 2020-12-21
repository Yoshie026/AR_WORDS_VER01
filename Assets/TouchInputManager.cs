using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchInputManager : MonoBehaviour
{
	// Singleton so that input can be accessed anywhere in the Scene.
	public static TouchInputManager instance;

	// Access TouchInputManager.instance.isJumping from anywhere in the Scene that it's needed. For example, your main character script.
	// This example is for a single input (jump) only, but the idea can be customized for more inputs.
	public bool isJumping = false;

	// Keeps track of where the touch started.
	private bool touchIsOverUI = false;

	private GameObject dir_light;
	private bool IsActive = false;

	// Set up the singleton.
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		dir_light = GameObject.Find("Sub Directional Light");
	}

	void Update()
	{
		// I also check for standard "Jump" here since it makes testing possible in Unity.
		// In my case the "Jump" axis is set to spacebar.
		// Also calls GetTouchInput() which is the real point of this code sample.
		isJumping = (Input.GetAxis("Jump") > 0f || GetTouchInput());
	}
	public void OnTap()
	{
		if(!IsActive)
			dir_light.SetActive(true);
		else
			dir_light.SetActive(false);
	}
	
	bool GetTouchInput()
	{

		// Replace GameManager.instance.isPresentingPauseMenu with your own check for disabling touches while another Canvas etc is presenting.
		// Also check to make sure that there are Touches being input, otherwise don't bother.
		if (!(Input.touchCount > 0))
		{
			return false;
		}

		Touch firstTouch = Input.GetTouch(0);

		// This is the key here. Unity's function will only work with TouchPhase.Began.
		if (firstTouch.phase == TouchPhase.Began)
		{

			// Here's the meat of this class. Get the first touch's ID and plug it into IsPointerOverGameObject().
			// Since we already checked for Touch.Phase.Began, this will only return true if the touch is over an EventSystem GUI interactable object like a button.
			if (EventSystem.current.IsPointerOverGameObject(firstTouch.fingerId))
			{
				// Keeps track of where the touch started ... in this case over UI.
				touchIsOverUI = true;
				// EventSystem will react to the touch for us. So do not let the player jump.
				return false;
			}
			else
			{
				// Touch is NOT starting over UI.
				touchIsOverUI = false;
				// Player can jump.
				return true;
			}
		}

		// For everything else -- TouchePhase.Stationary, TouchPhase.Move, TouchPhase.Ended, return the flag we initially stored.
		// Of course ... the name of my flag is "touchIsOverUI" and this function checks to see if the user can jump. So just !invert it.
		return !touchIsOverUI;
	}
}