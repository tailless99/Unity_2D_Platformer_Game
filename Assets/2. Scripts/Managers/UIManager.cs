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
        // Hierarchy�� ��ü�� �� ������ ����ó������ ã��
        // <> ���� Ÿ�Կ� �´� ���� ã�Ƽ� �־��ִ� �Լ�
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
    {   // ������� ���� ��, ������� ���
        
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // ������� ���� ���� ��ġ�� �˱� ���� ĵ������ ��ġ�� Ȯ���ؾ���

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damagereceived.ToString();
    }

    public void CharacterHealed(GameObject character, int damagereceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // ������� ���� ���� ��ġ�� �˱� ���� ĵ������ ��ġ�� Ȯ���ؾ���

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damagereceived.ToString();
    }
}
