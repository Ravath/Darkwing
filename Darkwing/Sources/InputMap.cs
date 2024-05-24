using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkWing
{
    public class InputMap
    {
        private readonly HashSet<string> _current_actions = [];
    
        private Dictionary<char, string> _action_map = _qwerty_action_map;

        private static readonly Dictionary<char, string> _qwerty_action_map = new() {
            { 'w', "up" },
            { 's', "down" },
            { 'a', "left" },
            { 'd', "right" },
            { 'k', "shoot" },
            { (char)27, "escape" },
        };
        private static readonly Dictionary<char, string> _azerty_action_map = new() {
            { 'z', "up" },
            { 's', "down" },
            { 'q', "left" },
            { 'd', "right" },
            { 'k', "shoot" },
            { (char)27, "escape" },
        };

        public bool IsQwerty()
        {
            return _action_map == _qwerty_action_map;
            // Else is azerty
        }

        public void SwapKeyMaps()
        {
            if(IsQwerty())
                _action_map = _azerty_action_map;
            else
                _action_map = _qwerty_action_map;
        }

        public void GetActions()
        {
            _current_actions.Clear();
            ConsoleKeyInfo cki;
            while (Console.KeyAvailable)
            {
                cki = Console.ReadKey();
                if(_action_map.ContainsKey(cki.KeyChar))
                {
                    _current_actions.Add(_action_map[cki.KeyChar]);
                }
            }
        }

        public bool RisedAction(string actionName)
        {
            return _current_actions.Contains(actionName);
        }
    }
}