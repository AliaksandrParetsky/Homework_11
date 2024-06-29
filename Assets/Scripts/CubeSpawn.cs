using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    [SerializeField] private Transform prefabCube;
    public Transform PrefabCube
    {
        get { return prefabCube; }
        set { prefabCube = value; }
    }

    [SerializeField] private Transform cubeParent;

    private void OnEnable()
    {
        InputListener.onSpawn += Spawn;
    }

    private void OnDisable()
    {
        InputListener.onSpawn -= Spawn;
    }

    public void Spawn()
    {
        Quaternion cubeSpaawnRotation = Quaternion.identity;
        Vector3 cubeSpawnPosition = new Vector3(-15.0f, transform.position.y, transform.position.z);

        if (prefabCube != null)
        {
            Instantiate(prefabCube, cubeSpawnPosition, cubeSpaawnRotation, cubeParent);
        }
        else
        { 
            Debug.LogError("Prefab is null!");
        }
    }
}
