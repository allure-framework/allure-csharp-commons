using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllureAttachmentWeaver.Behaviors
{
    /// <summary>
    /// Chains several method weavers into one weaver that applies the weavers one after the other.
    /// </summary>
    public class MultiWeaver : IMethodWeaver
    {
        private IEnumerable<IMethodWeaver> mWeavers;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiWeaver"/> class.
        /// </summary>
        /// <param name="weavers">The weavers.</param>
        public MultiWeaver(IEnumerable<IMethodWeaver> weavers)
        {
            mWeavers = weavers;
        }

        public void Weave(Mono.Cecil.MethodDefinition method)
        {
            foreach (IMethodWeaver weaver in mWeavers)
            {
                // if a weaver failed the resulting assembly is probably malformed so there is no
                // reason to continue so there is no exception handling.
                weaver.Weave(method);
            }
        }
    }
}
