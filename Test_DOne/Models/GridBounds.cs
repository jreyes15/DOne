namespace Test_DOne.Models
{

    /// <summary>
    /// Model to represent grid bounds layout
    /// </summary>
    public class GridBounds
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GridBounds() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_x">Horizontal Integer value</param>
        /// <param name="_y">Vertical Integer value</param>
        public GridBounds(int _x, int _y)
        {
            this.X = _x;
            this.Y = _y;
            this.done = true;
            this.Error = "";
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_x">Horizontal Integer value</param>
        /// <param name="_y">Vertical Integer value</param>
        public GridBounds(string _error)
        {
            this.X = 0;
            this.Y = 0;
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
        /// Processed was done
        /// </summary>
        public bool done { get; set; }
        /// <summary>
        /// Error description
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// User input text
        /// </summary>
        public string Input { get; set; }
    }
}