// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARActiveProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.AR;

[PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.visibleInAR, Equal<True>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
public class ARActiveProjectAttribute : ProjectBaseAttribute
{
  public ARActiveProjectAttribute()
    : base((Type) null)
  {
  }
}
