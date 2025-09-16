// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDatetimeScalarAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXAttributeFamily(typeof (PXDBFieldAttribute))]
public class PXDBDatetimeScalarAttribute : PXDBScalarAttribute
{
  public virtual bool PreserveTime { get; set; }

  public virtual bool UseTimeZone { get; set; }

  public PXDBDatetimeScalarAttribute(System.Type search)
    : base(search)
  {
    this.typeOfProperty = typeof (System.DateTime);
  }

  protected override void SetValue(PXCache cache, object row, object value)
  {
    System.DateTime? nullable = value as System.DateTime?;
    if (nullable.HasValue)
    {
      if (this.PreserveTime)
      {
        if (this.UseTimeZone)
          value = (object) PXTimeZoneInfo.ConvertTimeFromUtc(nullable.Value, LocaleInfo.GetTimeZone());
      }
      else
      {
        System.DateTime dateTime = nullable.Value;
        int year = dateTime.Year;
        dateTime = nullable.Value;
        int month = dateTime.Month;
        dateTime = nullable.Value;
        int day = dateTime.Day;
        value = (object) new System.DateTime(year, month, day);
      }
    }
    base.SetValue(cache, row, value);
  }
}
