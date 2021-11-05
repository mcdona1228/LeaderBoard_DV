using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;

    public GameObject playButton;
    public TextMeshProUGUI curTimeText;

    private float startTime;
    private float timeTaken;

    private int collectablesPicked;
    public int maxCollectables = 10;

    private bool isPlaying;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!isPlaying)
            return;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rig.velocity = new Vector3(x, rig.velocity.y, z);

        curTimeText.text = (Time.time - startTime).ToString("F2");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            Destroy(other.gameObject);

            if (collectablesPicked == maxCollectables)
                End();
        }
    }
    public void Begin()
    {
        startTime = Time.time;
        isPlaying = true;
        playButton.SetActive(false);
    }
    public void End()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        playButton.SetActive(true);
        LeaderBoard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        //curTimeText.text = (Time.time - startTime).ToString("F2");
    }
}
