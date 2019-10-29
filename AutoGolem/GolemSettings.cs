using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using System.Windows.Forms;

namespace AutoGolem
{
    public class GolemSettings : ISettings
    {
        [Menu("Enable", 1)]
        public ToggleNode Enable { get; set; } = new ToggleNode(true);

        [Menu("Hotkey", 2)]
        public HotkeyNode HotKey { get; set; } = new HotkeyNode(Keys.None);
    }

}
