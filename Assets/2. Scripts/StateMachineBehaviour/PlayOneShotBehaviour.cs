using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehaviour : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1.0f;
    public bool playOnEnter = true;
    public bool playOnExit = false;
    public bool playAfterDelay = false;
    public float playDelay = 0.25f;
    
    private float timeSinceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
        timeSinceEntered = 0;
        hasDelayedSoundPlayed = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSinceEntered += Time.deltaTime;
            if(timeSinceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnExit)
        {
           AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
        }
    }

}
