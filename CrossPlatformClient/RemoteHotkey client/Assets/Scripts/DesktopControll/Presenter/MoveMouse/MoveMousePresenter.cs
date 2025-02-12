using UnityEngine;

public class MoveMousePresenter : DesktopControllPresenterBase<MoveMouseView>
{
    public MoveMousePresenter(MoveMouseView view, DesktopControllModel model) : base(view, model)
    {
        View.mouseMoved += OnMove;
    }

    ~MoveMousePresenter() 
    {
        if (View != null)
        {
            View.mouseMoved -= OnMove;
        }
    }

    private void OnMove(Vector2 moveDelta)
    {
        int x = (int)(moveDelta.x * 100);
        int y = (int)(moveDelta.y * 100);

        Model.ExecuteCommand<MoveMouseCommand>(x.ToString(), y.ToString());
    }
}