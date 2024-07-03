using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] float speed;

    public static CubeMovement CurrentCube {  get; private set; }
    public static CubeMovement LastCube { get; private set; }
    public MoveDirection MoveDirection { get;  set; }

    private void OnEnable()
    {
        if(LastCube == null)
        {
            LastCube = GameObject.Find("StartPlatform").GetComponent<CubeMovement>();
        }

        CurrentCube = this;

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y,
            LastCube.transform.localScale.z);
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.X)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    public void StopMovement()
    {
        speed = 0.0f;
        float distanceToCenter = GetDistanceToCenter();

        if (Mathf.Abs(distanceToCenter) >= GetMaxLastCubeScale())
        {
            LastCube = null;
            CurrentCube = null;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            return;
        }

        SplitCube(distanceToCenter);

        LastCube = this;
    }

    private void SplitCube(float distanceToCenter)
    {
        if (MoveDirection == MoveDirection.X)
        {
            SplitCubeOnX(distanceToCenter, CheckSideCube(distanceToCenter));
        }
        else
        {
            SplitCubeOnZ(distanceToCenter, CheckSideCube(distanceToCenter));
        }
    }

    private float CheckSideCube(float distanceToCenter)
    {
        float directionFallingCube;

        if (distanceToCenter > 0.0f)
        {
            directionFallingCube = 1.0f;
        }
        else
        {
            directionFallingCube = -1.0f;
        }

        return directionFallingCube;
    }

    private float GetMaxLastCubeScale()
    {
        float max;
        if (MoveDirection == MoveDirection.X)
        {
            max = LastCube.transform.localScale.x;
        }
        else
        {
            max = LastCube.transform.localScale.z;
        }

        return max;
    }

    private float GetDistanceToCenter()
    {
        if(MoveDirection == MoveDirection.X)
        {
            return transform.position.x - LastCube.transform.position.x;
        }
        else
        {
            return transform.position.z - LastCube.transform.position.z;
        }
    }

    private void SplitCubeOnZ(float distanceToCenter, float directionFallingCube)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(distanceToCenter);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + (distanceToCenter / 2.0f);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2.0f * directionFallingCube);
        float fallingBlockZpos = cubeEdge + fallingBlockSize / 2.0f * directionFallingCube;

        SpawnDropCube(fallingBlockZpos, fallingBlockSize);
    }

    private void SplitCubeOnX(float distanceToCenter, float directionFallingCube)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(distanceToCenter);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (distanceToCenter / 2.0f);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2.0f * directionFallingCube);
        float fallingBlockXpos = cubeEdge + fallingBlockSize / 2.0f * directionFallingCube;

        SpawnDropCube(fallingBlockXpos, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockXpos, float fallingBlockSize)
    {
        GameObject fallingCube = new GameObject("FallingCube");

        if(MoveDirection == MoveDirection.X)
        {
            fallingCube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            fallingCube.transform.position = new Vector3(fallingBlockXpos, transform.position.y, transform.position.z);
        }
        else
        {
            fallingCube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            fallingCube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockXpos);
        }

        fallingCube.AddComponent<CubeMeshGeneration>();
        fallingCube.transform.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
        fallingCube.AddComponent<Rigidbody>();
        Destroy(fallingCube.gameObject, 1.0f);
    }

    public void DisablingScript()
    {
        if(gameObject.TryGetComponent<InputListener>(out var inputListener))
        {
            inputListener.enabled = false;
        }
    }
}
