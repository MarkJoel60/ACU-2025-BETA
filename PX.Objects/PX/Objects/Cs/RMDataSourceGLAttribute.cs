// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMDataSourceGLAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using System;
using System.ComponentModel;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

[PXDBInt]
[PXDBChildIdentity(typeof (RMDataSource.dataSourceID))]
[PXUIField(DisplayName = "Data Source")]
public class RMDataSourceGLAttribute : RMDataSourceAttribute
{
  public virtual void TextFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is RMDataSource rmDataSource))
    {
      object obj = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      if (obj != null)
        rmDataSource = PXResultset<RMDataSource>.op_Implicit(PXSelectBase<RMDataSource, PXSelect<RMDataSource, Where<RMDataSource.dataSourceID, Equal<Required<RMDataSource.dataSourceID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          obj
        }));
    }
    int? nullable1;
    if (rmDataSource != null)
    {
      RMDataSourceGL extension = sender.Graph.Caches[typeof (RMDataSource)].GetExtension<RMDataSourceGL>((object) rmDataSource);
      StringBuilder stringBuilder1 = new StringBuilder();
      nullable1 = extension.OrganizationID;
      if (nullable1.HasValue)
      {
        PX.Objects.GL.DAC.Organization organization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, Equal<Required<PX.Objects.GL.DAC.Organization.organizationID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) extension.OrganizationID
        }));
        if (organization != null)
          stringBuilder1.Append(organization.OrganizationCD?.Trim());
      }
      nullable1 = extension.LedgerID;
      if (nullable1.HasValue)
      {
        PX.Objects.GL.Ledger ledger = PXResultset<PX.Objects.GL.Ledger>.op_Implicit(PXSelectBase<PX.Objects.GL.Ledger, PXSelect<PX.Objects.GL.Ledger, Where<PX.Objects.GL.Ledger.ledgerID, Equal<Required<PX.Objects.GL.Ledger.ledgerID>>>>.Config>.Select(sender.Graph, new object[1]
        {
          (object) extension.LedgerID
        }));
        if (ledger != null)
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          stringBuilder1.Append(ledger.LedgerCD);
        }
      }
      if (!string.IsNullOrEmpty(extension.AccountClassID))
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        stringBuilder1.Append(extension.AccountClassID);
      }
      if (!string.IsNullOrEmpty(extension.StartAccount))
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        stringBuilder1.Append(extension.StartAccount);
      }
      if (!string.IsNullOrEmpty(extension.StartSub))
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        stringBuilder1.Append(extension.StartSub);
      }
      if (!string.IsNullOrEmpty(extension.StartBranch))
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        stringBuilder1.Append(extension.StartBranch);
      }
      if (!string.IsNullOrEmpty(extension.StartPeriod))
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        stringBuilder1.Append(extension.StartPeriod);
      }
      short? nullable2 = extension.StartPeriodYearOffset;
      if (nullable2.GetValueOrDefault() != (short) 0)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        StringBuilder stringBuilder2 = stringBuilder1;
        nullable2 = extension.StartPeriodYearOffset;
        int num = (int) nullable2.Value;
        stringBuilder2.Append((short) num);
      }
      nullable2 = extension.StartPeriodOffset;
      if (nullable2.GetValueOrDefault() != (short) 0)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(", ");
        StringBuilder stringBuilder3 = stringBuilder1;
        nullable2 = extension.StartPeriodOffset;
        int num = (int) nullable2.Value;
        stringBuilder3.Append((short) num);
      }
      if (stringBuilder1.Length == 0)
      {
        if (!string.IsNullOrEmpty(extension.EndAccount))
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          stringBuilder1.Append(extension.EndAccount);
        }
        if (!string.IsNullOrEmpty(extension.EndSub))
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          stringBuilder1.Append(extension.EndSub);
        }
        if (!string.IsNullOrEmpty(extension.EndBranch))
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          stringBuilder1.Append(extension.EndBranch);
        }
        if (!string.IsNullOrEmpty(extension.EndPeriod))
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          stringBuilder1.Append(extension.EndPeriod);
        }
        nullable2 = extension.EndPeriodYearOffset;
        if (nullable2.GetValueOrDefault() != (short) 0)
        {
          if (stringBuilder1.Length > 0)
            stringBuilder1.Append(", ");
          StringBuilder stringBuilder4 = stringBuilder1;
          nullable2 = extension.EndPeriodYearOffset;
          int num = (int) nullable2.Value;
          stringBuilder4.Append((short) num);
        }
        nullable2 = extension.EndPeriodOffset;
        if (nullable2.HasValue)
        {
          nullable2 = extension.EndPeriodOffset;
          nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
          int num1 = 0;
          if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            StringBuilder stringBuilder5 = stringBuilder1;
            nullable2 = extension.EndPeriodOffset;
            int num2 = (int) nullable2.Value;
            stringBuilder5.Append((short) num2);
          }
        }
      }
      if (stringBuilder1.Length == 0)
      {
        nullable2 = rmDataSource.AmountType;
        if (nullable2.GetValueOrDefault() != (short) 0)
        {
          nullable2 = rmDataSource.AmountType;
          switch (nullable2.Value)
          {
            case 1:
              stringBuilder1.Append(Messages.GetLocal("Turnover"));
              break;
            case 2:
              stringBuilder1.Append(Messages.GetLocal("Credit"));
              break;
            case 3:
              stringBuilder1.Append(Messages.GetLocal("Debit"));
              break;
            case 4:
              stringBuilder1.Append(Messages.GetLocal("Beg. Balance"));
              break;
            case 5:
              stringBuilder1.Append(Messages.GetLocal("Ending Balance"));
              break;
            case 21:
              stringBuilder1.Append(Messages.GetLocal("Curr. Turnover"));
              break;
            case 22:
              stringBuilder1.Append(Messages.GetLocal("Curr. Credit"));
              break;
            case 23:
              stringBuilder1.Append(Messages.GetLocal("Curr. Debit"));
              break;
            case 24:
              stringBuilder1.Append(Messages.GetLocal("Curr. Beg. Balance"));
              break;
            case 25:
              stringBuilder1.Append(Messages.GetLocal("Curr. Ending Balance"));
              break;
          }
        }
      }
      if (stringBuilder1.Length == 0 && !string.IsNullOrEmpty(rmDataSource.RowDescription))
      {
        switch (rmDataSource.RowDescription)
        {
          case "C":
            stringBuilder1.Append(Messages.GetLocal("Code"));
            break;
          case "D":
            stringBuilder1.Append(Messages.GetLocal("Description"));
            break;
          case "CD":
            stringBuilder1.Append(Messages.GetLocal("Code-Description"));
            break;
          case "DC":
            stringBuilder1.Append(Messages.GetLocal("Description-Code"));
            break;
        }
      }
      e.ReturnValue = stringBuilder1.Length > 0 ? (object) stringBuilder1.ToString() : (object) "";
    }
    if (((PXEventSubscriberAttribute) this)._AttributeLevel != 2 && !e.IsAltered)
      return;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    object returnState = e.ReturnState;
    int? nullable3 = new int?(100);
    bool? nullable4 = new bool?();
    string str = ((PXEventSubscriberAttribute) this)._FieldName + "Text";
    bool? nullable5 = new bool?(false);
    nullable1 = new int?();
    int? nullable6 = nullable1;
    bool? nullable7 = new bool?();
    PXFieldState instance = PXStringState.CreateInstance(returnState, nullable3, nullable4, str, nullable5, nullable6, (string) null, (string[]) null, (string[]) null, nullable7, (string) null, (string[]) null);
    selectingEventArgs.ReturnState = (object) instance;
    ((PXFieldState) e.ReturnState).Visible = false;
  }

  public virtual void DataSourceSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.ledgerID>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.accountClassID>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.startAccount>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.endAccount>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.startSub>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.endSub>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.organizationID>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.startBranch>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSourceGL.endBranch>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL");
    PXUIFieldAttribute.SetVisible<RMDataSource.rowDescription>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "GL" || ((IRMType) this._RowParentCache.Current).RMType == "PM");
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<RMDataSource.rowDescription>(sender, e.Row, (((IRMType) this._RowParentCache.Current).RMType == "GL" || ((IRMType) this._RowParentCache.Current).RMType == "PM") && ((RMDataSource) e.Row).Expand != "N");
  }

  public virtual void EnsurePeriodLength(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (((CancelEventArgs) e).Cancel)
      return;
    if (e.NewValue is string str)
    {
      if (str.Length < 6)
        str += new string(' ', 6 - str.Length);
      else if (str.Length > 6)
        str = str.Substring(0, 6);
      e.NewValue = (object) (str.Substring(2) + str.Substring(0, 2));
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying1 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) sourceGlAttribute1, __vmethodptr(sourceGlAttribute1, SuppressFieldVerifying));
    fieldVerifying1.AddHandler<RMDataSourceGL.startAccount>(pxFieldVerifying1);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) sourceGlAttribute2, __vmethodptr(sourceGlAttribute2, SuppressFieldVerifying));
    fieldVerifying2.AddHandler<RMDataSourceGL.startSub>(pxFieldVerifying2);
    PXGraph.FieldVerifyingEvents fieldVerifying3 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying3 = new PXFieldVerifying((object) sourceGlAttribute3, __vmethodptr(sourceGlAttribute3, SuppressFieldVerifying));
    fieldVerifying3.AddHandler<RMDataSourceGL.startPeriod>(pxFieldVerifying3);
    PXGraph.FieldUpdatingEvents fieldUpdating1 = this._Cache.Graph.FieldUpdating;
    RMDataSourceGLAttribute sourceGlAttribute4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating1 = new PXFieldUpdating((object) sourceGlAttribute4, __vmethodptr(sourceGlAttribute4, EnsurePeriodLength));
    fieldUpdating1.AddHandler<RMDataSourceGL.startPeriod>(pxFieldUpdating1);
    PXGraph.FieldVerifyingEvents fieldVerifying4 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute5 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying4 = new PXFieldVerifying((object) sourceGlAttribute5, __vmethodptr(sourceGlAttribute5, SuppressFieldVerifying));
    fieldVerifying4.AddHandler<RMDataSourceGL.endAccount>(pxFieldVerifying4);
    PXGraph.FieldVerifyingEvents fieldVerifying5 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute6 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying5 = new PXFieldVerifying((object) sourceGlAttribute6, __vmethodptr(sourceGlAttribute6, SuppressFieldVerifying));
    fieldVerifying5.AddHandler<RMDataSourceGL.endSub>(pxFieldVerifying5);
    PXGraph.FieldVerifyingEvents fieldVerifying6 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute7 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying6 = new PXFieldVerifying((object) sourceGlAttribute7, __vmethodptr(sourceGlAttribute7, SuppressFieldVerifying));
    fieldVerifying6.AddHandler<RMDataSourceGL.endPeriod>(pxFieldVerifying6);
    PXGraph.FieldUpdatingEvents fieldUpdating2 = this._Cache.Graph.FieldUpdating;
    RMDataSourceGLAttribute sourceGlAttribute8 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating2 = new PXFieldUpdating((object) sourceGlAttribute8, __vmethodptr(sourceGlAttribute8, EnsurePeriodLength));
    fieldUpdating2.AddHandler<RMDataSourceGL.endPeriod>(pxFieldUpdating2);
    PXGraph.FieldVerifyingEvents fieldVerifying7 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute9 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying7 = new PXFieldVerifying((object) sourceGlAttribute9, __vmethodptr(sourceGlAttribute9, SuppressFieldVerifying));
    fieldVerifying7.AddHandler<RMDataSourceGL.organizationID>(pxFieldVerifying7);
    PXGraph.FieldVerifyingEvents fieldVerifying8 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute10 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying8 = new PXFieldVerifying((object) sourceGlAttribute10, __vmethodptr(sourceGlAttribute10, SuppressFieldVerifying));
    fieldVerifying8.AddHandler<RMDataSourceGL.startBranch>(pxFieldVerifying8);
    PXGraph.FieldVerifyingEvents fieldVerifying9 = this._Cache.Graph.FieldVerifying;
    RMDataSourceGLAttribute sourceGlAttribute11 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying9 = new PXFieldVerifying((object) sourceGlAttribute11, __vmethodptr(sourceGlAttribute11, SuppressFieldVerifying));
    fieldVerifying9.AddHandler<RMDataSourceGL.endBranch>(pxFieldVerifying9);
  }

  protected virtual void DataSourceExpandSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), typeof (RMDataSource.expand).Name, new bool?(false), new int?(0), (string) null, new string[3]
    {
      "N",
      "A",
      "S"
    }, new string[3]
    {
      Messages.GetLocal("Nothing"),
      Messages.GetLocal("Account"),
      Messages.GetLocal("Sub")
    }, new bool?(true), "N", (string[]) null);
  }

  protected virtual void DataSourceRowDescriptionSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), typeof (RMDataSource.rowDescription).Name, new bool?(false), new int?(0), (string) null, new string[5]
    {
      "N",
      "C",
      "D",
      "CD",
      "DC"
    }, new string[5]
    {
      Messages.GetLocal("Not Set"),
      Messages.GetLocal("Code"),
      Messages.GetLocal("Description"),
      Messages.GetLocal("Code-Description"),
      Messages.GetLocal("Description-Code")
    }, new bool?(true), "N", (string[]) null);
  }

  protected virtual void DataSourceAmountTypeSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, typeof (RMDataSource.amountType).Name, new bool?(false), new int?(0), new int?(), new int?(), new int[11]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      21,
      22,
      23,
      24,
      25
    }, new string[11]
    {
      Messages.GetLocal("Not Set"),
      Messages.GetLocal("Turnover"),
      Messages.GetLocal("Credit"),
      Messages.GetLocal("Debit"),
      Messages.GetLocal("Beg. Balance"),
      Messages.GetLocal("Ending Balance"),
      Messages.GetLocal("Curr. Turnover"),
      Messages.GetLocal("Curr. Credit"),
      Messages.GetLocal("Curr. Debit"),
      Messages.GetLocal("Curr. Beg. Balance"),
      Messages.GetLocal("Curr. Ending Balance")
    }, (Type) null, new int?(0), (string[]) null, new bool?());
  }
}
