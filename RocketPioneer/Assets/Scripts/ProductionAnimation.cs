using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buildings.ProductionBuilding;
using Buildings.ProductionBuilding.States;
using Buildings;

public class ProductionAnimation : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] string animationName;


    public BuildingBase productionBuilding;
    public SuspendedState workingState;
    public BuildingProductionTimer productionTimer;

    private void Start()
    {
       // workingState = productionBuilding.SuspendedState;
        // workingState.OnProductionStart += WorkingState_OnProductionStart;
        productionTimer = productionBuilding._productionTimer;
        productionTimer.OnProductionStart += WorkingState_OnProductionStart;

       
    }

    private void WorkingState_OnProductionStart(object sender, System.EventArgs e)
    {
        playProduction();
    }

    // Start is called before the first frame update


    public void playProduction()
    {
        
       

        animator.Play(animationName, 0, 0);

        
    }
}
