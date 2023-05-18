using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Items.Container
{
   

    public class ItemsContainer : MonoBehaviour
    {
          public float _containerSize;

        public event EventHandler<bool> OnHaveItems;
        public event EventHandler<bool> OnFullInventory;

        public bool IsFull
        {
            get
            {
                return _items.Count >= _capacity; 
            }
        }
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }
        public int Capacity { get => _capacity; }

        [SerializeField] private ItemType _storeItemType;
        [SerializeField] private int _capacity = 10;
        [SerializeField] private int _sizeX = 3;
        [SerializeField] private int _sizeZ = 3;

        [Inject] private ContainerSlotsFactory _slotsFactory;
       public List<Item> _items = new List<Item>();
        private ContainerSlot[] _slots;
        public void RemoveFromBackPack()
        {
            for (int i = _items.Count-1; i > -1; i--)
            {
                if (_items[i] != null)
                {
                    DetachItemFromSlot(_items[i]);
                    var gameObject = _items[i].GetComponent<Transform>();
                    gameObject.transform.DOScale(0, 0.5f);
                   var go = gameObject.GetComponent<GameObject>();
                    Destroy(go, 0.5f);
                    Debug.Log(gameObject);
                  _items.RemoveAt(i);
  
                }
            }
            if (_items.Count == 0)
            {
                OnHaveItems?.Invoke(this, false);
            }




            OnFullInventory?.Invoke(this, false);

        }
        
        public void Init(ItemType storeItemType)
        {
            _storeItemType = storeItemType;
            _slots = _slotsFactory.CreateSlots(transform, _capacity, _containerSize, _sizeX, _sizeZ);
           
        }

        public bool AddItem(Item item)
        {
            if (IsFull)
            {
                
            }
            else
            {
               
            }
            if (!CanAddItem(item))
            {
                
                return false;
            }

            _items.Add(item);
            
            OnHaveItems?.Invoke(this, true);
            item.GoToSlot(this);
        
            if(_items.Count == _capacity)
            {
               
                OnFullInventory?.Invoke(this, true);
            }

            return true;
        }
        public bool TakeItem(out Item item)
        {
            if (!CanTakeItem())
            {
                item = null;
                return false;
            }

            Item lastItem = _items[_items.Count - 1];
            item = lastItem;
            if(_items.Count == _capacity)
            {
                OnFullInventory?.Invoke(this, true);
            }

            IEnumerable<ContainerSlot> busySlotsWithItem = _slots.Where(x => x.BusyItem == lastItem);

            
            if (busySlotsWithItem.Count() != 0)
            {
                busySlotsWithItem.First().Detach();
                
            }
           

            _items.RemoveAt(_items.Count - 1);

            return true;
        }
        public bool TakeItem(out Item item, ItemType type)
        {
            if (!CanTakeItem())
            {
               
                item = null;
                return false;
            }
            OnFullInventory?.Invoke(this, false);
            IEnumerable<Item> typedItems = _items.Where(x => x.Type == type);
            if (typedItems.Count() == 0)
            {
                item = null;
                return false;
            }

            Item queryTakeItem = typedItems.Last();
            item = queryTakeItem;

            IEnumerable<ContainerSlot> busySlotsWithItem = _slots.Where(x => x.BusyItem == queryTakeItem);
            if (busySlotsWithItem.Count() != 0)
            {
                busySlotsWithItem.First().Detach();
            }

            _items.Remove(queryTakeItem);
            if (_items.Count == 0)
            {
                OnHaveItems?.Invoke(this, false);
            }
            return true;
        }
         

        public bool CanAddItem(Item item)
        {
            
            if (item == null)
            {
                return false;
            }

                

            if (_storeItemType != ItemType.Null)
            {
                if (item.Type != _storeItemType)
                {
                    return false;
                }
            }

            return CanAddItem();
        }
        public bool CanAddItem()
        {
           
            return !IsFull;
        }
        public bool CanTakeItem()
        {
            return _items.Count > 0;
        }

        public ContainerSlot AttachItemToSlot(Item item)
        {
            if (_slots == null || _slots.Length == 0)
                throw new NullReferenceException("Attachment slot is null. Did you initialized this component?");

            IEnumerable<ContainerSlot> availableSlots = _slots.Where(x => x.IsBusy == false);    
            ContainerSlot availableSlot = availableSlots.FirstOrDefault();
            availableSlot.Attach(item);
            return availableSlot;

           
        }
        public void DetachItemFromSlot(Item item)
        {
          
            ContainerSlot itemSlot = _slots.Where(x => x.BusyItem == item).First();
            itemSlot.Detach();
            if (_items.Count == 0)
            {
                OnHaveItems?.Invoke(this, false);

            }
        }
    }
}
