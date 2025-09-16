// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateContactsSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <exclude />
public class CRDuplicateContactsSelectorAttribute : PXCustomSelectorAttribute
{
  private readonly System.Type SourceEntityID;
  private PXView View;

  public CRDuplicateContactsSelectorAttribute(System.Type sourceEntityID)
    : base(typeof (FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRDuplicateGrams>.On<BqlOperand<CRDuplicateGrams.entityID, IBqlInt>.IsEqual<Contact.contactID>>>, FbqlJoins.Inner<CRGrams>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRGrams.validationType, Equal<CRDuplicateGrams.validationType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRGrams.entityName, Equal<CRDuplicateGrams.entityName>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRGrams.fieldName, Equal<CRDuplicateGrams.fieldName>>>>>.And<BqlOperand<CRGrams.fieldValue, IBqlString>.IsEqual<CRDuplicateGrams.fieldValue>>>>>>, FbqlJoins.Left<CRValidation>.On<BqlOperand<CRValidation.type, IBqlString>.IsEqual<CRGrams.validationType>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRGrams.entityID, Equal<BqlField<Contact.contactID, IBqlInt>.AsOptional>>>>>.And<BqlOperand<CRDuplicateGrams.validationType, IBqlString>.IsEqual<ValidationTypesAttribute.contactToContact>>>.Aggregate<To<GroupBy<CRDuplicateGrams.entityID>, GroupBy<CRDuplicateGrams.validationType>, GroupBy<CRDuplicateGrams.entityID>, Sum<CRGrams.score>>>.Having<BqlAggregatedOperand<Sum<CRGrams.score>, IBqlDecimal>.IsGreaterEqual<BqlField<CRValidation.validationThreshold, IBqlDecimal>.Maximized>>, Contact>.SearchFor<Contact.contactID>), new System.Type[6]
    {
      typeof (Contact.isActive),
      typeof (Contact.displayName),
      typeof (Contact.contactType),
      typeof (Contact.salutation),
      typeof (Contact.eMail),
      typeof (Contact.phone1)
    })
  {
    ((PXSelectorAttribute) this).DirtyRead = true;
    ((PXSelectorAttribute) this).DescriptionField = typeof (Contact.displayName);
    this.SourceEntityID = sourceEntityID;
    ((PXSelectorAttribute) this).ValidateValue = false;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.View = new PXView(this._Graph, true, ((PXSelectorAttribute) this)._Select);
  }

  public IEnumerable GetRecords()
  {
    CRDuplicateContactsSelectorAttribute selectorAttribute = this;
    PXCache cach = selectorAttribute._Graph.Caches[selectorAttribute.SourceEntityID.DeclaringType];
    object[] currents = PXView.Currents;
    object obj = (currents != null ? ((IEnumerable<object>) currents).FirstOrDefault<object>() : (object) null) ?? cach.Current;
    if (obj != null)
    {
      int? sourceID = (int?) cach.GetValue(obj, selectorAttribute.SourceEntityID.Name);
      CRDuplicateRecord selectedRec = selectorAttribute._Graph.Caches[typeof (CRDuplicateRecord)].Current as CRDuplicateRecord;
      PXView view = selectorAttribute.View;
      object[] objArray = new object[1]{ (object) sourceID };
      foreach (PXResult pxResult in view.SelectMulti(objArray))
      {
        Contact record = pxResult.GetItem<Contact>();
        int? nullable = record.ContactID;
        int? duplicateContactId = selectedRec.DuplicateContactID;
        if (!(nullable.GetValueOrDefault() == duplicateContactId.GetValueOrDefault() & nullable.HasValue == duplicateContactId.HasValue))
        {
          int? contactId = record.ContactID;
          nullable = sourceID;
          if (!(contactId.GetValueOrDefault() == nullable.GetValueOrDefault() & contactId.HasValue == nullable.HasValue))
            continue;
        }
        yield return (object) record;
      }
    }
  }
}
