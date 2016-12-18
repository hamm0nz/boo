﻿#region license
// Copyright (c) 2004, Rodrigo B. de Oliveira (rbo@acm.org)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Rodrigo B. de Oliveira nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using Boo.Lang.Compiler.TypeSystem.Cci;
using Microsoft.Cci;

namespace Boo.Lang.Compiler.TypeSystem
{
	public class ExternalEvent : ExternalEntity<IEventDefinition>, IEvent
	{
	    private IMethod _add;

	    private IMethod _remove;

        public ExternalEvent(ICciTypeSystemProvider typeSystemServices, IEventDefinition event_)
            : base(typeSystemServices, event_)
		{
		}
		
		public virtual IType DeclaringType
		{
			get { return _provider.Map(_memberInfo.ContainingTypeDefinition); }
		}
		
		public virtual IMethod GetAddMethod()
		{
            if (null != _add) return _add;
			return _add = FindAddMethod();
		}

	    private IMethod FindAddMethod()
	    {
	        return _provider.Map(_memberInfo.Adder.ResolvedMethod);
	    }

	    public virtual IMethod GetRemoveMethod()
		{
            if (null != _remove) return _remove;
			return _remove = FindRemoveMethod();
		}

	    private IMethod FindRemoveMethod()
	    {
	        return _provider.Map(_memberInfo.Remover.ResolvedMethod);
	    }

	    public virtual IMethod GetRaiseMethod()
		{
			return _provider.Map(_memberInfo.Caller.ResolvedMethod);
		}

        public IEventDefinition EventInfo
		{
			get { return _memberInfo; }
		}
		
		public bool IsPublic
		{
			get { return GetAddMethod().IsPublic; }
		}

        public override EntityType EntityType
		{
			get { return EntityType.Event; }
		}
		
		public virtual IType Type
		{
			get
			{
				return _provider.Map(_memberInfo.Type.ResolvedType);
			}
		}
		
		public bool IsStatic
		{
			get
			{
				return GetAddMethod().IsStatic;
			}
		}

		public bool IsAbstract
		{
			get
			{
				return GetAddMethod().IsAbstract;
				
			}
		}

		public bool IsVirtual
		{
			get
			{
				return GetAddMethod().IsVirtual;
			}
		}

        protected override ITypeDefinition MemberType
		{
			get { return _memberInfo.Type.ResolvedType;  }
		}
	}
}