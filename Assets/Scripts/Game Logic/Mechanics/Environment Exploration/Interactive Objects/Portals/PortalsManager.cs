using UnityEngine;

public class PortalsManager
{
    private GameBootstrap gameManager;

    public void InteractDungeonPortal(DungeonPortal portal)
    {
        switch (portal.PortalState)
        {
            case PortalState.Unstable:
                /*if (gameManager.StabilizerCreatedAndAvailable)
                {
                    inventoryManager.RemoveItem("������������ �������");
                    GetComponent<Collider>().enabled = false;
                    //FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                    StartCoroutine(StabilizePortal_COR());
                    //PortalState = PortalState.Stabilizing;
                }
                else
                {
                    GetComponent<Collider>().enabled = false;
                    //FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                    //levelFinisher.Activate();
                }
                break;*/
                break;

            case PortalState.Stable:
                GetComponent<Collider>().enabled = false;
                //FindObjectOfType<InteractiveObjectPanel>().DisableUI();
                //levelFinisher.FinishGame();
                break;
        }
    }
}
