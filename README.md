# CAFU Music

## What

* BGM を再生するための UseCase を提供します

## Requirement

* CAFU Core v2.0.0

## Install

```shell
npm install github:umm-projects/cafu_music
```

## Usage

### スクリプト準備編

#### 1. BGM の AudioClip を表現する enum を定義

```csharp
public static class Enumerates {
    public enum MusicName {
        Title,
        Menu,
        Game,
    }
}
```

#### 2. `CAFU.Music.Data.Entity.MusicEntity<TEnum>` を継承した Entity クラスを作成

```csharp
using System;
using CAFU.Music.Data.Entity;
namespace SampleProject.Data.Entity {
    [Serializable]
    public class MusicEntity : MusicEntity<MusicName> {}
}
```

* Unity の仕様により Generic クラスを Serialize できないため、プロジェクトごとに継承する必要があります😢

#### 3. `CAFU.Music.Data.DataStore.Scene.MusicDataStore<TMusicEntity>` を継承した DataStore クラスを作成

```csharp
using CAFU.Music.Data.DataStore;
using SampleProject.Data.Entity;
namespace SampleProject.Data.DataStore.Scene {
    public class MusicDataStore : MusicDataStore<MusicEntity> {}
}
```

* Unity の仕様により Generic クラスを Serialize できないため、プロジェクトごとに継承する必要があります😢

### シーン準備編

#### 1. `CAFU.Music.Presentation.View.IMusicController<TMusicEntity>` を Controller クラスに実装

```csharp
using CAFU.Core.Presentation.View;
using CAFU.Music.Presentation.View;
using SampleProject.Data.Entity;
using UnityEngine;
namespace SampleProject.Presentation.View.SampleScene {
    public class Controller : Controller<SampleScenePresenter, SampleScenePresenter.Factory>, IMusicController<MusicEntity> {
        [SerializeField]
        private MusicDataStore musicDataStore;
        public IMusicDataStore<MusicEntity> MusicDataStore => this.musicDataStore;
    }
}
```

* Component の `Awake()` 実行順制御を行う関係で必要になります。

#### 2. `CAFU.Music.Presentation.Presenter.IMusicPresenter` を Presenter クラスに実装

```csharp
using CAFU.Core.Presentation.Presenter;
using CAFU.Music.Presentation.Presenter;
namespace SampleProject.Presentation.Presenter {
    public class SampleScenePresenter : IPresenter, IMusicPresenter {
        public IMusicUseCase MusicUseCase { get; private set; }
    }
}
```

* 拡張メソッドから利用します。
* Factory の記述は省略しています。
  * Zenject 使うと楽かな。

#### 3. Scene の任意の GameObject に `MusicDataStore` をアタッチ

* Hierarchy ルートの `DataStore` とかがヨサソウです。

#### 4. アタッチされている `Controller` の *Music Data Store* フィールドに 3. の GameObject を D&amp;D

* これにより、実行順制御が可能になります。

### 利用編

#### 再生

```csharp
this.GetPresenter().PlayMusic(MusicName.Title, true, true);
```

##### 引数

1. 再生する BGM を表す enum
1. ループするかどうか (default: `true`)
1. 既に同一の BGM が再生中の場合は、再生を止めずにそのままキープするかどうか (default: `true`)

#### 停止

```csharp
this.GetPresenter().Stop();
```

#### 中断

```csharp
this.GetPresenter().Pause();
```

#### 再開

```csharp
this.GetPresenter().Resume();
```

## License

Copyright (c) 2018 Tetsuya Mori

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)

