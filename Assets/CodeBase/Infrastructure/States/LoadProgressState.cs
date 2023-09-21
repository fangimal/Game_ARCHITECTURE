using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            
            _gameStateMachine.Enter<LoadSceneState, string>(_progressService.Plogress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInitNew() => 
            _progressService.Plogress = 
                _saveLoadService.LoadProgress() 
                ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress("Main");

            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();
            
            progress.HeroStats.Damage = 1f;
            progress.HeroStats.DamageRadius = 0.5f;
             
            return progress;
        }
    }
}