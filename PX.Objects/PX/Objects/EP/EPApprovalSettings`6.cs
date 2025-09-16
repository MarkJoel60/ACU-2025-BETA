// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalSettings`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

#nullable enable
namespace PX.Objects.EP;

public class EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus> : 
  IPrefetchable,
  IPXCompanyDependent
  where TSetupApproval : 
  #nullable disable
  IBqlTable, IAssignedMap
  where TSetupDocTypeField : IBqlField, IImplement<IBqlString>
  where THoldStatus : IBqlOperand, IConstant<string>, IImplement<IBqlString>
  where TPendingApprovalStatus : IBqlOperand, IConstant<string>, IImplement<IBqlString>
  where TRejectedStatus : IBqlOperand, IConstant<string>, IImplement<IBqlString>
{
  private static readonly string SlotKey = typeof (EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>).ToCodeString(true);
  private static readonly ImmutableDictionary<string, Type> DocTypeConstants = ImmutableDictionary.ToImmutableDictionary<(Type, string), string, Type>(EnumerableExtensions.Distinct<(Type, string), string>(((IEnumerable<Type>) typeof (TAllDocTypes).GetNestedTypes()).Where<Type>((Func<Type, bool>) (t => typeof (IConstant).IsAssignableFrom(t))).Select<Type, (Type, string)>((Func<Type, (Type, string)>) (t => (t, ((IConstant) Activator.CreateInstance(t)).Value.ToString()))), (Func<(Type, string), string>) (p => p.Value)), (Func<(Type, string), string>) (p => p.Value), (Func<(Type, string), Type>) (p => p.Type));
  private ImmutableHashSet<string> _approvableDocTypes = ImmutableHashSet<string>.Empty;
  private readonly ConcurrentDictionary<Type, IBqlUnary> _docTypeToCondition = new ConcurrentDictionary<Type, IBqlUnary>();

  public static ImmutableHashSet<string> ApprovableDocTypes
  {
    get
    {
      return EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.Slot._approvableDocTypes;
    }
  }

  private static EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus> Slot
  {
    get
    {
      return PXDatabase.GetSlot<EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>>(EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.SlotKey, new Type[1]
      {
        typeof (TSetupApproval)
      });
    }
  }

  void IPrefetchable.Prefetch()
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
    {
      HashSet<string> stringSet = new HashSet<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<TSetupApproval>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<TSetupDocTypeField>(),
        (PXDataField) new PXDataFieldValue("IsActive", (object) true)
      }))
        stringSet.Add(pxDataRecord.GetString(0).Trim());
      this._approvableDocTypes = ImmutableHashSet.ToImmutableHashSet<string>((IEnumerable<string>) stringSet);
    }
    else
      this._approvableDocTypes = ImmutableHashSet<string>.Empty;
    this._docTypeToCondition.Clear();
  }

  private IBqlUnary IsApprovableCondition(Type docTypeField)
  {
    Type type1 = (Type) null;
    foreach (string approvableDocType in this._approvableDocTypes)
    {
      Type type2;
      if (EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.DocTypeConstants.TryGetValue(approvableDocType, ref type2))
      {
        Type type3;
        if (!(type1 == (Type) null))
          type3 = BqlCommand.Compose(new Type[6]
          {
            typeof (Where<,,>),
            docTypeField,
            typeof (Equal<>),
            type2,
            typeof (Or<>),
            type1
          });
        else
          type3 = BqlCommand.Compose(new Type[4]
          {
            typeof (Where<,>),
            docTypeField,
            typeof (Equal<>),
            type2
          });
        type1 = type3;
      }
    }
    if (type1 == (Type) null)
      return (IBqlUnary) new Where<True, Equal<False>>();
    return (IBqlUnary) Activator.CreateInstance(BqlCommand.Compose(new Type[2]
    {
      typeof (Where<>),
      type1
    }));
  }

  public class IsApprovable<TDocTypeField> : CustomPredicate where TDocTypeField : IBqlField, IImplement<IBqlString>
  {
    public IsApprovable()
      : base(EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.Slot._docTypeToCondition.GetOrAdd(typeof (TDocTypeField), (Func<Type, IBqlUnary>) (key => EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.Slot.IsApprovableCondition(key))))
    {
    }
  }

  public class IsDocumentApprovable<TDocTypeField, TStatusField> : CustomPredicate
    where TDocTypeField : IBqlField, IImplement<IBqlString>
    where TStatusField : IBqlField, IImplement<IBqlString>
  {
    public IsDocumentApprovable()
      : base((IBqlUnary) new Where<TStatusField, In3<TPendingApprovalStatus, TRejectedStatus>, Or<EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.IsApprovable<TDocTypeField>>>())
    {
    }
  }

  public class IsDocumentLockedByApproval<TDocTypeField, TStatusField> : CustomPredicate
    where TDocTypeField : IBqlField, IImplement<IBqlString>
    where TStatusField : IBqlField, IImplement<IBqlString>
  {
    public IsDocumentLockedByApproval()
      : base((IBqlUnary) new Where<TStatusField, NotEqual<THoldStatus>, And<EPApprovalSettings<TSetupApproval, TSetupDocTypeField, TAllDocTypes, THoldStatus, TPendingApprovalStatus, TRejectedStatus>.IsApprovable<TDocTypeField>>>())
    {
    }
  }
}
