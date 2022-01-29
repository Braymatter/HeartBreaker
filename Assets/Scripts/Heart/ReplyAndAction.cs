using System;

namespace Heart
{
    public class ReplyAndAction
    {
        public string text;
        public Action action;

        public ReplyAndAction(string text, Action action)
        {
            this.text = text;
            this.action = action;
        }
    }
}