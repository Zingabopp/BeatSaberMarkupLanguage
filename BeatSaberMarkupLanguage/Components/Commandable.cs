using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.UI;

namespace BeatSaberMarkupLanguage.Components
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
	public class Commandable : MonoBehaviour
    {
        public event EventHandler Destroyed;
        protected Selectable[] Components;
        protected ICommand Command;

        public virtual void SetCommand(ICommand command)
        {
            if(Command != null)
            {
                SetCommandEvents(false);
            }
            Command = command;
            if (Command != null)
                SetCommandEvents(true);
        }

        protected virtual void OnCanExecuteChanged(object sender, EventArgs _)
        {
            if (Components == null)
                return;
            for(int i = 0; i < Components.Length; i++)
            {

            }
        }

        protected virtual void SetCommandEvents(bool enabled)
        {
            if (enabled)
            {
                Command.CanExecuteChanged -= OnCanExecuteChanged;
                Command.CanExecuteChanged += OnCanExecuteChanged;
            }
            else
            {
                Command.CanExecuteChanged -= OnCanExecuteChanged;
            }
        }

        #region Monobehaviour Messages
        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        protected virtual void Awake()
        {

        }
        /// <summary>
        /// Only ever called once on the first frame the script is Enabled. Start is called after every other script's Awake() and before Update().
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// Called when the script becomes enabled and active
        /// </summary>
        protected virtual void OnEnable()
        {

        }

        /// <summary>
        /// Called when the script becomes disabled or when it is being destroyed.
        /// </summary>
        protected virtual void OnDisable()
        {

        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {

        }
        #endregion
    }
}
