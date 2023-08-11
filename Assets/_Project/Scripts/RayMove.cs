using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPaint : MonoBehaviour
{
    public Texture2D brushTexture; // Текстура кисти
    public float brushSize = 0.1f; // Размер кисти
    public LayerMask paintableLayer; // Слой, на который можно наносить краску

    private MaterialPropertyBlock propBlock;

    private void Start()
    {
        propBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 centerScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2); // Центр экрана

            Ray ray = Camera.main.ScreenPointToRay(centerScreenPoint);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, paintableLayer))
            {
                Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

                if (hitRenderer != null)
                {
                    Vector2 pixelUV = hit.textureCoord;
                    print(pixelUV);

                    Texture2D currentTexture = hitRenderer.material.mainTexture as Texture2D;
                    Color[] textureData = currentTexture.GetPixels();

                    int textureWidth = currentTexture.width;
                    int textureHeight = currentTexture.height;
                    print(textureHeight);

                    int centerX = Mathf.FloorToInt(pixelUV.x * textureWidth);
                    int centerY = Mathf.FloorToInt(pixelUV.y * textureHeight);
                    print(centerX);

                    int radius = Mathf.RoundToInt(brushSize * Mathf.Max(textureWidth, textureHeight));

                    for (int y = centerY - radius; y <= centerY + radius; y++)
                    {
                        for (int x = centerX - radius; x <= centerX + radius; x++)
                        {
                            if (x >= 0 && x < textureWidth && y >= 0 && y < textureHeight)
                            {
                                int index = y * textureWidth + x;
                                textureData[index] = Color.black; // Измените на нужный цвет
                            }
                        }
                    }

                    Texture2D newTexture = new Texture2D(textureWidth, textureHeight);
                    newTexture.SetPixels(textureData);
                    newTexture.Apply();

                    propBlock.SetTexture("_MainTex", newTexture);
                    hitRenderer.SetPropertyBlock(propBlock);
                }
            }
        }
    }
}
