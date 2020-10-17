using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float turnSpeed;

    [SerializeField] private Animator m_animator = null;
    private float centerpoint;

    // Start is called before the first frame update
    void Start()
    {
        centerpoint = Screen.width / 2;
        m_animator.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.Translate(hor * Time.deltaTime * speed,
                            0, 
                            ver * Time.deltaTime * speed);

        m_animator.SetFloat("MoveSpeed", ver);









        Vector3 mp = Input.mousePosition;
        float diff = mp.x - centerpoint;

        if(diff > 100 || diff < -100)
        {
            diff = diff < 0 ? diff * (-diff) : diff * diff;
            Mathf.Clamp(diff, -1, 1);

            transform.Rotate(Vector3.up, diff * Time.deltaTime * turnSpeed);
        }

    }
}
