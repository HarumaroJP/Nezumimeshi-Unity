using System;
using Cysharp.Threading.Tasks;
using Nezumimeshi.Core;
using Nezumimeshi.Omusubi;
using UnityEngine;

namespace Nezumimeshi.Rat
{
    public class Rat : MonoBehaviour
    {
        [SerializeField] Transform catchPos;
        [SerializeField] float catchDelay;
        [SerializeField] MovementSettings movementSettings;

        RatMovement movement;

        void Start()
        {
            movement = new RatMovement(transform, movementSettings);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Omusubi"))
            {
                other.transform.SetParent(catchPos);

                Omusubi.Omusubi omusubi = other.GetComponent<Omusubi.Omusubi>();
                omusubi.Disable();
                omusubi.ResetPosition();

                Explode(omusubi);

                Game.Instance.eHandler.OnOmusubiGet.Send(omusubi);

                CatchOmusubiAsync(omusubi);
            }
        }

        void Explode(Omusubi.Omusubi omusubi)
        {
            if (omusubi.IsExplode)
            {
                omusubi.Explode();
            }
            else
            {
                omusubi.PlayGetSE();
            }
        }

        async void CatchOmusubiAsync(Omusubi.Omusubi omusubi)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(catchDelay));
            if (Game.Instance.gameCanceler.Token.IsCancellationRequested) return;

            omusubi.transform.SetParent(OmusubiFactory.Instance.transform);
            OmusubiFactory.Instance.Catch(omusubi);
        }
    }
}