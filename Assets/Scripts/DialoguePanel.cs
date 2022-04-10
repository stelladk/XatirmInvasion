using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialoguePanel : MonoBehaviour
{
    public Image background;
    public TMP_Text dialogue;
    public AudioSource speeker;

    Image radioImage;

    public void Awake()
    {
        radioImage = speeker.gameObject.GetComponent<Image>();

        background.enabled = false;
        radioImage.enabled = false;
        dialogue.text = "";
    }

    public void createDialogue(string speechText, AudioClip sound, float volume)
    {   
        if(speechText != ""){
            background.enabled = true;
            radioImage.enabled = true;
            dialogue.text = speechText;
        }else{
            background.enabled = false;
            dialogue.text = "";
        }

        if(sound != null && !speeker.isPlaying){
            speeker.PlayOneShot(sound, volume);
        }
    }

    public void endDialogue()
    {
        background.enabled = false;
        radioImage.enabled = false;
        dialogue.text = "";
    }

}
