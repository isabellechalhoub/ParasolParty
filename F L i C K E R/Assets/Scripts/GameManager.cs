using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Application.LoadLevel(0);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Event handling when Restart button clicked
	public void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	// Event handling when Exit button clicked - go to main menu
	public void ExitLevel()
	{
		Application.LoadLevel(0);
	}

	// Even handling for Exit game button on main menu
	public void ExitGame()
	{
		Application.Quit();
	}

	// Event handling for Play button on main menu - load level 1
	public void Play()
	{
		Application.LoadLevel(1);
	}
}
