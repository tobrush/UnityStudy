using UnityEngine;

public class UseSingletonManager : Singleton<UseSingletonManager>
{
    protected override void Awake()
    {
        base.Awake();

        // 추가할 기능
    }
}
