namespace Test_DOne.Models
{
    /// <summary>
    /// Model of the breadcrumbs
    /// </summary>
    public class Movement
    {
        public Movement() { }
        public Movement(int _x, int _y, string _axis) {
            this.lastValueXAxis = _x;
            this.lastValueYAxis = _y;
            this.lastAxis = _axis;
        }
        /// <summary>
        /// Current value of the X axis
        /// </summary>
        public int currentValueXAxis { get; set; }
        /// <summary>
        /// Current value of the Y axis
        /// </summary>
        public int currentValueYAxis { get; set; }
        /// <summary>
        /// Current value of cardinal direction
        /// </summary>
        public string currentAxis { get; set; }
        /// <summary>
        /// Last value of the X axis
        /// </summary>
        public int lastValueXAxis { get; set; }
        /// <summary>
        /// Last value of the Y axis
        /// </summary>
        public int lastValueYAxis { get; set; }
        /// <summary>
        /// Last value of cardinal direction
        /// </summary>
        public string lastAxis { get; set; }
        /// <summary>
        /// Input instruction text
        /// </summary>
        public string Input { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// Perform instruction indicator
        /// </summary>
        public bool done { get; set; }

    }
}