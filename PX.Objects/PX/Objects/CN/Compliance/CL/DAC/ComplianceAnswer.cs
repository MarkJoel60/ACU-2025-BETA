// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.DAC.ComplianceAnswer
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.DAC;

/// <summary>
/// This class should be used in case graph already has cache with type <see cref="T:PX.Objects.CS.CSAnswers" />.
/// </summary>
[PXCacheName("Compliance Answer")]
public class ComplianceAnswer : CSAnswers
{
  [PXDBGuidNotNull]
  public virtual Guid? NoteId { get; set; }

  public abstract class noteId : IBqlField, IBqlOperand
  {
  }
}
