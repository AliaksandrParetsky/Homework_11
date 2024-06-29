using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform cubeSpawn;

    private float currentPosY;
    private float sizeYcube;

    private bool firstUp = true;

    private void OnEnable()
    {
        InputListener.onSpawn += LiftUpCubeSpawn;
    }

    private void OnDisable()
    {
        InputListener.onSpawn -= LiftUpCubeSpawn;
    }

    private void Start()
    {
        if (this.cubeSpawn.TryGetComponent<CubeSpawn>(out var cubeSpawn))
        {
            currentPosY = cubeSpawn.transform.position.y;

            sizeYcube = cubeSpawn.PrefabCube.GetComponent<CubeMeshGeneration>().LengthCubeY;

            LiftUpCubeSpawn();

            cubeSpawn.Spawn();
        }
    }

    private void LiftUpCubeSpawn()
    {
        if (firstUp)
        {
            currentPosY = (currentPosY + sizeYcube) / 2;

            cubeSpawn.transform.position = new Vector3(0.0f, currentPosY, 0.0f);

            firstUp = false;
        }
        else
        {
            currentPosY = currentPosY + sizeYcube;

            cubeSpawn.transform.position = new Vector3(0.0f, currentPosY, 0.0f);
        }
    }
}
