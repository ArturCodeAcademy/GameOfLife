using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
