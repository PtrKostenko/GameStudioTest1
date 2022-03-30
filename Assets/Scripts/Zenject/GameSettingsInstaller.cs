using GameStudioTest1;
using UnityEngine;
using Zenject;

public class GameSettingsInstaller : MonoInstaller
{
    [SerializeField] private Game _gamePrefab;

    private void OnValidate()
    {
        Debug.Assert(_gamePrefab != null, $"{nameof(_gamePrefab)} need to be assigned", this);
    }

    public override void InstallBindings()
    {
        Container.Bind<Game>().FromComponentInNewPrefab(_gamePrefab).AsSingle().NonLazy();
        Container.Bind<SaveSystem>().To<JsonSaveSystem>().FromNew().AsSingle().Lazy();
        Container.Bind<LevelLoader>().To<SyncLevelLoader>().FromNew().AsSingle().Lazy();
        Container.Bind<GameStudioTest1.Input>().To<OldSystemInput>().FromNew().AsSingle().NonLazy();
        Container.Bind<PauseSystem>().To<TimescalePauseSystem>().FromNew().AsSingle().NonLazy();
    }
}