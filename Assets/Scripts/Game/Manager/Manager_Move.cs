using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Manager_Move : MonoBehaviour
{
    [Header("플레이어 위치")]
    static public int[] movePoint = new int[2] { 0, 0 };

    [Header("플레이어 생명")]
    static public int whoWin = -1; // -1: 진행중, 0: 플레이어0 승리, 1: 플레이어1 승리, 2: 무승부
    static public int[] PlayerLife = new int[2] { 5, 5 };
    public Slider lifeSlider_0;
    public Slider lifeSlider_1;

    [Header("타이머")]
    public TextMeshProUGUI timeText;
    public float gameTime = 20f;

    [Header("라운드 시작 타이머")]
    public TextMeshProUGUI roundStartText;
    public float roundStartTime;
    static public bool isRoundStarting;

    [Header("기타 설정")]
    static public int whoSpriteDisabled = -1; // -1: 없음, 0: 플레이어0, 1: 플레이어1, 2: 모두

    void Start()
    {
        ResetRound();
        whoSpriteDisabled = -1;
    }

    void Update()
    {
        // 라운드 승패 판정
        if (whoWin == 0) // 플레이어 0 승리
        {
            PlayerLife[1] -= 1;
            whoSpriteDisabled = 1;
            ResetRound();
        }
        else if (whoWin == 1) // 플레이어 1 승리
        {
            PlayerLife[0] -= 1;
            whoSpriteDisabled = 0;
            ResetRound();
        }
        else if (whoWin == 2)
        {
            PlayerLife[0] -= 1;
            PlayerLife[1] -= 1;
            whoSpriteDisabled = 2;
            ResetRound();
        }

        // 플레이어 생명 UI 업데이트
        lifeSlider_0.value = 1f - ((float)PlayerLife[0] / 5f);
        lifeSlider_1.value = 1f - ((float)PlayerLife[1] / 5f);

        // 라운드 시작 타이머
        if (isRoundStarting)
        {
            roundStartText.gameObject.SetActive(true);
            roundStartTime -= Time.deltaTime;
            if (roundStartTime <= 0f)
            {
                isRoundStarting = false;
                roundStartText.gameObject.SetActive(false);
            }
            else
            {
                int displayTime = Mathf.CeilToInt(roundStartTime);
                roundStartText.text = displayTime.ToString();
            }
        }

        // 경기 타이머 감소
        if (gameTime > 0f && !isRoundStarting)
        {
            gameTime -= Time.deltaTime;
            if (gameTime < 0f)
            {
                gameTime = 0f;
                whoWin = 2; // 시간 종료 시 무승부 처리
            }

            int seconds = Mathf.CeilToInt(gameTime);
            timeText.text = seconds.ToString();
        }

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

    void ResetRound()
    {
        whoWin = -1;
        isRoundStarting = true;
        roundStartTime = 3f;
        gameTime = 20f;
    }
}
