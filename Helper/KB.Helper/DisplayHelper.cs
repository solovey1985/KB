using System.Text;
using System.Linq;
using System.Collections;
namespace KB.Helper
{
    public class DisplayHelper
    {
        private Dictionary<int, string> _itemsToDisplay;
        private Dictionary<int, Action> _methodsToInvoke;
        public DisplayHelper()
        {
            _itemsToDisplay = new Dictionary<int, string>();
            _methodsToInvoke = new Dictionary<int, Action>();
        }
        public string GenerateIntroScreen()
        {
            var sb = new StringBuilder();
            foreach (var item in _itemsToDisplay)
            {
                sb.AppendLine($"{item.Key} - {item.Value}");
            }
            return sb.ToString();
        }

        public void AddItemsToDisplay(int number, string title)
        {
            _itemsToDisplay.Add(number, title);
        }

        public void AddItemsToInvoke(int number, Action method)
        {
            _methodsToInvoke.Add(number, method);
        }

        public void InvokeItem(int number)
        {
            _methodsToInvoke.TryGetValue(number, out var method);
            method?.Invoke();

        }

        public void AddItemsForDisplay(int number, string title, Action method)
        {
            AddItemsToDisplay(number, title);
            AddItemsToInvoke(number, method);
        }

        public string DisplayDemoTitle(int demo)
        {
            return _itemsToDisplay.GetValueOrDefault(demo);
        }
    }
}