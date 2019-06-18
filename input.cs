using OpenTK.Input;
using System.Collections.Generic;
using System;

namespace Template
{
    public class input
    {
        static List<Key> keysDown;
        static List<Key> keysDownLast;
        static List<MouseButton> buttonsDown;
        static List<MouseButton> buttonsDownLast;

        public static void Initialize(OpenTKApp app)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            buttonsDown = new List<MouseButton>();
            buttonsDownLast = new List<MouseButton>();

            app.KeyDown += screen_KeyDown;
            app.KeyUp += screen_KeyUp;
            app.MouseDown += screen_MouseDown;
            app.MouseUp += screen_MouseUp;

        }
        public static void screen_KeyDown(object o, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }
        public static void screen_KeyUp(object o, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key))
            {
                keysDown.Remove(e.Key);
            }
        }
        public static void screen_MouseDown(object o, MouseButtonEventArgs e)
        {
            buttonsDown.Add(e.Button);
        }
        public static void screen_MouseUp(object o, MouseButtonEventArgs e)
        {
            while (buttonsDown.Contains(e.Button))
            {
                buttonsDown.Remove(e.Button);
            }
        }
        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonsDownLast = new List<MouseButton>(buttonsDown);
        }
        public static bool keyPress(Key key)
        {
            return (keysDown.Contains(key) && !keysDownLast.Contains(key));
        }
        public static bool keyRelease(Key key)
        {
            return (!keysDown.Contains(key) && keysDownLast.Contains(key));
        }
        public static bool keyDown(Key key)
        {
            return (keysDown.Contains(key));
        }
        public static bool mousePress(MouseButton button)
        {
            return (buttonsDown.Contains(button) && !buttonsDownLast.Contains(button));
        }
        public static bool mouseRelease(MouseButton button)
        {
            return (!buttonsDown.Contains(button) && buttonsDownLast.Contains(button));
        }
        public static bool mouseDown(MouseButton button)
        {
            return (buttonsDown.Contains(button));
        }
    }
}
