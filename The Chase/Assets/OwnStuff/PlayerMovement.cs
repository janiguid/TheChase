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
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.Translate(hor * Time.deltaTime * speed,
                            0,
                            ver * Time.deltaTime * speed);


        CamUpdate();
    }

    void CamUpdate()
    {
        Vector3 mp = Input.mousePosition;
        float diff = mp.x - centerpoint;

        if (diff > 100 || diff < -100)
        {
            diff = diff < 0 ? diff * (-diff) : diff * diff;
            Mathf.Clamp(diff, -1, 1);

            transform.Rotate(Vector3.up, diff * Time.deltaTime * turnSpeed);
        }

        Vector3 adjustedLoc = transform.localPosition;
        adjustedLoc.y += 2;
        Ray eyeSight = new Ray(adjustedLoc, transform.forward);

        RaycastHit hit;
        

        if (Physics.Raycast(eyeSight, out hit))
        {

            if(hit.transform.tag == "Enemy")
            {

                if(hit.transform.GetComponent<EnemyMovement>().HasBeenSpotted() == false)
                {
                    hit.transform.GetComponent<EnemyMovement>().MarkAsSpotted();

                    if(AudioManager.Instance.HasQueueBegan() == false)
                    {
                        AudioManager.Instance.AddToSoundQueue(AudioManager.SoundName.Shock);

                        if (AudioManager.Instance.IsPlayingSound(AudioManager.SoundName.Chase) == false)
                        {
                            AudioManager.Instance.AddToSoundQueue(AudioManager.SoundName.Chase);
                        }

                        AudioManager.Instance.AddToSoundQueue(AudioManager.SoundName.Ambient);
                        AudioManager.Instance.BeginQueue();
                    }
                    else
                    {
                        AudioManager.Instance.PlaySecondary(AudioManager.SoundName.Shock);
                    }

                }

                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 adjustedLoc = transform.localPosition;
        adjustedLoc.y += 2;
        Debug.DrawRay(adjustedLoc, transform.forward * 10, Color.red);
    }
}
