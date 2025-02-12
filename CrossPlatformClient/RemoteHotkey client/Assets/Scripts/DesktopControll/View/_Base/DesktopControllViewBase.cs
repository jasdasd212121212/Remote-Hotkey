public abstract class DesktopControllViewBase
{
    protected ImageInputHelper DisplayImage { get; private set; }

    public DesktopControllViewBase(ImageInputHelper image) 
    {
        DisplayImage = image;
    }
}