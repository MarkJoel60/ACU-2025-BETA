// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ReasonApproveRejectFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

[PXHidden]
[Serializable]
public class ReasonApproveRejectFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reason")]
  public virtual string Reason { get; set; }

  public abstract class reason : IBqlField, IBqlOperand
  {
  }
}
