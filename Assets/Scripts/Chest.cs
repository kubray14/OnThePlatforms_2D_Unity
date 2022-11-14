using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chest : MonoBehaviour
{
    Animator anim;
    AudioSource chest;
    bool isOpen, isInstantiate = false;
    [SerializeField] GameObject star;



    void Start()
    {
        anim = GetComponent<Animator>();
        chest = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        
            isOpen = true;
            anim.SetBool("isOpen", isOpen);
  
            if (!isInstantiate)
            {
                chest.Play();
                Instantiate(star, new Vector3(transform.position.x, transform.position.y + 1.75f,transform.position.z),Quaternion.identity);
            }
            isInstantiate = true;
        }  
    }
}
