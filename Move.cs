using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Move = 0,
    EaseIn = 1,
    EaseOut = 2,
    EaseInOut = 3
}

public class Move : MonoBehaviour
{
    public MoveType moveType;
    public GameObject go;
    public Vector3 beginPosition;
    public Vector3 endPosition;
    public float moveTimeToEnd;
    public bool isPingpong;

    void Start()
    {
        if (moveType == MoveType.Move)
            StartCoroutine(MoveFunc(go, beginPosition, endPosition, moveTimeToEnd, isPingpong));
        else if (moveType == MoveType.EaseIn)
            StartCoroutine(EaseIn(go, beginPosition, endPosition, moveTimeToEnd, isPingpong));
        else if (moveType == MoveType.EaseOut)
            StartCoroutine(EaseOut(go, beginPosition, endPosition, moveTimeToEnd, isPingpong));
        else if (moveType == MoveType.EaseInOut)
            StartCoroutine(EaseInOut(go, beginPosition, endPosition, moveTimeToEnd, isPingpong));
        
    }
    IEnumerator MoveFunc(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        Vector3 speed = (end - begin) / time;
        Vector3 curPos = begin;

        while (true)
        {   
            curPos = begin + Mathf.PingPong(Time.time, time) * speed;
            gameObject.transform.position = curPos;
            yield return null;
            
            // �ж����ڵ�λ���Ƿ����յ� �Լ��Ƿ�ping pong
            if ((!pingpong) && (end - curPos).sqrMagnitude < 0.001)
            {
                break;
            }
        }
    }

    // ������
    IEnumerator EaseIn(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        Vector3 acc = 2 * (end - begin) / (time * time); // ������ٶ�
        Vector3 curPos = begin;
        float moveTime = 0;
        Vector3 moveBegin = begin;
        Vector3 moveEnd = end;

        while (true)
        {
            moveTime += Time.deltaTime;
            curPos = curPos + Time.deltaTime * acc * moveTime;
            gameObject.transform.position = curPos;
            yield return null;
            if ((moveEnd - curPos).sqrMagnitude < 0.001)
            {
                if (!isPingpong)
                    break;

                // move����ʼ����
                Vector3 tmp = moveBegin;
                moveBegin = moveEnd;
                moveEnd = tmp;

                // ���ٶȷ���ת
                acc = -acc;

                // move time����
                moveTime = 0;
            }
        }
    }

    // �쵽��
    IEnumerator EaseOut(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        Vector3 acc = 2 * (end - begin) / (time * time);
        Vector3 initSpeed = acc * time;
        Vector3 curPos = begin;
        float moveTime = 0;
        Vector3 moveBegin = begin;
        Vector3 moveEnd = end;

        while (true)
        {
            moveTime += Time.deltaTime;
            curPos = curPos + Time.deltaTime * (initSpeed - acc * moveTime);
            gameObject.transform.position = curPos;
            yield return null;
            // ���ɾ����ж� ��ֹif�ڵĴ����ܲ���
            if ((moveEnd - curPos).sqrMagnitude < 0.1)
            {
                if (!isPingpong)
                    break;

                // move����ʼ����
                Vector3 tmp = moveBegin;
                moveBegin = moveEnd;
                moveEnd = tmp;

                // ��ʼ�ٶȷ���ת
                initSpeed = -initSpeed;

                // ���ٶȷ���ת
                acc = -acc;

                // move time����
                moveTime = 0;
            }
        }
    }

    // �����쵽��
    IEnumerator EaseInOut(GameObject gameObject, Vector3 begin, Vector3 end, float time, bool pingpong)
    {
        Vector3 acc = 4 * (end - begin) / (time * time);
        Vector3 curPos = begin;
        Vector3 curSpeed = Vector3.zero;
        Vector3 moveBegin = begin;
        Vector3 moveEnd = end;
        Vector3 middle = (begin + end) / 2;

        bool arrivedMiddle = false;

        while (true)
        {
            curSpeed += acc * Time.deltaTime;
            curPos = curPos + Time.deltaTime * curSpeed;
            gameObject.transform.position = curPos;
            yield return null;

            // �����ٶȷ���ת��
            if (!arrivedMiddle && (middle - curPos).sqrMagnitude < 0.1)
            {
                acc = -acc;
                arrivedMiddle = true;
            }

            if ((moveEnd - curPos).sqrMagnitude < 0.1)
            {
                if (!isPingpong)
                    break;

                // move����ʼ����
                Vector3 tmp = moveBegin;
                moveBegin = moveEnd;
                moveEnd = tmp;

                arrivedMiddle = false;
            }
        }
    }


}
