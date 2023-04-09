using System.Collections.Generic;
using Nezumimeshi.Basis;
using UnityEngine;

namespace Nezumimeshi.Core
{
    /// <summary>
    /// BGMとSEの管理をするマネージャ
    /// </summary>
    [RequireComponent(typeof(AudioListener))]
    public class AudioSystem : SingletonMonoBehaviour<AudioSystem> {
        const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
        const float BGM_FADE_SPEED_RATE_LOW = 0.3f;

        float m_bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

        bool m_isFadeOut = false;

        AudioSource m_bgmSource;
        AudioClip m_nextBGMClip;
        List<AudioSource> m_seSourceList = new List<AudioSource>();


        public void OnLoaded() {
            //BGM
            m_bgmSource = gameObject.AddComponent<AudioSource>();

            m_bgmSource.playOnAwake = false;
            m_bgmSource.loop = true;

            //SE
            for (int i = 0; i < 3; i++) {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                m_seSourceList.Add(source);
            }

            (float bgm, float se) volume = (0.25f, 0.5f);

            ChangeVolume(volume.bgm, volume.se);

            m_delayPlaySe.AddListener(DelayPlaySe);
        }


        DelayAction<AudioClip> m_delayPlaySe;


        public void PlaySe(AudioClip clip, float delay = 0.0f) {
            if (clip == null) return;

            m_delayPlaySe.Invoke(clip, delay);
        }


        void DelayPlaySe(AudioClip clip) {
            foreach (AudioSource seSource in m_seSourceList) {
                if (seSource.isPlaying) continue;

                seSource.PlayOneShot(clip);
                return;
            }
        }


        public void PlayBGM(AudioClip clip, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH) {
            if (!m_bgmSource.isPlaying) {
                m_bgmSource.clip = clip;
                m_bgmSource.Play();
            } else if (m_bgmSource.clip.name != clip.name) {
                //fade out when bgm is playing.
                m_nextBGMClip = clip;
                FadeOutBGM(fadeSpeedRate);
            }
        }


        public void StopBGM() { m_bgmSource.Stop(); }


        void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW) {
            m_bgmFadeSpeedRate = fadeSpeedRate;
            m_isFadeOut = true;
        }


        void Update() {
            if (!m_isFadeOut) {
                return;
            }

            //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
            m_bgmSource.volume -= Time.deltaTime * m_bgmFadeSpeedRate;
            if (m_bgmSource.volume <= 0) {
                m_bgmSource.Stop();
                m_bgmSource.volume = 0.25f;
                m_isFadeOut = false;

                if (m_nextBGMClip != null) {
                    PlayBGM(m_nextBGMClip);
                }
            }
        }


        public void ChangeVolume(float bgmVolume, float seVolume) {
            m_bgmSource.volume = bgmVolume;

            foreach (AudioSource source in m_seSourceList) {
                source.volume = seVolume;
            }

            // Game.SetVolume(bgmVolume, seVolume);
        }
    }
}