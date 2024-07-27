using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //�ϐ��̍쐬
    //��]���Ă����u���b�N���ǂ���
    [SerializeField]
    private bool canRotate = true;
   //�֐��̍쐬
   //�ړ��p

    void Move (Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }
   //�ړ��֐����ĂԊ֐��i4��ށj
   public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }
    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }
    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }
    public void MoveDown()
    {
        Move(new Vector3( 0,-1, 0));
    }
    //��]�p�i�Q��ށj
    public void RotateRight()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }
    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0,  90);
        }
    }
}
