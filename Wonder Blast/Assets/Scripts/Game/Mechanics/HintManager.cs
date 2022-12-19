using Game.Core.Board;
using UnityEngine;
using Settings;
using System.Collections.Generic;
using Game.Core.Item;

namespace Game.Mechanics
{	
	public class HintManager : MonoBehaviour {
		public Board Board;
		private float updatePeriod = 0.4f;
		private float aggregateTime = 0;

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			aggregateTime += Time.deltaTime;
			if (aggregateTime < updatePeriod) 	return;

			ShowHintz();
			aggregateTime = 0;
		}
	
		private bool CoordinatesSafe(int i, int j) {
			return (i < Board.Cols && i >= 0 && j < Board.Rows && j >= 0);
		}
		

		private string markToStr(int[,] mark) {
			string res = "";
			for (int i = 0; i < 10; ++i) {
				for (int j = 0; j < 10; ++j) {
					res += mark[i, j];
					res += " ";
				}
				res += '\n';
			}
			return res;
		}


		private void MarkIsland(List<Cell> island, int[,] mark) {
			if (island == null) return;
			var size = island.Count;
			for (int i = 0; i < size; ++i) {
				var cell = island[i];
				mark[9 - cell.Y, cell.X] = size;
			}
		}

		private int[,] GenerateMarkGrid() {
			int rows = Board.Rows;
			int cols = Board.Cols;
			var cells = Board.Cells;

			// Initialize all cells of mark array to -1.
			int[,] mark = new int[rows, cols];
			for (int i = 0; i < rows; ++i) {
				for (int j = 0; j < cols; ++j)
					mark[i, j] = -1;
			}

			var matchFinder = new MatchFinder(cells);
			for (int i = 0; i < rows; ++i) {
				for (int j = 0; j < cols; ++j) {
					var cell = cells[j, i];
					if (mark[9 - i, j] == -1 && cell.Item != null) {
						var island = matchFinder.FindMatch(cell, cell.Item.GetItemType());
						MarkIsland(island, mark);
					}
				}
			}

			return mark;
		}

		private void ShowHintz() {
			int rows = Board.Rows;
			int cols = Board.Cols;
			var cells = Board.Cells;
	
			var markGrid = GenerateMarkGrid();

			for (int i = 0; i < rows; ++i) {
				for (int j = 0; j < cols; ++j) {	

					//change sprite to orb	
					 if(markGrid[9 - i, j] >= 10){
						
						cells[j, i].Item.ChangeSprite(2);
					}
					//change sprite to rocket				
					else if (markGrid[9 - i, j] >= 8) {
						// Set to hint sprite.
						cells[j, i].Item.ChangeSprite(3);
					}
					//change sprite to bomb	
					else if(markGrid[9 - i, j] >= 5){
						
						cells[j, i].Item.ChangeSprite(1);
					}
					
					// Set to default sprite.
					else{
						cells[j, i].Item.ChangeSprite(0);
					}
				}
			}
			
		}
	}
}

		
	