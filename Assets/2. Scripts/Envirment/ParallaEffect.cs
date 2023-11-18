using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaEffect : MonoBehaviour
{
    public Camera Cam;
    public Transform followTarget; // �÷��̾� Transform

    Vector2 startingPosition; // ���� Transform�� Position
    
    // -1.2, -0.6, -0.24 �� ���ϰ� �÷��̾��� ������ Z ���� ����
    float startingZ;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z - followTarget.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶�� ���� ���� ���� ��ġ�� ����
        Vector2 camMoveSinceStart = (Vector2)Cam.transform.position - startingPosition;
        
        // ���� �ٴ� ĳ����(�÷��̾�)�� �� ������Ʈ�� Z���� �Ÿ���
        float zDistasnceFromTaRGET = transform.position.z - followTarget.transform.position.z;
        
        // ��Ȳ ������
        // ���ǽ� ? (��) : (����)
        float clippingPlane = ((Cam.transform.position.z) + zDistasnceFromTaRGET > 0) ?
            Cam.farClipPlane : Cam.nearClipPlane;

        // Mathf.Abs() ���밪 ��� �Լ�
        // -2, 2 = 2
        float parallaFactore = Mathf.Abs(zDistasnceFromTaRGET) / clippingPlane;
        
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaFactore;
        Debug.Log(camMoveSinceStart / parallaFactore);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}
