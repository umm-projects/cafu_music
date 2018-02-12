using CAFU.Core.Data.DataStore;
using CAFU.Music.Data.Entity;
using CAFU.Music.Domain.Repository;
using UnityEngine;
using Zenject;

// ReSharper disable ArrangeAccessorOwnerBody

namespace CAFU.Music.Data.DataStore {

    public abstract class MusicDataStoreSingle<TEnum, TMusicEntity, TMusicDataStore> : MusicDataStoreBase<TEnum>
        where TEnum : struct
        where TMusicEntity : IMusicEntity
        where TMusicDataStore : MusicDataStoreSingle<TEnum, TMusicEntity, TMusicDataStore> {

        public class Factory : SceneDataStoreFactory<MusicDataStoreSingle<TEnum, TMusicEntity, TMusicDataStore>> {

        }

        [SerializeField]
        private TMusicEntity musicEntity;

        private TMusicEntity MusicEntity {
            get {
                return this.musicEntity;
            }
        }

        protected override void OnAwake() {
            base.OnAwake();
            MusicRepository<TEnum>.DataStoreFactory = new Factory();
            ProjectContext.Instance.Container.Bind<IMusicDataStore<TEnum>>().FromInstance(this);
        }

        public override AudioClip GetAudioClip(TEnum key) {
            return this.MusicEntity.AudioClip;
        }

    }

}
