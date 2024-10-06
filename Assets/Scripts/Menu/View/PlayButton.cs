using Assets.Scripts.Misc.Constants;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View
{
    public class PlayButton : MenuButton, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene(Scenes.Game);
        }
    }
}