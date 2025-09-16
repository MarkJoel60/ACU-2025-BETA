// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Graphs.ComplianceDocumentSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.Common.Helpers;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Graphs;

public class ComplianceDocumentSetupMaint : PXGraph<ComplianceDocumentSetupMaint>
{
  public PXFilter<ComplianceAttributeFilter> Filter;
  public PXSelect<ComplianceAttribute, Where<ComplianceAttribute.type, Equal<Current<ComplianceAttributeFilter.type>>>> Mapping;
  public PXSelect<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> LienWaiverSetup;
  public PXSelect<CSAttributeGroup, Where<CSAttributeGroup.entityType, Equal<ComplianceDocument.typeName>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>> MappingCommon;
  public CRNotificationSetupList<ComplianceNotification> ComplianceNotifications;
  public PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Current<NotificationSetup.setupID>>>> Recipients;
  public PXSelect<ComplianceAttributeType> AttributeType;
  public PXSave<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> Save;
  public PXCancel<PX.Objects.CN.Compliance.CL.DAC.LienWaiverSetup> Cancel;

  public ComplianceDocumentSetupMaint() => FeaturesSetHelper.CheckConstructionFeature();

  [PXDBString(10)]
  [PXDefault]
  [VendorContactType.ClassList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationSetupRecipient.contactID)}, Where = typeof (Where<BqlOperand<NotificationSetupRecipient.setupID, IBqlGuid>.IsEqual<BqlField<NotificationSetupRecipient.setupID, IBqlGuid>.FromCurrent>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSetupRecipient.contactType> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationSetupRecipient.contactType), typeof (SearchFor<PX.Objects.CR.Contact.contactID>.In<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<EPEmployee>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>>.And<BqlOperand<EPEmployee.defContactID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.contactID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<NotificationSetupRecipient.contactType>, Equal<NotificationContactType.employee>>>>>.And<BqlOperand<EPEmployee.acctCD, IBqlString>.IsNotNull>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSetupRecipient.contactID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<ComplianceAttributeFilter> args)
  {
    ComplianceAttributeFilter row = args.Row;
    if (row == null)
      return;
    int? type = row.Type;
    int? waiverAttributeTypeId = this.GetLienWaiverAttributeTypeId();
    ((PXSelectBase) this.Mapping).Enable(!(type.GetValueOrDefault() == waiverAttributeTypeId.GetValueOrDefault() & type.HasValue == waiverAttributeTypeId.HasValue));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CSAttributeGroup> args)
  {
    if (args.Operation != 2 || args.Row == null || this.DoesAttributeExist(args.Row.AttributeID))
      return;
    args.Cancel = true;
    ((PXSelectBase) this.MappingCommon).Cache.Clear();
  }

  protected virtual void _(PX.Data.Events.RowInserting<ComplianceAttribute> args)
  {
    ComplianceAttribute row = args.Row;
    if (row == null || ((PXSelectBase<ComplianceAttributeFilter>) this.Filter).Current == null)
      return;
    row.Type = ((PXSelectBase<ComplianceAttributeFilter>) this.Filter).Current.Type;
  }

  protected virtual void _(PX.Data.Events.RowInserting<CSAttributeGroup> args)
  {
    args.Row.EntityClassID = "COMPLIANCE";
    args.Row.EntityType = typeof (ComplianceDocument).FullName;
  }

  protected virtual void _(PX.Data.Events.RowDeleting<ComplianceAttribute> args)
  {
    int? attributeId = args.Row.AttributeId;
    PXSelect<ComplianceDocument> pxSelect = new PXSelect<ComplianceDocument>((PXGraph) this);
    if (((PXSelectBase<ComplianceAttributeType>) new PXSelect<ComplianceAttributeType, Where<ComplianceAttributeType.complianceAttributeTypeID, Equal<Required<ComplianceAttributeType.complianceAttributeTypeID>>>>((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) args.Row.Type
    }).Type.Trim() == "Status")
      ((PXSelectBase<ComplianceDocument>) pxSelect).WhereAnd<Where<ComplianceDocument.status, Equal<Required<ComplianceAttribute.attributeId>>>>();
    else
      ((PXSelectBase<ComplianceDocument>) pxSelect).WhereAnd<Where<ComplianceDocument.documentTypeValue, Equal<Required<ComplianceAttribute.attributeId>>>>();
    if (((PXSelectBase<ComplianceDocument>) pxSelect).SelectSingle(new object[1]
    {
      (object) attributeId
    }) != null)
      throw new PXException("The value can not be deleted. It is used in at least one compliance document.");
  }

  protected virtual void _(PX.Data.Events.RowDeleting<CSAttributeGroup> args)
  {
    if (this.ShowConfirmationDialog() == 2)
      args.Cancel = true;
    else
      this.DeleteAttributeFromCompliance(args.Row.AttributeID);
  }

  private WebDialogResult ShowConfirmationDialog()
  {
    return ((PXSelectBase<CSAttributeGroup>) this.MappingCommon).Ask("Warning", "This action will delete the attribute from the compliance documents and all attribute values from corresponding records", (MessageButtons) 1, (MessageIcon) 2);
  }

  private void DeleteAttributeFromCompliance(string attributeId)
  {
    foreach (CSAttributeGroup attributeGroup in this.GetAttributeGroups(attributeId))
      GraphHelper.Caches<CSAttributeGroup>((PXGraph) this).Delete(attributeGroup);
  }

  private IEnumerable<CSAttributeGroup> GetAttributeGroups(string attributeId)
  {
    return (IEnumerable<CSAttributeGroup>) ((PXSelectBase<CSAttributeGroup>) new PXSelect<CSAttributeGroup, Where<CSAttributeGroup.attributeID, Equal<Required<CSAttributeGroup.attributeID>>, And<CSAttributeGroup.entityClassID, Equal<ComplianceDocument.complianceClassId>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) attributeId
    }).FirstTableItems.ToList<CSAttributeGroup>();
  }

  private bool DoesAttributeExist(string attributeId)
  {
    return ((PXSelectBase<CSAttribute>) new PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>>((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) attributeId
    }) != null;
  }

  private int? GetLienWaiverAttributeTypeId()
  {
    return ((PXGraph) this).Select<ComplianceAttributeType>().Single<ComplianceAttributeType>((Expression<Func<ComplianceAttributeType, bool>>) (type => type.Type == "Lien Waiver")).ComplianceAttributeTypeID;
  }
}
