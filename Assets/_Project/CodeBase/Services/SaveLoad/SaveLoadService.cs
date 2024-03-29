using System.Collections.Generic;
using _Project.CodeBase.Data;
using _Project.CodeBase.Services.Progress;
using UnityEngine;

namespace _Project.CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IEnumerable<IProgressSaver> _saverServices;
        private readonly IPersistentProgressService _persistentProgressService;

        public SaveLoadService(IEnumerable<IProgressSaver> saverServices, IPersistentProgressService persistentProgressService)
        {
            _saverServices = saverServices;
            _persistentProgressService = persistentProgressService;
        }

        public void SaveProgress()
        {
            foreach (var saver in _saverServices) 
                saver.UpdateProgress(_persistentProgressService.Config);
            
            PlayerPrefs.SetString(ProgressKey, _persistentProgressService.Config.ToJson());
        }

        public PlayerConfig LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerConfig>();
        
    }
}