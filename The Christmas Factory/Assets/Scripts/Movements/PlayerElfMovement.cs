using UnityEngine;

public class PlayerElfMovement : MonoBehaviour
{

    [SerializeField]
    [Range(5, 50)]
    private float elfSpeed = 5;

    void Start()
    {

    }

    void Update()
    {
        MoveElf();
    }

    private void MoveElf()
    {
        float verticalMove = GetMoveValue(Input.GetAxis("Vertical"));
        float horizontalMove = GetMoveValue(Input.GetAxis("Horizontal"));
        transform.Translate(horizontalMove, verticalMove, 0f);
    }

    private float GetMoveValue(float axisValue)
    {
        return axisValue * elfSpeed * Time.deltaTime;
    }
}
