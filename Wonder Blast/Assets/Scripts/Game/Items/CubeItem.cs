using Game.Core.Item;
using UnityEngine;
using Settings;

namespace Game.Items
{
    public class CubeItem : Item
    {
        private ItemType _itemType;
        private ParticleSystem _particleSystem;

        public override ItemType GetItemType()
        {
            return _itemType;
        }

        public override bool IsMatchable()
        {
            return true;
        }

        public void SetCubeType(ItemType colorId)
        {
            _itemType = colorId;
        }

        public override bool TryExecute()
        {
            if (_particleSystem == null) return base.TryExecute();
            
            var particle = Instantiate(
                _particleSystem, transform.position, Quaternion.identity, Cell.Board.ParticlesParent);
            particle.Play();
            
            return base.TryExecute();
        }

        public void SetParticle(ParticleSystem particles)
        {
            _particleSystem = particles;
        }

        public void setToBombHint() {
            var imgLib = base._imageLibrary;
            if (_itemType == ItemType.BlueCube) {
                base.SetSprite(_imageLibrary.BlueCubeBombHintSprite);
            }
        }
    }
}
