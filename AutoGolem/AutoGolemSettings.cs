using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace AutoGolem
{
    public class AutoGolemSettings : ISettings
    {
        [Menu("Enable", 1)]
        public ToggleNode Enable { get; set; } = new ToggleNode(true);

        [Menu("Chaos Golem", 2)]
        public GolemSettings ChaosGolem { get; set; } = new GolemSettings();

        [Menu("Fire Golem", 3)]
        public GolemSettings FireGolem { get; set; } = new GolemSettings();

        [Menu("Ice Golem", 4)]
        public GolemSettings IceGolem { get; set; } = new GolemSettings();
        
        [Menu("Lighting Golem", 5)]
        public GolemSettings LightingGolem { get; set; } = new GolemSettings();

        [Menu("Stone Golem", 6)]
        public GolemSettings StoneGolem { get; set; } = new GolemSettings();

        [Menu("Bestial Ursal", 7)]
        public GolemSettings BestialUrsa { get; set; } = new GolemSettings();

        [Menu("Only summon when enemies are this far away", 8)]
        public RangeNode<int> MinDistance { get; set; } = new RangeNode<int>(1500, 0, 10000);

        [Menu("Only summon when life is at least this percentage", 9)]
        public RangeNode<int> MinLife { get; set; } = new RangeNode<int>(0, 0, 100);

        [Menu("Only summon when shield is at least this percentage", 10)]
        public RangeNode<int> MinShield { get; set; } = new RangeNode<int>(0, 0, 100);
    }

}
