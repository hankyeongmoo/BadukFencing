using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("플레이어 기본 설정")]
    public int playerIndex = 0; // 플레이어 인덱스 (0 또는 1)

    [Header("스테미너")]
    public float currentStamina = 200f;
    public float maxStamina = 200f;
    public float defaultStaminaRegenRate = 200f; // 기본 초당 회복량
    public float appliedStaminaRegenRate; // 적용되는 초당 회복량
    public int moveCount = 0; // 이동 횟수

    [Header("UI 요소(스테미너)")]
    public Slider staminaSlider1;
    public Slider staminaSlider2;
    public Image FullStamina;

    [Header("승패 판정")]
    private float timer;
    public SpriteRenderer playerSprite;
    private Color appliedColor;


    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        Manager_Move.PlayerLife[playerIndex] = 5;
        ResetStatus(playerIndex);
        appliedColor = playerSprite.color;
    }

    void Update()
    {
        // 이동 (좌우 화살표 키 입력 처리)
        if (currentStamina > 100f)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Manager_Move.movePoint[playerIndex] > 1 && !Manager_Move.isRoundStarting)
            {
                // 이동할 자리에 상대가 있다면 승리
                if (Manager_Move.movePoint[playerIndex] - 1 == Manager_Move.movePoint[playerIndex == 0 ? 1 : 0])
                {
                    Manager_Move.whoWin = playerIndex;
                }
                // 실제 이동 처리
                Manager_Move.movePoint[playerIndex] -= 1;
                currentStamina -= 100f;
                moveCount += 1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && Manager_Move.movePoint[playerIndex] < 11 && !Manager_Move.isRoundStarting)
            {
                // 이동할 자리에 상대가 있다면 승리
                if (Manager_Move.movePoint[playerIndex] + 1 == Manager_Move.movePoint[playerIndex == 0 ? 1 : 0])
                {
                    Manager_Move.whoWin = playerIndex;
                }
                // 실제 이동 처리
                Manager_Move.movePoint[playerIndex] += 1;
                currentStamina -= 100f;
                moveCount += 1;
            }
        }

        if (Manager_Move.isRoundStarting)
        {
            ResetStatus(playerIndex);
        }

        // 현재 위치 시각화
        transform.position = new Vector3((Manager_Move.movePoint[playerIndex] - 6) * 1.5f, 0, 0);
        
        // 스태미너 UI 업데이트
        if (staminaSlider1 != null && staminaSlider2 != null)
        {
            // 스테미너 슬라이더 값 설정
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

            // 스테미너가 가득 찼을 때 FullStamina 이미지 활성화
            if (currentStamina + appliedStaminaRegenRate * Time.fixedDeltaTime >= 100f && currentStamina < 100f)
            {
                GameObject FS = Instantiate(FullStamina.gameObject);
                FS.transform.SetParent(staminaSlider1.transform, false);
            }
            else if (currentStamina + appliedStaminaRegenRate * Time.fixedDeltaTime >= 200f && currentStamina < 200f)
            {
                GameObject FS = Instantiate(FullStamina.gameObject);
                FS.transform.SetParent(staminaSlider2.transform, false);
            }
        }

        // 스테미너 이동속도 비례 회복량 조정
        if (moveCount > 3)
        {
            if (moveCount > 16)
            {
                appliedStaminaRegenRate = 100f; // 최소 회복량
            }
            else
            {
                appliedStaminaRegenRate = defaultStaminaRegenRate * Mathf.Pow(0.95f, moveCount - 3);
            }
        }
    }

    void FixedUpdate()
    {
        // 스태미너 회복
        if (currentStamina < maxStamina)
        {
            currentStamina += appliedStaminaRegenRate * Time.fixedDeltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        // 비활성화된 플레이어 스프라이트 깜빡임 효과
        if ((Manager_Move.whoSpriteDisabled == playerIndex || Manager_Move.whoSpriteDisabled == 2) && Manager_Move.isRoundStarting)
        {
            timer += Time.fixedDeltaTime;
            appliedColor.a = Mathf.Abs(Mathf.Sin(timer * 5f));
            playerSprite.color = appliedColor;
        }
        else if (!Manager_Move.isRoundStarting)
        {
            appliedColor.a = 1f;
            playerSprite.color = appliedColor;
            timer = 0f;
        }
    }

    public void ResetStatus(int PI)
    {
        currentStamina = maxStamina;
        moveCount = 0;
        appliedStaminaRegenRate = defaultStaminaRegenRate;
        Manager_Move.movePoint[PI] = PI == 0 ? 3 : 9;
    }
}
