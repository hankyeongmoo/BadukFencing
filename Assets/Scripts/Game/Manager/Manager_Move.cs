using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager_Move : MonoBehaviour
{
    [Header("플레이어 위치")]
    static public int[] movePoint = new int[2] { 0, 0 };

    [Header("플레이어 생명")]
    static public int[] PlayerLife = new int[2] { 5, 5 };
    public Slider lifeSlider_0;
    public Slider lifeSlider_1;

    [Header("타이머")]
    public TextMeshProUGUI timeText;
    public float gameTime = 20f;

    void Update()
    {
        lifeSlider_0.value = 1f - ((float)PlayerLife[0] / 5f);
        lifeSlider_1.value = 1f - ((float)PlayerLife[1] / 5f);

        // 타이머 감소
        if (gameTime > 0f)
        {
            gameTime -= Time.deltaTime;
            if (gameTime < 0f)
            {
                gameTime = 0f;
            }

            int seconds = Mathf.FloorToInt(gameTime + 1f);
            timeText.text = seconds.ToString();
        }

        // 라운드 승패 판정


        // 게임 종료 처리
        if (PlayerLife[0] <= 0 && PlayerLife[1] <= 0)
        {
            Debug.Log("무승부!");
        }
        else if (PlayerLife[0] <= 0)
        {
            Debug.Log("Player 0 승리!");
        }
        else if (PlayerLife[1] <= 0)
        {
            Debug.Log("Player 1 승리!");
        }
    }
}
