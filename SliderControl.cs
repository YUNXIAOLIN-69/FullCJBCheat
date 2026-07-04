using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;

namespace FullCJBCheat.UI
{
    public class SliderControl : IControl
    {
        public Rectangle Bounds { get; set; }
        public bool Selected { get; set; }
        public string Label { get; }
        public float Current;
        public float Min, Max;
        private readonly Action<float> _update;

        public SliderControl(string label, float init, float min, float max, Action<float> update)
        {
            Label = label;
            Current = init;
            Min = min;
            Max = max;
            _update = update;
        }

        public void Left()
        {
            Current -= (Max - Min) / 100f;
            if (Current < Min) Current = Min;
            _update.Invoke(Current);
        }

        public void Right()
        {
            Current += (Max - Min) / 100f;
            if (Current > Max) Current = Max;
            _update.Invoke(Current);
        }

        public void Activate() { }

        public void Draw(SpriteBatch b)
        {
            if (Selected)
            {
                b.Draw(Game1.staminaRect, Bounds, Color.Orange * 0.35f);
                b.Draw(Game1.staminaRect, new Rectangle(Bounds.X, Bounds.Y, 4, Bounds.Height), Color.Orange);
            }
            Vector2 labelPos = new(Bounds.X + 12, Bounds.Y + 6);
            b.DrawString(Game1.smallFont, Label, labelPos, Color.White);

            int barStartX = Bounds.X + 200;
            int barW = Bounds.Width - 260;
            Rectangle barBg = new(barStartX, Bounds.Y + 8, barW, 12);
            b.Draw(Game1.staminaRect, barBg, Color.Gray);

            float rate = (Current - Min) / (Max - Min);
            Rectangle fillBar = new(barStartX, Bounds.Y + 8, (int)(barW * rate), 12);
            b.Draw(Game1.staminaRect, fillBar, Color.Orange);

            string valText = Current.ToString("0.00");
            b.DrawString(Game1.smallFont, valText, new Vector2(Bounds.Right - 50, Bounds.Y + 6), Color.White);
        }
    }
}
