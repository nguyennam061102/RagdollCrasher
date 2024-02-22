using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSounds, sfxSounds ;
    public AudioClip[] stepSounds;
    public AudioSource musicSource, sfxSource;
    public string sfxName;
    private void Start()
    {

    }
    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        musicSource.clip = s.clip;
        musicSource.Play();
    }
    public void PlaySfx(string name)
    {
            sfxName = name;
            Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            sfxSource.loop = false;
        sfxSource.clip = null;
        sfxSource.PlayOneShot(s.clip); 
    }
    public void PlaySfxLoop(string name)
    {
        if(sfxName != name)
        {
            sfxName = name;
            Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            sfxSource.loop = true;
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
        
    }
    public void StopSfx(string name)
    {
        Debug.Log("1");
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if(s.name == sfxName)
        {
            sfxSource.Stop();
            sfxName = null;
            sfxSource.clip = null;
        }
        
        
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void PlayeStepSound(){
        int i = Random.Range(0, 5);
        sfxSource.PlayOneShot(stepSounds[i]);
    }
}
