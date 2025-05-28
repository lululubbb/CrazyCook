using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Image using 仍然可以保留，以防万一，但此脚本不再直接操作它

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    // [SerializeField] private Image progressImage; // <-- 注释掉或删除这一行
    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            // progressImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized(); // <-- 注释掉或删除这一行

            // 更新时间文本的逻辑保持不变，或者你可以调整格式
            float remainingTime = GameManager.Instance.GetGamePlayingTimer();
            if (remainingTime < 0) remainingTime = 0; // 避免显示负数

            // 示例：格式化为 MM:SS
            // int minutes = Mathf.FloorToInt(remainingTime / 60);
            // int seconds = Mathf.FloorToInt(remainingTime % 60);
            // timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // 或者，如果你的 GameManager.GetGamePlayingTimer() 返回的就是适合直接显示的秒数
            timeText.text = Mathf.CeilToInt(remainingTime).ToString();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            Show();
        }
        else // 当游戏不是进行中状态时 (例如倒计时、游戏结束)，也应该隐藏游戏时钟
        {
            Hide();
        }
    }

    private void Show()
    {
        if (uiParent != null) uiParent.SetActive(true);
        else Debug.LogWarning("GameClockUI: uiParent is not assigned!"); // 最好在 Inspector 中正确设置
    }

    private void Hide()
    {
        if (uiParent != null) uiParent.SetActive(false);
        else Debug.LogWarning("GameClockUI: uiParent is not assigned!");
    }

    // 别忘了在 OnDestroy 中取消事件订阅，避免内存泄漏
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
        }
    }
}