using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = Vector3.up;
    public float timeToFade = 1f;
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    private Color startColor;
    private float timeElapsed = 0;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        // timeElapsed += Time.deltaTime; 로 인해서 매 프레임마다 숫자가 더해지고
        // timeToFade(1f이니 1초)까지 실행한다는 의미
        if (timeElapsed < timeToFade)
        {
            float newAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            //  1*(0/1) 로 시작해서 1*(1/1)로 끝나는 구문.
            // 즉 알파값이 점점 줄어든다.

            textMeshPro.color = new Color
            {
                r = startColor.r,
                g = startColor.g,
                b = startColor.b,
                a = newAlpha
            };
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
