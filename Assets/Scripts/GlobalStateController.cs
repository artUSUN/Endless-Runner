using UnityEngine;

public class GlobalStateController : MonoBehaviour
{
    [SerializeField] private GameObject input;
    [SerializeField] private Transform gameEndMenu;
    [SerializeField] private float enableGameEndMenuDelay = 1f;
    
    private void Update()
    {
        if (StateBus.GlobalState_GameOver) EventGameOver();
    }

    private void EventGameOver()
    { 
        StopAllCoroutines();
        StateBus.World_BarriersHandler.SwitchToGameOverBarriers();
        StateBus.Player_Data.Collider.enabled = false;
        StateBus.Player_Data.Rigidbody.isKinematic = true;
        StateBus.Player_Data.Animator.enabled = false;
        StateBus.Player_EnableRagdoll += true;
        StateBus.Input_Disable += true;
        StateBus.World_IsGameActive = false;
        StateBus.GameMenu.gameObject.SetActive(false);
        Invoke(nameof(ActivateGameEndMenu), enableGameEndMenuDelay);

        //StateBus.Player_Data.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void ActivateGameEndMenu()
    {
        gameEndMenu.gameObject.SetActive(true);
    }
}
