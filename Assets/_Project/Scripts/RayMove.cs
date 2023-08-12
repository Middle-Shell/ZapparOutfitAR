using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPaint : MonoBehaviour
{
    public float brushSize = 0.1f;
    public LayerMask paintableLayer;

    private MaterialPropertyBlock propBlock;
    private Texture2D currentTexture;
    private Texture2D tempTexture;

    private void Start()
    {
        propBlock = new MaterialPropertyBlock();
        Renderer renderer = GetComponent<Renderer>();
        currentTexture = renderer.material.mainTexture as Texture2D;
        tempTexture = new Texture2D(currentTexture.width, currentTexture.height);
        PaintOnTexture(tempTexture, Vector2.one, 1, new Color(0,0,0,0));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 centerScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, paintableLayer))
            {
                Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

                if (hitRenderer != null)
                {
                    Vector2 pixelUV = hit.textureCoord;
                    print(pixelUV);

                    // Применяем рисование на временную текстуру
                    PaintOnTexture(tempTexture, pixelUV, brushSize, Color.blue);

                    // Применяем изменения из временной текстуры на текущую
                    currentTexture.SetPixels(tempTexture.GetPixels());
                    currentTexture.Apply();

                    propBlock.SetTexture("_MainTex", currentTexture);
                    hitRenderer.SetPropertyBlock(propBlock);
                }
            }
        }
    }

    // Рисование на временной текстуре
    private void PaintOnTexture(Texture2D texture, Vector2 center, float radius, Color color)
    {
        int centerX = Mathf.FloorToInt(center.x * texture.width);
        int centerY = Mathf.FloorToInt(center.y * texture.height);
        int brushRadius = Mathf.RoundToInt(radius * Mathf.Max(texture.width, texture.height));

        Color[] textureData = texture.GetPixels();
        for (int y = centerY - brushRadius; y <= centerY + brushRadius; y++)
        {
            for (int x = centerX - brushRadius; x <= centerX + brushRadius; x++)
            {
                if (x >= 0 && x < texture.width && y >= 0 && y < texture.height)
                {
                    int index = y * texture.width + x;
                    textureData[index] = color;
                }
            }
        }

        texture.SetPixels(textureData);
        texture.Apply();
    }
}
