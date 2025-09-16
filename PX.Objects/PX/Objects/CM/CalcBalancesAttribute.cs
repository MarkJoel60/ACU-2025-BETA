// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CalcBalancesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

public class CalcBalancesAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _CuryAmtField;
  protected string _BaseAmtField;
  protected string _PayCuryIDField = "CashCuryID";
  protected string _DocCuryIDField = "DocCuryID";
  protected string _BaseCuryIDField = "BaseCuryID";
  protected string _PayCuryRateField = "CashCuryRate";
  protected string _PayCuryMultDivField = "CashCuryMultDiv";
  protected string _DocCuryRateField = "DocCuryRate";
  protected string _DocCuryMultDivField = "DocCuryMultDiv";

  public string PayCuryIDField
  {
    get => this._PayCuryIDField;
    set => this._PayCuryIDField = value;
  }

  public string DocCuryIDField
  {
    get => this._DocCuryIDField;
    set => this._DocCuryIDField = value;
  }

  public string BaseCuryIDField
  {
    get => this._BaseCuryIDField;
    set => this._BaseCuryIDField = value;
  }

  public string PayCuryRateField
  {
    get => this._PayCuryRateField;
    set => this._PayCuryRateField = value;
  }

  public string PayCuryMultDivField
  {
    get => this._PayCuryMultDivField;
    set => this._PayCuryMultDivField = value;
  }

  public string DocCuryRateField
  {
    get => this._DocCuryRateField;
    set => this._DocCuryRateField = value;
  }

  public string DocCuryMultDivField
  {
    get => this._DocCuryMultDivField;
    set => this._DocCuryMultDivField = value;
  }

  public CalcBalancesAttribute(Type CuryAmtField, Type BaseAmtField)
  {
    this._CuryAmtField = CuryAmtField.Name;
    this._BaseAmtField = BaseAmtField.Name;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object obj1 = sender.GetValue(e.Row, this._CuryAmtField);
    object obj2 = sender.GetValue(e.Row, this._BaseAmtField);
    string PayCuryID = (string) sender.GetValue(e.Row, this._PayCuryIDField);
    string DocCuryID = (string) sender.GetValue(e.Row, this._DocCuryIDField);
    string BaseCuryID = (string) sender.GetValue(e.Row, this._BaseCuryIDField);
    object PayCuryRate = sender.GetValue(e.Row, this._PayCuryRateField);
    string PayCuryMultDiv = (string) sender.GetValue(e.Row, this._PayCuryMultDivField);
    object DocCuryRate = sender.GetValue(e.Row, this._DocCuryRateField);
    string DocCuryMultDiv = (string) sender.GetValue(e.Row, this._DocCuryMultDivField);
    if (PayCuryRate == null)
    {
      PayCuryRate = (object) 1M;
      PayCuryMultDiv = "M";
    }
    if (DocCuryRate == null)
    {
      DocCuryRate = (object) 1M;
      DocCuryMultDiv = "M";
    }
    sender.RaiseFieldSelecting(this._CuryAmtField, e.Row, ref obj1, true);
    sender.RaiseFieldSelecting(this._BaseAmtField, e.Row, ref obj2, true);
    e.ReturnValue = (object) PaymentEntry.CalcBalances((Decimal?) ((PXFieldState) obj1).Value, (Decimal?) ((PXFieldState) obj2).Value, PayCuryID, DocCuryID, BaseCuryID, (Decimal) PayCuryRate, PayCuryMultDiv, (Decimal) DocCuryRate, DocCuryMultDiv, ((PXFieldState) obj1).Precision, ((PXFieldState) obj2).Precision);
    e.ReturnState = (object) PXDecimalState.CreateInstance(e.ReturnState, new int?(((PXFieldState) obj1).Precision), this._FieldName, new bool?(), new int?(), new Decimal?(), new Decimal?());
  }
}
