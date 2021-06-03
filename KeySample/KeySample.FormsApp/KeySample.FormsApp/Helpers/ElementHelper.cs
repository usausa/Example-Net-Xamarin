namespace KeySample.FormsApp.Helpers
{
    using System.Collections.Generic;

    using Smart.Forms;

    using Xamarin.Forms;

    public static class ElementHelper
    {
        public static VisualElement? FindFocused(Element parent)
        {
            foreach (var child in parent.LogicalChildren)
            {
                if ((child is VisualElement visualElement) && visualElement.IsFocused)
                {
                    return visualElement;
                }

                var focused = FindFocused(child);
                if (focused is not null)
                {
                    return focused;
                }
            }

            return null;
        }

        public static bool MoveFocus(VisualElement parent, bool forward)
        {
            var find = false;
            var first = default(VisualElement);
            var previous = default(VisualElement);
            foreach (var visual in EnumerateActive(parent))
            {
                if (visual.IsFocused)
                {
                    if (forward)
                    {
                        find = true;
                    }
                    else
                    {
                        return previous?.Focus() ?? false;
                    }
                }
                else if (find)
                {
                    return visual.Focus();
                }

                previous = visual;
                first ??= visual;
            }

            if (!find)
            {
                return first?.Focus() ?? false;
            }

            return false;
        }

        public static bool MoveFocusInPage(VisualElement element, bool forward)
        {
            var page = element.FindParent<Page>();
            if (page is null)
            {
                return false;
            }

            var find = false;
            var first = default(VisualElement);
            var previous = default(VisualElement);
            foreach (var visual in EnumerateActive(page))
            {
                if (visual == element)
                {
                    if (forward)
                    {
                        find = true;
                    }
                    else
                    {
                        return previous?.Focus() ?? false;
                    }
                }
                else if (find)
                {
                    return visual.Focus();
                }

                previous = visual;
                first ??= visual;
            }

            if (!find)
            {
                return first?.Focus() ?? false;
            }

            return false;
        }

        public static IEnumerable<VisualElement> EnumerateActive(Element parent)
        {
            foreach (var child in parent.LogicalChildren)
            {
                if (child is VisualElement visualElement)
                {
                    if (!visualElement.IsEnabled || !visualElement.IsVisible)
                    {
                        continue;
                    }

                    if (visualElement.IsTabStop && IsFocusableElement(visualElement))
                    {
                        yield return visualElement;
                    }
                }

                foreach (var descendant in EnumerateActive(child))
                {
                    yield return descendant;
                }
            }
        }

        private static bool IsFocusableElement(VisualElement visualElement)
        {
            return visualElement is Button ||
                   visualElement is CheckBox ||
                   visualElement is Entry ||
                   visualElement is ListView;
        }
    }
}
