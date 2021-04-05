namespace Test_DOne.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Model to represent the startup to the Rover
    /// </summary>
    public class Coordinate
    {
        public Coordinate() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_x">Horizontal Integer value</param>
        /// <param name="_y">Vertical Integer value</param>
        /// <param name="_z">Cardinal direction string value</param>
        public Coordinate(int _x, int _y, string _z)
        {
            this.X = _x;
            this.Y = _y;
            this.Z = _z;
            this.done = true;
            this.Error = "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_error">Cardinal direction string value</param>
        public Coordinate(string _error)
        {
            this.X = 0;
            this.Y = 0;
            this.Z = "";
            this.done = false;
            this.Error = _error;
        }
        /// <summary>
        /// Represents the X axis
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Represents the Y axis
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Represents the Cardinal direction
        /// </summary>
        public string Z { get; set; }
        /// <summary>
        /// Processed was done
        /// </summary>
        public bool done { get; set; }
        /// <summary>
        /// Error description
        /// </summary>
        public string Error { get; set; }
    }
}