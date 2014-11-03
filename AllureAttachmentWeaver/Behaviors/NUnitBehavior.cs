using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace AllureAttachmentWeaver
{
    public class NUnitAttachmentWeaver : BaseBehaviorWeaver
    {
        /// <summary>
        /// Weaves the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        public override void Weave(MethodDefinition method)
        {
            method.Body.GetILProcessor().Append(Instruction.Create(OpCodes.Pop));
        }
    }
}

