using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable] //<== MonoBehaviour가 아닌 클래스에 대해 Inspector에 나타납니다.  
public class BlockArray
{
    public GameObject[] blockColumn;   // 하위에 넣어질 배열 변수
}

public class BingoManager : MonoBehaviour
{

    public const int PANELSIZE = 5;                     //빙고판의 크기 상수 5*5
                                                        //public PlayerInfo PlayerData;						//플레이어간 넘겨받을 데이터의 변수
    public BlockArray[] blockRow = new BlockArray[PANELSIZE];//블럭25개를 담을 변수
    ArrayList NumberList = new ArrayList();             //1부터 25의 숫자를 담은 ArrayList(초기 랜덤하게 숫자를 정할때 사용)

    //4가지 변수는 12개의 빙고 판별
    int[] LowBingo = new int[PANELSIZE];
    int[] ColumnBingo = new int[PANELSIZE];
    int diagonalL;
    int diagonalR;

    bool[] BingoCheck = new bool[12];       //현재 어디의 빙고가 됐는지 판별
    int BingoCount;                         //현재 빙고의 수

	int[] PlayerData = new int[2];
    public Text MyBingo;
	public Text OtherBingo;
    PhotonView Trade;

    void Awake()
    {
        //포톤뷰는 간단하게 말해서 클라끼리 통신하는거랄까?
        Trade = GetComponent<PhotonView>();
    }

    
    void Start()
    {
        //배열에 대해서 초기화
        blockRow[0].blockColumn = new GameObject[PANELSIZE];
        blockRow[1].blockColumn = new GameObject[PANELSIZE];
        blockRow[2].blockColumn = new GameObject[PANELSIZE];
        blockRow[3].blockColumn = new GameObject[PANELSIZE];
        blockRow[4].blockColumn = new GameObject[PANELSIZE];

    }

    //게임 초기화
    public void InitGame()
    {
        InitBingoPanel();
        InitLogic();
        CountBingo();
    }

    //빙고와 큐브의 숫자들 초기화
    void InitBingoPanel()
    {
        for (int i = 0; i < 25; i++)
        {
            NumberList.Add(i + 1);
        }
        //현재 있는 블럭에 1~25까지 랜덤하게 번호 부여
        int blockCount = 0;
        int RandomNumber;

        for (int i = 0; i < PANELSIZE; i++)
        {
            for (int j = 0; j < PANELSIZE; j++)
            {
                RandomNumber = UnityEngine.Random.Range(0, NumberList.Count);
                blockRow[i].blockColumn[j] = GameObject.Find("Block" + blockCount.ToString());  //찾은 block 오브젝트 담아둠
                blockRow[i].blockColumn[j].GetComponent<BlockClick>().InitBlock();
                blockRow[i].blockColumn[j].transform.FindChild("Text").GetComponent<Text>().text = NumberList[RandomNumber].ToString();
                NumberList.RemoveAt(RandomNumber);
                blockCount++;
            }
        }
        MyBingo.text = 0.ToString();
        OtherBingo.text = 0.ToString();
    }

    //상대가 블럭을 클릭하고 난 후 내 빙고 로직이 끝나고 내가 빙고가 생겼으면 그걸 보내주는 코드
    public void UpdateOther()
    {
        Trade.RPC("UpdateOtherBingo", PhotonTargets.Others, PlayerData[1]);
    }

    [PunRPC]
    public void UpdateOtherBingo(int otherBingo)
    {
        OtherBingo.text = otherBingo.ToString();
        if (otherBingo >= 5)
        {
            GameManager.Instance.GameLose();
            return;
        }
    }

    //내가 클릭한 숫자 상대에게 보내주는 코드(클릭 숫자와 현재 빙고 상태를 보냄)
    public void SendNumber(int ClickNumber)
    {
		PlayerData[0] = ClickNumber;
        
        Trade.RPC("FindNumber", PhotonTargets.Others, PlayerData);
    }

    //상대가 클릭한 숫자를 내 큐브에서 찾아서 클릭상태로 바꿈
    [PunRPC]
    public void FindNumber(int[] blockNum)
    {
        OtherBingo.text = blockNum[1].ToString();
        if (blockNum[1] >= 5)
        {
            GameManager.Instance.GameLose();
            return;
        }

        //상대방이 클릭한 빙고를 받아서 클릭해줌
        for (int i = 0; i < PANELSIZE; i++)
        {
            for (int j = 0; j < PANELSIZE; j++)
            {
                int tempNum;
                int.TryParse(blockRow[i].blockColumn[j].transform.FindChild("Text").GetComponent<Text>().text, out tempNum);

                if (blockNum[0] == tempNum)
                {
                    blockRow[i].blockColumn[j].GetComponent<BlockClick>().ClickOtherBlock();
                    //Debug.Log(blockRow[i].blockColumn[j].name);
                }
            }
        }
    }

    //빙고인지 판별하는 로직(뭔가 더 최적화나 깔끔하게 바꾸고픈데 내가 c#에 대해서 익숙하지 않은듯...)
    public void BingoLogic()
    {
        InitLogic();

        for (int i = 0; i < PANELSIZE; i++)
        {
            for (int j = 0; j < PANELSIZE; j++)
            {
                //행 빙고 판별
                if (blockRow[i].blockColumn[j].GetComponent<BlockClick>().getClick())
                {
                    LowBingo[i]++;
                    if (LowBingo[i] == 5)
                    {
                        BingoCheck[i] = true;
                    }
                    
                }

                //열 빙고 판별
                if (blockRow[j].blockColumn[i].GetComponent<BlockClick>().getClick())
                {
                    ColumnBingo[i]++;
                    if (ColumnBingo[i] == 5)
                    {
                        BingoCheck[i + 5] = true;
                    }
                }

                //왼쪽위 대각선 빙고 판별
                if (i == j)
                {
                    if (blockRow[i].blockColumn[j].GetComponent<BlockClick>().getClick())
                    {
                        diagonalL++;
                        //Debug.Log("왼위");
                        if (diagonalL == 5)
                        {
                            BingoCheck[10] = true;
                        }
                    }
                }

                //오른쪽위 대각선 빙고 판별
                if ((i + j) == 4)
                {
                    if (blockRow[i].blockColumn[j].GetComponent<BlockClick>().getClick())
                    {
                        diagonalR++;
                        //Debug.Log("오른위");
                        if (diagonalR == 5)
                        {

                            BingoCheck[11] = true;
                        }
                    }
                }
            }
        }
        CountBingo();
    }

    //빙고로직 검사할때 쓰이는 것들 초기화
    void InitLogic()
    {
        Array.Clear(BingoCheck, 0, BingoCheck.Length);
        Array.Clear(LowBingo, 0, LowBingo.Length);
        Array.Clear(ColumnBingo, 0, ColumnBingo.Length);
        diagonalL = 0;
        diagonalR = 0;
    }

    //현재 빙고가 몇개째인지 판별
    void CountBingo()
    {
        BingoCount = 0;
        for (int i = 0; i < 12; i++)
        {
            if (BingoCheck[i])
            {
                BingoCount++;
            }
        }

        MyBingo.text = BingoCount.ToString();
		PlayerData[1] = BingoCount;
        
        //이 부분에서 무언가 순서가 문제인듯...
        //어찌 돌아가긴 함..
        if (BingoCount >= 5)
        {
            GameManager.Instance.GameWin();
        }
    }
}
