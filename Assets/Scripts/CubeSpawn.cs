using UnityEngine;

public class CubeSpawn : MonoBehaviour
{
    [SerializeField] private CubeMovement prefabCube;

    private MoveDirection moveDirection;
    private bool isMoveDirX;

    public CubeMovement PrefabCube
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
        Vector3 cubeSpawnXPosition = new Vector3(-5.0f, transform.position.y, transform.position.z);
        Vector3 cubeSpawnZPosition = new Vector3(transform.position.x, transform.position.y, 5.0f);

        if (prefabCube != null)
        {
            if(!isMoveDirX)
            {
                if (CubeMovement.LastCube == null)
                {
                    return;
                }

                cubeSpawnXPosition = new Vector3(cubeSpawnXPosition.x, transform.position.y, CubeMovement.LastCube.transform.position.z);

                var cube = Instantiate(prefabCube, cubeSpawnXPosition, cubeSpaawnRotation, cubeParent);
                moveDirection = MoveDirection.X;
                cube.MoveDirection = moveDirection;

                isMoveDirX = true;
            }
            else
            {
                if(CubeMovement.LastCube == null)
                {
                    return;
                }

                cubeSpawnZPosition = new Vector3(CubeMovement.LastCube.transform.position.x,transform.position.y, cubeSpawnZPosition.z);

                var cube = Instantiate(prefabCube, cubeSpawnZPosition, cubeSpaawnRotation, cubeParent);
                moveDirection = MoveDirection.Z;
                cube.MoveDirection = moveDirection;

                isMoveDirX = false;
            }
        }
        else
        { 
            Debug.LogError("Prefab is null!");
        }
    }
}
