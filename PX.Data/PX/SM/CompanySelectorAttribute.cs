// Decompiled with JetBrains decompiler
// Type: PX.SM.CompanySelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Update;
using System.Collections;

#nullable disable
namespace PX.SM;

internal class CompanySelectorAttribute : PXCustomSelectorAttribute
{
  public CompanySelectorAttribute()
    : base(typeof (UPCompany.companyID))
  {
  }

  protected virtual IEnumerable GetRecords() => (IEnumerable) PXCompanyHelper.SelectCompanies();

  public override void DescriptionFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string alias)
  {
    if (e.Row == null || sender.GetValue(e.Row, this._FieldOrdinal) == null)
    {
      base.DescriptionFieldSelecting(sender, e, alias);
    }
    else
    {
      UPCompany data = (UPCompany) null;
      int num1 = (int) sender.GetValue(e.Row, this._FieldOrdinal);
      foreach (UPCompany selectCompany in PXCompanyHelper.SelectCompanies())
      {
        int? companyId = selectCompany.CompanyID;
        int num2 = num1;
        if (companyId.GetValueOrDefault() == num2 & companyId.HasValue)
        {
          data = selectCompany;
          break;
        }
      }
      if (data == null)
        return;
      e.ReturnValue = sender.Graph.Caches[this._Type].GetValue((object) data, this._DescriptionField.Name);
    }
  }
}
