// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.PeriodKeyProviderBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions.Periods;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public abstract class PeriodKeyProviderBase
{
  public class SourcesSpecificationCollection : 
    PeriodKeyProviderBase.SourcesSpecificationCollection<PeriodKeyProviderBase.SourceSpecificationItem>
  {
  }

  public class SourceSpecificationItem
  {
    public virtual Type BranchSourceType { get; set; }

    public virtual Type BranchSourceFormulaType { get; set; }

    public virtual IBqlCreator BranchSourceFormula { get; protected set; }

    public virtual Type OrganizationSourceType { get; set; }

    public virtual bool IsMain { get; set; }

    public virtual bool IsAnySourceSpecified
    {
      get
      {
        return this.BranchSourceType != (Type) null || this.BranchSourceFormula != null || this.OrganizationSourceType != (Type) null;
      }
    }

    protected List<Type> SourceFields { get; set; }

    public virtual PeriodKeyProviderBase.SourceSpecificationItem Initialize()
    {
      if (this.BranchSourceFormulaType != (Type) null && this.BranchSourceFormula == null)
        this.BranchSourceFormula = PXFormulaAttribute.InitFormula(this.BranchSourceFormulaType);
      return this;
    }

    public virtual List<Type> GetSourceFields(PXCache cache)
    {
      if (this.SourceFields == null)
        this.SourceFields = this.BuildSourceFields(cache);
      return this.SourceFields;
    }

    protected virtual List<Type> BuildSourceFields(PXCache cache)
    {
      SQLExpression sqlExpression = SQLExpression.None();
      List<Type> typeList = new List<Type>();
      if (this.BranchSourceFormula != null)
      {
        IBqlCreator branchSourceFormula = this.BranchSourceFormula;
        ref SQLExpression local = ref sqlExpression;
        PXGraph graph = cache.Graph;
        BqlCommandInfo bqlCommandInfo = new BqlCommandInfo(false);
        bqlCommandInfo.Fields = typeList;
        bqlCommandInfo.BuildExpression = false;
        BqlCommand.Selection selection = new BqlCommand.Selection();
        branchSourceFormula.AppendExpression(ref local, graph, bqlCommandInfo, selection);
      }
      if (this.BranchSourceType != (Type) null)
        typeList.Add(this.BranchSourceType);
      if (this.OrganizationSourceType != (Type) null)
        typeList.Add(this.OrganizationSourceType);
      return typeList;
    }
  }

  public class KeyWithSourceValues<TSourceSpecificationItem, TKey>
    where TSourceSpecificationItem : PeriodKeyProviderBase.SourceSpecificationItem
    where TKey : OrganizationDependedPeriodKey, new()
  {
    public TSourceSpecificationItem SpecificationItem { get; set; }

    public virtual List<int?> KeyOrganizationIDs { get; set; }

    public TKey Key { get; set; }

    public virtual List<int?> SourceOrganizationIDs { get; set; }

    public virtual List<int?> SourceBranchIDs { get; set; }

    public KeyWithSourceValues()
    {
      this.Key = new TKey();
      this.KeyOrganizationIDs = new List<int?>();
    }

    public virtual bool SourcesEqual(object otherObject)
    {
      PeriodKeyProviderBase.KeyWithSourceValues<TSourceSpecificationItem, TKey> withSourceValues = (PeriodKeyProviderBase.KeyWithSourceValues<TSourceSpecificationItem, TKey>) otherObject;
      return this.SourceBranchIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v)).SequenceEqual<int?>((IEnumerable<int?>) withSourceValues.SourceBranchIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v))) && this.SourceOrganizationIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v)).SequenceEqual<int?>((IEnumerable<int?>) withSourceValues.SourceOrganizationIDs.OrderBy<int?, int?>((Func<int?, int?>) (v => v)));
    }
  }

  public class KeyWithSourceValuesCollection<TKeyWithSourceValues, TSourceSpecificationItem, TKey>
    where TKeyWithSourceValues : PeriodKeyProviderBase.KeyWithSourceValues<TSourceSpecificationItem, TKey>
    where TSourceSpecificationItem : PeriodKeyProviderBase.SourceSpecificationItem
    where TKey : OrganizationDependedPeriodKey, new()
  {
    public List<TKeyWithSourceValues> Items { get; set; }

    public TKeyWithSourceValues MainItem
    {
      get
      {
        return this.Items.SingleOrDefault<TKeyWithSourceValues>((Func<TKeyWithSourceValues, bool>) (item => item.SpecificationItem.IsMain));
      }
    }

    public List<int?> ConsolidatedOrganizationIDs { get; set; }

    public TKey ConsolidatedKey { get; set; }

    public KeyWithSourceValuesCollection() => this.Items = new List<TKeyWithSourceValues>();
  }

  public class SourcesSpecificationCollectionBase
  {
    public virtual IEnumerable<PeriodKeyProviderBase.SourceSpecificationItem> SpecificationItemBases { get; }

    public List<Type> DependsOnFields { get; set; }

    protected List<Type> SourceFields { get; set; }

    public virtual List<Type> GetSourceFields(PXCache cache)
    {
      if (this.SourceFields == null)
        this.SourceFields = this.BuildSourceFields(cache);
      return this.SourceFields;
    }

    protected virtual List<Type> BuildSourceFields(PXCache cache)
    {
      List<Type> list = this.SpecificationItemBases.SelectMany<PeriodKeyProviderBase.SourceSpecificationItem, Type>((Func<PeriodKeyProviderBase.SourceSpecificationItem, IEnumerable<Type>>) (item => (IEnumerable<Type>) item.GetSourceFields(cache))).ToList<Type>();
      list.AddRange((IEnumerable<Type>) this.DependsOnFields);
      return list;
    }
  }

  public class SourcesSpecificationCollection<TSourceSpecificationItem> : 
    PeriodKeyProviderBase.SourcesSpecificationCollectionBase
    where TSourceSpecificationItem : PeriodKeyProviderBase.SourceSpecificationItem
  {
    public override IEnumerable<PeriodKeyProviderBase.SourceSpecificationItem> SpecificationItemBases
    {
      get => (IEnumerable<PeriodKeyProviderBase.SourceSpecificationItem>) this.SpecificationItems;
    }

    public List<TSourceSpecificationItem> SpecificationItems { get; set; }

    public TSourceSpecificationItem MainSpecificationItem { get; set; }

    public SourcesSpecificationCollection()
    {
      this.SpecificationItems = new List<TSourceSpecificationItem>();
      this.DependsOnFields = new List<Type>();
    }
  }

  public class SourceSpecification<TBranchSource, TIsMain> : 
    PeriodKeyProviderBase.SourceSpecification<TBranchSource, BqlNone, BqlHelper.fieldStub, TIsMain>
    where TBranchSource : IBqlField
    where TIsMain : BoolConstant<TIsMain>, new()
  {
  }

  public class SourceSpecification<TBranchSource, TBranchFormula, TIsMain> : 
    PeriodKeyProviderBase.SourceSpecification<TBranchSource, TBranchFormula, BqlHelper.fieldStub, TIsMain>
    where TBranchSource : IBqlField
    where TBranchFormula : IBqlCreator
    where TIsMain : BoolConstant<TIsMain>, new()
  {
  }

  public class SourceSpecification<TBranchSource, TBranchFormula, TOrganizationSource, TIsMain> : 
    PeriodKeyProviderBase.SourceSpecificationItem
    where TBranchSource : IBqlField
    where TBranchFormula : IBqlCreator
    where TOrganizationSource : IBqlField
    where TIsMain : BoolConstant<TIsMain>, new()
  {
    public override Type BranchSourceType => BqlHelper.GetTypeNotStub<TBranchSource>();

    public override Type BranchSourceFormulaType => BqlHelper.GetTypeNotStub<TBranchFormula>();

    public override Type OrganizationSourceType => BqlHelper.GetTypeNotStub<TOrganizationSource>();

    public override PeriodKeyProviderBase.SourceSpecificationItem Initialize()
    {
      base.Initialize();
      this.IsMain = ((BqlConstant<TIsMain, IBqlBool, bool>) (object) new TIsMain()).Value;
      return (PeriodKeyProviderBase.SourceSpecificationItem) this;
    }
  }
}
