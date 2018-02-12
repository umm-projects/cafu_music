using CAFU.Music.Domain.UseCase;
using CAFU.Core.Domain.Model;
using UniRx;
using UnityEngine;
using Zenject;

namespace CAFU.Music.Domain.Model {

    public interface IMusicModel : ISingletonModel {

        ReactiveProperty<AudioClip> AudioClip { get; }

        IMusicPlayer MusicPlayer { get; set; }

    }

    public class MusicModel : IMusicModel {

        public class Factory : DefaultModelFactory<MusicModel> {

            protected override void Initialize(MusicModel instance) {
                base.Initialize(instance);
                instance.Initialize();
            }

        }

        public ReactiveProperty<AudioClip> AudioClip { get; private set; }

        public IMusicPlayer MusicPlayer { get; set; }

        [Inject]
        private void Initialize() {
            this.AudioClip = new ReactiveProperty<AudioClip>();
        }

    }

}