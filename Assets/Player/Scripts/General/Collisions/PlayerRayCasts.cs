
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
    }

    private void EyesRay()
    {
        playerPosition = _pController.transform.position;

        ray = new Ray(playerPosition, _pController.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if(hit.collider.TryGetComponent<RainbowMaterialList>(out RainbowMaterialList rml))
            {
                Debug.Log(GetRandomEnum<EnemyType>());
                Debug.Log(GetRandomEnum<ElementType>());
                Debug.Log(GetRandomEnum<AttackType>());
            }
        }
    }

    private static T GetRandomEnum<T>()
    {
        System.Array values = System.Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Range(0, values.Length));
    }

    public void PlayerRayCastsUpdate()
    {
        EyesRay();
        
    }
}
