using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int playerIndex = 0; // 플레이어 인덱스 (0 또는 1)

    void Start()
    {
        Manager_Move.movePoint[playerIndex] = 3; // 세 번째 칸에 존재
    }

    void Update()
    {
        // 좌우 화살표 키 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Manager_Move.movePoint[playerIndex] > 1)
            {
                Manager_Move.movePoint[playerIndex] -= 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Manager_Move.movePoint[playerIndex] < 11)
            {
                Manager_Move.movePoint[playerIndex] += 1;
            }
        }

        // 현재 위치 시각화
        transform.position = new Vector3((Manager_Move.movePoint[playerIndex] - 6) * 1.5f, 0, 0);
    }
}
