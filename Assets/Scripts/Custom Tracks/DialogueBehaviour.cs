using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueBehaviour : PlayableBehaviour
{
    public DialoguePanel panel;
    public string speechText = "";
    public bool pauseGame = false;

    public AudioClip sound;
    public float volume = 1;

    PlayableDirector director;
    bool hasPlayed = false;
    bool pauseScheduled = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Get Track data
        panel = playerData as DialoguePanel;
        
        if (!hasPlayed){
            panel.createDialogue(speechText, sound, volume);
            hasPlayed = true;
            if(Application.isPlaying && pauseGame){
                pauseScheduled = true;
            }
            pauseTimeline();
        }
    }

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        pauseTimeline();
    }

    // void resumeTimeline()
    // {
    //     if(Input.GetKeyDown(KeyCode.Return))
    //     {
    //         TimelineManager.Instance.ResumeTimeline();
    //     }
    // }

    void pauseTimeline(){
        if(panel != null) {
            if(pauseScheduled){
                pauseScheduled = false;
                TimelineManager.Instance.PauseTimeline(director, panel);
            }else{
                panel.endDialogue();
            }
        }
    }

}