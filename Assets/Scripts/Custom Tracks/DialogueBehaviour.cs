using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

public class DialogueBehaviour : PlayableBehaviour
{
    public DialoguePanel panel;
    public string speechText = "";
    public bool pause = false;

    public AudioClip sound;
    public float volume = 1;

    bool hasPlayed = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Get Track data
        panel = playerData as DialoguePanel;
        
        if (!hasPlayed){
            panel.createDialogue(speechText, sound, volume);
            hasPlayed = true;
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(panel != null) panel.endDialogue();
    }
}