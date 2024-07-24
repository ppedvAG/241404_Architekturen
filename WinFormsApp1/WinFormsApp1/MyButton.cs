namespace WinFormsApp1
{
    internal class MyButton : Button
    {

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Brushes.Lime,ClientRectangle);
            pevent.Graphics.FillEllipse(Brushes.Orange,ClientRectangle);

        }
        
    }
}
