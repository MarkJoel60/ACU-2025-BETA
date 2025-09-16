// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.APInvoiceEntryLinkLineExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;
using System.Collections;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

/// <summary>
/// Extends AP Invoice Entry with Project related functionality. Requires Project Accounting feature.
/// </summary>
public class APInvoiceEntryLinkLineExt : PXGraphExtension<LinkLineExtension, APInvoiceEntry>
{
  public PXAction<PX.Objects.AP.APInvoice> linkLine;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXUIField]
  [PXLookupButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LinkLine(PXAdapter adapter)
  {
    int? nullable1 = new int?();
    int? projectID = new int?();
    if (((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Current != null)
    {
      nullable1 = ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Current.AccountID;
      projectID = ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Current.ProjectID;
    }
    object[] array = ((PXAction) this.Base1.linkLine).Press(adapter).ToArray<object>();
    PX.Objects.AP.APTran current = ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).Current;
    if (current != null)
    {
      int? nullable2 = nullable1;
      int? accountId = current.AccountID;
      if (!(nullable2.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable2.HasValue == accountId.HasValue) && (!projectID.HasValue || ProjectDefaultAttribute.IsNonProject(projectID)))
      {
        int? nullable3 = current.ProjectID;
        if ((!nullable3.HasValue || ProjectDefaultAttribute.IsNonProject(current.ProjectID)) && current.PONbr != null)
        {
          PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[1]
          {
            (object) current.AccountID
          }));
          if (account != null)
          {
            nullable3 = account.AccountGroupID;
            if (nullable3.HasValue)
            {
              PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POLine, PXSelect<PX.Objects.PO.POLine, Where<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.lineNbr, Equal<Required<PX.Objects.PO.POLine.lineNbr>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<APInvoiceEntry>) this).Base, new object[3]
              {
                (object) current.PONbr,
                (object) current.POOrderType,
                (object) current.POLineNbr
              }));
              if (poLine != null)
                ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APInvoiceEntry>) this).Base.Transactions).SetValueExt<PX.Objects.AP.APTran.projectID>(current, (object) poLine.ProjectID);
            }
          }
        }
      }
    }
    return (IEnumerable) array;
  }
}
