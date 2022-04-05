using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUpgradeScreen()
    {
        var go = gameObject.transform.Find("UpgradeScreen");
        if(go != null)
        {
            go.gameObject.SetActive(!go.gameObject.active);
            var text = gameObject.transform.Find("Canvas/Button/UpgradeScreenButtonText");
            var t = text.GetComponent<Text>();
            t.text = !go.gameObject.active ? "Upgrade" : "Close";
        }
    }
}
