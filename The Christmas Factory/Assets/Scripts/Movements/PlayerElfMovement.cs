using UnityEngine;

public class PlayerElfMovement : MonoBehaviour
{

    [SerializeField]
    [Range(5, 50)]
    private float elfSpeed = 5;

    [SerializeField]
    private ElfAnimationController ElfAnimation;



    void Update()
    {
        if (!ElfAnimation.InAction)
        {
            //FIX IDLE-> SEARCH OTHER SOLUTION
            if (Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow))
            {
                ElfAnimation.SlideIdle();
            }

            MoveElf();
        }


    }

    private void MoveElf()
    {
        float verticalMove = GetMoveValue(Input.GetAxis("Vertical"));
        float horizontalMove = GetMoveValue(Input.GetAxis("Horizontal"));

        transform.Translate(horizontalMove, verticalMove, 0f);
        ElfAnimation.Walking(horizontalMove, verticalMove);
    }

    private float GetMoveValue(float axisValue)
    {
        return axisValue * elfSpeed * Time.deltaTime;
    }
}
