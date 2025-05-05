using UnityEngine;

public class CorrectButton : MonoBehaviour
{
    [SerializeField] private GameObject[] otherButtons;

    public void CloseOtherButtons()
    {
        foreach (GameObject button in otherButtons)
        {
            if (button != null)
            {
                button.SetActive(false);
            }
        }
    }
}
