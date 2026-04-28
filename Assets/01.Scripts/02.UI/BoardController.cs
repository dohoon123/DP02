using UnityEngine;

public class BoardController : MonoBehaviour
{
    // 현재 선택된(첫 번째로 터치된) 스틱을 기억할 변수
    private FruitStick selectedStick = null;

    /// <summary>
    /// UI 스틱이 터치되었을 때 호출되는 함수
    /// </summary>
    public void OnStickTouched(FruitStick touchedStick)
    {
        // 1. 첫 번째 터치 (선택된 스틱이 없을 때)
        if (selectedStick == null)
        {
            // 비어있는 스틱은 출발지가 될 수 없으므로 무시
            if (touchedStick.IsEmpty) return;

            selectedStick = touchedStick;
            
            // TODO: 선택 피드백 연출 (예: 맨 위 과일들을 살짝 위로 들어 올리는 애니메이션)
            Debug.Log("첫 번째 스틱 선택됨");
        }
        
        // 2. 두 번째 터치 (이미 선택된 스틱이 있을 때)
        else
        {
            // 방금 선택했던 스틱을 다시 터치했다면 '선택 취소'로 처리
            if (selectedStick == touchedStick)
            {
                selectedStick = null;
                // TODO: 선택 피드백 취소 연출 (들어 올렸던 과일 다시 내리기)
                Debug.Log("스틱 선택 취소");
                return;
            }

            // 다른 스틱을 터치했다면 이동 시도
            bool isMoveSuccess = selectedStick.TryMoveTo(touchedStick);

            if (isMoveSuccess)
            {
                // TODO: 출발지 -> 도착지로 과일이 포물선을 그리며 날아가는 연출
                Debug.Log("과일 이동 성공!");
            }
            else
            {
                // TODO: 이동 불가 피드백 연출 (도착지 스틱이 좌우로 살짝 흔들리며 거부)
                Debug.Log("규칙 위반: 이동 불가");
            }

            // 이동 성공 여부와 상관없이 두 번째 터치가 끝나면 선택 상태 초기화
            selectedStick = null;
        }
    }
}