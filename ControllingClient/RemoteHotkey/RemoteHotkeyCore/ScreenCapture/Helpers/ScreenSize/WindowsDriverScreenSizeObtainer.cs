namespace RemoteHotkeyCore.ScreenCapture.Helpers.ScreenSize;

public class WindowsDriverScreenSizeObtainer : IScreenSizeObtainer
{
    public void GetSize(ref int width, ref int height)
    {
        var managementScope = new System.Management.ManagementScope();
        managementScope.Connect();
        var q = new System.Management.ObjectQuery("SELECT CurrentHorizontalResolution, CurrentVerticalResolution FROM Win32_VideoController");
        var searcher = new System.Management.ManagementObjectSearcher(managementScope, q);
        var records = searcher.Get();

        int rr = records.Count;

        foreach (var record in records)
        {
            if (!int.TryParse(record.GetPropertyValue("CurrentHorizontalResolution").ToString(), out width))
            {
                throw new Exception("Throw some exception");
            }
            if (!int.TryParse(record.GetPropertyValue("CurrentVerticalResolution").ToString(), out height))
            {
                throw new Exception("Throw some exception");
            }
        }
    }
}