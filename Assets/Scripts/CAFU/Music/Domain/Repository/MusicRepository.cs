using CAFU.Core.Data.DataStore;
using CAFU.Core.Domain.Repository;
using CAFU.Music.Data.DataStore;
using UnityEngine;
using Zenject;

namespace CAFU.Music.Domain.Repository {

    public interface IMusicRepository<in TEnum> : IRepository where TEnum : struct {

        AudioClip GetAudioClip(TEnum key);

    }

    public class MusicRepository<TEnum> : IMusicRepository<TEnum> where TEnum : struct {

        public static IDataStoreFactory<IMusicDataStore<TEnum>> DataStoreFactory { private get; set; }

        public class Factory : DefaultRepositoryFactory<MusicRepository<TEnum>> {

            protected override void Initialize(MusicRepository<TEnum> instance) {
                base.Initialize(instance);
                instance.MusicDataStore = DataStoreFactory.Create();
            }

        }

        [Inject]
        private IMusicDataStore<TEnum> MusicDataStore { get; set; }

        public AudioClip GetAudioClip(TEnum key) {
            return this.MusicDataStore.GetAudioClip(key);
        }

    }

}
