using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Animator animator;
    private Rigidbody2D _rigidbody;
    private Vector3 _velocity;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += _velocity * speed * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetButtonDown("Jump") && Mathf.Approximately(_rigidbody.velocity.y, 0))
        {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);            
        }

        if (animator.GetBool("IsJumping") && Mathf.Approximately(_rigidbody.velocity.y, 0))
            animator.SetBool("IsJumping", false);            
            
        if (Input.GetAxisRaw("Horizontal") == -1)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        if (Input.GetAxisRaw("Horizontal") == 1)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
