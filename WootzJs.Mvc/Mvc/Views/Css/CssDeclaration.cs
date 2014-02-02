﻿using System;
using System.Collections.Generic;
using System.Text;
using WootzJs.Web;

namespace WootzJs.Mvc.Mvc.Views.Css
{
    public class CssDeclaration
    {
        protected ElementStyle node;

        private List<Action> actions = new List<Action>();

        internal virtual void Attach(ElementStyle node)
        {
            this.node = node;

            foreach (var action in actions)
            {
                action();
            }
        }

        protected void Act(Action action)
        {
            if (node == null)
                actions.Add(action);
            else
                action();
        }

        protected string Get(string name)
        {
            if (node != null)
                return node[name];

            throw new InvalidOperationException("Not attached");
        }

        protected void Set(string name, object value)
        {
            var val = value.ToString();
            if (node == null)
                Act(() => Set(name, value));
            else
                node[name] = val;
        }

        protected bool IsSet(string name)
        {
            return node[name] != "";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var property in GetType().GetProperties())
            {
                var value = property.GetValue(this, null);
                if (value != null)
                {
                    builder.Append(property.Name + ":" + value + ";");
                }
            }
            return builder.ToString();
        } 
    }
}