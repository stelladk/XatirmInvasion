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

    public AudioClip sound;
    public float volume = 1;

    Image background;
    TMP_Text dialogue;
    AudioSource speeker;
    Image radioImage;
    bool hasPlayed = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Get Track data
        panel = playerData as DialoguePanel;
        background = panel.background;
        dialogue = panel.dialogue;
        speeker = panel.speeker;
        radioImage = speeker.gameObject.GetComponent<Image>();

        if(speechText != ""){
            background.enabled = true;
            dialogue.text = speechText;
        }else{
            background.enabled = false;
            dialogue.text = "";
        }

        if(sound != null && !speeker.isPlaying && !hasPlayed){
            hasPlayed = true;
            speeker.PlayOneShot(sound, volume);
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if(panel != null) {
            background.enabled = true;
            radioImage.enabled = true;
            dialogue.text = "";
        }
    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if(panel != null) {
            background.enabled = false;
            radioImage.enabled = false;
            dialogue.text = "";
        }
    }
}