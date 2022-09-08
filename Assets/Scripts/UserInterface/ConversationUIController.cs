using TMPro;
using UnityEngine;

public class ConversationUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void LookToScreen()
    {
        transform.eulerAngles = Vector3.zero;
    }

    public void ChangeLabel(string content)
    {
        label.SetText(content);
    }
}
