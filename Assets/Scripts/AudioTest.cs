using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip sfx1;
    public AudioClip sfx2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySfx("m1", music1);
            AudioManager.Instance.PlaySfx("m2", music2);
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            AudioManager.Instance.Stop("m1");
            AudioManager.Instance.Stop("m2");
        }
    }
}
