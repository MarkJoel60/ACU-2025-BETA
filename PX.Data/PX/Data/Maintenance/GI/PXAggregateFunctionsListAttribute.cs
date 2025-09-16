// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.PXAggregateFunctionsListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Maintenance.GI;

public class PXAggregateFunctionsListAttribute : PXStringListAttribute
{
  public PXAggregateFunctionsListAttribute(bool isTotal = false)
  {
    string[] allowedValues;
    if (!isTotal)
      allowedValues = new string[6]
      {
        "AVG",
        "COUNT",
        "MAX",
        "MIN",
        "SUM",
        "STRINGAGG"
      };
    else
      allowedValues = new string[5]
      {
        "AVG",
        "COUNT",
        "MAX",
        "MIN",
        "SUM"
      };
    string[] allowedLabels;
    if (!isTotal)
      allowedLabels = new string[6]
      {
        "AVG",
        "COUNT",
        "MAX",
        "MIN",
        "SUM",
        "STRINGAGG"
      };
    else
      allowedLabels = new string[5]
      {
        "AVG",
        "COUNT",
        "MAX",
        "MIN",
        "SUM"
      };
    // ISSUE: explicit constructor call
    base.\u002Ector(allowedValues, allowedLabels);
  }
}
