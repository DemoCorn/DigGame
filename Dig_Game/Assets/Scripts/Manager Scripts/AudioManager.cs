using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<AudioSource> sources = new List<AudioSource>();
    
    public void Setup()
    {
        sources.Clear();
    }

    public int Register(AudioSource source)
    {
        sources.Add(source);
        return sources.Count - 1;
    }

    public void SetSource(int index, bool set)
    {
        if (sources.Count > index)
        {
            sources[index].enabled = set;
        }
    }

}
