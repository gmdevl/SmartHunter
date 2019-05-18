﻿using SmartHunter.Core.Data;
using System;
using System.Windows;
using System.Windows.Input;

namespace SmartHunter.Core.Windows
{
    public abstract class WidgetWindow : Window
    {
        public Widget Widget { get; private set; }

        protected abstract float ScaleMax { get; }
        protected abstract float ScaleMin { get; }
        protected abstract float ScaleStep { get; }

        public bool IsConfiguredForLayered { get; private set; }

        public WidgetWindow(Widget widget)
        {
            Widget = widget;

            ShowActivated = false;

            ConfigureForLayered();
        }

        public void ConfigureForLayered()
        {
            IsConfiguredForLayered = true;

            AllowsTransparency = true;
            WindowStyle = WindowStyle.None;
            ShowInTaskbar = false;
        }

        public void ConfigureForSolid()
        {
            IsConfiguredForLayered = false;

            AllowsTransparency = false;
            WindowStyle = WindowStyle.SingleBorderWindow;
            ShowInTaskbar = true;
            ResizeMode = ResizeMode.NoResize;
        }

        protected void WidgetWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        protected void WidgetWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int direction = 0;
            if (e.Delta > 0)
            {
                direction = 1;
            }
            else if (e.Delta < 0)
            {
                direction = -1;
            }

            float currentScale = Widget.Scale;
            currentScale += ScaleStep * direction;
            Widget.Scale = Math.Min(Math.Max(currentScale, ScaleMin), ScaleMax);
        }
    }
}
