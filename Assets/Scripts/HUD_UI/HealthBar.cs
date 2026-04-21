using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public float barWidth = 1f;
    public float barHeight = 0.5f;
    public float offsetY = -2f;

    private Transform background;
    private Transform fill;

    void Start()
    {
        background = CreateBar("Background", new Color(0.2f, 0f, 0f), 1);
        fill = CreateBar("Fill", Color.red, 2);
    }

    Transform CreateBar(string objName, Color color, int sortOrder)
    {
        GameObject obj = new GameObject(objName);
        obj.transform.SetParent(transform);
        obj.transform.localRotation = Quaternion.identity;

        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();

        SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
        sr.color = color;
        sr.sortingOrder = sortOrder;

        return obj.transform;
    }

    void Update()
    {
        float ratio = Mathf.Clamp01(health.CurrentHealth / health.maxHealth);

        background.localPosition = new Vector3(0f, offsetY, 0f);
        background.localScale = new Vector3(barWidth, barHeight, 1f);

        fill.localScale = new Vector3(barWidth * ratio, barHeight, 1f);
        fill.localPosition = new Vector3(-barWidth * (1f - ratio) / 2f, offsetY, 0f);

        transform.rotation = Quaternion.identity;
    }
}
