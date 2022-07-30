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

    // PlayerInput playerInput;
    // private bool resume = false;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            // playerInput = new PlayerInput();

            // playerInput.Dialogue.Continue.started += OnDialogueContinue;
            // playerInput.Dialogue.Continue.canceled += OnDialogueContinue;

        }else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        checkDialogueResume();
    }

    void checkDialogueResume()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            resumeTimeline();
        }
        // Debug.Log(resume);
        // if(resume){
        //     resumeTimeline();
        // }
    }
 
    // void OnDialogueContinue(InputAction.CallbackContext context)
    // {
    //     resume = context.ReadValueAsButton();
    // }

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