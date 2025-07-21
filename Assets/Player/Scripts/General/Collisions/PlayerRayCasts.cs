
using UnityEngine;

public class PlayerRayCasts
{
    PlayerController _pController;
    float _distance;

    Ray ray;
    Vector3 playerPosition;


    public PlayerRayCasts(PlayerController playerController, float distance)
    {
        _distance = distance;
        _pController = playerController;

        //ray = new Ray(playerPosition, _pController.transform.forward);
    }

    public void EyesRay()
    {
        playerPosition = _pController.transform.position;

        ray = new Ray(playerPosition, _pController.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if(hit.collider.TryGetComponent<RainbowMaterialList>(out RainbowMaterialList rml))
            {
                rml.gameObject.SetActive(false);
            }
        }
    }

    public void PlayerRayCastsUpdate()
    {
        EyesRay();
    }
}
