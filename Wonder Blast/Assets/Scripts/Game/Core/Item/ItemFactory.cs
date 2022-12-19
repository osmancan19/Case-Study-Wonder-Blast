using Game.Items;
using Settings;
using UnityEngine;
using System.Collections.Generic;


namespace Game.Core.Item
{
    public static class ItemFactory
    {
        private static GameObject _itemBasePrefab;
        private static ImageLibrary _imageLibrary;

        public static void Prepare(ImageLibrary imageLibrary)
        {
            _imageLibrary = imageLibrary;
        }
        
        public static Item CreateItem(ItemType itemType, Transform parent)
        {
            if (_itemBasePrefab == null)
            {
                _itemBasePrefab = Resources.Load("ItemBase") as GameObject;
            }
            
            var itemBase = GameObject.Instantiate(
                _itemBasePrefab, Vector3.zero, Quaternion.identity, parent).GetComponent<ItemBase>();
            
            Item item = null;
            List<Sprite> sprites = new List<Sprite>();
            switch (itemType)
            {
                case ItemType.GreenCube:
                    sprites.Add(_imageLibrary.GreenCubeSprite);
                    sprites.Add(_imageLibrary.GreenCubeBombHintSprite);
                    sprites.Add(_imageLibrary.GreenCubeOrb);
                    sprites.Add(_imageLibrary.GreenCubeRocket);
                    item = CreateCubeItem(itemType, itemBase, sprites);
                    break;
                case ItemType.YellowCube:
                    sprites.Add(_imageLibrary.YellowCubeSprite);
                    sprites.Add(_imageLibrary.YellowCubeBombHintSprite);
                    sprites.Add(_imageLibrary.YellowCubeOrb);
                    sprites.Add(_imageLibrary.YellowCubeRocket);
                    item = CreateCubeItem(itemType, itemBase, sprites);
                    break;
                case ItemType.BlueCube:
                    sprites.Add(_imageLibrary.BlueCubeSprite);
                    sprites.Add(_imageLibrary.BlueCubeBombHintSprite);
                    sprites.Add(_imageLibrary.BlueCubeOrb);
                    sprites.Add(_imageLibrary.BlueCubeRocket);
                    item = CreateCubeItem(itemType, itemBase, sprites);
                    break;
                case ItemType.RedCube:
                    sprites.Add(_imageLibrary.RedCubeSprite);
                    sprites.Add(_imageLibrary.RedCubeBombHintSprite);
                    sprites.Add(_imageLibrary.RedCubeOrb);
                    sprites.Add(_imageLibrary.RedCubeRocket);
                    item = CreateCubeItem(itemType, itemBase, sprites);
                    break;
                case ItemType.PinkCube:
                    sprites.Add(_imageLibrary.PinkCubeSprite);
                    sprites.Add(_imageLibrary.PinkCubeBombHintSprite);
                    sprites.Add(_imageLibrary.PinkCubeOrb);
                    sprites.Add(_imageLibrary.PinkCubeRocket);
                    item = CreateCubeItem(itemType, itemBase, sprites);
                    break;     
                case ItemType.PurpleCube:
                    sprites.Add(_imageLibrary.PurpleCubeSprite);
                    sprites.Add(_imageLibrary.PurpleCubeBombHintSprite);
                    sprites.Add(_imageLibrary.PurpleCubeOrb);
                    sprites.Add(_imageLibrary.PurpleCubeRocket);
                    item = CreateCubeItem(itemType, itemBase, sprites);
                    break;
                default:
                    Debug.LogWarning("Can not create item: " + itemType);
                    break;
             
            }
            
            return item;
        }

        private static Item CreateCubeItem(ItemType itemType, ItemBase itemBase, List<Sprite> sprites)
        {
            var cubeItem = itemBase.gameObject.AddComponent<CubeItem>();
            cubeItem.Prepare(itemBase, sprites, _imageLibrary);
            cubeItem.SetCubeType(itemType);
            
            return cubeItem;
        }
    }
}


