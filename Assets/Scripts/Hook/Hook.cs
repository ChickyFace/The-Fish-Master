using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    public Transform hookedTransform;

    private Camera mainCamera;
    private Collider2D coll;

    private int length;
    private int strength;
    private int fishCount;

    private bool canMove;

    //List<fish>

    private Tweener cameraTween;

    void Awake()
    {
        mainCamera = Camera.main;
        coll = GetComponent<Collider2D>();

    }
    void Update()
    {
     if (canMove && Input.GetMouseButton(0))
        {
            Vector3 vector = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 position = transform.position;
            position.x = vector.x;
            transform.position = position;

        }   
    }

    public void StartFishing()
    {
        length = -70; // IdleManager
        strength = 3; // IdleManager
        fishCount = 0;
        float time = (-length) * 0.1f;

        cameraTween = mainCamera.transform.DOMoveY(length, 1 + time * 0.25f, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y <= -10.1f)
                transform.SetParent(mainCamera.transform);
        }).OnComplete(delegate
        {
            coll.enabled = true;
            cameraTween = mainCamera.transform.DOMoveY(0, time * 2, false).OnUpdate(delegate
              {
                  if (mainCamera.transform.position.y >= -17f)
                      StopFishing();
              });
        });
        //ScreenGAME
        coll.enabled = false;
        canMove = true;
        //clearhook 

    }
    void StopFishing()
    {
        canMove = false;
        cameraTween.Kill(false);
        cameraTween = mainCamera.transform.DOMoveY(0, 2, false).OnUpdate(delegate
        {
            if (mainCamera.transform.position.y >= -10.1f)
            {
                transform.SetParent(null);
                transform.position = new Vector2(transform.position.x, -5.1f);
            }
        }).OnComplete(delegate
        {
            transform.position = Vector2.down * 5.1f;
            coll.enabled = true;
            int num = 0;
            //clearing out the hook fishes
            //idleManager totalgain =num
            //screenmanager endscreen.
        });



    }
}

