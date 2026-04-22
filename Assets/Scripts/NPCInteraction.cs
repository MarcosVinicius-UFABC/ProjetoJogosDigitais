using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [TextArea] public string [] textList;
    private int currentIndex = 0;
    public TextMeshPro textMeshPro3D;
    private TextMeshPro inputPrompt;
    public Vector3 inputPromptOffset = new Vector3(-1, 0, 0);
    private TextMeshPro spokenText;
    public Vector3 spokenTextOffset = new Vector3(0, 1, 0);
    public float speechLifetime = 3f;
    private float lifeTime = 0f;
    private bool playerNearby = false;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Entrou");
            inputPrompt = Instantiate(textMeshPro3D);
            inputPrompt.transform.SetParent(gameObject.transform.parent);
            inputPrompt.transform.position += inputPromptOffset;
            inputPrompt.text = "E - Interagir";
            Debug.Log(inputPrompt.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Saiu");
            Destroy(inputPrompt);
            inputPrompt = null;
        }
    }

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame && lifeTime <= 0)
        {
            if (spokenText == null)
            {
                if (inputPrompt != null)
                {
                    inputPrompt.gameObject.SetActive(false);
                }
                spokenText = Instantiate(textMeshPro3D);
                spokenText.transform.SetParent(gameObject.transform.parent);
                spokenText.transform.position += spokenTextOffset;
                Debug.Log(spokenText.transform.position);
            }
            UpdateSpokenText();
        }
        else if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame && lifeTime > 0)
        {
            UpdateSpokenText();
        }
        else if (lifeTime <=0)
        {
            if (spokenText != null)
            {
                Destroy(spokenText);
                spokenText = null;
            }
            if (inputPrompt != null)
                {
                    inputPrompt.gameObject.SetActive(true);
                }
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }
    private void UpdateSpokenText()
    {
        spokenText.text = textList[currentIndex];

        currentIndex ++;
        if (currentIndex >= textList.Length)
        {
            currentIndex = 0;
        }

        lifeTime = speechLifetime;
    }
}
