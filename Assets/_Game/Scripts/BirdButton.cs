using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdButton : MonoBehaviour
{
    [SerializeField] private string monologueText;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private bool isCorrectOption = false;

    private bool hasPlayed = false;

    public void BirdButtonClick()
    {
        if (isCorrectOption)
        {
            StartCoroutine(CorrectOption());
        }
        else
        {
            if (playOnce && hasPlayed)
                return;

            MonologueManager.Instance.ShowMonologue(monologueText);
            hasPlayed = true;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator CorrectOption()
    {
        MonologueManager.Instance.ShowMonologue(monologueText);
        GameObject questionPanel = GameObject.FindGameObjectWithTag("QuestionPanel");
        questionPanel.SetActive(false);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
