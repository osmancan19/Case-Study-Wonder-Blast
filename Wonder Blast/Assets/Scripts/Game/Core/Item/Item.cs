using Game.Core.Board;
using Game.Mechanics;
using UnityEngine;
using System.Collections.Generic;
using Settings;

namespace Game.Core.Item
{
	public abstract class Item : MonoBehaviour
	{
		public SpriteRenderer SpriteRenderer;
        public List<Sprite> Sprites;
		public FallAnimation FallAnimation;
        public ImageLibrary _imageLibrary;        


		private Cell _cell;
		public Cell Cell
		{
			get { return _cell; }
			set
			{
				if (_cell == value) return;
				
				var oldCell = _cell;
				_cell = value;
				
				if (oldCell != null && oldCell.Item == this)
				{
					oldCell.Item = null;
				}

				if (value != null)
				{
					value.Item = this;					
					gameObject.name = _cell.gameObject.name + " "+GetItemType();
				}
				
			}
		}

		public abstract ItemType GetItemType();

		public virtual bool IsMatchable()
		{
			return false;
		}
		
		public virtual bool TryExecute()
		{
			Cell.Item = null;
			Cell = null;
			
			Destroy(gameObject);
			return true;
		}

		public void Prepare(ItemBase itemBase, List<Sprite> sprites, ImageLibrary imageLibrary)
		{
            SpriteRenderer = itemBase.SpriteRenderer;
			Sprites = sprites;
            SpriteRenderer.sprite = sprites[0];
            FallAnimation = itemBase.FallAnimation;
			FallAnimation.Item = this;
			_imageLibrary = imageLibrary;           
		}

		public void Fall()
		{
			FallAnimation.FallTo(Cell.GetFallTarget());
		}
		
		public override string ToString()
		{
			return gameObject.name;
		}

		public void SetSprite(Sprite sprite)
		{
			SpriteRenderer.sprite = sprite;
		}

		public void ChangeSprite(int index) {
			if (index >= Sprites.Count) 	return;
			SpriteRenderer.sprite = Sprites[index];
		}
		
	}
}
