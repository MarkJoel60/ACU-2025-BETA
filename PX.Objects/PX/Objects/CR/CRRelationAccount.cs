// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelationAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR;

public class CRRelationAccount : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldDefaultingSubscriber
{
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CRRelation row))
      return;
    ((PXFieldState) e.ReturnState).Enabled = row.Role != "RE" && row.Role != "SR" && row.Role != "DE" && row.Role != "CH" && row.Role != "PR";
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    int? nullable = new int?();
    CRRelation row = (CRRelation) e.Row;
    if (row == null || row.Role != "RE" && row.Role != "SR" && row.Role != "DE" && row.Role != "CH" && row.Role != "PR" || !row.TargetNoteID.HasValue)
      return;
    object entityRow = new EntityHelper(sender.Graph).GetEntityRow(PXBuildManager.GetType(row.TargetType, false), row.TargetNoteID, false);
    if (entityRow == null)
      return;
    string targetType = row.TargetType;
    if (targetType != null)
    {
      switch (targetType.Length)
      {
        case 20:
          switch (targetType[16 /*0x10*/])
          {
            case 'C':
              if (targetType == "PX.Objects.CR.CRCase")
              {
                nullable = ((CRCase) entityRow).CustomerID;
                goto label_35;
              }
              goto label_35;
            case 'L':
              if (targetType == "PX.Objects.CR.CRLead")
                goto label_34;
              goto label_35;
            case 'n':
              if (targetType == "PX.Objects.AP.Vendor")
                break;
              goto label_35;
            default:
              goto label_35;
          }
          break;
        case 21:
          switch (targetType[11])
          {
            case 'C':
              switch (targetType)
              {
                case "PX.Objects.CR.CRQuote":
                  nullable = ((CRQuote) entityRow).BAccountID;
                  goto label_35;
                case "PX.Objects.CR.Contact":
                  goto label_34;
                default:
                  goto label_35;
              }
            case 'P':
              if (targetType == "PX.Objects.PO.POOrder")
              {
                nullable = ((PX.Objects.PO.POOrder) entityRow).VendorID;
                goto label_35;
              }
              goto label_35;
            case 'S':
              if (targetType == "PX.Objects.SO.SOOrder")
              {
                nullable = ((PX.Objects.SO.SOOrder) entityRow).CustomerID;
                goto label_35;
              }
              goto label_35;
            default:
              goto label_35;
          }
        case 22:
          switch (targetType[11])
          {
            case 'A':
              if (targetType == "PX.Objects.AR.Customer")
                break;
              goto label_35;
            case 'C':
              if (targetType == "PX.Objects.CR.BAccount")
                break;
              goto label_35;
            default:
              goto label_35;
          }
          break;
        case 23:
          switch (targetType[12])
          {
            case 'P':
              if (targetType == "PX.Objects.AP.APInvoice")
              {
                nullable = ((PX.Objects.AP.APRegister) entityRow).VendorID;
                goto label_35;
              }
              goto label_35;
            case 'R':
              if (targetType == "PX.Objects.AR.ARInvoice")
              {
                nullable = ((PX.Objects.AR.ARRegister) entityRow).CustomerID;
                goto label_35;
              }
              goto label_35;
            default:
              goto label_35;
          }
        case 24:
          switch (targetType[16 /*0x10*/])
          {
            case 'C':
              if (targetType == "PX.Objects.CR.CRCampaign")
                goto label_35;
              goto label_35;
            case 'E':
              if (targetType == "PX.Objects.CR.CREmployee")
                goto label_35;
              goto label_35;
            default:
              goto label_35;
          }
        case 27:
          if (targetType == "PX.Objects.CR.CROpportunity")
          {
            nullable = ((CROpportunity) entityRow).BAccountID;
            goto label_35;
          }
          goto label_35;
        case 35:
          if (targetType == "PX.Objects.EP.EPExpenseClaimDetails")
          {
            nullable = ((EPExpenseClaimDetails) entityRow).CustomerID;
            goto label_35;
          }
          goto label_35;
        default:
          goto label_35;
      }
      nullable = ((BAccount) entityRow).BAccountID;
      goto label_35;
label_34:
      nullable = ((Contact) entityRow).BAccountID;
    }
label_35:
    e.NewValue = (object) nullable;
  }
}
