using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


public class GameManager : MonoBehaviour
{
	private float LapsedTime = 0f;

	[FormerlySerializedAs("GameSpeed")] [SerializeField] private float gameSpeed = 1f;

	[SerializeField] private List<LiveCell> liveCells = new List<LiveCell>();

	[SerializeField] private List<LiveCell> testPointToCheck = new List<LiveCell>();

	// Start is called before the first frame update
	void Start()
	{			
		UpdateGameState();
		/*LiveCell zero = new LiveCell(0,0);
		print(GetNeighborCount(liveCells[2]));
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				print(AreCellsNeighbours(zero, new(i-2, j-2)));
			}
		}*/
	}

	void Updatee()
	{
		LapsedTime += Time.deltaTime;
		if (LapsedTime % gameSpeed <= 0.001f)
		{
			print("tick");
			print(liveCells);
			UpdateGameState();
		}
	}

	List<LiveCell> GetCellsToCheck(List<LiveCell> liveCells)
	{
		List<LiveCell> cellsToCheck = new List<LiveCell>();

		foreach (var currentCell in liveCells)
		{
			// yank af, but works for now
			// y  1
			cellsToCheck.Add(new LiveCell(currentCell.x + 1, currentCell.y + 1));
			cellsToCheck.Add(new LiveCell(currentCell.x, currentCell.y + 1));
			cellsToCheck.Add(new LiveCell(currentCell.x - 1, currentCell.y + 1));

			// y  0 
			cellsToCheck.Add(new LiveCell(currentCell.x + 1, currentCell.y));
			cellsToCheck.Add(new LiveCell(currentCell.x, currentCell.y));
			cellsToCheck.Add(new LiveCell(currentCell.x - 1, currentCell.y));

			// y -1
			cellsToCheck.Add(new LiveCell(currentCell.x + 1, currentCell.y - 1));
			cellsToCheck.Add(new LiveCell(currentCell.x, currentCell.y - 1));
			cellsToCheck.Add(new LiveCell(currentCell.x - 1, currentCell.y - 1));
		}
				
		// remove duplicates and return
		return cellsToCheck.Distinct().ToList();
	}
	
	bool AreCellsNeighbours(LiveCell cellOne, LiveCell cellTwo)
	{
		return Mathf.Abs(cellOne.x - cellTwo.x) <= 1 && Mathf.Abs(cellOne.y - cellTwo.y) <= 1;
	}

	bool IsCellAlive(LiveCell newCell)
	{
		return liveCells.Any(c => c.x == newCell.x && c.y == newCell.y);
	}

	int GetNeighborCount(LiveCell newCell)
	{
		return liveCells.Count(cell => AreCellsNeighbours(cell, newCell)) - 1;
	}
	
	private List<LiveCell> GetNewGameState(List<LiveCell> cellsToCheck)
	{ 
		List<LiveCell>newLiveCells = new List<LiveCell>();
		foreach (var newCell in cellsToCheck)
		{
			int neighborCount = GetNeighborCount(newCell);
			print($"{neighborCount} neighbors found, {newCell.x}, {newCell.y}");
			if (IsCellAlive(newCell))
			{
				if (neighborCount == 3 || neighborCount == 2)
				{
					newLiveCells.Add(newCell);
				}
			}
			else
			{
				if (neighborCount == 3)
				{
					newLiveCells.Add(newCell);
				}
			}
		}
		return newLiveCells;
	}

	void UpdateGameState()
	{
		// make list of positions that have at least one live cell neighboring,
		List<LiveCell> cellsToCheck = GetCellsToCheck(liveCells);
		foreach (var test in cellsToCheck)
		{
			print($"test: {test.x}, {test.y}");
		}
		
		// make list of new game state
		liveCells = GetNewGameState(cellsToCheck);
	}
}

[System.Serializable]
public struct LiveCell
{
	public int x, y;

	public LiveCell(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}