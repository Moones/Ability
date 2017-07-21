using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityData.AbilityMapDataProvider.AbilityMapData.Jungle
{
    using SharpDX;

    public abstract class Jungle
    {
        private readonly List<Vector3> entrances = new List<Vector3>();

        private readonly List<JungleCamp> mediumCamps = new List<JungleCamp>();
        private readonly List<JungleCamp> hardCamps = new List<JungleCamp>();
        private readonly List<JungleCamp> ancientCamps = new List<JungleCamp>();


        public IReadOnlyCollection<Vector3> Entrances => this.entrances;

        public IReadOnlyCollection<JungleCamp> MediumCamps => this.mediumCamps;
        public IReadOnlyCollection<JungleCamp> HardCamps => this.hardCamps;
        public IReadOnlyCollection<JungleCamp> AncientCamps => this.ancientCamps;

        public JungleCamp EasyCamp { get; }


    }
}
