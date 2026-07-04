using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;

namespace FullCJBCheat.UI
{
    public class DropdownControl : IControl
    {
        public Rectangle Bounds { get; set; }
        public bool Selected { get; set; }
        public string Label { get; }
        public int SelectIndex;
        public List<string> Options;
        private readonly Action<int> _onSelect;

        public DropdownControl(string label, List<string> opts, int initIdx, Action<int> onSel)
        {
            Label = label;
            Options = opts;
            SelectIndex = initIdx;
            _onSelect = onSel;
        }

        public void Left()
        {
            SelectIndex--;
            if (SelectIndex < 0) SelectIndex = Options.Count - 1;
            _onSelect.Invoke(SelectIndex);
        }

        public void Right()
        {
            SelectIndex++;
            if (SelectIndex >= Options.Count) SelectIndex = 0;
            _onSelect.Invoke(SelectIndex);
        }

        public void Activate() { }

        public void Draw(SpriteBatch b)
        {
            if (Selected)
            {
                b.Draw(Game1.staminaRect, Bounds, Color.Orange * 0.35f);
                b.Draw(Game1.staminaRect, new Rectangle(Bounds.X, Bounds.Y, 4, Bounds.Height), Color.Orange);
            }
            b.DrawString(Game1.smallFont, Label, new Vector2(Bounds.X + 12, Bounds.Y + 6), Color.White);
            string curOpt = Options[SelectIndex];
            b.DrawString(Game1.smallFont, $"▼ {curOpt}", new Vector2(Bounds.Right - 90, Bounds.Y + 6), Color.Orange);
        }
    }
}
