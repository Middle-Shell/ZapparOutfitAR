using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPaint : MonoBehaviour
{
    public float brushSize = 0.1f;
    public LayerMask paintableLayer;

    private MaterialPropertyBlock m_propBlock;
    private Texture2D m_currentTexture;
    private Texture2D m_tempTexture;
    private Renderer m_hitRenderer;
    private RaycastHit m_hit;
    private Ray m_ray;
    private Vector2 m_pixelUV;
    
    private void Start()
    {
        m_propBlock = new MaterialPropertyBlock();
        Renderer renderer = GetComponent<Renderer>();
        m_currentTexture = renderer.material.mainTexture as Texture2D;
        m_tempTexture = new Texture2D(m_currentTexture.width, m_currentTexture.height);
        PaintOnTexture(m_tempTexture, Vector2.one, 1, new Color(0,0,0,0));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Vector2 centerScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, paintableLayer))
            {
                m_hitRenderer = m_hit.collider.GetComponent<Renderer>();
                Debug.DrawRay(m_ray.origin, m_ray.direction * 100, Color.green);

                if (m_hitRenderer != null)
                {
                    m_pixelUV = m_hit.textureCoord;

                    // Применяем рисование на временную текстуру
                    PaintOnTexture(m_tempTexture, m_pixelUV, brushSize, new Color(0.2f, 0.01f, 0.38f, 1));

                    // Применяем изменения из временной текстуры на текущую
                    m_currentTexture.SetPixels(m_tempTexture.GetPixels());
                    m_currentTexture.Apply();

                    m_propBlock.SetTexture("_MainTex", m_currentTexture);
                    m_hitRenderer.SetPropertyBlock(m_propBlock);
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
