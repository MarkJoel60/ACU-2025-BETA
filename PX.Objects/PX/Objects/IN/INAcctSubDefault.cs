// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

public class INAcctSubDefault
{
  public const string MaskReasonCode = "0";
  public const string MaskItem = "I";
  public const string MaskSite = "W";
  public const string MaskClass = "P";
  public const string MaskVendor = "V";
  public const string MaskProject = "J";
  public const string MaskTask = "T";

  public static void Required(PXCache sender, PXRowSelectedEventArgs e)
  {
    INAcctSubDefault.AcctSubRequired acctSubRequired = new INAcctSubDefault.AcctSubRequired(sender, e);
  }

  public static void Required(PXCache sender, PXRowPersistingEventArgs e)
  {
    INAcctSubDefault.AcctSubRequired acctSubRequired = new INAcctSubDefault.AcctSubRequired(sender, e);
  }

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] AllowedValues, string[] AllowedLabels)
      : base(AllowedValues, AllowedLabels)
    {
    }

    public CustomListAttribute(Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class ClassListAttribute : INAcctSubDefault.CustomListAttribute
  {
    public ClassListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("W", "Warehouse"),
        PXStringListAttribute.Pair("P", "Posting Class")
      })
    {
    }
  }

  public class SalesCOGSListAttribute : INAcctSubDefault.CustomListAttribute
  {
    public SalesCOGSListAttribute()
      : base(INAcctSubDefault.SalesCOGSListAttribute.GetValuesToLabelsList())
    {
    }

    private static Tuple<string, string>[] GetValuesToLabelsList()
    {
      List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>()
      {
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("W", "Warehouse"),
        PXStringListAttribute.Pair("P", "Posting Class")
      };
      if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      {
        tupleList.Add(PXStringListAttribute.Pair("J", "Project"));
        tupleList.Add(PXStringListAttribute.Pair("T", "Project Task"));
      }
      return tupleList.ToArray();
    }
  }

  public class ReasonCodeListAttribute : INAcctSubDefault.CustomListAttribute
  {
    public ReasonCodeListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("0", "Reason Code"),
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("W", "Warehouse"),
        PXStringListAttribute.Pair("P", "Posting Class")
      })
    {
    }
  }

  public class POAccrualListAttribute : INAcctSubDefault.CustomListAttribute
  {
    public POAccrualListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("W", "Warehouse"),
        PXStringListAttribute.Pair("P", "Posting Class"),
        PXStringListAttribute.Pair("V", "Vendor")
      })
    {
    }
  }

  public class AcctSubRequired
  {
    public bool InvtAcct;
    public bool InvtSub;
    public bool SalesAcct;
    public bool SalesSub;
    public bool COGSAcct;
    public bool COGSSub;
    public bool ReasonCodeSub;
    public bool StdCstVarAcct;
    public bool StdCstVarSub;
    public bool StdCstRevAcct;
    public bool StdCstRevSub;
    public bool POAccrualAcct;
    public bool POAccrualSub;
    protected string[] _sources = new INAcctSubDefault.ClassListAttribute().AllowedValues;
    protected string[] _rcsources = new INAcctSubDefault.ReasonCodeListAttribute().AllowedValues;

    protected virtual void Populate(
      INPostClass postclass,
      INAcctSubDefault.AcctSubRequired.AcctSubDefaultClass option)
    {
      if (postclass == null)
        return;
      this.InvtAcct = this.InvtAcct || postclass.InvtAcctDefault == this._sources[(int) option];
      this.InvtSub = this.InvtSub || !string.IsNullOrEmpty(postclass.InvtSubMask) && postclass.InvtSubMask.IndexOf(char.Parse(this._sources[(int) option])) > -1;
      this.SalesAcct = this.SalesAcct || postclass.SalesAcctDefault == this._sources[(int) option];
      this.SalesSub = this.SalesSub || !string.IsNullOrEmpty(postclass.SalesSubMask) && postclass.SalesSubMask.IndexOf(char.Parse(this._sources[(int) option])) > -1;
      this.COGSAcct = this.COGSAcct || postclass.COGSAcctDefault == this._sources[(int) option];
      int num;
      if (!this.COGSSub)
      {
        bool? cogsSubFromSales = postclass.COGSSubFromSales;
        bool flag = false;
        num = !(cogsSubFromSales.GetValueOrDefault() == flag & cogsSubFromSales.HasValue) || string.IsNullOrEmpty(postclass.COGSSubMask) ? 0 : (postclass.COGSSubMask.IndexOf(char.Parse(this._sources[(int) option])) > -1 ? 1 : 0);
      }
      else
        num = 1;
      this.COGSSub = num != 0;
      this.StdCstVarAcct = this.StdCstVarAcct || postclass.StdCstVarAcctDefault == this._sources[(int) option];
      this.StdCstVarSub = this.StdCstVarSub || !string.IsNullOrEmpty(postclass.StdCstVarSubMask) && postclass.StdCstVarSubMask.IndexOf(char.Parse(this._sources[(int) option])) > -1;
      this.StdCstRevAcct = this.StdCstRevAcct || postclass.StdCstRevAcctDefault == this._sources[(int) option];
      this.StdCstRevSub = this.StdCstRevSub || !string.IsNullOrEmpty(postclass.StdCstRevSubMask) && postclass.StdCstRevSubMask.IndexOf(char.Parse(this._sources[(int) option])) > -1;
      this.POAccrualAcct = this.POAccrualAcct || postclass.POAccrualAcctDefault == this._sources[(int) option];
      this.POAccrualSub = this.POAccrualSub || !string.IsNullOrEmpty(postclass.POAccrualSubMask) && postclass.POAccrualSubMask.IndexOf(char.Parse(this._sources[(int) option])) > -1;
    }

    protected virtual void Populate(
      PX.Objects.CS.ReasonCode reasoncode,
      INAcctSubDefault.AcctSubRequired.AcctSubDefaultReasonCode option)
    {
      if (reasoncode == null)
        return;
      this.ReasonCodeSub = this.ReasonCodeSub || !string.IsNullOrEmpty(reasoncode.SubMask) && reasoncode.SubMask.IndexOf(char.Parse(this._rcsources[(int) option])) > -1;
    }

    public AcctSubRequired(PXCache sender, object data)
    {
      if (sender.GetItemType() == typeof (InventoryItem))
      {
        this.Populate((INPostClass) sender.Graph.Caches[typeof (INPostClass)].Current, INAcctSubDefault.AcctSubRequired.AcctSubDefaultClass.FromItem);
        this.StdCstVarAcct = this.StdCstVarAcct && data != null && ((InventoryItem) data).ValMethod == "T";
        this.StdCstVarSub = this.StdCstVarSub && data != null && ((InventoryItem) data).ValMethod == "T";
        this.StdCstRevAcct = this.StdCstRevAcct && data != null && ((InventoryItem) data).ValMethod == "T";
        this.StdCstRevSub = this.StdCstRevSub && data != null && ((InventoryItem) data).ValMethod == "T";
        foreach (PXResult<PX.Objects.CS.ReasonCode> pxResult in PXSelectBase<PX.Objects.CS.ReasonCode, PXSelectReadonly<PX.Objects.CS.ReasonCode, Where<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.sales>, And<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.creditWriteOff>, And<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.balanceWriteOff>>>>>.Config>.Select(sender.Graph, Array.Empty<object>()))
          this.Populate(PXResult<PX.Objects.CS.ReasonCode>.op_Implicit(pxResult), INAcctSubDefault.AcctSubRequired.AcctSubDefaultReasonCode.FromItem);
      }
      else if (sender.GetItemType() == typeof (INPostClass))
      {
        this.Populate((INPostClass) data, INAcctSubDefault.AcctSubRequired.AcctSubDefaultClass.FromClass);
        foreach (PXResult<PX.Objects.CS.ReasonCode> pxResult in PXSelectBase<PX.Objects.CS.ReasonCode, PXSelectReadonly<PX.Objects.CS.ReasonCode, Where<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.sales>, And<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.creditWriteOff>, And<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.balanceWriteOff>>>>>.Config>.Select(sender.Graph, Array.Empty<object>()))
          this.Populate(PXResult<PX.Objects.CS.ReasonCode>.op_Implicit(pxResult), INAcctSubDefault.AcctSubRequired.AcctSubDefaultReasonCode.FromClass);
      }
      else
      {
        if (!(sender.GetItemType() == typeof (INSite)) || !PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
          return;
        foreach (PXResult<INPostClass> pxResult in PXSelectBase<INPostClass, PXSelectReadonly<INPostClass>.Config>.Select(sender.Graph, Array.Empty<object>()))
          this.Populate(PXResult<INPostClass>.op_Implicit(pxResult), INAcctSubDefault.AcctSubRequired.AcctSubDefaultClass.FromSite);
        foreach (PXResult<PX.Objects.CS.ReasonCode> pxResult in PXSelectBase<PX.Objects.CS.ReasonCode, PXSelectReadonly<PX.Objects.CS.ReasonCode, Where<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.sales>, And<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.creditWriteOff>, And<PX.Objects.CS.ReasonCode.usage, NotEqual<ReasonCodeUsages.balanceWriteOff>>>>>.Config>.Select(sender.Graph, Array.Empty<object>()))
          this.Populate(PXResult<PX.Objects.CS.ReasonCode>.op_Implicit(pxResult), INAcctSubDefault.AcctSubRequired.AcctSubDefaultReasonCode.FromSite);
      }
    }

    public AcctSubRequired(PXCache sender, PXRowSelectedEventArgs e)
      : this(sender, e.Row)
    {
      this.OnRowSelected(sender, e);
    }

    public AcctSubRequired(PXCache sender, PXRowPersistingEventArgs e)
      : this(sender, e.Row)
    {
      this.OnRowPersisting(sender, e);
    }

    public virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      PXUIFieldAttribute.SetRequired<INPostClass.invtAcctID>(sender, this.InvtAcct || this.InvtSub);
      PXUIFieldAttribute.SetRequired<INPostClass.invtSubID>(sender, this.InvtSub);
      PXUIFieldAttribute.SetRequired<INPostClass.salesAcctID>(sender, this.SalesAcct || this.SalesSub);
      PXUIFieldAttribute.SetRequired<INPostClass.salesSubID>(sender, this.SalesSub);
      PXUIFieldAttribute.SetRequired<INPostClass.cOGSAcctID>(sender, this.COGSAcct || this.COGSSub);
      PXUIFieldAttribute.SetRequired<INPostClass.cOGSSubID>(sender, this.COGSSub);
      PXUIFieldAttribute.SetRequired<INPostClass.stdCstVarAcctID>(sender, this.StdCstVarAcct || this.StdCstVarSub);
      PXUIFieldAttribute.SetRequired<INPostClass.stdCstVarSubID>(sender, this.StdCstVarSub);
      PXUIFieldAttribute.SetRequired<INPostClass.stdCstRevAcctID>(sender, this.StdCstRevAcct || this.StdCstRevSub);
      PXUIFieldAttribute.SetRequired<INPostClass.stdCstRevSubID>(sender, this.StdCstRevSub);
      PXUIFieldAttribute.SetRequired<INPostClass.pOAccrualAcctID>(sender, this.POAccrualAcct || this.POAccrualSub);
      PXUIFieldAttribute.SetRequired<INPostClass.pOAccrualSubID>(sender, this.POAccrualSub);
      PXUIFieldAttribute.SetRequired<INPostClass.reasonCodeSubID>(sender, this.ReasonCodeSub);
    }

    public void ThrowFieldIsEmpty<Field>(PXCache sender, object data) where Field : IBqlField
    {
      if (sender.RaiseExceptionHandling<Field>(data, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) $"[{typeof (Field).Name}]"
      })))
        throw new PXRowPersistingException(typeof (Field).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) typeof (Field).Name
        });
    }

    public virtual void OnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
        return;
      if (this.InvtAcct && sender.GetValue<INPostClass.invtAcctID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.invtAcctID>(sender, e.Row);
      if (this.InvtSub && sender.GetValue<INPostClass.invtSubID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.invtSubID>(sender, e.Row);
      if (this.SalesAcct && sender.GetValue<INPostClass.salesAcctID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.salesAcctID>(sender, e.Row);
      if (this.SalesSub && sender.GetValue<INPostClass.salesSubID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.salesSubID>(sender, e.Row);
      if (this.COGSAcct && sender.GetValue<INPostClass.cOGSAcctID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.cOGSAcctID>(sender, e.Row);
      if (this.COGSSub && sender.GetValue<INPostClass.cOGSSubID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.cOGSSubID>(sender, e.Row);
      if (this.StdCstVarAcct && sender.GetValue<INPostClass.stdCstVarAcctID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.stdCstVarAcctID>(sender, e.Row);
      if (this.StdCstVarSub && sender.GetValue<INPostClass.stdCstVarSubID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.stdCstVarSubID>(sender, e.Row);
      if (this.StdCstRevAcct && sender.GetValue<INPostClass.stdCstRevAcctID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.stdCstRevAcctID>(sender, e.Row);
      if (this.StdCstRevSub && sender.GetValue<INPostClass.stdCstRevSubID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.stdCstRevSubID>(sender, e.Row);
      if (this.POAccrualAcct && sender.GetValue<INPostClass.pOAccrualAcctID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.pOAccrualAcctID>(sender, e.Row);
      if (this.POAccrualSub && sender.GetValue<INPostClass.pOAccrualSubID>(e.Row) == null)
        this.ThrowFieldIsEmpty<INPostClass.pOAccrualSubID>(sender, e.Row);
      if (!this.ReasonCodeSub || sender.GetValue<INPostClass.reasonCodeSubID>(e.Row) != null)
        return;
      this.ThrowFieldIsEmpty<INPostClass.reasonCodeSubID>(sender, e.Row);
    }

    protected enum AcctSubDefaultClass
    {
      FromItem,
      FromSite,
      FromClass,
    }

    protected enum AcctSubDefaultReasonCode
    {
      FromReasonCode,
      FromItem,
      FromSite,
      FromClass,
    }
  }
}
