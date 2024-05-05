using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static string name = "Paco";
    public bool isAttacking = false;
    public bool stun = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StunPlayer(float duration)
    {
        if (!stun)
        {
            StartCoroutine(StunCoroutine(duration));
        }
    }

    IEnumerator StunCoroutine(float duration)
    {
        stun = true;
        isAttacking = false;
        yield return new WaitForSeconds(duration);
        stun = false;
    }
}
