using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void StopMovement()
    {
        speed = 0.0f;
    }

    public void DisablingScript()
    {
        if(gameObject.TryGetComponent<InputListener>(out var inputListener))
        {
            inputListener.enabled = false;
        }
    }
}
