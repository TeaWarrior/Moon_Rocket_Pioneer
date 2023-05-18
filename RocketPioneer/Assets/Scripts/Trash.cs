using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactable;
using Items;
using DG.Tweening;

public class Trash : MonoBehaviour 
{
    [SerializeField] Transform graphyx;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player.PlayerBackpack>()._itemsContainer.RemoveFromBackPack();
            TweenAnimations();
        }
    }

    void TweenAnimations()
    {
        graphyx.DOScale(1.4f, 0.3f).OnComplete(() => { graphyx.DOScale(1f, 0.2f); });
        graphyx.DOLocalJump(Vector3.up, 0.5f, 1, 0.5f);
    }
}
