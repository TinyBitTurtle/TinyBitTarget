﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyBitTurtle.Core;

namespace TinyBitTurtle
{
    public class AudioCtrl : SingletonMonoBehaviour<AudioCtrl>
    {
        private enum Direction
        {
            FadeIn,
            FadeOut,

            FadeMax
        }

        public List<SoundNode> listSoundList = new List<SoundNode>();
        [System.Serializable]
        public struct SoundNode
        {
            public string id;
            public SoundObject soundObject;
        }

        [System.Serializable]
        public struct SoundObject
        {
            public enum soundType
            {
                music,
                SFX
            }
            [HideInInspector]
            public AudioSource audioSource;

            public AudioClip[] audioClip;
            public soundType type;
            [Range(0f, 1f)]
            public float volume;
            public float fadeDuration;
        }

        private Dictionary<string, SoundNode> soundDictionary = new Dictionary<string, SoundNode>();
        private SoundObject lastMusic;

        private void OnEnable()
        {
            // hook up the action to the callback func
            ActionCtrl.Instance.actionAudioPlay += AudioPlay;
        }

        private void OnDisable()
        {
            // disconnect the action to the callback func
            ActionCtrl.Instance.actionAudioPlay -= AudioPlay;
        }

        private void Start()
        {
            // go from List to dictionary for quick retrieval
            for (int i = 0; i < listSoundList.Count; ++i)
            {
                SoundNode soundNode = listSoundList[i];
                soundNode.soundObject.audioSource = gameObject.AddComponent<AudioSource>();
                soundNode.soundObject.audioSource.volume = soundNode.soundObject.volume;
                soundNode.soundObject.audioSource.loop = soundNode.soundObject.type == SoundObject.soundType.music ? true : false;
                soundDictionary.Add(soundNode.id, soundNode);
            }
        }

        // action callback
        private void AudioPlay(string audioName)
        {
            // illegal or already playing
            if (audioName == "" || soundDictionary.Count == 0 || soundDictionary[audioName].soundObject.audioSource.isPlaying)
                return;
            
            SoundObject soundObject = soundDictionary[audioName].soundObject;
            // random sounds id multiple clips are specified
            int whichSound = 0;
            if (soundDictionary[audioName].soundObject.audioClip.Length > 1)
            {
                whichSound = UnityEngine.Random.Range(0, soundDictionary[audioName].soundObject.audioClip.Length);
            }
            soundDictionary[audioName].soundObject.audioSource.clip = soundDictionary[audioName].soundObject.audioClip[whichSound];// hook up radom clip to source

            // we are dealing with musics
            if (soundDictionary[audioName].soundObject.type == SoundObject.soundType.music)
            {
                // optional fade out
                if (lastMusic.audioSource != null)
                {
                    if (lastMusic.fadeDuration != 0)
                    {
                        StartCoroutine(AudioFade(lastMusic, Direction.FadeOut));
                        lastMusic.audioSource = null;
                    }
                    else
                    {
                        // stop prev music
                        lastMusic.audioSource.Stop();
                    }
                }

                // remenber the last music
                lastMusic = soundDictionary[audioName].soundObject;
            }

            // optional volume specified
            if (soundObject.volume != 0)
                soundObject.audioSource.volume = soundObject.volume;

            soundObject.audioSource.Play();

            // optional fadeIn
            if (soundObject.fadeDuration != 0)
            {
                StartCoroutine(AudioFade(soundObject, Direction.FadeIn));
            }
        }
        
        private static IEnumerator AudioFade(SoundObject soundObject, Direction direction)
        {
            // set variables based on the direction
            bool fadeIn = direction == Direction.FadeIn;
            float step = fadeIn ? 1 * (1f / soundObject.fadeDuration) : -1 * (1f / soundObject.fadeDuration);
            float fadeTarget = fadeIn ? soundObject.audioSource.volume : 0;
            float fadeValue = fadeIn ? 0 : soundObject.audioSource.volume;

            if (fadeIn)
            {
                while (fadeValue < fadeTarget)
                {
                    fadeValue += Time.deltaTime * step;
                    soundObject.audioSource.volume = fadeValue;

                    yield return null;
                }

            }
            else
            {
                while (fadeValue > fadeTarget)
                {
                    fadeValue += Time.deltaTime * step;
                    soundObject.audioSource.volume = fadeValue;

                    yield return null;
                }

                soundObject.audioSource.Stop();
            }

            soundObject.volume = soundObject.audioSource.volume = fadeTarget;
        }
    }
}