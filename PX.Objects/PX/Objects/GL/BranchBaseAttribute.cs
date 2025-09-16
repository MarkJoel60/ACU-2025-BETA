// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BranchBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL;

[PXDBInt]
[PXInt]
[PXUIField(DisplayName = "Branch", FieldClass = "COMPANYBRANCH")]
public abstract class BranchBaseAttribute : PXEntityAttribute, IPXFieldSelectingSubscriber
{
  public const string _FieldClass = "COMPANYBRANCH";
  public const string _DimensionName = "BRANCH";
  private bool _IsDetail = true;
  private bool _Suppress;

  [InjectDependencyOnTypeLevel]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public bool IsDetail
  {
    get => this._IsDetail;
    set => this._IsDetail = value;
  }

  public bool IsEnabledWhenOneBranchIsAccessible { get; set; }

  protected BranchBaseAttribute(
    Type sourceType,
    Type searchType = null,
    bool addDefaultAttribute = true,
    bool onlyActive = false,
    bool useDefaulting = true)
  {
    if (sourceType == (Type) null & useDefaulting)
      sourceType = this.GetDefaultSourceType();
    if (searchType == (Type) null)
      searchType = typeof (Search2<Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where<MatchWithBranch<Branch.branchID>>>);
    if (addDefaultAttribute)
      ((PXAggregateAttribute) this)._Attributes.Add(sourceType != (Type) null ? (PXEventSubscriberAttribute) new PXDefaultAttribute(sourceType) : (PXEventSubscriberAttribute) new PXDefaultAttribute());
    if (onlyActive)
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<Branch.active, Equal<True>>), "Branch is inactive.", Array.Empty<Type>()));
    if (sourceType == (Type) null || !typeof (IBqlField).IsAssignableFrom(sourceType) || sourceType == typeof (AccessInfo.branchID))
      this.IsDetail = false;
    if (this.IsDetail)
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new InterBranchRestrictorAttribute(BqlCommand.Compose(new Type[5]
      {
        typeof (Where<>),
        typeof (SameOrganizationBranch<,>),
        typeof (Branch.branchID),
        typeof (Current<>),
        sourceType
      })));
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && this.IsDetail)
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(BqlCommand.Compose(new Type[5]
      {
        typeof (Where<,>),
        typeof (Branch.baseCuryID),
        typeof (EqualBaseCuryID<>),
        typeof (Current<>),
        sourceType
      }), "The base currency of the {0} branch differs from the base currency of the originating branch.", new Type[1]
      {
        typeof (Branch.branchCD)
      }));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("BRANCH", searchType, typeof (Branch.branchCD), new Type[4]
    {
      typeof (Branch.branchCD),
      typeof (Branch.acctName),
      typeof (Branch.ledgerID),
      typeof (PX.Objects.GL.DAC.Organization.organizationName)
    })
    {
      ValidComboRequired = true,
      DescriptionField = typeof (Branch.acctName),
      CacheGlobal = true
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Initialize();
  }

  protected virtual Type GetDefaultSourceType() => typeof (AccessInfo.branchID);

  public bool Suppress()
  {
    if (PXGraph.ProxyIsActive || PXGraph.GeneratorIsActive)
      return false;
    IEnumerable<BranchInfo> activeBranches = this._currentUserInformationProvider?.GetActiveBranches();
    return (activeBranches == null || activeBranches.Count<BranchInfo>() <= 1) && !this.IsEnabledWhenOneBranchIsAccessible;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    this._Suppress = this.Suppress();
    this.AddBranchIdInconsistencyValidation(sender);
  }

  protected virtual void AddBranchIdInconsistencyValidation(PXCache sender)
  {
    sender.Graph.OnBeforeCommit += (Action<PXGraph>) (graph =>
    {
      foreach (object row in sender.Inserted)
        this.ValidateBranchID(sender, row);
      foreach (object row in sender.Updated)
        this.ValidateBranchID(sender, row);
    });
  }

  protected virtual void ValidateBranchID(PXCache sender, object row)
  {
    int? nullable = sender.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldName) as int?;
    if (!nullable.HasValue || PXAccess.GetBranch(nullable) != null)
      return;
    Type itemType = sender.GetItemType();
    string str1 = PXMessages.LocalizeFormatNoPrefix("Branch ID Validation: {0}", new object[1]
    {
      (object) itemType.FullName
    });
    string str2 = PXMessages.LocalizeFormatNoPrefix("The {0} branch ID of the {1} branch does not exist in the current tenant.", new object[2]
    {
      (object) nullable,
      (object) itemType.FullName
    });
    sender.Graph.SetDataConsistencyIssue(str1, str2, false);
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!(typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber)))
      return;
    subscribers.Remove(this as ISubscriber);
    subscribers.Add(this as ISubscriber);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!this._Suppress || !(e.ReturnState is PXFieldState))
      return;
    PXFieldState returnState = (PXFieldState) e.ReturnState;
    returnState.Enabled = false;
    if (!this._IsDetail)
      return;
    returnState.Visible = false;
    returnState.Visibility = (PXUIVisibility) 1;
  }

  public static void VerifyFieldInPXCache<Table, Field>(PXGraph graph, PXResultset<Table> resultset)
    where Table : class, IBqlTable, new()
    where Field : IBqlField
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      return;
    foreach (PXResult<Table> pxResult in resultset)
    {
      Table able = PXResult<Table>.op_Implicit(pxResult);
      object obj = graph.Caches[typeof (Table)].GetValue<Field>((object) able);
      try
      {
        graph.Caches[typeof (Table)].RaiseFieldVerifying<Field>((object) able, ref obj);
      }
      catch (PXSetPropertyException ex)
      {
        graph.Caches[typeof (Table)].RaiseExceptionHandling<Field>((object) able, obj, (Exception) ex);
        GraphHelper.MarkUpdated(graph.Caches[typeof (Table)], (object) able);
      }
    }
  }
}
