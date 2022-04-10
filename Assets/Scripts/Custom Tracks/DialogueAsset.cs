using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using TMPro;

public class DialogueAsset : PlayableAsset
{
    [Header("Dialogue")]
    [SerializeField][TextArea] string speechText = "";

    [Header("Audio")]
    [SerializeField] AudioClip sound;
    [SerializeField][Range(0,1)] float volume = 1;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

        var dialogueBehaviour = playable.GetBehaviour();
        dialogueBehaviour.speechText = speechText;
        dialogueBehaviour.sound = sound;
        dialogueBehaviour.volume = volume;

        return playable;
    }
}