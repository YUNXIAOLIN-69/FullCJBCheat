using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;

namespace FullCJBCheat.UI
{
    public class ButtonControl : IControl
    {
        public Rectangle Bounds { get; set; }
        public bool Selected { get; set; }
        public string Label { get; }
        private readonly Action _click;

        public ButtonControl(string label, Action click)
        {
            Label = label;
            _click = click;
        }

        public void Left() { }
        public void Right() { }
        public void Activate() => _click.Invoke();

        public void Draw(SpriteBatch b)
        {
            if (Selected)
            {
                b.Draw(Game1.staminaRect, Bounds, Color.Orange * 0.35f);
                b.Draw(Game1.staminaRect, new Rectangle(Bounds.X, Bounds.Y, 4, Bounds.Height), Color.Orange);
            }
            b.DrawString(Game1.smallFont, Label, new Vector2(Bounds.X + 12, Bounds.Y + 6), Color.White);
            b.DrawString(Game1.smallFont, ">>> 执行", new Vector2(Bounds.Right - 80, Bounds.Y + 6), Color.Orange);
        }
    }
}
