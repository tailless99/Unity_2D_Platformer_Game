using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;
    

    private void Awake()
    {
        // FindObjectOfType<>()
        // Hierarchy를 전체를 다 뒤져서 가장처음으로 찾는
        // <> 안의 타입에 맞는 것을 찾아서 넣어주는 함수
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.CharacterDamaged += CharacterTookDamage;
        CharacterEvents.CharacterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterDamaged -= CharacterTookDamage;
        CharacterEvents.CharacterHealed -= CharacterHealed;
    }

    public void CharacterTookDamage(GameObject character, int damagereceived)
    {   // 대미지를 받을 때, 대미지를 출력
        
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // 대미지를 받은 곳의 위치를 알기 위해 캔버스위 위치를 확인해야함

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damagereceived.ToString();
    }

    public void CharacterHealed(GameObject character, int damagereceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // 대미지를 받은 곳의 위치를 알기 위해 캔버스위 위치를 확인해야함

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damagereceived.ToString();
    }
}
