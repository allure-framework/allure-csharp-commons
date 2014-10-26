using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;

namespace AllureAttachmentWeaver
{
    /// <summary>
    /// A method weaver is used to weave a specific behavior.
    /// </summary>
    public interface IMethodWeaver
    {
        // the reason we provide a single Weave method instead of a 'ShouldWeave and DoWeave'
        // is because in the case of multiple weavers we won't know which weaver to invoke.

        /// <summary>
        /// Weaves the behavior to the specified method.
        /// </summary>
        /// <param name="method">The method into which the behavior should be weaved.</param>
        void Weave(MethodDefinition method);
    }
}
