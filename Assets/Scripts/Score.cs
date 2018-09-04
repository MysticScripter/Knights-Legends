using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    private float score;

    private void Start()
    {
        hiscoreText.text = "High Score : " + PlayerPrefs.GetFloat("highscore", 0.0f);
        UpdateScore(0.0f);
    }

    public void UpdateScore(float scoreAdd)
    {
        score += scoreAdd;
        scoreText.text = "Score : " + (object)score;
        if (score >= PlayerPrefs.GetFloat("highscore", 0.0f))
        {
            PlayerPrefs.SetFloat("highscore", score);
            hiscoreText.text = "High Score : " + score;
        }      
    }
}
