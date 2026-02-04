using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public int playerIndex = 0; // 플레이어 인덱스 (0 또는 1)

    [Header("스태미너")]
    public float currentStamina = 200f;
    public float maxStamina = 200f;
    public float staminaRegenRate = 200f; // 초당 회복량
    public Slider staminaSlider1;
    public Slider staminaSlider2;

    void Start()
    {
        Manager_Move.movePoint[playerIndex] = 3; // 세 번째 칸에 존재
    }

    void Update()
    {
        // 이동 (좌우 화살표 키 입력 처리)
        if (currentStamina > 100f)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Manager_Move.movePoint[playerIndex] > 1)
            {
                Manager_Move.movePoint[playerIndex] -= 1;
                currentStamina -= 100f;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Manager_Move.movePoint[playerIndex] < 11)
            {
                Manager_Move.movePoint[playerIndex] += 1;
                currentStamina -= 100f;
            }
        }

        // 현재 위치 시각화
        transform.position = new Vector3((Manager_Move.movePoint[playerIndex] - 6) * 1.5f, 0, 0);
        
        // 스태미너 UI 업데이트
        if (currentStamina < 100f)
        {
            staminaSlider1.value = currentStamina / 100f;
            staminaSlider2.value = 0f;
        }
        else if (currentStamina >= 100f)
        {
            staminaSlider1.value = 1f;
            staminaSlider2.value = (currentStamina - 100) / 100f;
        }
    }

    void FixedUpdate()
    {
        // 스태미너 회복
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }
    }
}
