using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FullCJBCheat.UI
{
    public interface IControl
    {
        Rectangle Bounds { get; set; }
        bool Selected { get; set; }
        string Label { get; }
        void Draw(SpriteBatch batch);
        void Left();
        void Right();
        void Activate();
    }
}
