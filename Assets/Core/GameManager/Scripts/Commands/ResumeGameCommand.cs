﻿using Core.Utilities.Commands;
using UnityEngine;

namespace Core.GameManager.Scripts.Commands
{
    public class ResumeGameCommand : ISimpleCommand
    {
        public void Execute()
        {
            // puaseMenuUI.SetActive(true);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
