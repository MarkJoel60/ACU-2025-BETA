// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APApproveBills
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APApproveBills : PXGraph<
#nullable disable
APApproveBills>
{
  public PXFilter<ApproveBillsFilter> Filter;
  public PXSave<ApproveBillsFilter> Save;
  public PXCancel<ApproveBillsFilter> Cancel;
  public PXAction<ApproveBillsFilter> ViewDocument;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<APInvoice> APDocumentList;
  public PXSetup<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>> vendorclass;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (this.APDocumentList.Current != null)
      PXRedirectHelper.TryRedirect(this.APDocumentList.Cache, (object) this.APDocumentList.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  public override int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    return viewName == "Filter" && this.APDocumentList.Cache.IsDirty && this.Filter.View.Ask("Some documents are selected in the table. Once you change any criteria, all the documents will be unselected. Do you want to continue?", MessageButtons.YesNo) == WebDialogResult.No ? 0 : base.ExecuteUpdate(viewName, keys, values, parameters);
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<PX.Objects.CS.FeaturesSet.branch>>.Or<FeatureInstalled<PX.Objects.CS.FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APInvoice.branchID> e)
  {
  }

  public virtual void _(PX.Data.Events.RowUpdated<ApproveBillsFilter> e)
  {
    this.APDocumentList.Cache.Clear();
    this.APDocumentList.Cache.ClearQueryCacheObsolete();
    e.Row.PendingRefresh = new bool?(true);
  }

  protected virtual void _(PX.Data.Events.RowSelected<ApproveBillsFilter> e)
  {
    ApproveBillsFilter row = e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<ApproveBillsFilter.payInLessThan>(e.Cache, (object) row, row != null && row.ShowPayInLessThan.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<ApproveBillsFilter.dueInLessThan>(e.Cache, (object) row, row != null && row.ShowDueInLessThan.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<ApproveBillsFilter.discountExpiresInLessThan>(e.Cache, (object) row, row != null && row.ShowDiscountExpiresInLessThan.GetValueOrDefault());
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ApproveBillsFilter.showPayInLessThan> e)
  {
    e?.Cache?.SetDefaultExt<ApproveBillsFilter.payInLessThan>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ApproveBillsFilter.showDueInLessThan> e)
  {
    e?.Cache?.SetDefaultExt<ApproveBillsFilter.dueInLessThan>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ApproveBillsFilter.showDiscountExpiresInLessThan> e)
  {
    e?.Cache?.SetDefaultExt<ApproveBillsFilter.discountExpiresInLessThan>(e.Row);
  }

  protected virtual void ApproveBillsFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled(this.APDocumentList.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<APInvoice.paySel>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.payLocationID>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.payAccountID>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.payTypeID>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.payDate>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<APInvoice.separateCheck>(this.APDocumentList.Cache, (object) null, true);
    PXUIFieldAttribute.SetVisible<ApproveBillsFilter.curyID>(sender, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetDisplayName<APInvoice.selected>(this.APDocumentList.Cache, "Approve");
    PXUIFieldAttribute.SetDisplayName<APInvoice.vendorID>(this.APDocumentList.Cache, "Vendor ID");
    this.APDocumentList.Cache.AllowInsert = this.APDocumentList.Cache.AllowDelete = false;
    if (!(e.Row is ApproveBillsFilter row))
      return;
    row.Days = PXMessages.LocalizeNoPrefix(row.Days);
  }

  protected virtual void APInvoice_PayLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<APInvoice.separateCheck>(e.Row);
    sender.SetDefaultExt<APInvoice.payAccountID>(e.Row);
    sender.SetDefaultExt<APInvoice.payTypeID>(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<APInvoice> e)
  {
    if (e.Cache.ObjectsEqual<APInvoice.paySel>((object) e.Row, (object) e.OldRow))
      return;
    Decimal? nullable;
    Decimal valueOrDefault;
    if (!this.Accessinfo.CuryViewState)
    {
      valueOrDefault = e.Row.CuryDocBal.GetValueOrDefault();
    }
    else
    {
      nullable = e.Row.DocBal;
      valueOrDefault = nullable.GetValueOrDefault();
    }
    Decimal num1 = valueOrDefault;
    ApproveBillsFilter current = this.Filter.Current;
    nullable = current.CuryApprovedTotal;
    Decimal num2 = e.Row.PaySel.GetValueOrDefault() ? num1 : -num1;
    current.CuryApprovedTotal = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num2) : new Decimal?();
  }

  protected virtual void APInvoice_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APInvoice row = (APInvoice) e.Row;
    System.DateTime? nullable1;
    if (row.PaySel.GetValueOrDefault())
    {
      nullable1 = row.PayDate;
      if (!nullable1.HasValue)
        sender.RaiseExceptionHandling<APInvoice.payDate>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[payDate]"
        }));
    }
    bool? paySel = row.PaySel;
    if (paySel.GetValueOrDefault())
    {
      nullable1 = row.DocDate;
      if (nullable1.HasValue)
      {
        nullable1 = row.PayDate;
        if (nullable1.HasValue)
        {
          nullable1 = row.DocDate;
          System.DateTime dateTime1 = nullable1.Value;
          ref System.DateTime local = ref dateTime1;
          nullable1 = row.PayDate;
          System.DateTime dateTime2 = nullable1.Value;
          if (local.CompareTo(dateTime2) > 0)
            sender.RaiseExceptionHandling<APInvoice.payDate>(e.Row, (object) row.PayDate, (Exception) new PXSetPropertyException("{0} cannot be less than Document Date.", PXErrorLevel.RowError, new object[1]
            {
              (object) "[payDate]"
            }));
        }
      }
    }
    paySel = row.PaySel;
    int? nullable2;
    if (paySel.GetValueOrDefault())
    {
      nullable2 = row.PayLocationID;
      if (!nullable2.HasValue)
        sender.RaiseExceptionHandling<APInvoice.payLocationID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[payLocationID]"
        }));
    }
    paySel = row.PaySel;
    if (paySel.GetValueOrDefault())
    {
      nullable2 = row.PayAccountID;
      if (!nullable2.HasValue)
        sender.RaiseExceptionHandling<APInvoice.payAccountID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[payAccountID]"
        }));
    }
    paySel = row.PaySel;
    if (!paySel.GetValueOrDefault() || row.PayTypeID != null)
      return;
    sender.RaiseExceptionHandling<APInvoice.payTypeID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) "[payTypeID]"
    }));
  }

  protected virtual IEnumerable apdocumentlist()
  {
    APApproveBills graph1 = this;
    ApproveBillsFilter current = graph1.Filter.Current;
    if (current != null && current.SelectionDate.HasValue)
    {
      System.DateTime dateTime1 = current.SelectionDate.Value;
      ref System.DateTime local1 = ref dateTime1;
      short? nullable = current.PayInLessThan;
      double valueOrDefault1 = (double) nullable.GetValueOrDefault();
      System.DateTime dateTime2 = local1.AddDays(valueOrDefault1);
      System.DateTime dateTime3 = current.SelectionDate.Value;
      ref System.DateTime local2 = ref dateTime3;
      nullable = current.DueInLessThan;
      double valueOrDefault2 = (double) nullable.GetValueOrDefault();
      System.DateTime dateTime4 = local2.AddDays(valueOrDefault2);
      System.DateTime dateTime5 = current.SelectionDate.Value;
      ref System.DateTime local3 = ref dateTime5;
      nullable = current.DiscountExpiresInLessThan;
      double valueOrDefault3 = (double) nullable.GetValueOrDefault();
      System.DateTime dateTime6 = local3.AddDays(valueOrDefault3);
      PXView pxView = new PXView((PXGraph) graph1, false, BqlCommand.CreateInstance(graph1.getAPDocumentSelect()));
      PXGraph graph2 = pxView.Graph;
      BqlCommand bqlSelect = pxView.BqlSelect;
      object[] parameters = new object[3]
      {
        (object) dateTime2,
        (object) dateTime4,
        (object) dateTime6
      };
      foreach (PXResult<APInvoice> pxResult in graph2.QuickSelect(bqlSelect, parameters, (PXFilterRow[]) null, false))
      {
        APInvoice apInvoice = (APInvoice) pxResult;
        if (string.IsNullOrEmpty(apInvoice.PayTypeID))
        {
          try
          {
            graph1.APDocumentList.Cache.SetDefaultExt<APInvoice.payTypeID>((object) apInvoice);
          }
          catch (PXSetPropertyException ex)
          {
            graph1.APDocumentList.Cache.RaiseExceptionHandling<APInvoice.payTypeID>((object) apInvoice, (object) apInvoice.PayTypeID, (Exception) ex);
          }
        }
        if (!apInvoice.PayAccountID.HasValue)
        {
          try
          {
            graph1.APDocumentList.Cache.SetDefaultExt<APInvoice.payAccountID>((object) apInvoice);
          }
          catch (PXSetPropertyException ex)
          {
            graph1.APDocumentList.Cache.RaiseExceptionHandling<APInvoice.payAccountID>((object) apInvoice, (object) apInvoice.PayAccountID, (Exception) ex);
          }
        }
        yield return (object) apInvoice;
      }
    }
  }

  protected virtual System.Type getAPDocumentSelect(bool groupBy = false)
  {
    System.Type type1 = typeof (Select2<,,>);
    if (groupBy)
      type1 = typeof (Select5<,,,>);
    System.Type type2 = typeof (APInvoice);
    System.Type type3 = typeof (InnerJoin<Vendor, On<Vendor.bAccountID, Equal<APInvoice.vendorID>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<APInvoice.curyInfoID>>, LeftJoin<APApproveBills.APAdjust, On<APApproveBills.APAdjust.adjdDocType, Equal<APInvoice.docType>, And<APApproveBills.APAdjust.adjdRefNbr, Equal<APInvoice.refNbr>, And<APApproveBills.APAdjust.released, Equal<False>>>>, LeftJoin<APApproveBills.APPayment, On<APApproveBills.APPayment.docType, Equal<APInvoice.docType>, And<APApproveBills.APPayment.refNbr, Equal<APInvoice.refNbr>, PX.Data.And<Where<APApproveBills.APPayment.docType, Equal<APDocType.prepayment>, Or<APApproveBills.APPayment.docType, Equal<APDocType.debitAdj>>>>>>>>>>);
    System.Type type4 = typeof (Where<APInvoice.openDoc, Equal<True>, And2<Where<APInvoice.released, Equal<True>, Or<APInvoice.prebooked, Equal<True>>>, And<APApproveBills.APAdjust.adjgRefNbr, PX.Data.IsNull, And<APApproveBills.APPayment.refNbr, PX.Data.IsNull, And2<Match<Vendor, Current<AccessInfo.userName>>, And2<Where<APInvoice.curyID, Equal<Current<ApproveBillsFilter.curyID>>, Or<Current<ApproveBillsFilter.curyID>, PX.Data.IsNull>>, And2<Where2<Where<Current<ApproveBillsFilter.showApprovedForPayment>, Equal<True>, And<APInvoice.paySel, Equal<True>>>, PX.Data.Or<Where<Current<ApproveBillsFilter.showNotApprovedForPayment>, Equal<True>, And<APInvoice.paySel, Equal<False>>>>>, And2<Where<Vendor.bAccountID, Equal<Current<ApproveBillsFilter.vendorID>>, Or<Current<ApproveBillsFilter.vendorID>, PX.Data.IsNull>>, And2<Where<Vendor.vendorClassID, Equal<Current<ApproveBillsFilter.vendorClassID>>, Or<Current<ApproveBillsFilter.vendorClassID>, PX.Data.IsNull>>, PX.Data.And<Where2<Where2<Where<Current<ApproveBillsFilter.showPayInLessThan>, Equal<True>, And<APInvoice.payDate, LessEqual<Required<APInvoice.payDate>>>>, Or2<Where<Current<ApproveBillsFilter.showDueInLessThan>, Equal<True>, And<APInvoice.dueDate, LessEqual<Required<APInvoice.dueDate>>>>, PX.Data.Or<Where<Current<ApproveBillsFilter.showDiscountExpiresInLessThan>, Equal<True>, And<APInvoice.discDate, LessEqual<Required<APInvoice.discDate>>>>>>>, PX.Data.Or<Where<Current<ApproveBillsFilter.showPayInLessThan>, Equal<False>, And<Current<ApproveBillsFilter.showDueInLessThan>, Equal<False>, And<Current<ApproveBillsFilter.showDiscountExpiresInLessThan>, Equal<False>>>>>>>>>>>>>>>>);
    System.Type type5 = typeof (Aggregate<GroupBy<APInvoice.paySel, Sum<APInvoice.docBal, Sum<APInvoice.curyDocBal>>>>);
    return groupBy ? BqlCommand.Compose(type1, type2, type3, type4, type5) : BqlCommand.Compose(type1, type2, type3, type4);
  }

  public APApproveBills()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    PXUIFieldAttribute.SetDisplayName<APInvoice.paySel>(this.APDocumentList.Cache, "Selected");
  }

  protected virtual IEnumerable filter()
  {
    APApproveBills graph = this;
    ApproveBillsFilter current = graph.Filter.Current;
    if (current != null && current.SelectionDate.HasValue && current.PendingRefresh.GetValueOrDefault())
    {
      System.DateTime dateTime1 = current.SelectionDate.Value;
      ref System.DateTime local1 = ref dateTime1;
      short? nullable1 = current.PayInLessThan;
      double valueOrDefault1 = (double) nullable1.GetValueOrDefault();
      System.DateTime dateTime2 = local1.AddDays(valueOrDefault1);
      System.DateTime dateTime3 = current.SelectionDate.Value;
      ref System.DateTime local2 = ref dateTime3;
      nullable1 = current.DueInLessThan;
      double valueOrDefault2 = (double) nullable1.GetValueOrDefault();
      System.DateTime dateTime4 = local2.AddDays(valueOrDefault2);
      dateTime3 = current.SelectionDate.Value;
      ref System.DateTime local3 = ref dateTime3;
      nullable1 = current.DiscountExpiresInLessThan;
      double valueOrDefault3 = (double) nullable1.GetValueOrDefault();
      System.DateTime dateTime5 = local3.AddDays(valueOrDefault3);
      Decimal num1 = 0M;
      Decimal num2 = 0M;
      PXView pxView = new PXView((PXGraph) graph, true, BqlCommand.CreateInstance(graph.getAPDocumentSelect(true)));
      object[] objArray = new object[3]
      {
        (object) dateTime2,
        (object) dateTime4,
        (object) dateTime5
      };
      foreach (PXResult<APInvoice> pxResult in pxView.SelectMulti(objArray))
      {
        APInvoice apInvoice = (APInvoice) pxResult;
        Decimal num3 = num1;
        Decimal? nullable2;
        Decimal num4;
        if (!apInvoice.PaySel.GetValueOrDefault())
          num4 = 0M;
        else if (!graph.Accessinfo.CuryViewState)
        {
          nullable2 = apInvoice.CuryDocBal;
          num4 = nullable2.GetValueOrDefault();
        }
        else
        {
          nullable2 = apInvoice.DocBal;
          num4 = nullable2.GetValueOrDefault();
        }
        num1 = num3 + num4;
        Decimal num5 = num2;
        Decimal valueOrDefault4;
        if (!graph.Accessinfo.CuryViewState)
        {
          nullable2 = apInvoice.CuryDocBal;
          valueOrDefault4 = nullable2.GetValueOrDefault();
        }
        else
        {
          nullable2 = apInvoice.DocBal;
          valueOrDefault4 = nullable2.GetValueOrDefault();
        }
        num2 = num5 + valueOrDefault4;
      }
      graph.Filter.Current.CuryApprovedTotal = new Decimal?(num1);
      graph.Filter.Current.CuryDocsTotal = new Decimal?(num2);
      graph.Filter.Current.PendingRefresh = new bool?(false);
    }
    yield return (object) graph.Filter.Current;
    graph.Filter.Cache.IsDirty = false;
  }

  [PXHidden]
  [Serializable]
  public class APAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
    public virtual string AdjgDocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    public virtual string AdjgRefNbr { get; set; }

    [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "")]
    public virtual string AdjdDocType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    public virtual string AdjdRefNbr { get; set; }

    [PXDBBool]
    public virtual bool? Released { get; set; }

    public abstract class adjgDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APApproveBills.APAdjust.adjgDocType>
    {
    }

    public abstract class adjgRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APApproveBills.APAdjust.adjgRefNbr>
    {
    }

    public abstract class adjdDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APApproveBills.APAdjust.adjdDocType>
    {
    }

    public abstract class adjdRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APApproveBills.APAdjust.adjdRefNbr>
    {
    }

    public abstract class released : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APApproveBills.APAdjust.released>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class APPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    public virtual string RefNbr { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APApproveBills.APPayment.docType>
    {
    }

    public abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APApproveBills.APPayment.refNbr>
    {
    }
  }
}
