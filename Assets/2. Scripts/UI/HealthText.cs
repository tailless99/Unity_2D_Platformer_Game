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

        // timeElapsed += Time.deltaTime; �� ���ؼ� �� �����Ӹ��� ���ڰ� ��������
        // timeToFade(1f�̴� 1��)���� �����Ѵٴ� �ǹ�
        if (timeElapsed < timeToFade)
        {
            float newAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            //  1*(0/1) �� �����ؼ� 1*(1/1)�� ������ ����.
            // �� ���İ��� ���� �پ���.

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
