using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;

namespace FullCJBCheat.UI
{
    public class ToggleControl : IControl
    {
        public Rectangle Bounds { get; set; }
        public bool Selected { get; set; }
        public string Label { get; }
        public bool Value { get; set; }
        private readonly Action<bool> _onChange;

        public ToggleControl(string label, bool initVal, Action<bool> onChange)
        {
            Label = label;
            Value = initVal;
            _onChange = onChange;
        }

        public void Left() => Toggle();
        public void Right() => Toggle();
        public void Activate() => Toggle();

        private void Toggle()
        {
            Value = !Value;
            _onChange.Invoke(Value);
        }

        public void Draw(SpriteBatch b)
        {
            if (Selected)
            {
                b.Draw(Game1.staminaRect, Bounds, Color.Orange * 0.35f);
                b.Draw(Game1.staminaRect, new Rectangle(Bounds.X, Bounds.Y, 4, Bounds.Height), Color.Orange);
            }
            b.DrawString(Game1.smallFont, Label, new Vector2(Bounds.X + 12, Bounds.Y + 6), Color.White);
            string state = Value ? "开启" : "关闭";
            Color stateCol = Value ? Color.LimeGreen : Color.Gray;
            b.DrawString(Game1.smallFont, state, new Vector2(Bounds.Right - 70, Bounds.Y + 6), stateCol);
        }
    }
}
