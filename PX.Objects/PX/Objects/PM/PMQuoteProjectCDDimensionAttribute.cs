// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteProjectCDDimensionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class PMQuoteProjectCDDimensionAttribute : PXDimensionAttribute
{
  public PMQuoteProjectCDDimensionAttribute()
    : base("PROJECT")
  {
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      base.FieldVerifying(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      string newValue = e.NewValue as string;
      PMQuoteProjectCDDimensionAttribute.CheckProjectCD(sender.Graph, newValue, ((Exception) ex).Message, true);
      throw ex;
    }
  }

  public static void CheckProjectCD(
    PXGraph graph,
    string quoteProjectCD,
    string errorMessage,
    bool forAttribute)
  {
    Segment[] array = GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select(graph, new object[1]
    {
      (object) "PROJECT"
    })).ToArray<Segment>();
    short startIndex1 = 0;
    bool? nullable1;
    for (int index = 0; index <= array.Length - 1; ++index)
    {
      Segment segment = array[index];
      nullable1 = segment.AutoNumber;
      bool flag1 = false;
      if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
      {
        nullable1 = segment.Validate;
        if (nullable1.GetValueOrDefault())
        {
          bool flag2 = false;
          if (string.IsNullOrWhiteSpace(quoteProjectCD))
            flag2 = true;
          else if (quoteProjectCD.Length < (int) startIndex1 + (int) segment.Length.Value)
            flag2 = true;
          else if (string.IsNullOrWhiteSpace(quoteProjectCD.Substring((int) startIndex1, (int) segment.Length.Value)))
            flag2 = true;
          if (flag2)
          {
            if (forAttribute)
              throw new PXSetPropertyException("The {0}-{1} segment of the New Project ID cannot be empty.", new object[2]
              {
                (object) segment.SegmentID,
                (object) segment.Descr
              });
            throw new PXException("The {0}-{1} segment of the New Project ID cannot be empty.", new object[2]
            {
              (object) segment.SegmentID,
              (object) segment.Descr
            });
          }
        }
      }
      startIndex1 += segment.Length.Value;
    }
    short num1 = 0;
    foreach (Segment segment in array)
    {
      nullable1 = segment.AutoNumber;
      bool flag = false;
      short? nullable2;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      {
        nullable1 = segment.Validate;
        if (nullable1.GetValueOrDefault())
        {
          string str1 = quoteProjectCD;
          int startIndex2 = (int) num1;
          nullable2 = segment.Length;
          int length = (int) nullable2.Value;
          string str2 = str1.Substring(startIndex2, length);
          string descr;
          if (!string.IsNullOrEmpty(segment.Descr))
          {
            descr = segment.Descr;
          }
          else
          {
            nullable2 = segment.SegmentID;
            descr = nullable2.ToString();
          }
          string str3 = descr;
          if (errorMessage.Contains(str3))
          {
            if (forAttribute)
              throw new PXSetPropertyException("'{0}' is not a valid value for the {1}-{2} segment of the {3} segmented key. Either add this segment value to the list of segment values on the Segment Values (CS203000) form or change it to the valid one.", new object[4]
              {
                (object) str2,
                (object) segment.SegmentID,
                (object) segment.Descr,
                (object) "PROJECT"
              });
            throw new PXException("'{0}' is not a valid value for the {1}-{2} segment of the {3} segmented key. Either add this segment value to the list of segment values on the Segment Values (CS203000) form or change it to the valid one.", new object[4]
            {
              (object) str2,
              (object) segment.SegmentID,
              (object) segment.Descr,
              (object) "PROJECT"
            });
          }
        }
      }
      int num2 = (int) num1;
      nullable2 = segment.Length;
      int num3 = (int) nullable2.Value;
      num1 = (short) (num2 + num3);
    }
  }
}
