using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.InputSystem;

public class TimelineManager : MonoBehaviour
{
    public enum GameMode{Gameplay, Dialogue}

    public static TimelineManager Instance = null;

    public GameMode gameMode = GameMode.Gameplay;

    private PlayableDirector director;
    private DialoguePanel panel;

    UserInput userInput;
    private bool resume = false;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            userInput = new UserInput();

            userInput.Controls.ContinueDialogue.started += OnDialogueContinue;
            userInput.Controls.ContinueDialogue.canceled += OnDialogueContinue;

        }else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        userInput.Controls.Enable();
    }

    void OnDisable()
    {
        userInput.Controls.Disable();
    }

    void Update()
    {
        checkDialogueResume();
    }

    void checkDialogueResume()
    {
        if(resume){
            resumeTimeline();
            resume = false;
        }
    }
 
    void OnDialogueContinue(InputAction.CallbackContext context)
    {
        resume = context.ReadValueAsButton();
    }

    public void pauseTimeline(PlayableDirector director, DialoguePanel panel)
	{
		this.panel = panel;
        this.director = director;
		this.director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
		gameMode = GameMode.Dialogue; 
	}

    public void resumeTimeline()
	{
		director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        panel.endDialogue();
		gameMode = GameMode.Gameplay;
	}
}