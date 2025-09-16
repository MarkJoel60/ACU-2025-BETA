// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMDataSourcePMAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using System;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

[PXDBInt]
[PXDBChildIdentity(typeof (RMDataSource.dataSourceID))]
[PXUIField(DisplayName = "Data Source")]
public class RMDataSourcePMAttribute : RMDataSourceGLAttribute
{
  public override void TextFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._RowParentCache.Current == null)
      return;
    if (((IRMType) this._RowParentCache.Current).RMType == "GL")
    {
      base.TextFieldSelecting(sender, e);
    }
    else
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
        RMDataSourcePM extension1 = sender.Graph.Caches[typeof (RMDataSource)].GetExtension<RMDataSourcePM>((object) rmDataSource);
        RMDataSourceGL extension2 = sender.Graph.Caches[typeof (RMDataSource)].GetExtension<RMDataSourceGL>((object) rmDataSource);
        StringBuilder stringBuilder1 = new StringBuilder();
        if (this._RowParentCache != null && this._RowParentCache.Current != null && this._RowParentCache.Current is IRMType && ((IRMType) this._RowParentCache.Current).RMType == "PM")
        {
          if (!string.IsNullOrEmpty(extension1.StartAccountGroup))
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            stringBuilder1.Append(extension1.StartAccountGroup);
          }
          if (!string.IsNullOrEmpty(extension1.StartProject))
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            stringBuilder1.Append(extension1.StartProject);
          }
          if (!string.IsNullOrEmpty(extension1.StartProjectTask))
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            stringBuilder1.Append(extension1.StartProjectTask);
          }
          if (!string.IsNullOrEmpty(extension1.StartInventory))
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            stringBuilder1.Append(extension1.StartInventory);
          }
          if (!string.IsNullOrEmpty(extension2.StartPeriod))
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            stringBuilder1.Append(extension2.StartPeriod);
          }
          short? nullable2 = extension2.StartPeriodOffset;
          if (nullable2.GetValueOrDefault() != (short) 0)
          {
            if (stringBuilder1.Length > 0)
              stringBuilder1.Append(", ");
            StringBuilder stringBuilder2 = stringBuilder1;
            nullable2 = extension2.StartPeriodOffset;
            int num = (int) nullable2.Value;
            stringBuilder2.Append((short) num);
          }
          if (stringBuilder1.Length == 0)
          {
            if (!string.IsNullOrEmpty(extension1.EndAccountGroup))
            {
              if (stringBuilder1.Length > 0)
                stringBuilder1.Append(", ");
              stringBuilder1.Append(extension1.EndAccountGroup);
            }
            if (!string.IsNullOrEmpty(extension1.EndProject))
            {
              if (stringBuilder1.Length > 0)
                stringBuilder1.Append(", ");
              stringBuilder1.Append(extension1.EndProject);
            }
            if (!string.IsNullOrEmpty(extension1.EndProjectTask))
            {
              if (stringBuilder1.Length > 0)
                stringBuilder1.Append(", ");
              stringBuilder1.Append(extension1.EndProjectTask);
            }
            if (!string.IsNullOrEmpty(extension2.EndPeriod))
            {
              if (stringBuilder1.Length > 0)
                stringBuilder1.Append(", ");
              stringBuilder1.Append(extension2.EndPeriod);
            }
            nullable2 = extension2.EndPeriodOffset;
            if (nullable2.HasValue)
            {
              nullable2 = extension2.EndPeriodOffset;
              nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
              int num1 = 0;
              if (!(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue))
              {
                if (stringBuilder1.Length > 0)
                  stringBuilder1.Append(", ");
                StringBuilder stringBuilder3 = stringBuilder1;
                nullable2 = extension2.EndPeriodOffset;
                int num2 = (int) nullable2.Value;
                stringBuilder3.Append((short) num2);
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
                case 6:
                  stringBuilder1.Append(Messages.GetLocal("Actual Amount"));
                  break;
                case 7:
                  stringBuilder1.Append(Messages.GetLocal("Actual Quantity"));
                  break;
                case 8:
                  stringBuilder1.Append(Messages.GetLocal("Original Budgeted Amount"));
                  break;
                case 9:
                  stringBuilder1.Append(Messages.GetLocal("Original Budgeted Quantity"));
                  break;
                case 10:
                  stringBuilder1.Append(Messages.GetLocal("Revised Budgeted Amount"));
                  break;
                case 11:
                  stringBuilder1.Append(Messages.GetLocal("Revised Budgeted Quantity"));
                  break;
                case 12:
                  stringBuilder1.Append(Messages.GetLocal("Amount Turnover"));
                  break;
                case 13:
                  stringBuilder1.Append(Messages.GetLocal("Quantity Turnover"));
                  break;
                case 26:
                  stringBuilder1.Append(Messages.GetLocal("Revised Committed Amount"));
                  break;
                case 27:
                  stringBuilder1.Append(Messages.GetLocal("Revised Committed Quantity"));
                  break;
                case 28:
                  stringBuilder1.Append(Messages.GetLocal("Committed Open Amount"));
                  break;
                case 29:
                  stringBuilder1.Append(Messages.GetLocal("Committed Open Quantity"));
                  break;
                case 31 /*0x1F*/:
                  stringBuilder1.Append(Messages.GetLocal("Committed Received Quantity"));
                  break;
                case 32 /*0x20*/:
                  stringBuilder1.Append(Messages.GetLocal("Committed Invoiced Amount"));
                  break;
                case 33:
                  stringBuilder1.Append(Messages.GetLocal("Committed Invoiced Quantity"));
                  break;
                case 34:
                  stringBuilder1.Append(Messages.GetLocal("Change Order Quantity"));
                  break;
                case 35:
                  stringBuilder1.Append(Messages.GetLocal("Change Order Amount"));
                  break;
                case 36:
                  stringBuilder1.Append(Messages.GetLocal("Original Committed Amount"));
                  break;
                case 37:
                  stringBuilder1.Append(Messages.GetLocal("Original Committed Quantity"));
                  break;
                case 38:
                  stringBuilder1.Append(Messages.GetLocal("Draft Change Order Quantity"));
                  break;
                case 39:
                  stringBuilder1.Append(Messages.GetLocal("Draft Change Order Amount"));
                  break;
              }
            }
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
  }

  public override void DataSourceSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.DataSourceSelected(sender, e);
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.startAccountGroup>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.endAccountGroup>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.startProject>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.endProject>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.startProjectTask>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.endProjectTask>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.startInventory>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
    PXUIFieldAttribute.SetVisible<RMDataSourcePM.endInventory>(sender, e.Row, ((IRMType) this._RowParentCache.Current).RMType == "PM");
  }

  public override void CacheAttached(PXCache sender) => base.CacheAttached(sender);

  protected override void DataSourceExpandSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    if (((IRMType) this._RowParentCache.Current).RMType == "PM")
      e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), typeof (RMDataSource.expand).Name, new bool?(false), new int?(0), (string) null, new string[5]
      {
        "N",
        "G",
        "P",
        "T",
        "I"
      }, new string[5]
      {
        Messages.GetLocal("Nothing"),
        Messages.GetLocal("Account Group"),
        Messages.GetLocal("Project"),
        Messages.GetLocal("Project Task"),
        Messages.GetLocal("Inventory")
      }, new bool?(true), "N", (string[]) null);
    else
      base.DataSourceExpandSelecting(sender, e);
  }

  protected override void DataSourceRowDescriptionSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    base.DataSourceRowDescriptionSelecting(sender, e);
  }

  protected override void DataSourceAmountTypeSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._RowParentCache == null || this._RowParentCache.Current == null || !(this._RowParentCache.Current is IRMType))
      return;
    if (((IRMType) this._RowParentCache.Current).RMType == "PM")
      e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, typeof (RMDataSource.amountType).Name, new bool?(false), new int?(0), new int?(), new int?(), new int[18]
      {
        0,
        6,
        7,
        12,
        13,
        8,
        9,
        10,
        11,
        36,
        37,
        26,
        27,
        28,
        29,
        31 /*0x1F*/,
        32 /*0x20*/,
        33
      }, new string[18]
      {
        Messages.GetLocal("Not Set"),
        Messages.GetLocal("Actual Amount"),
        Messages.GetLocal("Actual Quantity"),
        Messages.GetLocal("Amount Turnover"),
        Messages.GetLocal("Quantity Turnover"),
        Messages.GetLocal("Original Budgeted Amount"),
        Messages.GetLocal("Original Budgeted Quantity"),
        Messages.GetLocal("Revised Budgeted Amount"),
        Messages.GetLocal("Revised Budgeted Quantity"),
        Messages.GetLocal("Original Committed Amount"),
        Messages.GetLocal("Original Committed Quantity"),
        Messages.GetLocal("Revised Committed Amount"),
        Messages.GetLocal("Revised Committed Quantity"),
        Messages.GetLocal("Committed Open Amount"),
        Messages.GetLocal("Committed Open Quantity"),
        Messages.GetLocal("Committed Received Quantity"),
        Messages.GetLocal("Committed Invoiced Amount"),
        Messages.GetLocal("Committed Invoiced Quantity")
      }, (Type) null, new int?(0), (string[]) null, new bool?());
    else
      base.DataSourceAmountTypeSelecting(sender, e);
  }
}
