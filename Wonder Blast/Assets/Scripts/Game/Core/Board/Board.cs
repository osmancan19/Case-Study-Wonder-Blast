using System;
using System.Collections.Generic;
using Game.Mechanics;
using UnityEngine;
using Settings;
using Game.Core.Item;

namespace Game.Core.Board
{
	public class Board : MonoBehaviour
	{
		public const int Rows = 10;
		public const int Cols = 10;
	
		public Cell CellPrefab;
		

		public Transform CellsParent;
		public Transform ItemsParent;
		public Transform ParticlesParent;

		private ImageLibrary _imageLibrary;

		public ImageLibrary ImageLibrary {
			get { return _imageLibrary; }
		}

		[HideInInspector] public Cell[,] Cells = new Cell[Cols, Rows];
	
		public void Prepare(ImageLibrary imageLibrary)
		{
			_imageLibrary = imageLibrary;
			CreateCells();
			PrepareCells();
		}

		private void CreateCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					var cell = Instantiate(CellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
					Cells[x, y] = cell;
				}
			}
		}

		private void PrepareCells()
		{
			for (var y = 0; y < Rows; y++)
			{
				for (var x = 0; x < Cols; x++)
				{
					Cells[x, y].Prepare(x, y, this);
				}
			}
		}

		public void CellTapped(Cell cell)
		{
			if (cell == null) return;
			
			if (!cell.HasItem() || !cell.Item.IsMatchable()) return;

			var tappedItemType = cell.Item.GetItemType();

			switch (tappedItemType) 
			{
                default:
					ExplodeMatchingCells(cell);
                    break;
            }
		}


		private void ExplodeMatchingCells(Cell cell)
		{
			Direction[] directions = new Direction[] {Direction.Up, Direction.Right, Direction.Left, Direction.Down};
				
			var matchFinder = new MatchFinder(Cells);
			var cells = matchFinder.FindMatch(cell, cell.Item.GetItemType());

			if (cells == null) return;

			for (var i = 0; i < cells.Count; i++)
			{
                var explodedCell = cells[i];
				var item = explodedCell.Item;

                
                item.TryExecute();
			}

    }

    public Cell GetNeighbourWithDirection(Cell cell, Direction direction)
		{
			var x = cell.X;
			var y = cell.Y;
			switch (direction)
			{
				case Direction.None:
					break;
				case Direction.Up:
					y += 1;
					break;
				case Direction.UpRight:
					y += 1;
					x += 1;
					break;
				case Direction.Right:
					x += 1;
					break;
				case Direction.DownRight:
					y -= 1;
					x += 1;
					break;
				case Direction.Down:
					y -= 1;
					break;
				case Direction.DownLeft:
					y -= 1;
					x -= 1;
					break;
				case Direction.Left:
					x -= 1;
					break;
				case Direction.UpLeft:
					y += 1;
					x -= 1;
					break;
				default:
					throw new ArgumentOutOfRangeException("direction", direction, null);
			}

			if (x >= Cols || x < 0 || y >= Rows || y < 0) return null;

			return Cells[x, y];
		}
	}

	public class BoardStats {
		public Item.ItemType type;
		public int i;
		public int j;
		public int neighborCount;
		public BoardStats(Item.ItemType _type, int _neighborCount, int _i, int _j) {
			type = _type;
			neighborCount = _neighborCount;
			i = _i;
			j = _j;
		}
	}
}
