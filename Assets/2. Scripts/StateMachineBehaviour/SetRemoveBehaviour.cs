using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRemoveBehaviour : StateMachineBehaviour
{
    public float fadetime = 0.5f;
    public float fadeDelay = 0;
    private float timerElapsed = 0;
    private float fadeDelayElapsed = 0;
    SpriteRenderer spriteRenderer;
    GameObject objectToRemove;
    Color startColor;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timerElapsed = 0;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objectToRemove = animator.gameObject;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            timerElapsed += Time.deltaTime;

            float newAlpha = startColor.a * (1 - (timerElapsed / fadetime));

            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if(timerElapsed > fadetime)
            {
                Destroy(objectToRemove);
            }
        }
    }
}
