// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRRelationTypeListAttribure
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public class CRRelationTypeListAttribure : PXStringListAttribute, IPXFieldDefaultingSubscriber
{
  private readonly System.Type RoleType;
  private readonly Dictionary<CRRelationTypeListAttribure.TypeEntityList, CRRelationTypeListAttribure.Item> TypeArray;

  public CRRelationTypeListAttribure(
    System.Type roleType,
    string[] overrideAllowedValues = null,
    string[] overrideAllowedLabels = null)
    : base(new string[0], new string[0])
  {
    this.RoleType = roleType;
    Dictionary<CRRelationTypeListAttribure.TypeEntityList, CRRelationTypeListAttribure.Item> dictionary1 = new Dictionary<CRRelationTypeListAttribure.TypeEntityList, CRRelationTypeListAttribure.Item>();
    dictionary1.Add(CRRelationTypeListAttribure.TypeEntityList.Contact, new CRRelationTypeListAttribure.Item(new string[1]
    {
      "PX.Objects.CR.Contact"
    }, new string[1]{ "Contact" }));
    dictionary1.Add(CRRelationTypeListAttribure.TypeEntityList.BAccount, new CRRelationTypeListAttribure.Item(new string[1]
    {
      "PX.Objects.CR.BAccount"
    }, new string[1]{ "Customer" }));
    Dictionary<CRRelationTypeListAttribure.TypeEntityList, CRRelationTypeListAttribure.Item> dictionary2 = dictionary1;
    string[] fieldNames = overrideAllowedValues;
    if (fieldNames == null)
      fieldNames = new string[15]
      {
        "PX.Objects.AP.APInvoice",
        "PX.Objects.AR.ARInvoice",
        "PX.Objects.CR.BAccount",
        "PX.Objects.CR.CRCampaign",
        "PX.Objects.CR.CRCase",
        "PX.Objects.CR.Contact",
        "PX.Objects.AR.Customer",
        "PX.Objects.CR.CREmployee",
        "PX.Objects.EP.EPExpenseClaimDetails",
        "PX.Objects.CR.CRLead",
        "PX.Objects.CR.CROpportunity",
        "PX.Objects.PO.POOrder",
        "PX.Objects.SO.SOOrder",
        "PX.Objects.CR.CRQuote",
        "PX.Objects.AP.Vendor"
      };
    string[] fieldDisplayNames = overrideAllowedLabels;
    if (fieldDisplayNames == null)
      fieldDisplayNames = new string[15]
      {
        "AP Invoice",
        "AR Invoice",
        "Business Account",
        "Campaign",
        "Case",
        "Contact",
        "Customer",
        "Employee",
        "Expense Receipt",
        "Lead",
        "Opportunity",
        "Purchase Order",
        "Sales Order",
        "Sales Quote",
        "Vendor"
      };
    CRRelationTypeListAttribure.Item obj = new CRRelationTypeListAttribure.Item(fieldNames, fieldDisplayNames);
    dictionary2.Add(CRRelationTypeListAttribure.TypeEntityList.All, obj);
    this.TypeArray = dictionary1;
    this._AllowedValues = ((IEnumerable<string>) this.TypeArray[CRRelationTypeListAttribure.TypeEntityList.All].FieldNames).ToArray<string>();
    this._AllowedLabels = ((IEnumerable<string>) this.TypeArray[CRRelationTypeListAttribure.TypeEntityList.All].FieldDisplayNames).ToArray<string>();
  }

  public virtual void CacheAttached(PXCache sender) => base.CacheAttached(sender);

  public static int GetTargetID<Field>(PXCache cache, object data, string role) where Field : IBqlField
  {
    foreach (CRRelationTypeListAttribure typeListAttribure in cache.GetAttributesOfType<CRRelationTypeListAttribure>(data, typeof (Field).Name))
    {
      if (typeListAttribure != null)
        return typeListAttribure.GetTargetID(role);
    }
    return -1;
  }

  public virtual int GetTargetID(string role)
  {
    CRRelationTypeListAttribure.TypeEntityList targetId1 = CRRelationTypeListAttribure.TypeEntityList.All;
    if (role == null)
      return (int) targetId1;
    CRRelationTypeListAttribure.TypeEntityList targetId2;
    if (role != null && role.Length == 2)
    {
      switch (role[0])
      {
        case 'A':
          if (role == "AL")
          {
            targetId2 = CRRelationTypeListAttribure.TypeEntityList.BAccount;
            goto label_14;
          }
          goto label_13;
        case 'B':
          if (role == "BU")
            break;
          goto label_13;
        case 'D':
          if (role == "DM")
            break;
          goto label_13;
        case 'E':
          if (role == "EV")
            break;
          goto label_13;
        case 'R':
          if (role == "RF")
            break;
          goto label_13;
        case 'S':
          if (role == "SV" || role == "SE")
            break;
          goto label_13;
        case 'T':
          if (role == "TE")
            break;
          goto label_13;
        default:
          goto label_13;
      }
      targetId2 = CRRelationTypeListAttribure.TypeEntityList.Contact;
      goto label_14;
    }
label_13:
    targetId2 = CRRelationTypeListAttribure.TypeEntityList.All;
label_14:
    return (int) targetId2;
  }

  protected virtual CRRelationTypeListAttribure.Item GetTargetType(string role)
  {
    return this.TypeArray[(CRRelationTypeListAttribure.TypeEntityList) this.GetTargetID(role)];
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    string role = sender.GetValue(e.Row, this.RoleType.Name) as string;
    CRRelationTypeListAttribure.Item targetType = this.GetTargetType(role);
    e.ReturnState = (object) (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(-1), (string) null, targetType.FieldNames, Messages.GetLocal(targetType.FieldDisplayNames), new bool?(true), (string) null, targetType.NeutralFieldDisplayNames);
    if (e.Row == null)
      return;
    ((PXFieldState) e.ReturnState).Enabled = ((PXFieldState) e.ReturnState).Enabled && role != null;
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    CRRelationTypeListAttribure.Item targetType = this.GetTargetType(sender.GetValue(e.Row, this.RoleType.Name) as string);
    if (targetType.FieldNames.Length != 1)
      return;
    e.NewValue = (object) targetType.FieldNames[0];
  }

  public enum TypeEntityList
  {
    None,
    Contact,
    BAccount,
    All,
  }

  public class Item
  {
    public string[] FieldNames;
    public string[] NeutralFieldDisplayNames;
    public string[] FieldDisplayNames;

    public Item(string[] fieldNames, string[] fieldDisplayNames)
    {
      this.FieldNames = fieldNames;
      this.FieldDisplayNames = fieldDisplayNames;
      this.NeutralFieldDisplayNames = new string[fieldDisplayNames.Length];
      Array.Copy((Array) fieldDisplayNames, (Array) this.NeutralFieldDisplayNames, fieldDisplayNames.Length);
    }
  }
}
