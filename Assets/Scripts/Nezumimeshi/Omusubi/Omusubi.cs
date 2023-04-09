using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nezumimeshi.Core;
using UnityEngine;

namespace Nezumimeshi.Omusubi
{
    public class Omusubi : MonoBehaviour, IGameLoop, IPoolable<Omusubi>
    {
        [SerializeField] SpriteRenderer _renderer;
        [SerializeField] GameObject _exploder;
        [SerializeField] Collider2D col;
        [SerializeField] Rigidbody2D rig;

        [SerializeField] AudioClip getSE;
        [SerializeField] AudioClip explodeSE;

        [Header("調整用パラメータ")] [SerializeField] float explodeTime;
        [SerializeField] float rotatePower;
        [SerializeField] float fadeOutTime;

        public Omusubi Entity => this;

        public int Id { get; private set; }
        public int Point { get; private set; }
        public bool IsExplode { get; private set; }

        public void ApplyProfile(OmusubiProfile profile)
        {
            _renderer.sprite = profile.Icon;
            Id = profile.Id;
            Point = profile.Point;
            IsExplode = profile.isExplode;
        }

        public void OnFixedUpdate()
        {
            rig.AddTorque(rotatePower);
        }

        public async void FadeDestroy()
        {
            Disable();
            //地面に落ちたらだんだん消えていく
            await _renderer.DOFade(0f, fadeOutTime).Play().AsyncWaitForCompletion();

            if (Game.Instance.gameCanceler.Token.IsCancellationRequested) return;

            //プールに返却
            OmusubiFactory.Instance.Catch(this);
        }

        public async void Explode()
        {
            _exploder.SetActive(true);

            AudioSystem.Instance.PlaySe(explodeSE);
            await UniTask.Delay(TimeSpan.FromSeconds(explodeTime));

            if (Game.Instance.gameCanceler.Token.IsCancellationRequested) return;
            _exploder.SetActive(false);
        }


        public void PlayGetSE()
        {
            AudioSystem.Instance.PlaySe(getSE);
        }

        public void ResetPosition()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void ResetColor()
        {
            _renderer.color = Color.white;
            _exploder.SetActive(false);
        }

        public void Enable()
        {
            rig.simulated = true;
            col.enabled = true;

            GameLoop.Add(this);
        }

        public void Disable()
        {
            rig.simulated = false;
            col.enabled = false;

            GameLoop.Remove(this);
        }

        public void OnUpdate() { }
        public void OnReleased() { }

        public void OnCatched()
        {
            ResetPosition();
            ResetColor();
        }
    }
}