using CAFU.Core.Data.Entity;
using UnityEngine;
// ReSharper disable ArrangeAccessorOwnerBody

#pragma warning disable 649

namespace CAFU.Music.Data.Entity {

    public interface IMusicEntity : IEntity {

        AudioClip AudioClip { get; }

    }

    public interface IMusicEntity<out TEnum> : IMusicEntity where TEnum : struct {

        TEnum Key { get; }

    }

    public class MusicEntity<TEnum> : IMusicEntity<TEnum> where TEnum : struct {

        [SerializeField]
        private TEnum key;

        public TEnum Key {
            get {
                return this.key;
            }
        }

        [SerializeField]
        private AudioClip audioClip;

        public AudioClip AudioClip {
            get {
                return this.audioClip;
            }
        }

    }

}