using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;
    public bool isLoop;
    public bool hasCooldown;
    public bool isMusic=false; // Bu �zelli�i ekleyerek ses t�r�n� belirtin
    [HideInInspector] public AudioSource source;
}
