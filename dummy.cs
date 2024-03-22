public static bool _Crashing = false;
public static void Crash3()
{
    _Crashing = !_Crashing;
    if (!_Crashing) return;

    //Prevent Crash From Client
    MemoryManager.Mem.Write<byte>(MemoryManager.BaseAddress + 0x11571A0, 0xC3);

    var CAutomobileSyncTree = MemoryManager.Mem.Read<nint>(MemoryManager.BaseAddress + 0x294CFE0); //deref
    var CVehicleControlDataNode = MemoryManager.Mem.Read<nint>(CAutomobileSyncTree + 0xE0);

    //Patch
    MemoryManager.Mem.Write(MemoryManager.BaseAddress + 0x1121E7E, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });

    new Thread(() =>
    {
        while (_Crashing)
        {
            MemoryManager.Mem.Write<byte>(CVehicleControlDataNode + 0x119, 1); //Is Submarine
            System.Threading.Thread.Sleep(100);
        }

        MemoryManager.Mem.Write<byte>(CVehicleControlDataNode + 0x119, 0);
    }).Start();

    Notifications.Add(new Notification(
                        $"Crash: {_Crashing}",
                        "\uf096",
                        AudioType.Error,
                        new System.Numerics.Vector3(175, 25, 25),
                        5,
                        0
                        ));        
}
