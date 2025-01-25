using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioManager manager;
    [SerializeField] TextMeshProUGUI musicName;
    [SerializeField] RectTransform imageRect;
    [SerializeField] Animator animator;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            string name = manager.sounds[Mathf.RoundToInt(Random.Range(0f, manager.sounds.Length - 1))].name;
            manager.Play(name);
            musicName.text = name;
            imageRect.sizeDelta = new Vector2(name.Length * 40, imageRect.sizeDelta.y);
            musicName.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(name.Length * 40, imageRect.sizeDelta.y);
            animator.SetTrigger("Show");
        }
        
        foreach (Sound s in manager.sounds)
        {
            isPlaying = false;

            if (s.source.isPlaying)
            {
                isPlaying = true;
                return;
            }

        }
    }
}
