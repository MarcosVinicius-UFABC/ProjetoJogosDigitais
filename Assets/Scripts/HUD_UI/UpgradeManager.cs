using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    private PlayerXP playerXP;
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private PlayerAttack playerAttack;

    private GameObject overlayCanvas;
    private float damageMultiplier = 1f;
    private AudioSource levelUpSoundEffect;
    private AudioSource backgroundMusic;

    private class UpgradeOption
    {
        public string title;
        public string description;
        public Color color;
        public int minLevel = 1;
        public bool isAttackUpgrade = false;
        public Action apply;
    }

    private List<UpgradeOption> allUpgrades;

    void Start()
    {
        //Substituídos os FindObjectOfType por FindFirstObjectByType
        playerXP = FindFirstObjectByType<PlayerXP>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerController = FindFirstObjectByType<PlayerController>();
        playerAttack = FindFirstObjectByType<PlayerAttack>();

        if (playerXP == null)
        {
            Debug.LogError("UpgradeManager: PlayerXP not found in scene.");
            return;
        }

        BuildUpgradePool();
        playerXP.OnLevelUp += ShowUpgradeUI;

        GameObject XPBar = GameObject.Find("XPBar");
        if (XPBar != null) 
          levelUpSoundEffect = XPBar.GetComponent<AudioSource>();
        
        GameObject camera = GameObject.Find("Main Camera");
        if (camera != null) 
          backgroundMusic = camera.GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        if (playerXP != null)
            playerXP.OnLevelUp -= ShowUpgradeUI;
    }

    void BuildUpgradePool()
    {
        allUpgrades = new List<UpgradeOption>
        {
            new UpgradeOption
            {
                title = "Vital Surge",
                description = "+25 Max HP\nAlso restores 25 HP",
                color = new Color(0.65f, 0.12f, 0.12f),
                apply = () =>
                {
                    playerHealth.AddMax(25f);
                    playerHealth.ReceiveHealing(25f);
                }
            },
            new UpgradeOption
            {
                title = "Swift Steps",
                description = "+1 Move Speed",
                color = new Color(0.1f, 0.35f, 0.65f),
                apply = () => playerController.speed += 1f
            },
            new UpgradeOption
            {
                title = "Power Boost",
                description = "+25% Damage\non all attacks",
                color = new Color(0.65f, 0.42f, 0.05f),
                apply = () =>
                {
                    damageMultiplier += 0.25f;
                    playerAttack.damageMultiplier = damageMultiplier;
                }
            },
            new UpgradeOption
            {
                title = "Orbital Strike",
                description = "Summon orbs that\ncircle and damage\nnearby enemies",
                color = new Color(0.15f, 0.5f, 0.35f),
                minLevel = 3,
                isAttackUpgrade = true,
                apply = () => playerAttack.ActivateOrbitals()
            }
        };
    }

    void ShowUpgradeUI()
    {
        EnsureEventSystem();
        Time.timeScale = 0f;
        backgroundMusic.Pause();
        levelUpSoundEffect.Play();
        BuildUI();
    }

    void EnsureEventSystem()
    {
        //if (FindObjectOfType<EventSystem>() == null)
        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }
    }

    void BuildUI()
    {
        overlayCanvas = new GameObject("UpgradeCanvas");
        Canvas canvas = overlayCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 50;
        overlayCanvas.AddComponent<CanvasScaler>();
        overlayCanvas.AddComponent<GraphicRaycaster>();

        // Semi-transparent dark backdrop
        GameObject bg = MakePanel(overlayCanvas.transform, "Overlay", new Color(0f, 0f, 0f, 0.8f));
        Stretch(bg.GetComponent<RectTransform>());

        // Header texts
        MakeText(overlayCanvas.transform, "LEVEL UP!", 40, FontStyle.Bold, Color.white,
            new Vector2(0f, 0.72f), new Vector2(1f, 1f));
        MakeText(overlayCanvas.transform, "Choose an upgrade", 22, FontStyle.Normal, new Color(0.85f, 0.85f, 0.85f),
            new Vector2(0f, 0.6f), new Vector2(1f, 0.78f));

        // Shuffle and lay out cards
        List<UpgradeOption> statPool   = allUpgrades.FindAll(u => !u.isAttackUpgrade);
        List<UpgradeOption> attackPool = allUpgrades.FindAll(u => u.isAttackUpgrade && u.minLevel <= playerXP.Level);
        Shuffle(statPool);
        Shuffle(attackPool);

        List<UpgradeOption> options = new List<UpgradeOption>();
        if (playerXP.Level % 3 == 0 && attackPool.Count > 0)
        {
            options.Add(attackPool[0]);
            options.AddRange(statPool.GetRange(0, Mathf.Min(2, statPool.Count)));
        }
        else
        {
            options.AddRange(statPool.GetRange(0, Mathf.Min(3, statPool.Count)));
        }

        const float cardW = 310f, cardH = 390f, spacing = 50f;
        float totalW = options.Count * cardW + (options.Count - 1) * spacing;
        float startX = -totalW / 2f + cardW / 2f;

        for (int i = 0; i < options.Count; i++)
            MakeCard(overlayCanvas.transform, options[i], new Vector2(startX + i * (cardW + spacing), -10f), cardW, cardH);
    }

    void MakeCard(Transform parent, UpgradeOption option, Vector2 pos, float w, float h)
    {
        GameObject card = MakePanel(parent, "Card_" + option.title, option.color);
        RectTransform rt = card.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;
        rt.sizeDelta = new Vector2(w, h);

        Button btn = card.AddComponent<Button>();
        btn.targetGraphic = card.GetComponent<Image>();
        ColorBlock cb = btn.colors;
        cb.normalColor = option.color;
        cb.highlightedColor = Lighten(option.color, 0.22f);
        cb.pressedColor = Darken(option.color, 0.15f);
        cb.colorMultiplier = 1f;
        btn.colors = cb;
        btn.onClick.AddListener(() => SelectUpgrade(option));

        // Darker strip at top acts as an icon area
        GameObject strip = MakePanel(card.transform, "Strip", Darken(option.color, 0.28f));
        RectTransform stripRt = strip.GetComponent<RectTransform>();
        stripRt.anchorMin = new Vector2(0f, 0.62f);
        stripRt.anchorMax = Vector2.one;
        stripRt.offsetMin = stripRt.offsetMax = Vector2.zero;

        MakeCardText(card.transform, option.title, 26, FontStyle.Bold, Color.white,
            new Vector2(0f, 0.36f), new Vector2(1f, 0.62f));
        MakeCardText(card.transform, option.description, 18, FontStyle.Normal, new Color(1f, 1f, 1f, 0.9f),
            new Vector2(0f, 0.12f), new Vector2(1f, 0.36f));
        MakeCardText(card.transform, "[ Click to choose ]", 14, FontStyle.Italic, new Color(1f, 1f, 1f, 0.55f),
            new Vector2(0f, 0f), new Vector2(1f, 0.14f));
    }

    void SelectUpgrade(UpgradeOption option)
    {
        option.apply();
        if (option.isAttackUpgrade)
            allUpgrades.Remove(option);
        Destroy(overlayCanvas);
        Time.timeScale = 1f;
        backgroundMusic.UnPause();
    }

    // --- UI helpers ---

    GameObject MakePanel(Transform parent, string name, Color color)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent, false);
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        Image img = obj.AddComponent<Image>();
        img.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), Vector2.one * 0.5f);
        img.color = color;
        return obj;
    }

    void Stretch(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    void MakeText(Transform parent, string content, int size, FontStyle style, Color color,
        Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject obj = new GameObject("Text");
        obj.transform.SetParent(parent, false);
        Text t = obj.AddComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        t.text = content;
        t.fontSize = size;
        t.fontStyle = style;
        t.color = color;
        t.alignment = TextAnchor.MiddleCenter;
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }

    void MakeCardText(Transform parent, string content, int size, FontStyle style, Color color,
        Vector2 anchorMin, Vector2 anchorMax)
    {
        GameObject obj = new GameObject("Text");
        obj.transform.SetParent(parent, false);
        Text t = obj.AddComponent<Text>();
        t.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        t.text = content;
        t.fontSize = size;
        t.fontStyle = style;
        t.color = color;
        t.alignment = TextAnchor.MiddleCenter;
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = new Vector2(8f, 0f);
        rt.offsetMax = new Vector2(-8f, 0f);
    }

    // --- Utilities ---

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            T tmp = list[i]; list[i] = list[j]; list[j] = tmp;
        }
    }

    Color Lighten(Color c, float v) =>
        new Color(Mathf.Clamp01(c.r + v), Mathf.Clamp01(c.g + v), Mathf.Clamp01(c.b + v), c.a);

    Color Darken(Color c, float v) =>
        new Color(Mathf.Clamp01(c.r - v), Mathf.Clamp01(c.g - v), Mathf.Clamp01(c.b - v), c.a);
}
