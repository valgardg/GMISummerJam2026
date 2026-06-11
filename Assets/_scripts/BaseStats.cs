using TMPro;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public GameObject statsPanel;
    public TMP_Text sustenanceText;
    public TMP_Text warmthText;
    public TMP_Text entertainmentText;

    void Update()
    {
        sustenanceText.text = $"Sustenance: {PlayerBase.Instance.sustenanceSupply:F1}";
        warmthText.text = $"Warmth: {PlayerBase.Instance.warmthSupply:F1}";
        entertainmentText.text = $"Entertainment: {PlayerBase.Instance.entertainmentSupply:F1}";

        if (PlayerBase.Instance.IsPlayerInBase)
        {
            statsPanel.SetActive(true);
        }
        else
        {
            statsPanel.SetActive(false);
        }
    }
}
