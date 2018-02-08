﻿using System.Collections.Generic;
using System.Linq;
using CAFU.Core.Data.DataStore;
using CAFU.Music.Data.Entity;
using CAFU.Music.Domain.Repository;
using UnityEngine;

// ReSharper disable ArrangeAccessorOwnerBody

namespace CAFU.Music.Data.DataStore {

    public abstract class MusicDataStoreMultiple<TEnum, TMusicEntity> : MusicDataStoreBase<TEnum> where TEnum : struct where TMusicEntity : IMusicEntity {

        public class Factory : SceneDataStoreFactory<Factory, MusicDataStoreMultiple<TEnum, TMusicEntity>> {

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
        }

        public override AudioClip GetAudioClip(TEnum key) {
            return this.MusicEntityList.OfType<IMusicEntity<TEnum>>().ToList().Find(x => Equals(x.Key, key)).AudioClip;
        }

    }

}