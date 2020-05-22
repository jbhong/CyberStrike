using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardEntry : MonoBehaviour
{
    [SerializeField]
    private Text positionField;

    [SerializeField]
    private Text nameField;

    [SerializeField]
    private Text scoreField;

    public void Setup(int _position,  string _user, int _score)
    {
        positionField.text = _position.ToString();

        nameField.text = _user;

        scoreField.text = _score.ToString();
    }
}
