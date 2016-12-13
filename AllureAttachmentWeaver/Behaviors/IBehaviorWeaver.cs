using System;
using Mono.Cecil;

namespace AllureAttachmentWeaver
{
    public interface IBehaviorWeaver
    {
        void Weave(MethodDefinition method);
    }
}

