using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Nezumimeshi.Basis;
using Nezumimeshi.Core;
using Nezumimeshi.Profile;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nezumimeshi.Omusubi
{
    public class OmusubiFactory : ObjectPool<OmusubiFactory, Omusubi>
    {
        [SerializeField] float createInterval;
        [SerializeField] Dictionary<Rarity, int> probabilities;
        Dictionary<Rarity, List<OmusubiProfile>> rarityOmusubiPair = new Dictionary<Rarity, List<OmusubiProfile>>();
        OmusubiSettings _settings;

        List<Omusubi> releasedOmusubis = new List<Omusubi>();

        bool isResetting;

        void Start()
        {
            _settings = Game.Instance.omusubiSettings;
            CreatePool(25, true); //プールの生成
            DivideRarity();

            Game.Instance.eHandler.OnGameStart.AddListener(() =>
            {
                DoCreationCycle(Game.Instance.gameCanceler.Token);
            });

            Game.Instance.eHandler.OnBack.AddListener(() =>
            {
                isResetting = true;

                foreach (Omusubi omusubi in releasedOmusubis)
                {
                    omusubi.transform.SetParent(transform);
                    omusubi.ResetPosition();
                    omusubi.ResetColor();

                    Catch(omusubi);
                }

                releasedOmusubis.Clear();
                isResetting = false;
            });
        }

        void DivideRarity()
        {
            foreach (OmusubiProfile profile in _settings.profiles)
            {
                if (rarityOmusubiPair.ContainsKey(profile.Rarity))
                {
                    rarityOmusubiPair[profile.Rarity].Add(profile);
                }
                else
                {
                    rarityOmusubiPair.Add(profile.Rarity, new List<OmusubiProfile>() { profile });
                }
            }
        }

        async void DoCreationCycle(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                CreateRandom();
                await UniTask.Delay(TimeSpan.FromSeconds(createInterval), cancellationToken: cToken);
            }
        }

        protected override void OnReleased(Omusubi entity)
        {
            if (isResetting) return;
            releasedOmusubis.Add(entity);
        }

        protected override void OnCatched(Omusubi entity)
        {
            if (isResetting) return;
            releasedOmusubis.Remove(entity);
        }


        void CreateRandom()
        {
            int result = Random.Range(1, 101);
            OmusubiProfile resultProfile = null;

            int beforeNumber = 0;
            foreach (KeyValuePair<Rarity, int> pair in probabilities)
            {
                if (beforeNumber < result && result <= pair.Value)
                {
                    List<OmusubiProfile> profileList = rarityOmusubiPair[pair.Key];
                    resultProfile = profileList[Random.Range(0, profileList.Count)];

                    break;
                }

                beforeNumber = pair.Value;
            }

            Create(resultProfile);
        }

        void Create(OmusubiProfile profile)
        {
            Omusubi omusubi = Release();
            omusubi.ApplyProfile(profile);
            omusubi.ResetPosition();
            omusubi.ResetColor();
            omusubi.Enable();
        }
    }
}