using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    //�񎟌��z��̍쐬
    private Transform[,] grid;

    //CheckPosition�ɒǋL


    
    [SerializeField]
    private Transform emptySprite;//�{�[�h��p�̎l�p�g�i�[�p
    [SerializeField]
    private int height = 30, width = 10, header = 8;//�{�[�h�̍���,�{�[�h�̕�,�{�[�h�̍����������l
    //�֐��̍쐬

    private void Awake()
    {
        grid = new Transform[width, height];
    }
    private void Start()
    {
        CreateBoard();
    }
    //�{�[�h���쐬����֐��̍쐬
    void CreateBoard()
    {
        if (emptySprite)
        {
            for (int y= 0; y< height -header; y++)
            {
                for(int x=0; x < width; x++)
                {
                    Transform clone = Instantiate(emptySprite,
                        new Vector3(x, y, 0), Quaternion.identity);

                    clone.transform.parent = transform;
                }
            }
        }
    }
    
    //�u���b�N���g���ɂ���̂����肷��֐����ĂԊ֐�
    public bool CheckPosition(Block block)
    {
        foreach (Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);
            if (!BoardOutCheck((int)pos.x, (int)pos.y))
            {
                return false;
            }
            if(BlockCheck((int)pos.x, (int)pos.y, block))
            {
                return false;
            }
        }
        return true;
    }
    //�g���ɂ���̂����肷��֐�
    bool BoardOutCheck(int x,int y)
    {
        //x����0�ȏ�width���� y����0�ȏ�
        return (x >= 0 && x < width && y >= 0);
    }

    //�ړ���Ƀu���b�N���Ȃ������肷��֐�
    bool BlockCheck(int x, int y,Block block)
    {
        //�񎟌��z�񂪋�ł͂Ȃ��̂͑��̃u���b�N���鎞
        //�e���Ⴄ�̂͑��̃u���b�N�����鎞
        return (grid[x, y] != null && grid[x, y].parent != block.transform);
    }
    //�u���b�N���������|�W�V�������L�^����֐�
    public void SaveBlockInGrid(Block block)
    {
        foreach(Transform item in block.transform)
        {
            Vector2 pos = Rounding.Round(item.position);

            grid[(int)pos.x, (int)pos.y] = item;
        }
    }
    //���ׂĂ̍s���`�F�b�N���āA���܂��Ă���΍폜����֐�
    public void ClearAlRows()
    {
        for(int y=0; y < height; y++)
        {
            if (IsComplete(y))
            {
                ClearRow(y);
                //
                ShiftRowsDown(y+1);
                y--;
            }
        }
    }
    //���ׂĂ̍s���`�F�b�N����֐�
    bool IsComplete(int y)
    {
        for(int x=0; x < width; x++)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    //�폜����֐�
    void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
            grid[x, y] = null;
        }
    }
    //��ɂ���u���b�N����i������֐�
    void ShiftRowsDown(int startY)
    {
        for(int y= startY; y<height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if (grid[x,y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y]= null;
                    grid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    public bool OverLimit(Block block)
    {
        foreach(Transform item in block.transform)
        {
            if(item.transform.position.y >= height - header)
            {
                return true;
            }
        }
        return false;
    }
}