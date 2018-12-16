using GameEngine.Game.GameStates.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Game.GameStates
{
    public enum EGameState
    {
        MAINMENU,
        GAME,
        EDITOR,
        PAUSE
    }

    public class StateManager : GameEngine.Data.System
    {
        private EGameState? m_gameState;
        private Dictionary<EGameState, IGameState> m_gameStates;

        public StateManager(GameEngine.Data.System system)
            : base(system)
        {
            m_gameStates = new Dictionary<EGameState, IGameState>();

            SetGameState(EGameState.MAINMENU);
        }

        public void SetGameState(EGameState gameState)
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].Dispose();
            }

            bool stateHasChanged = false;
            
            switch (gameState)
            {
                case EGameState.MAINMENU:
                    if (!m_gameState.HasValue 
                        || m_gameState.Value == EGameState.PAUSE)
                    {
                        m_gameStates.Clear();
                        m_gameStates[gameState] = new MainMenuState(this.Parent);
                        m_gameStates[gameState].Init();

                        stateHasChanged = true;
                    }
                    break;
                case EGameState.GAME:
                    if (m_gameState.HasValue 
                        && (m_gameState.Value == EGameState.MAINMENU
                        || m_gameState.Value == EGameState.PAUSE))
                    {
                        if (m_gameState.Value == EGameState.MAINMENU)
                        {
                            m_gameStates[gameState] = new GameState(this.Parent);
                            m_gameStates[gameState].Init();
                        }
                        else if (m_gameState.Value == EGameState.PAUSE)
                        {
                            m_gameStates[gameState].Resume();
                        }

                        stateHasChanged = true;
                    }
                    break;
                case EGameState.EDITOR:
                    if (m_gameState.HasValue 
                        && (m_gameState.Value == EGameState.MAINMENU
                        || m_gameState.Value == EGameState.PAUSE))
                    {
                        if (!m_gameStates.ContainsKey(gameState))
                        {
                            m_gameStates[gameState] = new EditorState(this.Parent);
                        }

                        stateHasChanged = true;
                    }
                    break;
                case EGameState.PAUSE:
                    if (m_gameState.HasValue 
                        && (m_gameState.Value == EGameState.GAME
                        || m_gameState.Value == EGameState.EDITOR))
                    {
                        m_gameStates[m_gameState.Value].Pause();

                        m_gameStates[gameState] = new PauseState(this.Parent);
                        m_gameStates[gameState].Init();

                        stateHasChanged = true;
                    }
                    break;
            }


            if (stateHasChanged)
            {
                Console.WriteLine(
                    string.Format("L'état du jeu a été modifié ! ({0} => {1})",
                    m_gameState, gameState));

                this.m_gameState = gameState;
            }
            else
            {
                Console.WriteLine(
                    string.Format("L'état du jeu n'a pas pu être modifié ! ({0} => {1})",
                    m_gameState, gameState));
            }
        }

        public void Input()
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].Key();
            }
        }

        public void Update(double elapsedTime)
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].Update(elapsedTime);
            }
        }

        public void Render()
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].Render();
            }
        }
    }
}
