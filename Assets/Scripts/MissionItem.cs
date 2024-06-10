using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : MonoBehaviour
{
    public Image backgroundImage;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI rewardText;
    public GameObject completeTag;

    public Sprite defaultBg;
    public Sprite claimedBg;

    private int id;
    private int reward;

    public void Init(MissionInfo info, bool isCompleted)
    {
        descText.text = info.desc;
        rewardText.text = info.reward.ToString();
        reward = info.reward;
        id = info.id;

        if (isCompleted)
        {
            if (PlayerPrefs.GetInt("MissionStatus" + id) != 2)
            {
                PlayerPrefs.SetInt("MissionStatus" + id, 1);
            }
        }
        
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        int state = PlayerPrefs.GetInt("MissionStatus" + id, 0);
        if (state == 0)
        {
            backgroundImage.sprite = defaultBg;
            completeTag.SetActive(false);
        }
        if (state == 1)
        {
            backgroundImage.sprite = defaultBg;
            completeTag.SetActive(true);
        }
        if (state == 2)
        {
            backgroundImage.sprite = claimedBg;
            completeTag.SetActive(true);
        }
    }

    public void Claim()
    {
        if (PlayerPrefs.GetInt("MissionStatus" + id) != 1)
        {
            return;
        }
        PlayerPrefs.SetInt("MissionStatus" + id, 2);
        MoneyManager.instance.SetMoneyCount(reward);
        
        UpdateStatus();
    }
}