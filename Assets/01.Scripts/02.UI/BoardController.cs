using UnityEngine;
using UnityEngine.InputSystem; // 새로운 Input System 네임스페이스 추가

public class BoardController : MonoBehaviour
{
    private FruitStick m_SelectedStick = null;

    private void Update()
    {
        // 마우스가 연결되어 있지 않으면 예외 처리
        if (Mouse.current == null) return;

        // 마우스 좌클릭 감지 (구 Input.GetMouseButtonDown(0) 역할)
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 마우스 현재 좌표 가져오기 (구 Input.mousePosition 역할)
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                FruitStick touchedStick = hit.collider.GetComponent<FruitStick>();
                if (touchedStick != null)
                {
                    HandleStickTouch(touchedStick);
                }
            }
        }
    }

    private void HandleStickTouch(FruitStick touchedStick)
    {
        if (m_SelectedStick == null)
        {
            if (touchedStick.IsEmpty) return;

            m_SelectedStick = touchedStick;
            Debug.Log("스틱 선택됨: " + touchedStick.gameObject.name);
        }
        else
        {
            if (m_SelectedStick == touchedStick)
            {
                m_SelectedStick = null;
                Debug.Log("선택 취소");
                return;
            }

            bool isSuccess = m_SelectedStick.TryMoveTo(touchedStick);

            if (isSuccess)
            {
                Debug.Log("이동 성공!");
            }
            else
            {
                Debug.Log("이동 실패: 규칙 위반 또는 공간 부족");
            }

            m_SelectedStick = null;
        }
    }
}