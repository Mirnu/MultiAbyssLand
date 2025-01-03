﻿using Assets.Scripts.Misc.Constants;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View
{
    public class PlayButton : MenuButton, IPointerClickHandler
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            NetworkManager.singleton.StartHost();
            base.OnPointerClick(eventData);
        }
    }
}