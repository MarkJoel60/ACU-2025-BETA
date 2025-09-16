// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelationContactSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR;

public class CRRelationContactSelectorAttribute : 
  PXCustomSelectorAttribute,
  IPXFieldDefaultingSubscriber
{
  public CRRelationContactSelectorAttribute()
    : base(typeof (Search<Contact.contactID>))
  {
    ((PXSelectorAttribute) this).Filterable = true;
    ((PXSelectorAttribute) this).DescriptionField = typeof (Contact.displayName);
    ((PXSelectorAttribute) this).SelectorMode = (PXSelectorMode) 16 /*0x10*/;
    ((PXSelectorAttribute) this).DirtyRead = true;
  }

  public IEnumerable GetRecords()
  {
    BqlCommand select = ((PXSelectorAttribute) this)._Select;
    PXCache cach = PXView.CurrentGraph.Caches[typeof (CRRelation)];
    data = (CRRelation) null;
    if (PXView.Currents != null)
    {
      object[] currents = PXView.Currents;
      int index = 0;
      while (index < currents.Length && !(currents[index] is CRRelation data))
        ++index;
    }
    if (data == null)
      data = cach.Current as CRRelation;
    if (data == null)
      return (IEnumerable) null;
    BqlCommand bqlCommand = BqlCommand.AppendJoin<LeftJoin<BAccount, On<BAccount.bAccountID, Equal<Contact.bAccountID>>>>(select).WhereAnd<Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>>();
    if (EnumerableExtensions.IsIn<int>(CRRelationTypeListAttribure.GetTargetID<CRRelation.targetType>(cach, (object) data, data.Role), (IEnumerable<int>) new int[2]
    {
      3,
      2
    }))
    {
      if (data.EntityID.HasValue && data.TargetType != "PX.Objects.CR.CREmployee")
        bqlCommand = bqlCommand.WhereAnd<Where<Contact.bAccountID, Equal<Current<CRRelation.entityID>>>>();
    }
    else
      bqlCommand = !data.EntityID.HasValue ? (data.Role == "SV" || data.Role == "TE" || data.Role == "SE" ? bqlCommand.WhereAnd<Where<Contact.contactType, Equal<ContactTypesAttribute.employee>>>() : bqlCommand.WhereAnd<Where<Contact.contactType, Equal<ContactTypesAttribute.person>>>()) : bqlCommand.WhereAnd<Where<Contact.bAccountID, Equal<Current<CRRelation.entityID>>, And<Contact.contactType, Equal<ContactTypesAttribute.person>>>>();
    if (bqlCommand == null)
      return (IEnumerable) null;
    PXView view = this._Graph.TypedViews.GetView(bqlCommand, false);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents1 = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> records = view.Select(currents1, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) records;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ((PXSelectorAttribute) this).FieldSelecting(sender, e);
    if (!(e.Row is CRRelation row))
      return;
    int num;
    switch (CRRelationTypeListAttribure.GetTargetID<CRRelation.targetType>(sender, (object) row, row.Role))
    {
      case 2:
        num = 1;
        break;
      case 3:
        if (row.TargetType != typeof (PX.Objects.AR.Customer).FullName && row.TargetType != typeof (PX.Objects.AP.Vendor).FullName && row.TargetType != typeof (BAccount).FullName && row.TargetType != typeof (PX.Objects.PO.POOrder).FullName)
        {
          num = row.TargetType != typeof (PX.Objects.SO.SOOrder).FullName ? 1 : 0;
          break;
        }
        goto default;
      default:
        num = 0;
        break;
    }
    bool flag = num != 0;
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.Enabled = !flag;
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    CRRelation row = (CRRelation) e.Row;
    if (row == null)
      return;
    int? nullable = new int?();
    object obj = (object) null;
    if (row.TargetNoteID.HasValue)
      obj = new EntityHelper(sender.Graph).GetEntityRow(PXBuildManager.GetType(row.TargetType, false), row.TargetNoteID, false);
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
                nullable = (int?) ((CRCase) obj)?.ContactID;
                goto label_35;
              }
              goto label_35;
            case 'L':
              if (targetType == "PX.Objects.CR.CRLead")
              {
                nullable = (int?) ((CRLead) obj)?.RefContactID;
                goto label_35;
              }
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
                  nullable = (int?) ((CRQuote) obj)?.ContactID;
                  goto label_35;
                case "PX.Objects.CR.Contact":
                  nullable = (int?) ((Contact) obj)?.ContactID;
                  goto label_35;
                default:
                  goto label_35;
              }
            case 'P':
              if (targetType == "PX.Objects.PO.POOrder")
                goto label_35;
              goto label_35;
            case 'S':
              if (targetType == "PX.Objects.SO.SOOrder")
              {
                nullable = (int?) ((PX.Objects.SO.SOOrder) obj)?.ContactID;
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
                goto label_35;
              goto label_35;
            case 'R':
              if (targetType == "PX.Objects.AR.ARInvoice")
                goto label_35;
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
                break;
              goto label_35;
            default:
              goto label_35;
          }
          break;
        case 27:
          if (targetType == "PX.Objects.CR.CROpportunity")
          {
            nullable = (int?) ((CROpportunity) obj)?.ContactID;
            goto label_35;
          }
          goto label_35;
        case 35:
          if (targetType == "PX.Objects.EP.EPExpenseClaimDetails")
            goto label_35;
          goto label_35;
        default:
          goto label_35;
      }
      if (!(obj is BAccount baccount))
        baccount = PXSelectorAttribute.Select<CRRelation.entityID>(sender, (object) row) as BAccount;
      nullable = (int?) baccount?.DefContactID;
    }
label_35:
    if (!nullable.HasValue)
      return;
    e.NewValue = (object) nullable;
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || !((PXSelectorAttribute) this).ValidateValue || sender.Keys.Count != 0 && !(((PXEventSubscriberAttribute) this)._FieldName != sender.Keys[sender.Keys.Count - 1]))
      return;
    PXSelectorAttribute.ViewWithParameters viewWithParameters = ((PXSelectorAttribute) this).GetViewWithParameters(sender, e.NewValue, false);
    object obj = (object) null;
    try
    {
      obj = ((PXSelectorAttribute.ViewWithParameters) ref viewWithParameters).SelectSingleBound(e.Row);
    }
    catch (FormatException ex)
    {
    }
    catch (InvalidCastException ex)
    {
    }
    if (obj != null)
      return;
    ((PXSelectorAttribute) this).throwNoItem(PXSelectorAttribute.hasRestrictedAccess(sender, ((PXSelectorAttribute) this)._PrimarySimpleSelect, e.Row), e.ExternalCall, e.NewValue);
  }
}
