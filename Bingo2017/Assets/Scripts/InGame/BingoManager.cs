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

public class PlayerInfo
{
	public int Bingo;
	public int ClickNumber;
}

public class BingoManager : MonoBehaviour {

	public const int PANELSIZE = 5;						//빙고판의 크기 5*5
	//public PlayerInfo PlayerData;						//플레이어간 넘겨받을 데이터의 변수
	public BlockArray[] blockRow = new BlockArray[PANELSIZE];//블럭25개를 담을 변수
	ArrayList NumberList = new ArrayList();				//1부터 25의 숫자를 담은 ArrayList(초기 랜덤하게 숫자를 정할때 사용)

	//4가지 변수는 12개의 빙고 판별
	int[] LowBingo = new int[PANELSIZE];
	int[] ColumnBingo = new int[PANELSIZE];
   	int diagonalL;
    int diagonalR;
	
	bool[] BingoCheck = new bool[12];		//현재 어디의 빙고가 됐는지 판별
	int BingoCount;							//현재 빙고의 수

	public Button click;
	public Text MyBingo;

	void Start()
	{
		int number = 1;
		click.onClick.AddListener(() => FindNumber(number++));

		blockRow[0].blockColumn = new GameObject[PANELSIZE];
		blockRow[1].blockColumn = new GameObject[PANELSIZE];
		blockRow[2].blockColumn = new GameObject[PANELSIZE];
		blockRow[3].blockColumn = new GameObject[PANELSIZE];
		blockRow[4].blockColumn = new GameObject[PANELSIZE];

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
				blockRow[i].blockColumn[j] = GameObject.Find("Block" + blockCount.ToString());	//찾은 block 오브젝트 담아둠
				blockRow[i].blockColumn[j].transform.FindChild("Text").GetComponent<Text>().text = NumberList[RandomNumber].ToString();
				NumberList.RemoveAt(RandomNumber);
				blockCount++;
			}
		}
	}


	void FindNumber(int blockNum)
	{
		for (int i = 0; i < PANELSIZE; i++)
		{
			for (int j = 0; j < PANELSIZE; j++)
			{
				int tempNum;
				int.TryParse(blockRow[i].blockColumn[j].transform.FindChild("Text").GetComponent<Text>().text, out tempNum);

				if (blockNum == tempNum)
				{
					blockRow[i].blockColumn[j].GetComponent<BlockClick>().ClickBlock();
					Debug.Log(blockRow[i].blockColumn[j].name);
				}
			}
		}
	}

	public void BingoLogic()
	{
		InitLogic();

		for (int i = 0; i < PANELSIZE; i++){
			for (int j = 0; j < PANELSIZE; j++){
				//행 빙고 판별
				if (blockRow[i].blockColumn[j].GetComponent<BlockClick>().getClick()) {
					LowBingo[i]++;
					if (LowBingo[i] == 5)
					{
						BingoCheck[i] = true;
					}
				}
				
				//열 빙고 판별
				if (blockRow[j].blockColumn[i].GetComponent<BlockClick>().getClick()) {
					ColumnBingo[i]++;
					if (ColumnBingo[i] == 5)
					{
						BingoCheck[i + 5] = true;
					}
				}
				
				//왼쪽위 대각선 빙고 판별
				if (i == j) {
					if (blockRow[i].blockColumn[j].GetComponent<BlockClick>().getClick()) {
						diagonalL++;
						//Debug.Log("왼위");
						if (diagonalL == 5)
						{
							BingoCheck[10] = true;
						}
					}
				}
				
				//오른쪽위 대각선 빙고 판별
				if ((i + j) == 4) {
					if (blockRow[i].blockColumn[j].GetComponent<BlockClick>().getClick()) {
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

	void InitLogic()
	{
		Array.Clear(BingoCheck, 0, BingoCheck.Length);
		Array.Clear(LowBingo, 0, LowBingo.Length);
		Array.Clear(ColumnBingo, 0, ColumnBingo.Length);
		diagonalL = 0;
		diagonalR = 0;
	}

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
		MyBingo.text = "Bingo: " + BingoCount.ToString();
	}
}
