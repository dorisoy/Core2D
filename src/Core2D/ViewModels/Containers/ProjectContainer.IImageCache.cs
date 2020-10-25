﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core2D.Renderer;

namespace Core2D.Containers
{
    [DataContract(IsReference = true)]
    public partial class ProjectContainer : BaseContainer, IImageCache
    {
        private readonly IDictionary<string, byte[]> _images = new Dictionary<string, byte[]>();

        private IEnumerable<IImageKey> GetKeys() => _images.Select(i => new ImageKey() { Key = i.Key }).ToList();

        [IgnoreDataMember]
        public IEnumerable<IImageKey> Keys => GetKeys();

        public string AddImageFromFile(string path, byte[] bytes)
        {
            var name = System.IO.Path.GetFileName(path);
            var key = "Images\\" + name;

            if (_images.Keys.Contains(key))
            {
                return key;
            }

            _images.Add(key, bytes);
            RaisePropertyChanged(nameof(Keys));
            return key;
        }

        public void AddImage(string key, byte[] bytes)
        {
            if (_images.Keys.Contains(key))
            {
                return;
            }

            _images.Add(key, bytes);
            RaisePropertyChanged(nameof(Keys));
        }

        public byte[] GetImage(string key)
        {
            if (_images.TryGetValue(key, out byte[] bytes))
            {
                return bytes;
            }
            else
            {
                return null;
            }
        }

        public void RemoveImage(string key)
        {
            _images.Remove(key);
            RaisePropertyChanged(nameof(Keys));
        }

        public void PurgeUnusedImages(ICollection<string> used)
        {
            foreach (var kvp in _images.ToList())
            {
                if (!used.Contains(kvp.Key))
                {
                    _images.Remove(kvp.Key);
                }
            }
            RaisePropertyChanged(nameof(Keys));
        }
    }
}
