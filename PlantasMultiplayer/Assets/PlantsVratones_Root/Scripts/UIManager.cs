using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("General UI Refernces")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameCompleted)
        {
            AudioManager.instance.PlaySFX(4);
            winPanel.SetActive(true);
        }
        else
        {
            winPanel.SetActive(false);
        }

        if (GameManager.Instance.gameOver)
        {
            losePanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(false);
        }
    }
}
