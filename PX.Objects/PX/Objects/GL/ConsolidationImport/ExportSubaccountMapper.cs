// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ConsolidationImport.ExportSubaccountMapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.ConsolidationImport;

public class ExportSubaccountMapper : IExportSubaccountMapper
{
  protected readonly ExportSubaccountMapper.SegmentWithMappingInfo[] OrderedSegmentWithMappingInfos;

  public ExportSubaccountMapper(
    IReadOnlyCollection<Segment> segments,
    IEnumerable<SegmentValue> segmentValues)
  {
    short?[] segmentIdsNeedingMappedValues = segments.Where<Segment>((Func<Segment, bool>) (segment => segment.Validate.GetValueOrDefault())).Select<Segment, short?>((Func<Segment, short?>) (segment => segment.SegmentID)).ToArray<short?>();
    Dictionary<short?, IGrouping<short?, SegmentValue>> segmentValuesGroupsBySegmentID = segmentValues.Where<SegmentValue>((Func<SegmentValue, bool>) (segmentValue => ((IEnumerable<short?>) segmentIdsNeedingMappedValues).Contains<short?>(segmentValue.SegmentID))).GroupBy<SegmentValue, short?>((Func<SegmentValue, short?>) (segmentValue => segmentValue.SegmentID)).ToDictionary<IGrouping<short?, SegmentValue>, short?, IGrouping<short?, SegmentValue>>((Func<IGrouping<short?, SegmentValue>, short?>) (group => group.Key), (Func<IGrouping<short?, SegmentValue>, IGrouping<short?, SegmentValue>>) (group => group));
    this.OrderedSegmentWithMappingInfos = segments.Select<Segment, ExportSubaccountMapper.SegmentWithMappingInfo>((Func<Segment, ExportSubaccountMapper.SegmentWithMappingInfo>) (segment => new ExportSubaccountMapper.SegmentWithMappingInfo(segment, segment.Validate.GetValueOrDefault() ? (IEnumerable<SegmentValue>) segmentValuesGroupsBySegmentID[segment.SegmentID] : Enumerable.Empty<SegmentValue>()))).OrderBy<ExportSubaccountMapper.SegmentWithMappingInfo, short?>((Func<ExportSubaccountMapper.SegmentWithMappingInfo, short?>) (segment => segment.Segment.SegmentID)).ToArray<ExportSubaccountMapper.SegmentWithMappingInfo>();
  }

  public string GetMappedSubaccountCD(PX.Objects.GL.Sub subaccount)
  {
    if (subaccount == null)
      throw new ArgumentNullException(nameof (subaccount));
    int num1 = 0;
    SortedList<short, string> sortedList1 = new SortedList<short, string>();
    foreach (ExportSubaccountMapper.SegmentWithMappingInfo segmentWithMappingInfo in this.OrderedSegmentWithMappingInfos)
    {
      Segment segment = segmentWithMappingInfo.Segment;
      short? nullable1 = segment.ConsolNumChar;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      int num2 = 0;
      if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
      {
        int num3 = num1;
        nullable1 = segment.Length;
        int num4 = (int) nullable1.Value;
        num1 = num3 + num4;
      }
      else
      {
        string subCd = subaccount.SubCD;
        int startIndex = num1;
        nullable1 = segment.Length;
        int length1 = (int) nullable1.Value;
        string key1 = subCd.Substring(startIndex, length1);
        string str1 = (string) null;
        if (segment.Validate.GetValueOrDefault())
        {
          if (segmentWithMappingInfo.SegmentValueMap.ContainsKey(key1))
          {
            string segmentValue = segmentWithMappingInfo.SegmentValueMap[key1];
            string str2;
            if (segmentValue == null)
            {
              str2 = (string) null;
            }
            else
            {
              nullable1 = segmentWithMappingInfo.Segment.ConsolNumChar;
              str2 = segmentValue.PadRight((int) nullable1.GetValueOrDefault(), ' ');
            }
            str1 = str2;
          }
          else if (segmentWithMappingInfo.SegmentValueMap.ContainsKey(key1.TrimEnd()))
          {
            string segmentValue = segmentWithMappingInfo.SegmentValueMap[key1.TrimEnd()];
            string str3;
            if (segmentValue == null)
            {
              str3 = (string) null;
            }
            else
            {
              nullable1 = segmentWithMappingInfo.Segment.ConsolNumChar;
              str3 = segmentValue.PadRight((int) nullable1.GetValueOrDefault(), ' ');
            }
            str1 = str3;
          }
        }
        else
          str1 = key1;
        if (str1 != null)
        {
          int length2 = str1.Length;
          nullable1 = segment.ConsolNumChar;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          int valueOrDefault = nullable2.GetValueOrDefault();
          if (length2 == valueOrDefault & nullable2.HasValue)
          {
            SortedList<short, string> sortedList2 = sortedList1;
            nullable1 = segment.ConsolOrder;
            int key2 = (int) nullable1.Value;
            string str4 = str1;
            sortedList2[(short) key2] = str4;
            int num5 = num1;
            nullable1 = segment.Length;
            int num6 = (int) nullable1.Value;
            num1 = num5 + num6;
            continue;
          }
        }
        return string.Empty;
      }
    }
    return string.Join(string.Empty, (IEnumerable<string>) sortedList1.Values);
  }

  protected class SegmentWithMappingInfo
  {
    public readonly Segment Segment;
    public readonly Dictionary<string, string> SegmentValueMap;

    public SegmentWithMappingInfo(Segment segment, IEnumerable<SegmentValue> segmentValues)
    {
      this.Segment = segment;
      this.SegmentValueMap = segmentValues.ToDictionary<SegmentValue, string, string>((Func<SegmentValue, string>) (segmentValue => segmentValue.Value), (Func<SegmentValue, string>) (segmentValue => segmentValue.MappedSegValue));
    }
  }
}
