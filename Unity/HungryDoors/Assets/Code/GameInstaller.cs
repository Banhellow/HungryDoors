using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GUIManager _guiManager;
    public ItemManager _itemManager;
    public SoundManager _soundManager;
    public DoorController _doorController;

    public override void InstallBindings()
    {
        Container.Bind<GUIManager>().FromInstance(_guiManager).AsSingle();
        Container.Bind<ItemManager>().FromInstance(_itemManager).AsSingle();
        Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
        Container.Bind<DoorController>().FromInstance(_doorController).AsSingle();
        Container.BindFactory<Item, Vector3, Quaternion, Transform, Item, ItemFactory>();
    }
}
