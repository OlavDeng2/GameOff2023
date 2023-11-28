using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameVersionText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().SetText("Version " + Application.version);
    }
}
