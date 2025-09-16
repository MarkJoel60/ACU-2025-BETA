// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AlternateAutoNumberAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FS;

public abstract class AlternateAutoNumberAttribute : AutoNumberAttribute
{
  private string originalRefNbr;

  protected abstract string GetInitialRefNbr(string baseRefNbr);

  public virtual string GetNextRefNbr(string baseRefNbr, string lastRefNbr)
  {
    string newNumberSymbol = this.GetNewNumberSymbol();
    if (string.IsNullOrWhiteSpace(lastRefNbr) || newNumberSymbol != null && lastRefNbr.Trim() == newNumberSymbol.Trim())
      return this.GetInitialRefNbr(baseRefNbr);
    lastRefNbr = lastRefNbr.Trim();
    int num = lastRefNbr.LastIndexOf("-") + 1;
    string str = (int.Parse(lastRefNbr.Substring(num)) + 1).ToString().Trim();
    if (str.Length < lastRefNbr.Length - num)
      str = str.PadLeft(lastRefNbr.Length - num, '0');
    return lastRefNbr.Substring(0, num) + str;
  }

  public AlternateAutoNumberAttribute(Type setupField, Type dateField)
    : base(setupField, dateField)
  {
  }

  public AlternateAutoNumberAttribute()
    : base(typeof (FSSetup.empSchdlNumberingID), typeof (AccessInfo.businessDate))
  {
  }

  public override void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Row == null)
      return;
    if (e.TranStatus == 2 && e.Operation == 2 && this.originalRefNbr != null)
    {
      sender.SetValue(e.Row, this._FieldName, (object) this.originalRefNbr);
      sender.Normalize();
    }
    if (e.TranStatus != 2 && e.TranStatus != 1)
      return;
    this.originalRefNbr = (string) null;
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null || e.Operation != 2)
      return;
    this.originalRefNbr = (string) sender.GetValue(e.Row, this._FieldName);
    if (!this.SetRefNbr(sender, e.Row))
      throw new AutoNumberException();
    sender.Normalize();
  }

  protected abstract bool SetRefNbr(PXCache cache, object row);
}
