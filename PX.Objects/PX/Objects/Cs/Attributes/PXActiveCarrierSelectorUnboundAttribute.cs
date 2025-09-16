// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Attributes.PXActiveCarrierSelectorUnboundAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS.Attributes;

[PXString(15, IsUnicode = true)]
public class PXActiveCarrierSelectorUnboundAttribute : PXActiveCarrierSelectorAttribute
{
  public PXActiveCarrierSelectorUnboundAttribute(Type type)
    : base(type)
  {
  }

  public PXActiveCarrierSelectorUnboundAttribute(Type SearchType, params Type[] fields)
    : base(SearchType, fields)
  {
  }
}
