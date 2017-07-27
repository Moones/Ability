// <copyright file="AbilityDatabase.cs" company="EnsageSharp">
//    Copyright (c) 2017 Moones.
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/
// </copyright>
namespace Ability.Core.AbilityFactory.Database
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Security.Permissions;
    using System.Text;

    using Ability.Core.AbilityFactory.AbilitySkill.Data;
    using Ability.Core.Properties;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    ///     The default priority database.
    /// </summary>
    //[Export(typeof(IAbilityDatabase))]
    internal class AbilityDatabase : IAbilityDatabase
    {
        #region Fields

        /// <summary>
        ///     The cached priority data.
        /// </summary>
        private readonly Dictionary<string, PriorityData> cachedPriorityData = new Dictionary<string, PriorityData>();

        /// <summary>
        ///     The default priority data.
        /// </summary>
        private readonly PriorityData defaultPriorityData;

        /// <summary>
        ///     The priority data.
        /// </summary>
        private readonly Dictionary<string, PriorityData> priorityData = new Dictionary<string, PriorityData>();

        /// <summary>
        ///     The skill data dictionary.
        /// </summary>
        private readonly Dictionary<string, SkillJson> skillDataDictionary = new Dictionary<string, SkillJson>();

        #endregion

        #region Constructors and Destructors

        //[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
        internal AbilityDatabase()
        {
            // var asd = JObject.Parse(Encoding.Default.GetString(Resources.PriorityData).Substring(3));
            // foreach (var data in asd)
            // {
            // var priority = JsonConvert.DeserializeObject<PriorityData>(data.Value.ToString());
            // this.priorityData.Add(data.Key, priority);
            // if (data.Key == "default")
            // {
            // this.defaultPriorityData = priority;
            // }
            // }

            // Console.WriteLine(Encoding.ASCII.GetString(Resources.SkillData));
            var skillDataJson = JObject.Parse(Encoding.Default.GetString(Resources.SkillData).Substring(3));
            foreach (var data in skillDataJson)
            {
                var skillData = JsonConvert.DeserializeObject<SkillJson>(data.Value.ToString());
                this.skillDataDictionary.Add(data.Key, skillData);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        public uint GetCastPriority(string skillName)
        {
            return this.defaultPriorityData.CastPriority.ContainsKey(skillName)
                       ? this.defaultPriorityData.CastPriority[skillName]
                       : 4;
        }

        /// <summary>
        ///     The get priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        public uint GetCastPriority(string skillName, string heroName)
        {
            var data = this.GetData(heroName);
            return data != null && data.CastPriority.ContainsKey(skillName)
                       ? data.CastPriority[skillName]
                       : this.GetCastPriority(skillName);
        }

        /// <summary>
        ///     The get damage dealt priority.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="uint" />.
        /// </returns>
        public uint GetDamageDealtPriority(string skillName)
        {
            return this.defaultPriorityData.DamageDealtPriority.ContainsKey(skillName)
                       ? this.defaultPriorityData.DamageDealtPriority[skillName]
                       : 4;
        }

        /// <summary>
        ///     The get data.
        /// </summary>
        /// <param name="heroName">
        ///     The hero name.
        /// </param>
        /// <returns>
        ///     The <see cref="Dictionary" />.
        /// </returns>
        public PriorityData GetData(string heroName)
        {
            PriorityData data;
            if (this.cachedPriorityData.TryGetValue(heroName, out data) || !this.priorityData.ContainsKey(heroName))
            {
                return data;
            }

            data = this.priorityData[heroName];
            this.cachedPriorityData.Add(heroName, data);
            return data;
        }

        /// <summary>
        ///     The get skill data.
        /// </summary>
        /// <param name="skillName">
        ///     The skill name.
        /// </param>
        /// <returns>
        ///     The <see cref="SkillJson" />.
        /// </returns>
        public SkillJson GetSkillData(string skillName)
        {
            return this.skillDataDictionary.ContainsKey(skillName) ? this.skillDataDictionary[skillName] : null;
        }

        #endregion
    }
}