using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移のライブラリ

public class GameManager : MonoBehaviour
{
   //変数の作成
   
   
   Spawner spawner;//スポナー
   Block activeBlock;//生成されたブロック格納

   [SerializeField]
   private float dropInterval = 0.25f;//次にブロックが落ちるまでのインターバル時間
   float nextdropTimer;//次にブロックが落ちるまでの時間

    //ボードのスクリプトを格納
    Board board;

    //入力受付タイマー（３種類）
    float nextKeyDownTimer, nextKeyLeftRightTimer, nextKeyRotateTimer;
    //入力インターバル（３種類）
    [SerializeField]
    private float nextKeyDownInterval, nextKeyLeftRightInterval, nextKeyRotateInterval;

    //パネルの格納
    [SerializeField]
    private GameObject gameOverPanel;
    //ゲームオーバー判定
    bool gameOver;
    
    
    

    private void Start()
   {
        //スポナーオブジェクトをスポナー変数に格納するコードの記述
        spawner = GameObject.FindObjectOfType<Spawner>();
        //ボードを変数に格納する
        board = GameObject.FindObjectOfType<Board>();

        spawner.transform.position=Rounding.Round(spawner.transform.position);

        //タイマーの初期設定
        nextKeyDownTimer = Time.time + nextKeyDownInterval;
        nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
        nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

        //スポナークラスからブロック生成関数を読んで変数に格納する
        if (!activeBlock)
        {
            activeBlock = spawner.SpawnBlock();
        }
        //ゲームオーバーパネルの非表示設定
        if (gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(false);
        }
   }
    private void Update()
    {

        if (gameOver)
        {
            return;
        }
        PlayerInput();

        //Updateで時間の判定をして判定次第で落下関数を呼ぶ

        /*
        if (Time.time> nextdropTimer)
        {
            nextdropTimer = Time.time + dropInterval;

            if (activeBlock)
            {
                activeBlock.MoveDown();

                //UpdateでBoardクラスの関数を呼び出してボードから出ていないか確認
                if (!board.CheckPosition(activeBlock))
                { 
                    activeBlock.MoveUp();

                    board.SaveBlockInGrid(activeBlock);

                    activeBlock = spawner.SpawnBlock();
                }
            }
        }
        */

    }
    //キーの入力を検知してブロックを動かす関数
    //ボードの底について時に次のブロックを生成する関数
    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.D) && (Time.time>nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.D))
        {
            activeBlock.MoveRight();//右に動かす

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.MoveLeft();
            }
        }
        else if (Input.GetKey(KeyCode.A) && (Time.time > nextKeyLeftRightTimer)
            || Input.GetKeyDown(KeyCode.A))
        {
            activeBlock.MoveLeft();//左に動かす

            nextKeyLeftRightTimer = Time.time + nextKeyLeftRightInterval;
            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.MoveRight();
            }
        }
        else if(Input.GetKey(KeyCode.E) && (Time.time > nextKeyRotateTimer))
        {
            activeBlock.RotateRight();
            nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.RotateLeft();
            }
        }
        else if (Input.GetKey(KeyCode.Q) && (Time.time > nextKeyRotateTimer))
        {
            activeBlock.RotateLeft();
            nextKeyRotateTimer = Time.time + nextKeyRotateInterval;

            if (!board.CheckPosition(activeBlock))
            {
                activeBlock.RotateRight();
            }
        }
        else if (Input.GetKey(KeyCode.S) && (Time.time > nextKeyDownTimer)
            || (Time.time>nextdropTimer))
        {
            activeBlock.MoveDown();//下に動かす

            nextKeyDownTimer = Time.time + nextKeyDownInterval;
            nextdropTimer = Time.time + dropInterval;
            if (!board.CheckPosition(activeBlock))
            {
                if (board.OverLimit(activeBlock))
                {
                    GameOver();
                }
                else
                {
                    //底についた時の処理
                    BottomBoard();
                }
                
            }
        }

        void BottomBoard()
        {
            activeBlock.MoveUp();
            board.SaveBlockInGrid(activeBlock);

            activeBlock = spawner.SpawnBlock();

            nextKeyDownTimer = Time.time;
            nextKeyLeftRightTimer = Time.time;
            nextKeyRotateTimer = Time.time;

            board.ClearAlRows();
        }

    }

    //ゲームオーバーになったらパネルを表示する
    void GameOver()
    {
        activeBlock.MoveUp();

        //ゲームオーバーパネルの表示設定
        if (!gameOverPanel.activeInHierarchy)
        {
            gameOverPanel.SetActive(true);
        }

        gameOver = true;
    }
    //シーンを再読み込みする
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
