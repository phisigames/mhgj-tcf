using UnityEngine;

public class PlayerElfMovement : MonoBehaviour
{

    [SerializeField]
    [Range(5, 50)]
    private float elfSpeed = 5;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private SpriteRenderer _renderer;

    void Update()
    {
        MoveElf();
    }

    private void MoveElf()
    {
        float verticalMove = GetMoveValue(Input.GetAxis("Vertical"));
        float horizontalMove = GetMoveValue(Input.GetAxis("Horizontal"));

        if ((verticalMove == 0) && (horizontalMove == 0))
        {
            myAnimator.SetBool("isWalking", false);
        }
        else
        {
            myAnimator.SetBool("isWalking", true);
            if (horizontalMove > 0)
            {
                _renderer.flipX = false;
            }
            else if (horizontalMove < 0)
            {
                _renderer.flipX = true;
            }
        }

        transform.Translate(horizontalMove, verticalMove, 0f);
    }

    private float GetMoveValue(float axisValue)
    {
        return axisValue * elfSpeed * Time.deltaTime;
    }
}
