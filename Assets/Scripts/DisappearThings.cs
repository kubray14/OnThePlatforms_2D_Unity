using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearThings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Destroy(this.gameObject,0.5f);
    }
}
