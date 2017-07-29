using System;

namespace Logic
{
    /// <summary>
    /// Class contains data about change element event.
    /// </summary>
    public class NewElementEventArgs : EventArgs
    {
        #region properties
        /// <summary>
        /// Some info about event.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Row number where element was changed.
        /// </summary>
        public int I { get; }

        /// <summary>
        /// Column number where element was changed.
        /// </summary>
        public int J { get; }
        #endregion

        #region ctors
        /// <summary>
        /// Ctor with parameters.
        /// </summary>
        /// <param name="message">Some info about event.</param>
        /// <param name="i">Row number where element was changed.</param>
        /// <param name="j">Column number where element was changed.</param>
        public NewElementEventArgs(string message, int i, int j)
        {
            Message = message;
            I = i;
            J = j;
        }
        #endregion
    }
}
