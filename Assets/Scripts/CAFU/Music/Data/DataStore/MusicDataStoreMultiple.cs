using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Music.Data.Entity;
using CAFU.Music.Domain.Repository;
using UnityEngine;
using Zenject;

// ReSharper disable ArrangeAccessorOwnerBody

namespace CAFU.Music.Data.DataStore {

    public abstract class MusicDataStoreMultiple<TEnum, TMusicEntity, TMusicDataStore> : MusicDataStoreBase<TEnum>
        where TEnum : struct
        where TMusicEntity : IMusicEntity
        where TMusicDataStore : MusicDataStoreSingle<TEnum, TMusicEntity, TMusicDataStore> {

        public class Factory : SceneDataStoreFactory<MusicDataStoreMultiple<TEnum, TMusicEntity, TMusicDataStore>> {

        }

        [SerializeField]
        private List<TMusicEntity> musicEntityList;

        private IEnumerable<TMusicEntity> MusicEntityList {
            get {
                return this.musicEntityList;
            }
        }

        protected override void OnAwake() {
            base.OnAwake();
            MusicRepository<TEnum>.DataStoreFactory = new Factory();
            ProjectContext.Instance.Container.Bind<IMusicDataStore<TEnum>>().FromInstance(this);
        }

        public override AudioClip GetAudioClip(TEnum key) {
            return this.MusicEntityList.OfType<IMusicEntity<TEnum>>().ToList().Find(x => Equals(x.Key, key)).AudioClip;
        }

    }

}
