using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[System.Serializable] //<== MonoBehaviour가 아닌 클래스에 대해 Inspector에 나타납니다.  
public class Testitem  
{  
    public Button[] ButtonArray;   // 하위에 넣어질 배열 변수  
}  

public class BingoManager : MonoBehaviour {

	public const int PANELSIZE = 5;
	//public Button[,] ButtonArray = new Button[PANELSIZE, PANELSIZE];
	//public Button[] ButtonArray2 = new Button[PANELSIZE];

	public Testitem[] arr = new Testitem[PANELSIZE];
	public Text bb;
	int bbcount;
	ArrayList NumberList = new ArrayList();
	
	public Button ischeck;
	public Button ischeck2;

   	int diagonalL;
    int diagonalR;
	int[] LowBingo = new int[PANELSIZE];
	int[] ColumnBingo = new int[PANELSIZE];
	bool[] BingoCheck = new bool[12];
	int blockCount;
	
	void Start()
	{
		arr[0].ButtonArray = new Button[PANELSIZE];
		arr[1].ButtonArray = new Button[PANELSIZE];
		arr[2].ButtonArray = new Button[PANELSIZE];
		arr[3].ButtonArray = new Button[PANELSIZE];
		arr[4].ButtonArray = new Button[PANELSIZE];
		
		for (int i = 0; i < 25; i++)
		{
			NumberList.Add(i + 1);
		}
		blockCount = 0;
		for (int i = 0; i < PANELSIZE; i++)
		{
			for (int j = 0; j < PANELSIZE; j++)
			{
				arr[i].ButtonArray[j] = GameObject.Find("Block" + blockCount.ToString()).GetComponent<Button>();
				int temp = UnityEngine.Random.Range(0, NumberList.Count);
				GameObject.Find("Block" + blockCount.ToString()).transform.FindChild("Text").GetComponent<Text>().text = NumberList[temp].ToString();
				NumberList.RemoveAt(temp);
				blockCount++;
			}
		}
		ischeck2.onClick.AddListener(check1);
		Debug.Log(NumberList.Count);
		
	}

	public void check()
	{
		Array.Clear(BingoCheck, 0, BingoCheck.Length);
		Array.Clear(LowBingo, 0, LowBingo.Length);
		Array.Clear(ColumnBingo, 0, ColumnBingo.Length);
		
		diagonalL = 0;
		diagonalR = 0;

		for (int i = 0; i < PANELSIZE; i++){
			for (int j = 0; j < PANELSIZE; j++){
				//행 빙고 판별
				if (arr[i].ButtonArray[j].GetComponent<BlockClick>().IsClick()) {
					LowBingo[i]++;
					if (LowBingo[i] == 5)
					{
						BingoCheck[i] = true;
					}
				}
				
				//열 빙고 판별
				if (arr[j].ButtonArray[i].GetComponent<BlockClick>().IsClick()) {
					ColumnBingo[i]++;
					if (ColumnBingo[i] == 5)
					{
						BingoCheck[i + 5] = true;
					}
				}
				
				//왼쪽위 대각선 빙고 판별
				if (i == j) {
					if (arr[i].ButtonArray[j].GetComponent<BlockClick>().IsClick()) {
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
					if (arr[i].ButtonArray[j].GetComponent<BlockClick>().IsClick()) {
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
		check1();
	}

	void check1()
	{
		bbcount = 0;
		
		//Debug.Log(LowBingoCheck[0]);
		for (int i = 0; i < 12; i++)
		{
			if (BingoCheck[i])
			{
				bbcount++;
			}
		}
		bb.text = "Now Bingo: " + bbcount.ToString();
	}
}
