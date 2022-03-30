using GameStudioTest1;
using GameStudioTest1.AI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private Level _level;
    [SerializeField] private List<ControllableUnit> _playerUnits;
    [SerializeField] private List<AIUnit> _aiUnits;
    [SerializeField] private AiDirector _aiDirector;


    private void OnValidate()
    {
        Debug.Assert(_level != null, $"{nameof(_level)} need to be assigned", this);
        Debug.Assert(_playerUnits != null, $"{nameof(_playerUnits)} need to be assigned", this);
        Debug.Assert(_aiUnits != null, $"{nameof(_aiUnits)} need to be assigned", this);
        Debug.Assert(_aiDirector != null, $"{nameof(_aiDirector)} need to be assigned", this);
    }


    public override void InstallBindings()
    {
        Container.Bind<Level>().FromInstance(_level).AsSingle().NonLazy();
        Container.Bind<List<ControllableUnit>>().FromInstance(_playerUnits).NonLazy();
        Container.Bind<List<AIUnit>>().FromInstance(_aiUnits).NonLazy();
        Container.Bind<AiDirector>().FromInstance(_aiDirector).AsSingle().NonLazy();
    }
}
