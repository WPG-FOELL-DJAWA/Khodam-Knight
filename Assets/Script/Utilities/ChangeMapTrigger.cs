using UnityEngine;

public class ChangeMapTrigger : MonoBehaviour
{
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private MapName _mapDestination;
    [SerializeField] private Vector3 _sceneCoordinate;

    private void changeScene(MapName targetScene, Vector3 targetPost)
    {
        _playerSO.lastScene = targetScene;
        _playerSO.lastPosition = targetPost;
        LoadScene.instance.loadScene(targetScene);
    }


    private void OnCollisionEnter(Collision other)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            changeScene(_mapDestination, _sceneCoordinate);
        }
    }
}
