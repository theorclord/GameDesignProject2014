using UnityEngine;
using System.Collections;

public class OpenMenu : MonoBehaviour
{
    public GameObject castleMenu;
    void Update()
    {
        if (Input.GetKeyDown("space"))
            Instantiate(castleMenu);
    }
}