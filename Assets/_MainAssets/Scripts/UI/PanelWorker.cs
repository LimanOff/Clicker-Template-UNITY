using UnityEngine;

public class PanelWorker : MonoBehaviour
{
    public void ClosePanel(GameObject panel)
    {
        panel.Deactivate();
    }

    public void OpenPanel(GameObject panel)
    {
        panel.Activate();
    }
}
