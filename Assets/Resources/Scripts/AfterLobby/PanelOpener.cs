using UnityEngine;

public class PanelOpener: MonoBehaviour
{
    public GameObject Panel;
    public GameObject Dialogue;
    public GameObject Prompt;

    // Update is called once per frame
    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(!Panel.activeSelf);
            Dialogue.SetActive(!Panel.activeSelf);
            Prompt.SetActive(!Panel.activeSelf);
        }


    }
}
