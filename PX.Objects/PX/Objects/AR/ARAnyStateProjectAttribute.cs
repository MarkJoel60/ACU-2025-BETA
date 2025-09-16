// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAnyStateProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CT;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.AR;

[PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.baseType, NotEqual<CTPRType.projectTemplate>, And<PMProject.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
public class ARAnyStateProjectAttribute(Type where) : ProjectAttribute(where)
{
}
