// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEntryLight
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;

#nullable disable
namespace PX.Objects.CA;

public class CATranEntryLight : CATranEntry
{
  public PXSelect<CATransfer> transfer;
  public PXSelect<PX.Objects.GL.Account> account;
  public PXSelect<ARPaymentChargeTran> arPaymentCharges;
  public PXSelect<APPaymentChargeTran> apPaymentCharges;

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  public override void CATran_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARPaymentChargeTran.cashAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<APPaymentChargeTran.cashAccountID> e)
  {
  }
}
