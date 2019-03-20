using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface EventInterface
{
    bool hasLighting(out List<Light> lightSources);
    bool hasSounds(out List<AudioSource> soundSources);
    bool hasVoices(out List<AudioSource> voiceSources);
    bool hasNPCs(out List<ReactiveNPC> reactiveNPCs);
    bool hasNPCAnimations(out List<AnimationClip> npcClips);
    bool hasObjectAnimations(out List<AnimationClip> objectClips);
    bool hasParticles(out List<GameObject> particleObjects);
}

//The methods return a bool based on if the variable was filled with data
public abstract class AbstractEventClass : MonoBehaviour, EventInterface
{
    public List<Light> lightSources;
    public List<AudioSource> soundSources;
    public List<AudioSource> voiceSources;
    public List<ReactiveNPC> reactiveNPCs;
    public List<AnimationClip> npcClips;
    public List<AnimationClip> objectClips;
    public List<GameObject> particleObjects;

    public abstract void startEvent();

    public abstract bool hasLighting(out List<Light> lightSources);

    public abstract bool hasNPCAnimations(out List<AnimationClip> npcClips);


    public abstract bool hasNPCs(out List<ReactiveNPC> reactiveNPCs);


    public abstract bool hasObjectAnimations(out List<AnimationClip> objectClips);


    public abstract bool hasParticles(out List<GameObject> particleObjects);


    public abstract bool hasSounds(out List<AudioSource> soundSources);


    public abstract bool hasVoices(out List<AudioSource> voiceSources);

}