using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GUIManager _guiManager;

    public override void InstallBindings()
    {
        Container.Bind<GUIManager>().FromInstance(_guiManager).AsSingle();
    }
}
