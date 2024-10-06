using UnityEngine;

public class MotionController : MonoBehaviour
{
    private Animator animator; // Automatically fetch the Animator component
    public float turnSpeed = 1.0f;

    void Start()
    {
        // Fetch the Animator component attached to the GameObject this script is on (Player)
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for turning (A/D or Left/Right Arrow Keys)
        float turnInput = Input.GetAxis("Horizontal"); // -1 for left, 1 for right

        // Apply turn input to the Animator's TurnAmount parameter
        animator.SetFloat("TurnAmount", turnInput * turnSpeed);
    }
}
