using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RayPaint : MonoBehaviour
{
    public float brushSize = 0.1f;
    public LayerMask paintableLayer;
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private Camera _camera;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _finalButton;

    private MaterialPropertyBlock m_propBlock;
    private Texture2D m_currentTexture;
    private Texture2D m_tempTexture;
    private Renderer m_hitRenderer;
    private RaycastHit m_hit;
    private Ray m_ray;
    private Vector2 m_pixelUV;
    private int m_totalPixels = 0;
    private int m_paintedPixels = 0;
    public float requiredPercentage = 90f; // Желаемый процент закрашивания.
    private Vector2 m_centerScreenPoint;
    private Color m_paintColor;

    void Start()
    {
        Texture2D texture = new Texture2D(128, 128);
        GetComponent<Renderer>().material.mainTexture = texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color color = Color.cyan;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        SwitchColor("1");
        m_propBlock = new MaterialPropertyBlock();
        Renderer renderer = GetComponent<Renderer>();
        m_currentTexture = renderer.material.mainTexture as Texture2D;
        m_tempTexture = new Texture2D(m_currentTexture.width, m_currentTexture.height);
        m_totalPixels = m_currentTexture.width * m_currentTexture.height;
        m_centerScreenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        _slider.onValueChanged.AddListener(delegate { SwitchBrushSize(); });
        StartCoroutine(CheckPaintedPercentage());

    }

    private void Update()
    {
        //if (Input.GetMouseButton(0))
        {
            m_ray = _camera.ScreenPointToRay(m_centerScreenPoint);//Input.mousePosition);
            Debug.DrawRay(m_ray.origin, m_ray.direction * 100, Color.green);
            if (Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, paintableLayer))
            {
                m_hitRenderer = m_hit.collider.GetComponent<Renderer>();
                //Debug.DrawRay(m_ray.origin, m_ray.direction * 100, Color.green);

                if (m_hitRenderer != null)
                {
                    m_pixelUV = m_hit.textureCoord;

                    // Применяем рисование на временную текстуру
                    PaintOnTexture(m_tempTexture, m_pixelUV, brushSize, m_paintColor);
                    
                    
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
        if (CompareColor(color, m_currentTexture.GetPixel(centerX, centerY)))
        {
            return;
        }

        int brushRadius = Mathf.RoundToInt(radius * Mathf.Max(texture.width, texture.height));

        Color[] textureData = texture.GetPixels();
        if (color.a != 0)
        {
            m_paintedPixels += (brushRadius * 2 + 1) * (brushRadius * 2 + 1);
        }

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
        CheckPaintedPercentage();
    }

    private IEnumerator CheckPaintedPercentage()
    {
        while (true)
        {
            // Проверяем процент закрашенных пикселей
            float paintedPercentage = (m_paintedPixels * 100f) / m_totalPixels;
            _percentText.text = "Закрашено - " + ((int) paintedPercentage > 100 ? 100 : (int) paintedPercentage) + "%";
            yield return new WaitForSeconds(2f);
            if (paintedPercentage >= requiredPercentage)
            {
                _finalButton.SetActive(true);
            }
        }
    }

    public bool CompareColor (Color a, Color b) {
        const float accdelta=0.001f;
        bool result=false;
        if (Mathf.Abs(a.r-b.r)<accdelta)
            if (Mathf.Abs(a.g-b.g)<accdelta)
                if (Mathf.Abs(a.b-b.b)<accdelta) result=true;

        return result;
    }

    public void SwitchColor(string name)
    {
        switch (name)
        {
            case "1":
                m_paintColor = new Color(0.09f, 0.43f, 0.46f, 1.0f);
                break;
            case "2":
                m_paintColor = new Color(0.59f, 0.2f, 0.19f, 1.0f);
                break;
            case "3":
                m_paintColor = new Color(0.76f, 0.27f, 0.14f, 1.0f);
                break;
            case "4":
                m_paintColor = new Color (0.29f, 0.14f, 0.31f, 1.0f);
                break;
        }
    }

    public void SwitchBrushSize()
    {
        brushSize = _slider.value;
    }
}
