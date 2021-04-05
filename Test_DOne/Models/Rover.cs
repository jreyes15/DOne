namespace Test_DOne.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Test_DOne.Models.Extensions;
    /// <summary>
    /// Model for the Land Robotic Rovers
    /// </summary>
    public class Rover
    {
        public Rover() { }
        /// <summary>
        /// Constructor of the rover model
        /// </summary>
        /// <param name="_coordinate">string to set the starting coordinate</param>
        /// <param name="_movements">string to set the breadcrumbs</param>
        /// <param name="_name">Identifier for the rover</param>
        /// <param name="limits">grid bounds</param>
        public Rover(string _coordinate, string _movements, string _name, GridBounds limits)
        {
            // Init the identifier, visual porpouse
            this.name = _name;
            // Cast the text to breadcrumbs
            this.movements = _movements.ToMovements(_coordinate.ToCoordinate(),limits);
            // Cast the text to coordinate
            this.coordinate = _coordinate.ToCoordinate();
            // Set the last coordinate, this means, the result
            this.coordinateResult = new Coordinate(this.movements.Last().currentValueXAxis, this.movements.Last().currentValueYAxis, this.movements.Last().currentAxis);
        }
        /// <summary>
        /// Identifier
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Started coordinate
        /// </summary>
        public Coordinate coordinate { get; set; }
        /// <summary>
        /// Movement description
        /// </summary>
        public List<Movement> movements { get; set; }
        /// <summary>
        /// Resultant coordinate
        /// </summary>
        public Coordinate coordinateResult { get; set; }
    }
}