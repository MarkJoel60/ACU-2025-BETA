// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.MassProcess;
using PX.Objects.CR.Extensions.SideBySideComparison.Merge;
using PX.Objects.CR.Wizard;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI.WebControls;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <summary>
/// Extension that is used for deduplication purposes. Extension uses CRGrams mechanizm. Works with BAccount and Contact entities.
/// </summary>
public abstract class CRDuplicateEntities<TGraph, TMain> : PXGraphExtension<
#nullable disable
TGraph>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, INotable, new()
{
  [PXHidden]
  public PXSelectExtension<Document> Documents;
  [PXHidden]
  public PXSelectExtension<DuplicateDocument> DuplicateDocuments;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSetupOptional<CRSetup> Setup;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<FieldValue, Where<FieldValue.attributeID, IsNull>, OrderBy<Asc<FieldValue.order>>> PopupConflicts;
  protected PXView dbView;
  [PXOverride]
  public FbqlSelect<SelectFromBase<CRDuplicateRecord, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DuplicateContact>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<BAccountR>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.CRLead>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.Address>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<CRActivityStatistics>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Order<By<Asc<DuplicateContact.contactPriority>, Asc<DuplicateContact.contactID>>>, CRDuplicateRecord>.View Duplicates;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRDuplicateRecord, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DuplicateContact>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<BAccountR>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.CRLead>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.Address>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<CRActivityStatistics>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Order<By<Asc<DuplicateContact.contactPriority>, Asc<DuplicateContact.contactID>>>, CRDuplicateRecord>.View DuplicatesForMerging;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CRDuplicateRecordForLinking, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DuplicateContact>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<BAccountR>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.CRLead>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.Address>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<CRActivityStatistics>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Order<By<Asc<DuplicateContact.contactPriority>, Asc<DuplicateContact.contactID>>>, CRDuplicateRecordForLinking>.View DuplicatesForLinking;
  public PXAction<TMain> CheckForDuplicates;
  public PXAction<TMain> DuplicateMerge;
  public PXAction<TMain> DuplicateAttach;
  public PXAction<TMain> ViewMergingDuplicate;
  public PXAction<TMain> ViewLinkingDuplicate;
  public PXAction<TMain> ViewLinkingDuplicateRefContact;
  public PXAction<TMain> ViewMergingDuplicateBAccount;
  public PXAction<TMain> ViewLinkingDuplicateBAccount;
  public PXAction<TMain> MarkAsValidated;
  public PXAction<TMain> CloseAsDuplicate;

  protected abstract PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.DocumentMapping GetDocumentMapping();

  protected abstract PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.DuplicateDocumentMapping GetDuplicateDocumentMapping();

  public IEnumerable popupConflicts()
  {
    return (IEnumerable) this.Base.Caches[typeof (FieldValue)].Cached.Cast<FieldValue>().Where<FieldValue>((Func<FieldValue, bool>) (fld => !fld.Hidden.GetValueOrDefault()));
  }

  /// <summary>
  /// The delegate that fetches the duplicates for the current record, if there are some possible duplicates found already.
  /// </summary>
  /// <param name="forceSelect">Skip "if there are some possible duplicates found already" check on duplicates ckecking</param>
  /// <returns></returns>
  public virtual IEnumerable duplicates(bool? forceSelect = null)
  {
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain> duplicateEntities = this;
    if (duplicateEntities.Base.Caches[typeof (CRSetup)].Current is CRSetup)
    {
      DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) duplicateEntities.DuplicateDocuments).Current ?? (((PXSelectBase<DuplicateDocument>) duplicateEntities.DuplicateDocuments).Current = ((PXSelectBase<DuplicateDocument>) duplicateEntities.DuplicateDocuments).SelectSingle(Array.Empty<object>()));
      if (duplicateDocument != null && (forceSelect.GetValueOrDefault() ? 1 : (duplicateDocument.DuplicateFound.GetValueOrDefault() ? 1 : 0)) != 0)
      {
        List<object> objectList = (List<object>) null;
        if (((PXSelectBase) duplicateEntities.DuplicateDocuments).Cache.GetStatus((object) duplicateDocument) == 2 && !PXTransactionScope.IsScoped)
        {
          using (new PXTransactionScope())
          {
            duplicateEntities.PersistGrams(duplicateEntities.GetGramContext(duplicateDocument));
            objectList = duplicateEntities.dbView.SelectMulti(new object[1]
            {
              (object) true
            });
          }
        }
        else
          objectList = duplicateEntities.dbView.SelectMulti(new object[1]
          {
            (object) true
          });
        foreach (PXResult pxResult in objectList)
        {
          CRGrams crGrams = pxResult.GetItem<CRGrams>();
          CRDuplicateGrams crDuplicateGrams = pxResult.GetItem<CRDuplicateGrams>();
          DuplicateContact p2 = pxResult.GetItem<DuplicateContact>();
          PX.Objects.CR.CRLead crLead = pxResult.GetItem<PX.Objects.CR.CRLead>();
          BAccountR baccountR = pxResult.GetItem<BAccountR>();
          CRDuplicateRecord p1 = new CRDuplicateRecord()
          {
            ContactID = crGrams.EntityID,
            ValidationType = crGrams.ValidationType,
            DuplicateContactID = crDuplicateGrams.EntityID,
            Score = crGrams.Score,
            DuplicateContactType = p2?.ContactType,
            DuplicateBAccountID = (int?) p2?.BAccountID,
            DuplicateRefContactID = (int?) crLead?.RefContactID,
            Phone1 = p2?.Phone1
          };
          PX.Objects.CR.Address address;
          switch (p2.ContactType)
          {
            case "PN":
              address = pxResult.GetItem<PX.Objects.CR.Address>();
              break;
            case "LD":
              address = pxResult.GetItem<PX.Objects.CR.Address>();
              break;
            case "AP":
              address = (PX.Objects.CR.Address) pxResult.GetItem<Address2>();
              break;
            default:
              // ISSUE: reference to a compiler-generated method
              \u003CPrivateImplementationDetails\u003E.ThrowInvalidOperationException();
              break;
          }
          PX.Objects.CR.Address p5 = address;
          CRActivityStatistics activityStatistics;
          switch (p1.DuplicateContactType)
          {
            case "PN":
              activityStatistics = CRActivityStatistics.PK.Find((PXGraph) duplicateEntities.Base, p2.NoteID);
              break;
            case "LD":
              activityStatistics = CRActivityStatistics.PK.Find((PXGraph) duplicateEntities.Base, crLead.NoteID);
              break;
            case "AP":
              activityStatistics = CRActivityStatistics.PK.Find((PXGraph) duplicateEntities.Base, baccountR.NoteID);
              break;
            default:
              // ISSUE: reference to a compiler-generated method
              \u003CPrivateImplementationDetails\u003E.ThrowInvalidOperationException();
              break;
          }
          CRActivityStatistics p6 = activityStatistics;
          PX.Objects.CR.Standalone.Location location;
          switch (p1.DuplicateContactType)
          {
            case "PN":
              location = (PX.Objects.CR.Standalone.Location) null;
              break;
            case "LD":
              location = (PX.Objects.CR.Standalone.Location) null;
              break;
            case "AP":
              location = pxResult.GetItem<PX.Objects.CR.Standalone.Location>();
              break;
            default:
              // ISSUE: reference to a compiler-generated method
              \u003CPrivateImplementationDetails\u003E.ThrowInvalidOperationException();
              break;
          }
          PX.Objects.CR.Standalone.Location p7 = location;
          yield return (object) new CRDuplicateResult(p1, p2, pxResult.GetItem<BAccountR>(), pxResult.GetItem<PX.Objects.CR.CRLead>(), p5, p6, p7);
        }
      }
    }
  }

  protected virtual IEnumerable duplicatesForMerging()
  {
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain> duplicateEntities = this;
    foreach (CRDuplicateResult result in ((IEnumerable<PXResult<CRDuplicateRecord>>) ((PXSelectBase<CRDuplicateRecord>) duplicateEntities.Duplicates).Select(Array.Empty<object>())).ToList<PXResult<CRDuplicateRecord>>().Cast<CRDuplicateResult>().Where<CRDuplicateResult>(new Func<CRDuplicateResult, bool>(duplicateEntities.WhereMergingMet)))
    {
      ((PXResult) result).GetItem<CRDuplicateRecord>().CanBeMerged = new bool?(duplicateEntities.CanBeMerged(result));
      yield return (object) result;
    }
  }

  protected virtual IEnumerable duplicatesForLinking()
  {
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain> duplicateEntities = this;
    foreach (CRDuplicateResult crDuplicateResult in ((IEnumerable<PXResult<CRDuplicateRecord>>) ((PXSelectBase<CRDuplicateRecord>) duplicateEntities.Duplicates).Select(Array.Empty<object>())).ToList<PXResult<CRDuplicateRecord>>().Cast<CRDuplicateResult>().Where<CRDuplicateResult>(new Func<CRDuplicateResult, bool>(duplicateEntities.WhereLinkingMet)))
    {
      CRDuplicateRecord crDuplicateRecord = ((PXResult) crDuplicateResult).GetItem<CRDuplicateRecord>();
      CRDuplicateRecordForLinking p1 = new CRDuplicateRecordForLinking();
      p1.ContactID = crDuplicateRecord.ContactID;
      p1.ValidationType = crDuplicateRecord.ValidationType;
      p1.DuplicateContactID = crDuplicateRecord.DuplicateContactID;
      p1.DuplicateRefContactID = crDuplicateRecord.DuplicateRefContactID;
      p1.DuplicateBAccountID = crDuplicateRecord.DuplicateBAccountID;
      p1.Score = crDuplicateRecord.Score;
      p1.DuplicateContactType = crDuplicateRecord.DuplicateContactType;
      yield return (object) new CRDuplicateResult((CRDuplicateRecord) p1, ((PXResult) crDuplicateResult).GetItem<DuplicateContact>(), ((PXResult) crDuplicateResult).GetItem<BAccountR>(), ((PXResult) crDuplicateResult).GetItem<PX.Objects.CR.CRLead>(), ((PXResult) crDuplicateResult).GetItem<PX.Objects.CR.Address>(), ((PXResult) crDuplicateResult).GetItem<CRActivityStatistics>(), ((PXResult) crDuplicateResult).GetItem<PX.Objects.CR.Standalone.Location>());
    }
  }

  public virtual System.Type MatchingConditions
  {
    get
    {
      return typeof (BqlOperand<CRGrams.validationType, IBqlString>.IsEqual<SwitchMirror<IBqlString, TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>, ValidationTypesAttribute.leadToLead>>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>, ValidationTypesAttribute.leadToContact>>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>, ValidationTypesAttribute.contactToContact>>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>, ValidationTypesAttribute.contactToLead>>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.lead>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.bAccountProperty>>>, ValidationTypesAttribute.leadToAccount>>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.person>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.bAccountProperty>>>, ValidationTypesAttribute.contactToAccount>, ValidationTypesAttribute.accountToAccount>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Contact2.contactType, IsNotNull>>>, And<BqlOperand<Contact2.contactType, IBqlString>.IsEqual<ContactTypesAttribute.bAccountProperty>>>>.Or<BqlOperand<Current<DuplicateDocument.contactType>, IBqlString>.IsEqual<ContactTypesAttribute.bAccountProperty>>>>>.And<BqlOperand<DuplicateContact.contactType, IBqlString>.IsEqual<ContactTypesAttribute.bAccountProperty>>>.Else<Empty>>);
    }
  }

  public virtual System.Type AdditionalConditions
  {
    get => typeof (BqlOperand<True, IBqlBool>.IsEqual<True>);
  }

  public virtual string WarningMessage => "";

  public virtual bool HardBlockOnly { get; set; }

  public virtual CRGramProcessor Processor { get; set; }

  protected static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.contactDuplicate>();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.Processor = new CRGramProcessor((PXGraph) this.Base);
    PXDBAttributeAttribute.Activate(this.Base.Caches[typeof (TMain)]);
    GraphHelper.EnsureCachePersistence((PXGraph) this.Base, typeof (CRActivityStatistics));
    this.dbView = new PXView((PXGraph) this.Base, false, BqlTemplate.OfCommand<SelectFromBase<CRGrams, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRDuplicateGrams>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRDuplicateGrams.validationType, Equal<CRGrams.validationType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRDuplicateGrams.entityName, Equal<CRGrams.entityName>>>>, And<BqlOperand<CRDuplicateGrams.fieldName, IBqlString>.IsEqual<CRGrams.fieldName>>>, And<BqlOperand<CRDuplicateGrams.fieldValue, IBqlString>.IsEqual<CRGrams.fieldValue>>>>.And<BqlOperand<CRDuplicateGrams.entityID, IBqlInt>.IsNotEqual<CRGrams.entityID>>>>>, FbqlJoins.Left<Contact2>.On<BqlOperand<Contact2.contactID, IBqlInt>.IsEqual<CRGrams.entityID>>>, FbqlJoins.Inner<DuplicateContact>.On<BqlOperand<DuplicateContact.contactID, IBqlInt>.IsEqual<CRDuplicateGrams.entityID>>>, FbqlJoins.Left<BAccountR>.On<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<DuplicateContact.bAccountID>>>, FbqlJoins.Left<PX.Objects.CR.CRLead>.On<BqlOperand<PX.Objects.CR.CRLead.contactID, IBqlInt>.IsEqual<CRDuplicateGrams.entityID>>>, FbqlJoins.Left<CRValidation>.On<BqlOperand<CRValidation.type, IBqlString>.IsEqual<CRGrams.validationType>>>, FbqlJoins.Left<PX.Objects.CR.Address>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.contactType, In3<ContactTypesAttribute.person, ContactTypesAttribute.lead>>>>>.And<BqlOperand<PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<DuplicateContact.defAddressID>>>>, FbqlJoins.Left<Address2>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>.And<BqlOperand<Address2.addressID, IBqlInt>.IsEqual<BAccountR.defAddressID>>>>, FbqlJoins.Left<PX.Objects.CR.Standalone.Location>.On<BqlOperand<PX.Objects.CR.Standalone.Location.locationID, IBqlInt>.IsEqual<BAccountR.defLocationID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CRGrams.entityID, Equal<BqlField<DuplicateDocument.contactID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<True, IBqlBool>.IsEqual<P.AsBool>>>, And<Brackets<BqlPlaceholder.M>>>, And<BqlOperand<DuplicateContact.isActive, IBqlBool>.IsEqual<True>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DuplicateContact.contactType, NotEqual<ContactTypesAttribute.bAccountProperty>>>>>.Or<BqlOperand<DuplicateContact.contactID, IBqlInt>.IsEqual<BAccountR.defContactID>>>>>>.And<Brackets<BqlPlaceholder.W>>>.Aggregate<To<GroupBy<CRGrams.entityID>, GroupBy<CRGrams.validationType>, GroupBy<CRDuplicateGrams.entityID>, GroupBy<DuplicateContact.contactType>, Sum<CRGrams.score>, Max<CRValidation.validationThreshold>>>.Having<BqlAggregatedOperand<Sum<CRGrams.score>, IBqlDecimal>.IsGreaterEqual<BqlField<CRValidation.validationThreshold, IBqlDecimal>.Maximized>>>.Replace<BqlPlaceholder.M>(this.MatchingConditions).Replace<BqlPlaceholder.W>(this.AdditionalConditions).ToCommand());
    this.GenerateUDFColumns<CRDuplicateRecord>();
    this.GenerateUDFColumns<CRDuplicateRecordForLinking>();
  }

  public virtual void GenerateUDFColumns<T>() where T : CRDuplicateRecord
  {
    if (this.Base.Accessinfo.ScreenID == null)
      return;
    foreach (Tuple<PXFieldState, short, short, string> tuple in (IEnumerable<Tuple<PXFieldState, short, short, string>>) ((IEnumerable<Tuple<PXFieldState, short, short, string>>) KeyValueHelper.GetAttributeFields(this.Base.Accessinfo.GetNormalizedScreenID())).OrderBy<Tuple<PXFieldState, short, short, string>, short>((Func<Tuple<PXFieldState, short, short, string>, short>) (t => t.Item3)).ThenBy<Tuple<PXFieldState, short, short, string>, short>((Func<Tuple<PXFieldState, short, short, string>, short>) (t => t.Item2)))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.\u003C\u003Ec__DisplayClass32_0<T> cDisplayClass320 = new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.\u003C\u003Ec__DisplayClass32_0<T>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass320.\u003C\u003E4__this = this;
      PXFieldState pxFieldState;
      tuple.Deconstruct<PXFieldState, short, short, string>(out pxFieldState, out short _, out short _, out string _);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass320.fieldState = pxFieldState;
      // ISSUE: reference to a compiler-generated field
      if (!this.Base.Caches[typeof (T)].Fields.Contains(cDisplayClass320.fieldState.Name))
      {
        // ISSUE: reference to a compiler-generated field
        this.Base.Caches[typeof (T)].Fields.Add(cDisplayClass320.fieldState.Name);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        this.Base.FieldSelecting.AddHandler(typeof (T), cDisplayClass320.fieldState.Name, new PXFieldSelecting((object) cDisplayClass320, __methodptr(\u003CGenerateUDFColumns\u003Eb__2)));
      }
    }
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable checkForDuplicates(PXAdapter adapter)
  {
    DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current ?? ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
    if (duplicateDocument == null)
      return adapter.Get();
    string duplicateStatus = duplicateDocument.DuplicateStatus;
    if (this.CheckIsActive())
      this.CheckIfAnyDuplicates(duplicateDocument, true);
    if (duplicateDocument.DuplicateStatus != duplicateStatus)
    {
      GraphHelper.MarkUpdated(((PXSelectBase) this.DuplicateDocuments).Cache, (object) duplicateDocument);
      this.Base.Actions.PressSave();
    }
    if (duplicateDocument.DuplicateStatus == "PD" || duplicateDocument.DuplicateFound.GetValueOrDefault())
      ((PXSelectBase) this.DuplicateDocuments).Cache.RaiseExceptionHandling<DuplicateDocument.duplicateStatus>((object) duplicateDocument, (object) duplicateDocument.DuplicateStatus, (Exception) new PXSetPropertyException(this.WarningMessage, (PXErrorLevel) 2));
    else
      ((PXSelectBase) this.DuplicateDocuments).Cache.RaiseExceptionHandling<DuplicateDocument.duplicateStatus>((object) duplicateDocument, (object) duplicateDocument.DuplicateStatus, (Exception) null);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable duplicateMerge(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.\u003C\u003Ec__DisplayClass36_0 cDisplayClass360 = new PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.\u003C\u003Ec__DisplayClass36_0();
    if (!WebDialogResultExtension.IsPositive(this.Base.GetProcessingExtension<MergeEntitiesExt<TGraph, TMain>>().AskMerge((object) ((PXSelectBase<CRDuplicateRecord>) this.Duplicates).Current.DuplicateContactID)))
      return adapter.Get();
    this.Base.Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass360.graph = this.Base.CloneGraphState<TGraph>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass360.mergeExt = cDisplayClass360.graph.GetProcessingExtension<MergeEntitiesExt<TGraph, TMain>>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass360.thisExt = cDisplayClass360.graph.GetProcessingExtension<PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass360, __methodptr(\u003CduplicateMerge\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void duplicateAttach()
  {
    DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current ?? ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
    if (duplicateDocument == null)
      return;
    this.DoDuplicateAttach(duplicateDocument);
    if (!this.Base.IsContractBasedAPI)
      return;
    this.Base.Actions.PressSave();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewMergingDuplicate()
  {
    this.ViewDuplicate(((PXSelectBase<CRDuplicateRecord>) this.DuplicatesForMerging).Current);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewLinkingDuplicate()
  {
    this.ViewDuplicate((CRDuplicateRecord) ((PXSelectBase<CRDuplicateRecordForLinking>) this.DuplicatesForLinking).Current);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewLinkingDuplicateRefContact()
  {
    this.ViewDuplicateRefContact((CRDuplicateRecord) ((PXSelectBase<CRDuplicateRecordForLinking>) this.DuplicatesForLinking).Current);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewMergingDuplicateBAccount()
  {
    this.ViewDuplicateBAccount(((PXSelectBase<CRDuplicateRecord>) this.DuplicatesForMerging).Current);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewLinkingDuplicateBAccount()
  {
    this.ViewDuplicateBAccount((CRDuplicateRecord) ((PXSelectBase<CRDuplicateRecordForLinking>) this.DuplicatesForLinking).Current);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable markAsValidated(PXAdapter adapter)
  {
    DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current ?? ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
    if (duplicateDocument == null)
      return adapter.Get();
    duplicateDocument.DuplicateStatus = "VA";
    duplicateDocument.DuplicateFound = new bool?(false);
    ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Update(duplicateDocument);
    this.Base.Actions.PressSave();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable closeAsDuplicate(PXAdapter adapter)
  {
    DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current ?? ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
    if (duplicateDocument == null)
      return adapter.Get();
    duplicateDocument.DuplicateStatus = "DD";
    duplicateDocument.IsActive = new bool?(false);
    ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Update(duplicateDocument);
    this.Base.Actions.PressSave();
    return adapter.Get();
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccountR.type> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<DuplicateDocument> e)
  {
    if (e.Row == null)
      return;
    bool flag = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DuplicateDocument>>) e).Cache.GetOriginal((object) e.Row) != null;
    PXAction<TMain> markAsValidated = this.MarkAsValidated;
    bool? isActive;
    int num1;
    if (flag)
    {
      isActive = e.Row.IsActive;
      if (isActive.GetValueOrDefault())
      {
        num1 = e.Row.DuplicateStatus != "VA" ? 1 : 0;
        goto label_5;
      }
    }
    num1 = 0;
label_5:
    ((PXAction) markAsValidated).SetEnabled(num1 != 0);
    PXAction<TMain> closeAsDuplicate = this.CloseAsDuplicate;
    int num2;
    if (flag)
    {
      isActive = e.Row.IsActive;
      if (isActive.GetValueOrDefault())
      {
        num2 = e.Row.DuplicateStatus != "DD" ? 1 : 0;
        goto label_9;
      }
    }
    num2 = 0;
label_9:
    ((PXAction) closeAsDuplicate).SetEnabled(num2 != 0);
    ((PXAction) this.DuplicateMerge).SetEnabled(flag);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CRDuplicateRecord> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRDuplicateRecord>>) e).Cache.IsDirty = false;
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetReadOnly<CRDuplicateRecord.duplicateRefContactID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRDuplicateRecord>>) e).Cache, (object) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CRDuplicateRecord> e) => e.Cancel = true;

  protected virtual void _(PX.Data.Events.RowPersisting<FieldValue> e) => e.Cancel = true;

  protected virtual void _(PX.Data.Events.RowPersisted<DuplicateDocument> e)
  {
    DuplicateDocument row = e.Row;
    if (row == null || e.TranStatus != null)
      return;
    (bool IsGramsCreated, string NewDuplicateStatus, DateTime? GramValidationDate) tuple = this.PersistGrams(this.GetGramContext(row));
    if (!tuple.IsGramsCreated)
      return;
    ((PXSelectBase) this.DuplicateDocuments).Cache.SetValue<DuplicateDocument.duplicateStatus>((object) row, (object) tuple.NewDuplicateStatus);
    ((PXSelectBase) this.DuplicateDocuments).Cache.SetValue<DuplicateDocument.grammValidationDateTime>((object) row, (object) tuple.GramValidationDate);
    if (e.Operation != 2)
      return;
    this.CheckBlockingOnEntry(row);
  }

  [PXOverride]
  public virtual void Persist(Action del)
  {
    if (((PXSelectBase) this.Documents).View.Answer == 7)
    {
      if (!WizardScope.IsScoped)
        return;
      ((PXSelectBase) this.Documents).View.Answer = (WebDialogResult) 0;
    }
    else
    {
      del();
      this.OnAfterPersist();
    }
  }

  public virtual PXGraph MergePostProcessing(TMain target, TMain duplicate)
  {
    EntityHelper entityHelper = new EntityHelper((PXGraph) this.Base);
    object entity1 = (object) duplicate;
    PXGraph instance1 = PXGraph.CreateInstance(entityHelper.GetPrimaryGraphType(ref entity1, false));
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.RunActionWithAppliedSearch(instance1, entity1, "CloseAsDuplicate");
    instance1.Actions.PressSave();
    object entity2 = (object) target;
    PXGraph instance2 = PXGraph.CreateInstance(entityHelper.GetPrimaryGraphType(ref entity2, false));
    this.Base.Actions.PressCancel();
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.RunActionWithAppliedSearch((PXGraph) this.Base, entity2, "CheckForDuplicates");
    PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateEntities<TGraph, TMain>.RunActionWithAppliedSearch(instance2, entity2, "Cancel");
    return instance2;
  }

  public abstract TMain GetTargetEntity(int targetID);

  public abstract PX.Objects.CR.Contact GetTargetContact(TMain targetEntity);

  public abstract PX.Objects.CR.Address GetTargetAddress(TMain targetEntity);

  public abstract PXResult<PX.Objects.CR.Contact> GetGramContext(DuplicateDocument duplicateDocument);

  public abstract void DoDuplicateAttach(DuplicateDocument duplicateDocument);

  public virtual void ValidateEntitiesBeforeMerge(List<TMain> duplicateEntities)
  {
  }

  protected abstract bool WhereMergingMet(CRDuplicateResult result);

  protected virtual bool WhereLinkingMet(CRDuplicateResult result) => !this.WhereMergingMet(result);

  protected abstract bool CanBeMerged(CRDuplicateResult result);

  public virtual bool CheckIsActive()
  {
    DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current ?? ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
    return duplicateDocument != null && duplicateDocument.IsActive.GetValueOrDefault();
  }

  public virtual void CheckBlockingOnEntry(DuplicateDocument duplicateDocument)
  {
    if (duplicateDocument == null)
      return;
    if (((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current == null)
      ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current = duplicateDocument;
    if (this.Base.IsImport || this.Base.IsContractBasedAPI)
      ((PXSelectBase) this.Documents).View.Answer = (WebDialogResult) 0;
    if (((PXSelectBase) this.Documents).View.Answer == null && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.DuplicateDocuments).Cache.GetStatus((object) duplicateDocument), (PXEntryStatus) 3, (PXEntryStatus) 4) && this.Processor.IsValidationOnEntryActive(duplicateDocument.ContactType) && this.CheckIsActive() && this.Processor.IsAnyBlockingRulesConfigured(duplicateDocument.ContactType) && !this.Processor.GramSourceUpdated(this.GetGramContext(duplicateDocument)))
    {
      (bool flag, List<CRDuplicateResult> duplicates) = this.CheckIfAnyDuplicates(duplicateDocument);
      if (flag)
      {
        IEnumerable<(bool, string)> source = this.Processor.CheckIsBlocked(this.GetGramContext(duplicateDocument), (IEnumerable<CRDuplicateResult>) duplicates);
        List<(bool, string)> list = source != null ? source.ToList<(bool, string)>() : (List<(bool, string)>) null;
        if (list != null)
        {
          ((PXSelectBase) this.DuplicateDocuments).Cache.SetValue<DuplicateDocument.duplicateFound>((object) duplicateDocument, (object) true);
          ((PXSelectBase) this.DuplicateDocuments).Cache.SetValue<DuplicateDocument.duplicateStatus>((object) duplicateDocument, (object) "PD");
          if (list.Any<(bool, string)>((Func<(bool, string), bool>) (_ => _.IsBlocked && _.BlockType == "B")))
          {
            PXUIFieldAttribute.SetError<DuplicateDocument.duplicateStatus>(((PXSelectBase) this.DuplicateDocuments).Cache, (object) duplicateDocument, this.WarningMessage, duplicateDocument.DuplicateStatus);
            PXGraph.ThrowWithoutRollback((Exception) new PXSetPropertyException("The {0} cannot be saved because at least one duplicate has been found for this record.", new object[1]
            {
              (object) this.GetEntityNameByType(duplicateDocument.ContactType)
            }));
          }
          if (list.Any<(bool, string)>((Func<(bool, string), bool>) (_ => _.IsBlocked)))
          {
            if (this.Base.IsImport || this.Base.IsContractBasedAPI || this.HardBlockOnly)
              ((PXSelectBase) this.Documents).View.Answer = (WebDialogResult) 6;
            ((PXSelectBase<Document>) this.Documents).Ask("Warning", "At least one duplicate has been found. Do you want to save the record?", (MessageButtons) 4, (MessageIcon) 3, true);
          }
        }
      }
    }
    if (((PXSelectBase) this.Documents).View.Answer != 7)
      return;
    PXUIFieldAttribute.SetWarning<DuplicateDocument.duplicateStatus>(((PXSelectBase) this.DuplicateDocuments).Cache, (object) duplicateDocument, this.WarningMessage);
  }

  private string GetEntityNameByType(string contactType)
  {
    switch (contactType)
    {
      case "LD":
        return "Lead";
      case "PN":
        return "Contact";
      case "AP":
        return "Business Account";
      default:
        return "Entity";
    }
  }

  public virtual void OnAfterPersist()
  {
    DuplicateDocument duplicateDocument = ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).Current ?? ((PXSelectBase<DuplicateDocument>) this.DuplicateDocuments).SelectSingle(Array.Empty<object>());
    if (duplicateDocument == null || duplicateDocument.DuplicateStatus == "VA" || !EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.DuplicateDocuments).Cache.GetStatus((object) duplicateDocument), (PXEntryStatus) 3, (PXEntryStatus) 4) || !this.Processor.IsValidationOnEntryActive(duplicateDocument.ContactType) || !this.CheckIsActive() || this.Processor.GramSourceUpdated(this.GetGramContext(duplicateDocument)))
      return;
    ((PXAction) this.CheckForDuplicates).Press();
  }

  public virtual (bool, List<CRDuplicateResult>) CheckIfAnyDuplicates(
    DuplicateDocument duplicateDocument,
    bool withUpdate = false)
  {
    ((PXSelectBase) this.Duplicates).View.Clear();
    List<CRDuplicateResult> list = ((PXSelectBase<CRDuplicateRecord>) this.Duplicates).Select(new object[1]
    {
      (object) true
    }).Cast<CRDuplicateResult>().ToList<CRDuplicateResult>();
    bool flag = list.Count > 0;
    if (withUpdate)
    {
      ((PXSelectBase) this.DuplicateDocuments).Cache.SetValue<DuplicateDocument.duplicateFound>((object) duplicateDocument, (object) flag);
      ((PXSelectBase) this.DuplicateDocuments).Cache.SetValue<DuplicateDocument.duplicateStatus>((object) duplicateDocument, flag ? (object) "PD" : (object) "VA");
    }
    return (flag, list);
  }

  public virtual (bool IsGramsCreated, string NewDuplicateStatus, DateTime? GramValidationDate) PersistGrams(
    PXResult<PX.Objects.CR.Contact> entities)
  {
    try
    {
      return this.Processor.PersistGrams(entities);
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
    }
    return (false, (string) null, new DateTime?());
  }

  internal static object RunActionWithAppliedSearch(
    PXGraph graph,
    object entity,
    string actionName)
  {
    graph.Views[graph.PrimaryView].Cache.Current = entity;
    List<object> objectList = new List<object>();
    List<string> stringList = new List<string>();
    foreach (string key in (IEnumerable<string>) graph.Views[graph.PrimaryView].Cache.Keys)
    {
      objectList.Add(graph.Views[graph.PrimaryView].Cache.GetValue(entity, key));
      stringList.Add(key);
    }
    PXAdapter pxAdapter = new PXAdapter(graph.Views[graph.PrimaryView])
    {
      StartRow = 0,
      MaximumRows = 1,
      Searches = objectList.ToArray(),
      SortColumns = stringList.ToArray()
    };
    if (((OrderedDictionary) graph.Actions).Contains((object) actionName))
    {
      IEnumerator enumerator = graph.Actions[actionName].Press(pxAdapter).GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
          return enumerator.Current;
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    return (object) null;
  }

  public virtual void Highlight(PXGridCellCollection cells, CRDuplicateResult row)
  {
    if (row == null)
      return;
    TMain mainRecord = ((PXSelectBase<Document>) this.Documents).Current.Base as TMain;
    PX.Objects.CR.Contact mainContact = this.GetTargetContact(mainRecord);
    PX.Objects.CR.Address mainAddress = this.GetTargetAddress(mainRecord);
    if ((object) mainRecord == null)
      return;
    PXCache mainRecordCache = this.Base.Caches[mainRecord.GetType()];
    PXCache mainContactCache = this.Base.Caches[((object) mainContact).GetType()];
    PXCache mainAddressCache = this.Base.Caches[((object) mainAddress).GetType()];
    foreach (PXGridCell cell in (CollectionBase) cells)
    {
      if (cell.Value != null && cell.Column.Visible)
      {
        string[] strArray = cell.DataField.Split(new char[1]
        {
          '_'
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 2)
        {
          string str = strArray[0];
          string fieldName = strArray[1];
          object entity = ((PXResult) row)[str];
          if (entity != null && FieldsEqual())
            ((Style) cell.Style).CssClass = "green20";

          bool FieldsEqual()
          {
            PXCache cach = this.Base.Caches[entity.GetType()];
            System.Type type = entity.GetType();
            if ((object) type != null)
            {
              if (typeof (TMain).IsAssignableFrom(type))
                return object.Equals(cach.GetValue(entity, fieldName), mainRecordCache.GetValue((object) mainRecord, fieldName));
              if (typeof (PX.Objects.CR.Contact).IsAssignableFrom(type))
                return object.Equals(cach.GetValue(entity, fieldName), mainContactCache.GetValue((object) mainContact, fieldName));
              if (typeof (PX.Objects.CR.Address).IsAssignableFrom(type))
                return object.Equals(cach.GetValue(entity, fieldName), mainAddressCache.GetValue((object) mainAddress, fieldName));
            }
            return false;
          }
        }
      }
    }
  }

  [PXSuppressActionValidation]
  public virtual void ViewDuplicate(CRDuplicateRecord duplicateRecord)
  {
    if (duplicateRecord == null)
      return;
    this.OpenEntityScreen((IBqlTable) PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<CRDuplicateRecord.duplicateContactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateRecord.DuplicateContactID
    })), (PXRedirectHelper.WindowMode) 1);
  }

  [PXSuppressActionValidation]
  public virtual void ViewDuplicateRefContact(CRDuplicateRecord duplicateRecord)
  {
    if (duplicateRecord == null)
      return;
    this.OpenEntityScreen((IBqlTable) PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<CRDuplicateRecord.duplicateRefContactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateRecord.DuplicateRefContactID
    })), (PXRedirectHelper.WindowMode) 1);
  }

  public virtual void ViewDuplicateBAccount(CRDuplicateRecord duplicateRecord)
  {
    if (duplicateRecord == null)
      return;
    this.OpenEntityScreen((IBqlTable) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<CRDuplicateRecord.duplicateBAccountID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) duplicateRecord.DuplicateBAccountID
    })), (PXRedirectHelper.WindowMode) 1);
  }

  private void OpenEntityScreen(IBqlTable entity, PXRedirectHelper.WindowMode windowMode)
  {
    if (entity == null)
      return;
    PXRedirectHelper.TryRedirect(new PXPrimaryGraphCollection((PXGraph) this.Base)[entity], (object) entity, windowMode);
  }

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type Key = typeof (Document.key);

    public System.Type Extension => typeof (Document);

    public System.Type Table => this._table;

    public DocumentMapping(System.Type table) => this._table = table;
  }

  protected class DuplicateDocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type ContactID = typeof (DuplicateDocument.contactID);
    public System.Type RefContactID = typeof (DuplicateDocument.refContactID);
    public System.Type BAccountID = typeof (DuplicateDocument.bAccountID);
    public System.Type ContactType = typeof (DuplicateDocument.contactType);
    public System.Type DuplicateStatus = typeof (DuplicateDocument.duplicateStatus);
    public System.Type DuplicateFound = typeof (DuplicateDocument.duplicateFound);
    public System.Type Email = typeof (DuplicateDocument.email);
    public System.Type IsActive = typeof (DuplicateDocument.isActive);

    public System.Type Extension => typeof (DuplicateDocument);

    public System.Type Table => this._table;

    public DuplicateDocumentMapping(System.Type table) => this._table = table;
  }
}
