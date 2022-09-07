using TMPro;
using UnityEngine;

public class ConversationUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void ChangeLabel(string content)
    {
        label.SetText(content);
    }
}
