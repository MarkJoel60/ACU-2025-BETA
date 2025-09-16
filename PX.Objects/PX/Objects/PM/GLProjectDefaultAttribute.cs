// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GLProjectDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Project Default Attribute specific for GL Module. Defaulting of ProjectID field in GL depends on the Ledger type.
/// Budget and Report Ledgers do not require Project and hense it is always defaulted with Non-Project for these ledgers.
/// </summary>
public class GLProjectDefaultAttribute : ProjectDefaultAttribute
{
  protected readonly Type ledgerType;

  public GLProjectDefaultAttribute(Type ledgerType)
    : base("GL")
  {
    this.ledgerType = ledgerType;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXEventSubscriberAttribute) this).CacheAttached(sender);
    // ISSUE: method pointer
    sender.Graph.FieldUpdated.AddHandler(this.ledgerType, this.ledgerType.Name, new PXFieldUpdated((object) this, __methodptr(\u003CCacheAttached\u003Eb__2_0)));
  }

  protected override bool IsDefaultNonProject(PXCache sender, object row)
  {
    object ledgerID = sender.GetValue(row, this.ledgerType.Name);
    Ledger ledger = Ledger.PK.Find(sender.Graph, ledgerID as int?);
    return ledger != null && (ledger.BalanceType == "R" || ledger.BalanceType == "B") || base.IsDefaultNonProject(sender, row);
  }
}
