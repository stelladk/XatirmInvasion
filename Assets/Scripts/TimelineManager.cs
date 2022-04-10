using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    public enum GameMode{Gameplay, Dialogue}

    public static TimelineManager Instance = null;

    public GameMode gameMode = GameMode.Gameplay;

    private PlayableDirector director;
    private DialoguePanel panel;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    public void PauseTimeline(PlayableDirector director, DialoguePanel panel)
	{
		this.panel = panel;
        this.director = director;
		this.director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
		gameMode = GameMode.Dialogue; //InputManager will be waiting for a spacebar to resume
	}

    public void ResumeTimeline()
	{
		director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        panel.endDialogue();
		gameMode = GameMode.Gameplay;
	}
}