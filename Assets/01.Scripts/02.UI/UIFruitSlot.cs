using UnityEngine;
using System.Collections.Generic;


public class UIFruitSlot : MonoBehaviour {
    
    public enum FruitType 
    { 
        None,
        Strawberry, 
        Grape, 
        Tangerine, 
        Apple 
    }
    
    public FruitType m_FruitType = FruitType.None;
}
