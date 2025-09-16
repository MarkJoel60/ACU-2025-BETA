// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAnyStateProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CT;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.AP;

[PXRestrictor(typeof (Where<PMProject.visibleInAP, Equal<True>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.baseType, NotEqual<CTPRType.projectTemplate>, And<PMProject.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new System.Type[] {typeof (PMProject.contractCD)})]
public class APAnyStateProjectAttribute(System.Type where) : ProjectAttribute(where)
{
}
