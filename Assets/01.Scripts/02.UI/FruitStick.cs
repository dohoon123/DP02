using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitStick : MonoBehaviour
{
    [Header("Prefabs & Settings")]
    [SerializeField]
    private FruitSlot m_FruitSlotPrefab;

    [SerializeField]
    private List<FruitData> m_FruitPool = new();

    private List<FruitType> m_ListOfFruits = new();
    private List<FruitSlot> m_InstantiatedSlots = new();

    private readonly int kVALUE_MAX_CAPACITY = 5;

    public int CurrentCount => m_ListOfFruits.Count;
    public bool IsEmpty => m_ListOfFruits.Count == 0;
    public bool IsFull => m_ListOfFruits.Count >= kVALUE_MAX_CAPACITY;

    private void Awake()
    {
        InitSlots();
    }

    private void InitSlots()
    {
        if (m_FruitSlotPrefab == null) 
            return;

        // 1~4개의 슬롯만 랜덤하게 생성 (Random.Range의 int 오버로드는 최댓값 미포함)
        int initialCount = Random.Range(1, 5); 

        for (int i = 0; i < initialCount; i++)
        {
            FruitSlot newSlot = Instantiate(m_FruitSlotPrefab, this.transform);
            FruitData randomData = GetRandomFruitData();
            
            newSlot.Setup(randomData);
            
            // 시각적 오브젝트 리스트와 논리 데이터 리스트 양쪽에 모두 추가 (동기화)
            m_InstantiatedSlots.Add(newSlot);
            m_ListOfFruits.Add(randomData.type);
        }

        // 생성 완료 후 위치 정렬
        RepositionSlots();
    }

    private FruitData GetRandomFruitData()
    {
        int randomIndex = Random.Range(0, m_FruitPool.Count);
        return m_FruitPool[randomIndex];
    }

    /// <summary>
    /// 현재 스틱이 보유한 모든 과일 슬롯의 높이를 누적하여 위치를 재정렬합니다.
    /// </summary>
    public void RepositionSlots()
    {
        float currentY = 0f; 
        float padding = 0.1f; 

        for (int i = 0; i < m_InstantiatedSlots.Count; i++)
        {
            FruitSlot slot = m_InstantiatedSlots[i];
            float halfHeight = slot.SpriteHeight / 2f;
            
            // TODO: 나중에 이 부분을 DOTween을 이용한 애니메이션으로 변경 가능
            slot.transform.localPosition = new Vector3(0, currentY + halfHeight, 0);
            
            currentY += slot.SpriteHeight + padding;
        }
    }

    public void GetTopFruitGroup(out FruitType type, out int count)
    {
        count = 0;
        type = default;

        if (IsEmpty) return;

        type = m_ListOfFruits.Last();
        count = 1;

        for (int i = m_ListOfFruits.Count - 2; i >= 0; i--)
        {
            if (m_ListOfFruits[i] == type) count++;
            else break;
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

    public bool TryMoveTo(FruitStick targetStick)
    {
        if (this == targetStick || IsEmpty) 
            return false;

        GetTopFruitGroup(out FruitType topType, out int groupCount);

        if (targetStick.CanAccept(topType, groupCount))
        {
            // 1. 논리 데이터 이동
            m_ListOfFruits.RemoveRange(m_ListOfFruits.Count - groupCount, groupCount);
            for (int i = 0; i < groupCount; i++)
            {
                targetStick.m_ListOfFruits.Add(topType);
            }

            // 2. 시각적 슬롯(오브젝트) 이동
            // 뽑아낼 슬롯들을 추출
            List<FruitSlot> movingSlots = m_InstantiatedSlots.GetRange(m_InstantiatedSlots.Count - groupCount, groupCount);
            m_InstantiatedSlots.RemoveRange(m_InstantiatedSlots.Count - groupCount, groupCount);

            // 대상 스틱으로 오브젝트 전달
            foreach (var slot in movingSlots)
            {
                // 부모를 새로운 스틱으로 변경
                slot.transform.SetParent(targetStick.transform); 
                targetStick.m_InstantiatedSlots.Add(slot);
            }

            // 3. 양쪽 스틱 모두 좌표 재정렬
            this.RepositionSlots();
            targetStick.RepositionSlots();

            return true;
        }

        return false;
    }
}