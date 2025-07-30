using UnityEngine;
using UnityEngine.Video; // <-- Add this

public class Navigation : MonoBehaviour
{
    public GameObject panel1; // <-- panel1 contains the VideoPlayer    
    public GameObject panel2;
    public GameObject panel3; 
    public GameObject panel4;
    public GameObject panel5;

    public AudioClip activateSound;
    public AudioClip deactivateSound;
    private AudioSource audioSource;

    private GameObject[] panels;
    private int currentPanelIndex = -1;

    private void Awake()
    {
        panels = new GameObject[] { panel1, panel2, panel3, panel4, panel5 };

        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Page1() => ShowPanel(0);
    public void Page2() => ShowPanel(1);
    public void Page3() => ShowPanel(2);
    public void Page4() => ShowPanel(3);
    public void Page5() => ShowPanel(4);

    private void ShowPanel(int index)
    {
        if (currentPanelIndex == index)
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }
            currentPanelIndex = -1;

            PlaySound(deactivateSound);
        }
        else
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(i == index);
            }

            currentPanelIndex = index;
            PlaySound(activateSound);

            // ✅ Restart video if panel1 is shown
            if (index == 0)
            {
                VideoPlayer vp = panel1.GetComponent<VideoPlayer>();
               

                if (vp != null )
                {
                    
        vp.enabled = false;      // Hide last frame
        vp.time = 0;             // Rewind to start
        vp.enabled = true;       // Reactivate cleanly
        vp.Play();               // Play from beginning


                   
                }
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
