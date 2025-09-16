// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// Represents Subaccount Field
/// Subaccount field usually exists in pair with Account field. Use AccountType argument to specify the respective Account field.
/// </summary>
[PXDBInt]
[PXInt]
[PXUIField]
[PXRestrictor(typeof (Where<Sub.active, Equal<True>>), "Subaccount {0} is inactive.", new Type[] {typeof (Sub.subCD)})]
public class SubAccountAttribute : PXEntityAttribute, IPXRowInsertingSubscriber
{
  public const 
  #nullable disable
  string DimensionName = "SUBACCOUNT";
  private readonly Type _branchID;
  private readonly Type _accounType;

  public SubAccountAttribute()
    : this((Type) null)
  {
  }

  public SubAccountAttribute(Type AccountType)
    : this(AccountType, (Type) null)
  {
  }

  public SubAccountAttribute(Type AccountType, Type BranchType, bool AccountAndBranchRequired = false)
    : this(typeof (Search<Sub.subID, Where<Match<Current<AccessInfo.userName>>>>), AccountType, BranchType, AccountAndBranchRequired)
  {
  }

  public SubAccountAttribute(
    Type SearchType,
    Type AccountType,
    Type BranchType,
    bool AccountAndBranchRequired = false)
  {
    List<Type> typeList = !(SearchType == (Type) null) ? new List<Type>()
    {
      SearchType.GetGenericTypeDefinition()
    } : throw new PXArgumentException(nameof (SearchType), "The argument cannot be null.");
    typeList.AddRange((IEnumerable<Type>) SearchType.GetGenericArguments());
    for (int index = 0; index < typeList.Count; ++index)
    {
      if (typeof (IBqlWhere).IsAssignableFrom(typeList[index]) && AccountType != (Type) null)
      {
        this._accounType = AccountType;
        typeList[index] = BqlCommand.Compose(new Type[4]
        {
          typeof (Where2<,>),
          SubAccountAttribute.GetIsNullAndMatchWhere(AccountType, AccountAndBranchRequired),
          typeof (And<>),
          typeList[index]
        });
        if (BranchType != (Type) null)
        {
          this._branchID = BranchType;
          typeList[index] = BqlCommand.Compose(new Type[4]
          {
            typeof (Where2<,>),
            SubAccountAttribute.GetIsNullAndMatchWhere(BranchType, AccountAndBranchRequired),
            typeof (And<>),
            typeList[index]
          });
        }
        SearchType = BqlCommand.Compose(typeList.ToArray());
      }
    }
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("SUBACCOUNT", SearchType, typeof (Sub.subCD))
    {
      CacheGlobal = true,
      DescriptionField = typeof (Sub.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
  }

  private static Type GetIsNullAndMatchWhere(Type entityType, bool IsRequired)
  {
    Type nullAndMatchWhere = BqlCommand.Compose(new Type[4]
    {
      typeof (Where<>),
      typeof (Match<>),
      typeof (Optional<>),
      entityType
    });
    if (!IsRequired)
      nullAndMatchWhere = BqlCommand.Compose(new Type[6]
      {
        typeof (Where<,,>),
        typeof (Optional<>),
        entityType,
        typeof (IsNull),
        typeof (Or<>),
        nullAndMatchWhere
      });
    return nullAndMatchWhere;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
    {
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1]).ValidComboRequired = false;
      PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
      Type itemType = sender.GetItemType();
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      SubAccountAttribute accountAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) accountAttribute, __vmethodptr(accountAttribute, FieldDefaulting));
      fieldDefaulting.AddHandler(itemType, fieldName, pxFieldDefaulting);
    }
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (this._branchID != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._branchID);
      string name = this._branchID.Name;
      SubAccountAttribute accountAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) accountAttribute, __vmethodptr(accountAttribute, RelatedFieldUpdated));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    if (this._accounType != (Type) null)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type itemType = BqlCommand.GetItemType(this._accounType);
      string name = this._accounType.Name;
      SubAccountAttribute accountAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) accountAttribute, __vmethodptr(accountAttribute, RelatedFieldUpdated));
      fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    }
    PXGraph.RowPersistingEvents rowPersisting = sender.Graph.RowPersisting;
    Type itemType1 = sender.GetItemType();
    SubAccountAttribute accountAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) accountAttribute1, __vmethodptr(accountAttribute1, RowPersisting));
    rowPersisting.AddHandler(itemType1, pxRowPersisting);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    int? defaultSubId = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
    if (((e.Operation & 3) == 2 || (e.Operation & 3) == 1) && !defaultSubId.HasValue && !PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
    {
      defaultSubId = this.GetDefaultSubID(sender, e.Row);
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) defaultSubId);
      PXUIFieldAttribute.SetError(sender, e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (string) null);
    }
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    int? nullable = defaultSubId;
    int num = 0;
    if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
      return;
    PXCache cach = sender.Graph.Caches[typeof (Sub)];
    PXSelectBase<Sub> pxSelectBase = (PXSelectBase<Sub>) new PXSelectReadonly<Sub, Where<Sub.subCD, Equal<Current<Sub.subCD>>>>(sender.Graph);
    Sub sub1 = (Sub) null;
    foreach (Sub sub2 in cach.Inserted)
    {
      if (object.Equals((object) sub2.SubID, (object) defaultSubId))
      {
        PXView view = ((PXSelectBase) pxSelectBase).View;
        object[] objArray1 = new object[1]{ (object) sub2 };
        object[] objArray2 = Array.Empty<object>();
        if ((sub1 = (Sub) view.SelectSingleBound(objArray1, objArray2)) != null)
        {
          cach.RaiseRowPersisting((object) sub2, (PXDBOperation) 2);
          cach.RaiseRowPersisted((object) sub1, (PXDBOperation) 2, (PXTranStatus) 0, (Exception) null);
          sub1 = sub2;
          break;
        }
      }
    }
    if (sub1 == null)
      return;
    cach.SetStatus((object) sub1, (PXEntryStatus) 0);
    cach.Remove((object) sub1);
  }

  protected virtual void RelatedFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.SetSubAccount(sender, e.Row);
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.SetSubAccount(sender, e.Row);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    e.NewValue = (object) this.GetDefaultSubID(sender, e.Row);
    ((CancelEventArgs) e).Cancel = true;
  }

  private void SetSubAccount(PXCache sender, object row)
  {
    PXFieldState valueExt1 = (PXFieldState) sender.GetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (valueExt1 == null || valueExt1.Value == null)
      return;
    sender.SetValue(row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
    sender.SetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName, valueExt1.Value);
    if (!FieldErrorScope.NeedsReset(sender.GetBqlField(((PXEventSubscriberAttribute) this)._FieldName).FullName))
      return;
    PXFieldState valueExt2 = (PXFieldState) sender.GetValueExt(row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (sender.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName) != null || valueExt2 == null || valueExt2.ErrorLevel < 4)
      return;
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, row, (object) null, (Exception) null);
  }

  private int? GetDefaultSubID(PXCache sender, object row)
  {
    if (SubAccountAttribute.Definitions.DefaultSubID.HasValue)
      return SubAccountAttribute.Definitions.DefaultSubID;
    object defaultSubId = (object) "0";
    sender.RaiseFieldUpdating(((PXEventSubscriberAttribute) this)._FieldName, row, ref defaultSubId);
    return (int?) defaultSubId;
  }

  protected static SubAccountAttribute.Definition Definitions
  {
    get
    {
      SubAccountAttribute.Definition definitions = PXContext.GetSlot<SubAccountAttribute.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<SubAccountAttribute.Definition>(PXDatabase.GetSlot<SubAccountAttribute.Definition>(typeof (SubAccountAttribute.Definition).FullName, new Type[1]
        {
          typeof (Sub)
        }));
      return definitions;
    }
  }

  public static Sub GetSubaccount(PXGraph graph, int? subID)
  {
    return (subID.HasValue ? PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXSelect<Sub, Where<Sub.subID, Equal<Required<Sub.subID>>>>.Config>.Select(graph, new object[1]
    {
      (object) subID
    })) : throw new ArgumentNullException(nameof (subID))) ?? throw new PXException("{0} with ID '{1}' does not exists", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName(typeof (Sub)),
      (object) subID
    });
  }

  /// <summary>
  /// Returns deafult subID if default subaccount exists, else returns null.
  /// </summary>
  public static int? TryGetDefaultSubID() => SubAccountAttribute.Definitions.DefaultSubID;

  public class dimensionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SubAccountAttribute.dimensionName>
  {
    public dimensionName()
      : base("SUBACCOUNT")
    {
    }
  }

  protected class Definition : IPrefetchable, IPXCompanyDependent
  {
    private int? _DefaultSubID;

    public int? DefaultSubID => this._DefaultSubID;

    public void Prefetch()
    {
      this._DefaultSubID = new int?();
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Sub>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<Sub.subID>(),
        (PXDataField) new PXDataFieldOrder<Sub.subID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this._DefaultSubID = pxDataRecord.GetInt32(0);
      }
    }
  }
}
