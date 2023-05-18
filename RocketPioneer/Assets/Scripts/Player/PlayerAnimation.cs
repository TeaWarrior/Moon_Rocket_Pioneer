using System;
using UnityEngine;
using Zenject;
using Items.Container;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] string isRun, isHoldingItem;

        [Inject] private Player _player;

        [SerializeField] PlayerMovement movement;
        [SerializeField] ItemsContainer container;


        private void Start()
        {
            
            movement.OnMove += Movement_OnMove;
            container.OnHaveItems += Container_OnHaveItems;
        }

        private void Container_OnHaveItems(object sender, bool value)
        {
            HoldingItems(value);
        }

        private void Movement_OnMove(object sender, bool value)
        {
            Running(value);
        }

        private void Update()
        {
           
        }

        public void Running( bool isRunning)
        {
            _animator.SetBool(isRun, isRunning);
        }
        public void HoldingItems( bool isHolding)
        {
            _animator.SetBool(isHoldingItem, isHolding);
        }
    }
}
