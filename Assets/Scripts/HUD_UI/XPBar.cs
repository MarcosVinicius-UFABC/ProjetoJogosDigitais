using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public PlayerXP playerXP;

    private Image fill;
    private Text levelText;
    private Text killText;

    void Start()
    {
        if (playerXP == null)
        {
            playerXP = FindFirstObjectByType<PlayerXP>();
        }

        GameObject canvasObj = new GameObject("XPCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 10;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        CreateBar(canvasObj.transform, "Background", new Color(0.15f, 0.15f, 0.15f));

        fill = CreateBar(canvasObj.transform, "Fill", new Color(0.2f, 0.5f, 1f));
        fill.type = Image.Type.Filled;
        fill.fillMethod = Image.FillMethod.Horizontal;
        fill.fillOrigin = 0;

        GameObject textObj = new GameObject("LevelText");
        textObj.transform.SetParent(canvasObj.transform, false);
        levelText = textObj.AddComponent<Text>();
        levelText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        levelText.fontSize = 20;
        levelText.color = Color.white;
        levelText.alignment = TextAnchor.MiddleRight;
        ApplyTopBarRect(textObj.GetComponent<RectTransform>());

        GameObject killObj = new GameObject("KillText");
        killObj.transform.SetParent(canvasObj.transform, false);
        killText = killObj.AddComponent<Text>();
        killText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        killText.fontSize = 20;
        killText.color = Color.white;
        killText.alignment = TextAnchor.MiddleLeft;
        RectTransform killRect = killObj.GetComponent<RectTransform>();
        killRect.anchorMin = new Vector2(0f, 1f);
        killRect.anchorMax = new Vector2(1f, 1f);
        killRect.pivot = new Vector2(0.5f, 1f);
        killRect.offsetMin = new Vector2(10f, -85f);
        killRect.offsetMax = new Vector2(-10f, -53f);
    }

    Image CreateBar(Transform parent, string name, Color color)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent, false);

        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();

        Image img = obj.AddComponent<Image>();
        img.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        img.color = color;
        ApplyTopBarRect(obj.GetComponent<RectTransform>());
        return img;
    }

    void ApplyTopBarRect(RectTransform rt)
    {
        rt.anchorMin = new Vector2(0f, 1f);
        rt.anchorMax = new Vector2(1f, 1f);
        rt.pivot = new Vector2(0.5f, 1f);
        rt.offsetMin = new Vector2(10f, -50f);
        rt.offsetMax = new Vector2(-10f, -5f);
    }

    void Update()
    {
        if (playerXP == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) playerXP = p.GetComponent<PlayerXP>();
            if (playerXP == null) return;
        }
        fill.fillAmount = playerXP.CurrentXP / playerXP.xpToNextLevel;
        levelText.text = $"LVL {playerXP.Level}";
        if (GameManager.Instance != null)
            killText.text = $"Kills: {GameManager.Instance.KillCount}";
    }
}
