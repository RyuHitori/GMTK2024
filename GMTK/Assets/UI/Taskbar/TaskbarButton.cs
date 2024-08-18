using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TaskbarButton : MonoBehaviour
{
    public Window target;
    public WindowManager windowManager;

    private void Update()
    {
        if (target != null)
        {
            bool windowActive = target.gameObject.activeSelf;
            transform.Find("Border").gameObject.SetActive(windowActive);
        }
        
    }

    public void OnClick()
    {
        if (target != null)
        {
            bool windowActive = target.gameObject.activeSelf;

            if (windowActive)
            {
                Minimize();
            }
            else
            {
                Maximize();
                Focus();
            }

            
        }
    }

    public void Minimize()
    {
        target.SetTargetScale(0);
        StartCoroutine(DisableWindow());
        
    }

    IEnumerator DisableWindow()
    {
        yield return new WaitForSeconds(0.1f);
        target.gameObject.SetActive(false);
        Unfocus();
    }

    public void Close()
    {
        target.SetTargetScale(0);
        Destroy(target.gameObject, 0.1f);
        Destroy(gameObject, 0.1f);
        windowManager.RemoveWindow(target.id);
    }


    public void Maximize()
    {

        target.gameObject.SetActive(true);
        target.SetTargetScale(1);

        Focus();
    }

    public void Focus()
    {
        target.transform.SetAsLastSibling();
        
    }

    public void Unfocus()
    {
        target.transform.SetAsFirstSibling();
    }
}