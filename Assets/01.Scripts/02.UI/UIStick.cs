using System.Collections.Generic;
using System.Linq;

public enum FruitType 
{ 
    None,
    Strawberry, 
    Grape, 
    Tangerine, 
    Apple 
}

public class FruitStick
{
    private List<FruitType> m_ListOfFruits = new();

    private readonly int kVALUE_MAX_CAPACITY = 5;

    public int CurrentCount => m_ListOfFruits.Count;
    public bool IsEmpty => m_ListOfFruits.Count == 0;
    public bool IsFull => m_ListOfFruits.Count >= kVALUE_MAX_CAPACITY;

    public void GetTopFruitGroup(out FruitType type, out int count)
    {
        count = 0;
        type = default;

        if (IsEmpty) 
            return;

        type = m_ListOfFruits.Last();
        count = 1;

        for (int i = m_ListOfFruits.Count - 2; i >= 0; i--)
        {
            if (m_ListOfFruits[i] == type)
            {
                count++;
            }
            else
            {
                break;
            }
        }
    }

    public bool CanAccept(FruitType type, int count)
    {
        if (m_ListOfFruits.Count + count > kVALUE_MAX_CAPACITY) 
            return false;

        if (IsEmpty || m_ListOfFruits.Last() == type)
            return true;

        return false;
    }

    /// <summary>
    /// 다른 스틱으로 과일을 이동시킵니다.
    /// </summary>
    public bool TryMoveTo(FruitStick targetStick)
    {
        if (this == targetStick || IsEmpty) 
            return false;

        GetTopFruitGroup(out FruitType topType, out int groupCount);

        if (targetStick.CanAccept(topType, groupCount))
        {
            m_ListOfFruits.RemoveRange(m_ListOfFruits.Count - groupCount, groupCount);

            for (int i = 0; i < groupCount; i++)
            {
                targetStick.m_ListOfFruits.Add(topType);
            }
            
            return true;
        }

        return false;
    }
}