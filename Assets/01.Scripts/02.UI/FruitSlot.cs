using UnityEngine;

public enum FruitType
{
    None,
    Strawberry,
    Grape,
    Tangerine,
    Apple
}

public class FruitSlot : MonoBehaviour 
{
    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;  
    
    public FruitType m_FruitType { get; private set; } = FruitType.None;
    public float SpriteHeight => m_SpriteRenderer.sprite != null ? m_SpriteRenderer.bounds.size.y : 0f;

    public void Setup(FruitData data)
    {
        if (data == null) 
            return;

        m_FruitType = data.type;
        m_SpriteRenderer.sprite = data.fruitSprite;
    }
}