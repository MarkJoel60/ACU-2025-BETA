// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineSubAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOLineSubAccountAttribute(
  Type AccountType,
  Type BranchType,
  bool AccountAndBranchRequired = false) : SubAccountAttribute(typeof (Search<Sub.subID, Where<Match<Current<AccessInfo.userName>>>>), AccountType, BranchType, AccountAndBranchRequired)
{
  protected override void RelatedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (sender.Graph.IsImportFromExcel)
      return;
    base.RelatedFieldUpdated(sender, e);
  }
}
