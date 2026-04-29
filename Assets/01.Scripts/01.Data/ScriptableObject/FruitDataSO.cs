using UnityEngine;

[CreateAssetMenu(fileName = "NewFruitData", menuName = "Tanghulu Game/Fruit Data")]
public class FruitData : ScriptableObject
{
    public FruitType type;
    public Sprite fruitSprite;
}