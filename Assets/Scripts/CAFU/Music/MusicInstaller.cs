using CAFU.Music.Domain.Model;
using CAFU.Music.Domain.Repository;
using CAFU.Music.Domain.UseCase;
using Zenject;

namespace Modules.Scripts.CAFU.Music {

    // ReSharper disable once ClassNeverInstantiated.Global
    public class MusicInstaller<TEnum> : Installer<MusicInstaller<TEnum>>
        where TEnum : struct {

        public override void InstallBindings() {
            this.Container.Bind<IMusicUseCase<TEnum>>().To<MusicUseCase<TEnum>>().AsTransient();
            this.Container.Bind<IMusicRepository<TEnum>>().To<MusicRepository<TEnum>>().AsTransient();
            this.Container.Bind<IMusicModel>().To<MusicModel>().AsTransient();
        }

    }

}