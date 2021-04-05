namespace Test_DOne.Models
{
    using System.Collections.Generic;
    using Test_DOne.Models.Extensions;
    /// <summary>
    /// Model result, this model include grid bounds and rovers (each rover includs the result and their breadcrumbs)
    /// </summary>
    public class Script
    {
        public Script() { }
        public Script(string _gb)
        {
            this.gridBounds = _gb.ToGridBounds();
            this.rovers = new List<Rover>();
        }

        /// <summary>
        /// Grid bounds
        /// </summary>
        public GridBounds gridBounds { get; set; }
        /// <summary>
        /// List of Land Robotic Rovers on the Script
        /// </summary>
        public List<Rover> rovers { get; set; }
    }
}