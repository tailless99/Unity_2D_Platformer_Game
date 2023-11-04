using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaEffect : MonoBehaviour
{
    public Camera Cam;
    public Transform followTarget; // 플레이어 Transform

    Vector2 startingPosition; // 현재 Transform의 Position
    
    // -1.2, -0.6, -0.24 이 값하고 플레이어의 포지션 Z 값의 차이
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
        // 카메라와 현재 나의 시작 위치의 차이
        Vector2 camMoveSinceStart = (Vector2)Cam.transform.position - startingPosition;
        
        // 따라 다닐 캐릭터(플레이어)와 현 오브젝트의 Z축의 거리값
        float zDistasnceFromTaRGET = transform.position.z - followTarget.transform.position.z;
        
        // 삼황 연산자
        // 조건식 ? (참) : (거짓)
        float clippingPlane = ((Cam.transform.position.z) + zDistasnceFromTaRGET > 0) ?
            Cam.farClipPlane : Cam.nearClipPlane;

        // Mathf.Abs() 절대값 출력 함수
        // -2, 2 = 2
        float parallaFactore = Mathf.Abs(zDistasnceFromTaRGET) / clippingPlane;
        
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaFactore;
        Debug.Log(camMoveSinceStart / parallaFactore);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}
