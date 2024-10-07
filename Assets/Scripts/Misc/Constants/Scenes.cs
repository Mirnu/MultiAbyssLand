using System;

namespace Assets.Scripts.Misc.Constants
{
    [Serializable]
    public enum ScenesEnum
    {
        Menu,
        Game
    }

    public static class Scenes
    {
        public static string Menu = "Menu";
        public static string Game = "Game";
    }
}
