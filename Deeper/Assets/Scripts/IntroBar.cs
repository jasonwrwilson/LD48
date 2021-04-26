using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroBar : MonoBehaviour
{
    public GameManager gameManager;
    public Text titleText;
    public Text recordText;

    public float vanishTimer;
    private float vanishTimerCountdown;

    public float fadeTimer;
    private float fadeTimerCountdown;

    
    // Start is called before the first frame update
    void Start()
    {
        ResetIntro();
    }

    public void ResetIntro()
    {
        fadeTimerCountdown = fadeTimer;
        vanishTimerCountdown = vanishTimer;

        Image image = GetComponent<Image>();
        Color imageColor = image.color;
        imageColor.a = 1;

        Color textColor = recordText.color;
        textColor.a = 1;
        recordText.color = textColor;

        Color titleColor = titleText.color;
        titleColor.a = 1;
        titleText.color = titleColor;
    }

    // Update is called once per frame
    void Update()
    {
        recordText.text = "Current Record: " + gameManager.GetRecordDepth() + "m";

        if (vanishTimerCountdown > 0)
        {
            vanishTimerCountdown -= Time.deltaTime;
            if (vanishTimerCountdown <= 0)
            {
                vanishTimerCountdown = 0;
            }
        }
        else if (fadeTimerCountdown > 0)
        {
            fadeTimerCountdown -= Time.deltaTime;

            if (fadeTimerCountdown <= 0)
            {
                fadeTimerCountdown = 0;
                gameObject.SetActive(false);
            }

            float alpha = fadeTimerCountdown / fadeTimer;

            Image image = GetComponent<Image>();
            Color imageColor = image.color;
            imageColor.a = Mathf.Min(imageColor.a, alpha);

            Color textColor = recordText.color;
            textColor.a = Mathf.Min(textColor.a, alpha);
            recordText.color = textColor;

            Color titleColor = titleText.color;
            titleColor.a = Mathf.Min(imageColor.a, alpha);
            titleText.color = titleColor;
        }
    }
}
