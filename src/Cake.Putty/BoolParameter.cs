using System;

namespace Cake.Putty
{
    /// <summary>
    /// Decorates a bool property with parameter names.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BoolParameterAttribute: Attribute
    {
        /// <summary>
        /// Value on true.
        /// </summary>
        public string OnTrue { get; set; }
        /// <summary>
        /// Value on false.
        /// </summary>
        public string OnFalse { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="onTrue"></param>
        /// <param name="onFalse"></param>
        public BoolParameterAttribute(string onTrue, string onFalse)
        {
            OnTrue = onTrue;
            OnFalse = onFalse;
        }
    }
}
