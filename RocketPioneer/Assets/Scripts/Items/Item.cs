using Buildings.Storage;
using Items.Container;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Items
{
    public class Item : MonoBehaviour
    {
        public ItemType Type { get => _itemType; }
        public Action<Item> OnDisappear;

        [SerializeField] private ItemType _itemType;
        [SerializeField] private float _itemPositionLerp = 10f;
        [SerializeField] private float _itemRotationLerp = 10f;

        private readonly float _moveEndThreshold = 0.01f;
        private ContainerSlot _currentSlot;

        public void GoToSlot(ItemsContainer container)
        {
            _currentSlot = container.AttachItemToSlot(this);
        }

        private void Start()
        {
            DTween_CreateItem();
        }
        public void Disappear()
        {
            OnDisappear?.Invoke(this);
            Destroy(gameObject);
        }

        public void OnSlotAttach(ContainerSlot slot)
        {
            transform.SetParent(slot.transform);
           // DTween_CreateItem();
        }
        public void OnSlotDetach(ContainerSlot slot)
        {

            _currentSlot = null;
            transform.SetParent(null);
           

        }
       public void DTween_CreateItem()
        {
            transform.DOScale(0, 0);
            //transform.DOJump(Vector3.up, 1f, 1,0.5f);
            transform.DOScale(1.4f, 0.2f).OnComplete(() => { transform.DOScale(1f, 0.1f); });
                
            

        }

        private void Update()
        {
            if (_currentSlot == null)
            {
                return;
            }

            moveToSlot();

            float distanceToTarget = Vector3.Distance(transform.position, _currentSlot.transform.position);
            if (distanceToTarget <= _moveEndThreshold)
            {
                transform.position = _currentSlot.transform.position;
                transform.localRotation = Quaternion.identity;
                _currentSlot = null;
            }
        }
        private void moveToSlot()
            
        {
            /* transform.position = Vector3.Slerp(
                 transform.position,
                 _currentSlot.transform.position,
                 _itemPositionLerp * Time.deltaTime
                 );

             transform.rotation = Quaternion.Lerp(
                 transform.rotation,
                 _currentSlot.transform.rotation,
                 _itemRotationLerp * Time.deltaTime
                 ); 
            */


            transform.position = _currentSlot.transform.position;
              
               transform.rotation = _currentSlot.transform.rotation;
        
        }
    }
}
