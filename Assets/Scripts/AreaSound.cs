using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    public AudioSource audioSource;
    public float stopDistance;

    private Transform player;
    private float defaultVolume;

    // Start is called before the first frame update
    void Start()
    {
        defaultVolume = audioSource.volume;
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        // distance from us to the player
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > stopDistance)
            audioSource.volume = defaultVolume;
        else
            audioSource.volume = 0.0f;
    }
}
