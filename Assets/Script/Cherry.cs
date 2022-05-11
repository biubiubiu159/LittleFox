using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cherry : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerController>().CountCherry();
        Destroy(gameObject);
    }
}
