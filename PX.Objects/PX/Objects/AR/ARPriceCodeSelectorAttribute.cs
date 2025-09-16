// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceCodeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.Repositories;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARPriceCodeSelectorAttribute : PXCustomSelectorAttribute
{
  private 
  #nullable disable
  System.Type _priceTypeField;

  public virtual bool SuppressReadDeletedSupport { get; set; }

  public ARPriceCodeSelectorAttribute(System.Type priceTypeField)
    : base(typeof (ARPriceCodeSelectorAttribute.ARPriceCode.priceCode), new System.Type[2]
    {
      typeof (ARPriceCodeSelectorAttribute.ARPriceCode.priceCode),
      typeof (ARPriceCodeSelectorAttribute.ARPriceCode.description)
    })
  {
    this._priceTypeField = priceTypeField;
    ((PXSelectorAttribute) this).SuppressUnconditionalSelect = true;
    ((PXSelectorAttribute) this).DescriptionField = typeof (ARPriceCodeSelectorAttribute.ARPriceCode.description);
  }

  protected virtual PXView CustomerCodeView(PXGraph graph)
  {
    return graph.TypedViews.GetView((BqlCommand) new Select<PX.Objects.CR.BAccount, Where2<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>, And2<Where<PX.Objects.CR.BAccount.cOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>, Or<Not<FeatureInstalled<FeaturesSet.visibilityRestriction>>>>, And<Match<Current<AccessInfo.userName>>>>>>(), true);
  }

  protected virtual PXView PriceClassCodeView(PXGraph graph)
  {
    return graph.TypedViews.GetView((BqlCommand) new Select<ARPriceClass>(), true);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string str = sender.GetValue(e.Row, this._priceTypeField.Name) as string;
    if (string.IsNullOrEmpty(str))
      return;
    switch (str)
    {
      case "B":
        break;
      case "C":
        new CustomerRepository(sender.Graph).GetByCD(e.NewValue.ToString());
        break;
      case "P":
        if (ARPriceClass.PK.Find(sender.Graph, e.NewValue.ToString()) != null)
          break;
        throw new PXSetPropertyException(PXMessages.LocalizeFormat("{0} '{1}' cannot be found in the system.", new object[2]
        {
          (object) "Customer Price Class",
          (object) e.NewValue.ToString()
        }));
      default:
        base.FieldVerifying(sender, e);
        break;
    }
  }

  protected virtual void ModifyFilters(
    PXFilterRow[] filters,
    string originalFieldName,
    string newFieldName)
  {
    if (filters == null)
      return;
    foreach (PXFilterRow filter in filters)
    {
      if (string.Compare(filter.DataField, originalFieldName, true) == 0)
        filter.DataField = newFieldName;
    }
  }

  protected virtual void ModifyColumns(
    string[] filters,
    string originalFieldName,
    string newFieldName)
  {
    if (filters == null || filters.Length == 0)
      return;
    int index = ((IEnumerable<string>) filters).FindIndex<string>((Predicate<string>) (x => x != null && x.Equals(originalFieldName, StringComparison.InvariantCultureIgnoreCase)));
    if (index < 0)
      return;
    filters[index] = newFieldName;
  }

  protected virtual IEnumerable GetRecords()
  {
    PXGraph currentGraph = PXView.CurrentGraph;
    PXCache cach = currentGraph?.Caches[((PXEventSubscriberAttribute) this)._BqlTable];
    object obj = ((IEnumerable<object>) PXView.Currents).FirstOrDefault<object>((Func<object, bool>) (c => ((PXEventSubscriberAttribute) this)._BqlTable.IsAssignableFrom(c.GetType()))) ?? cach?.Current;
    if (obj == null)
      return (IEnumerable) Array<ARPriceCodeSelectorAttribute.ARPriceCode>.Empty;
    string str = cach.GetValue(obj, this._priceTypeField.Name) as string;
    if (string.IsNullOrEmpty(str) || str == "B")
      return (IEnumerable) Array<ARPriceCodeSelectorAttribute.ARPriceCode>.Empty;
    string[] sortColumns = PXView.SortColumns;
    PXFilterRow[] filters = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    int startRow = PXView.StartRow;
    int num = 0;
    ARPriceCodeSelectorAttribute.ARPriceCode[] array;
    if (str == "C")
    {
      ModifyFields(sortColumns, filters, typeof (PX.Objects.CR.BAccount.acctCD).Name, typeof (PX.Objects.CR.BAccount.acctName).Name);
      array = GraphHelper.RowCast<PX.Objects.CR.BAccount>((IEnumerable) this.CustomerCodeView(currentGraph).Select(PXView.Currents, PXView.Parameters, PXView.Searches, sortColumns, PXView.Descendings, filters, ref startRow, PXView.MaximumRows, ref num)).Select<PX.Objects.CR.BAccount, ARPriceCodeSelectorAttribute.ARPriceCode>((Func<PX.Objects.CR.BAccount, ARPriceCodeSelectorAttribute.ARPriceCode>) (x => new ARPriceCodeSelectorAttribute.ARPriceCode()
      {
        PriceCode = x.AcctCD.Trim(),
        Description = x.AcctName
      })).ToArray<ARPriceCodeSelectorAttribute.ARPriceCode>();
    }
    else
    {
      ModifyFields(sortColumns, filters, typeof (ARPriceClass.priceClassID).Name, typeof (ARPriceClass.description).Name);
      array = GraphHelper.RowCast<ARPriceClass>((IEnumerable) this.PriceClassCodeView(currentGraph).Select(PXView.Currents, PXView.Parameters, PXView.Searches, sortColumns, PXView.Descendings, filters, ref startRow, PXView.MaximumRows, ref num)).Select<ARPriceClass, ARPriceCodeSelectorAttribute.ARPriceCode>((Func<ARPriceClass, ARPriceCodeSelectorAttribute.ARPriceCode>) (x => new ARPriceCodeSelectorAttribute.ARPriceCode()
      {
        PriceCode = x.PriceClassID.Trim(),
        Description = x.Description
      })).ToArray<ARPriceCodeSelectorAttribute.ARPriceCode>();
    }
    PXView.StartRow = 0;
    PXDelegateResult records = new PXDelegateResult();
    records.IsResultSorted = true;
    records.IsResultFiltered = true;
    ((List<object>) records).AddRange((IEnumerable<object>) array);
    return (IEnumerable) records;

    void ModifyFields(string[] columns, PXFilterRow[] filters, string priceCode, string desc)
    {
      this.ModifyColumns(columns, typeof (ARPriceCodeSelectorAttribute.ARPriceCode.priceCode).Name, priceCode);
      this.ModifyColumns(columns, typeof (ARPriceCodeSelectorAttribute.ARPriceCode.description).Name, desc);
      this.ModifyFilters(filters, typeof (ARPriceCodeSelectorAttribute.ARPriceCode.priceCode).Name, priceCode);
      this.ModifyFilters(filters, typeof (ARPriceCodeSelectorAttribute.ARPriceCode.description).Name, desc);
    }
  }

  protected virtual bool IsReadDeletedSupported
  {
    get => !this.SuppressReadDeletedSupport && ((PXSelectorAttribute) this).IsReadDeletedSupported;
  }

  [PXHidden]
  public class ARPriceCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(30, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC", IsKey = true)]
    [PXUIField]
    public virtual string PriceCode { get; set; }

    [PXString(256 /*0x0100*/, IsUnicode = true, IsKey = true)]
    [PXUIField]
    public virtual string Description { get; set; }

    public abstract class priceCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPriceCodeSelectorAttribute.ARPriceCode.priceCode>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARPriceCodeSelectorAttribute.ARPriceCode.description>
    {
    }
  }
}
