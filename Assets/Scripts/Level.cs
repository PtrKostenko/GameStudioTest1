using GameStudioTest1.AI;
using GameStudioTest1.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace GameStudioTest1
{
    [RequireComponent(typeof(GuidComponent))]
    public class Level : MonoBehaviour, IMemorizable
    {
        [SerializeField] private LevelUI _levelUI;
        [SerializeField] private LevelArea _levelArea;

        [Inject] private Game _game;
        [Inject] private Input _input;
        [Inject] private PauseSystem _pauseSystem;
        [Inject] private AiDirector _aiDirector;
        [Inject] private List<ControllableUnit> _playerUnits;

        private int _activePlayerUnitIdx = 0;


        public UnityEvent LevelInited;
        public UnityEvent LevelFailed;
        public UnityEvent LevelFinished;


        public IntEvent SaveGameRequested => _levelUI.SaveGameClicked;
        public LoadGameEvent LoadGameRequested => _levelUI.LoadGameClicked;
        public UnityEvent RestartRequested => _levelUI.RestartClicked;
        public UnityEvent ExitToMainMenuRequested => _levelUI.ExitToMainMenuClicked;


        public ControllableUnit ActivePlayerUnit => _playerUnits[_activePlayerUnitIdx];
        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();


        private void Start()
        {
            _game.SubscribeToCurrentLevel(this);
            Init();
        }

        private void Update()
        {
            if (_input != null) ProcessInput();
        }

        public void Init()
        {
            _levelArea.ExitAreaEntered.AddListener(OnUnitEnteredExitArea);

            _levelUI.PauseClicked.AddListener(Pause);
            _levelUI.ContinueClicked.AddListener(Unpause);
            _levelUI.OnNextUnitClicked.AddListener(ChangeActivePlayerUnit);
            _levelUI.SetActiveUnit(ActivePlayerUnit);

            _aiDirector.Init();

            foreach(var unit in _playerUnits)
            {
                unit.Died.AddListener(OnPlayerDied);
            }


            LevelInited?.Invoke();
        }

        public void ChangeActivePlayerUnit()
        {
            _activePlayerUnitIdx++;
            if (_activePlayerUnitIdx >= _playerUnits.Count)
            {
                _activePlayerUnitIdx = 0;
            }
            _levelUI.SetActiveUnit(ActivePlayerUnit);
        }


        public List<IMemorizable> GetMemorizables()
        {
            List<IMemorizable> levelMemorizables = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<IMemorizable>().ToList(); //necessary evil
            return levelMemorizables;
        }

        public List<Memento> GetMementos()
        {
            List<Memento> mementos = GetMemorizables().Select(m => m.MakeMemento()).ToList();
            return mementos;
        }

        public void SetLevelState(List<Memento> newMementos)
        {
            List<IMemorizable> levelMemorizables = GetMemorizables();
            
            foreach (Memento mem in newMementos)
            {
                if (mem.ID == "00000000-0000-0000-0000-000000000000")
                {
                    //prefab's memento
                    continue;
                }
                IMemorizable m = levelMemorizables.Find(lm => lm.ID == mem.ID && mem.TypeName == lm.GetType().ToString()); //TODO: rework it
                if (m is null)
                {
                    Debug.LogError($"No such memorizable for id:{mem.ID}-{mem.TypeName}");
                }
                else
                {
                    m.SetFromMemento(mem);
                }
            }
        }


        public void UpdateUI()
        {
            _levelUI.UpdateSaveLoadMenu();
            _levelUI.SetActiveUnit(ActivePlayerUnit);
        }

        private void ProcessInput()
        {
            Vector2 inputAxis = _input.GetMoveAxis();
            Vector3 moveAxis = new Vector3(inputAxis.x, 0, inputAxis.y);

            if (_levelArea.IsInsideArea(ActivePlayerUnit.GetPossiblePosition(moveAxis)))
            {
                ActivePlayerUnit.Move(moveAxis);

            }

            if (_input.IsChangeUnitInput())
            {
                ChangeActivePlayerUnit();
                
            }
        }


        private void OnUnitEnteredExitArea(Unit unit)
        {
            Debug.Log("Unit entered exit area");
            if (_playerUnits.All(pu => _levelArea.IsInExitArea(pu)))
            {
                LevelFinished?.Invoke();
                Debug.Log("Level finished");
            }
        }
        private void OnPlayerDied(Unit unit)
        {
            Debug.Log("Player unit died");
            LevelFailed?.Invoke();
            Debug.Log("Level failed");
        }

        public void Pause() => _pauseSystem.Pause();
        public void Unpause() => _pauseSystem.Unpause();

        public Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_activePlayerUnitIdx), _activePlayerUnitIdx);
            return mem;
        }

        public void SetFromMemento(Memento memento)
        {
            _activePlayerUnitIdx = System.Convert.ToInt32(memento.TryGetValue(nameof(_activePlayerUnitIdx)));
            UpdateUI();
        }
    } 
}
