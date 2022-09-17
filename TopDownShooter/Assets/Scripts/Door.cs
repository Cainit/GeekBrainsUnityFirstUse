using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float speedOpen = 1.0f;
    public int NeedKey = -1;

    bool Opend = false;
    float Timer = 0;

    Transform Left;
    Transform Right;

    Vector3 originLeft;
    Vector3 originRight;

    Vector3 destLeft;
    Vector3 destRight;

    void Awake()
    {
        Left = transform.Find("Left");
        Right = transform.Find("Right");

        originLeft = Left.localPosition;
        originRight = Right.localPosition;

        destLeft = originLeft;
        destRight = originRight;

        destLeft.x = destLeft.x + 2.8f;
        destRight.x = destRight.x - 2.8f;
    }

    void Update()
    {
        if (Opend)
            Timer += Time.deltaTime * speedOpen;
        else
            Timer -= Time.deltaTime * speedOpen;

        if(Timer >= 0f && Timer <= 1f)
        {
            Left.localPosition = Vector3.Lerp(originLeft, destLeft, Timer);
            Right.localPosition = Vector3.Lerp(originRight, destRight, Timer);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !Opend)
        {
            if (NeedKey != -1 && !PlayerController.Instance.IsKey(NeedKey))
                return;

            Opend = true;
            if (Timer < 0) Timer = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && Opend)
        {
            Opend = false;
            if (Timer > 0) Timer = 1;
        }
    }
}
