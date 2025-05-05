using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdButton : MonoBehaviour
{
    [SerializeField] private string monologueText;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private bool isCorrectOption = false;
    [SerializeField] private AudioClip monologueAudioClip;
    [SerializeField] private AudioSource playerAudioSource;

    private bool hasPlayed = false;

    public void BirdButtonClick()
    {
        MonologueManager.Instance.ShowMonologue(monologueText);
        playerAudioSource.PlayOneShot(monologueAudioClip);
        if (isCorrectOption)
        {
            StartCoroutine(CorrectOption());
        }
        else
        {
            if (playOnce && hasPlayed)
                return;

            hasPlayed = true;
            gameObject.SetActive(false);
        }
    }

    private IEnumerator CorrectOption()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }
}
