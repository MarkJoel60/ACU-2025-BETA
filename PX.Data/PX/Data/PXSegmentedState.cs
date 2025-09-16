// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSegmentedState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Provides data to set up the presentation of the segmented DAC field input control.</summary>
/// <example><para>The code below gets the field state object of the SubItemID.</para>
///   <code title=" " lang="CS">
/// PXSegmentedState subItem =
///     this.ResultRecords.Cache.GetValueExt&lt;InventoryTranDetEnqResult.subItemID&gt;
///     (this.ResultRecords.Current) as PXSegmentedState;</code>
/// </example>
public class PXSegmentedState : PXStringState
{
  protected PXSegment[] _Segments;
  protected bool _ValidCombos;
  protected DimensionLookupMode _LookupMode;
  protected string _SegmentValueField;
  protected string _SegmentDescriptionName;
  protected string _SegmentViewName;
  protected string[] _SegmentFieldList;
  private static ConcurrentDictionary<string, string[]> _segmentHeaderListCached = new ConcurrentDictionary<string, string[]>();
  protected string[] _SegmentHeaderList;
  protected string _Wildcard;

  protected PXSegmentedState(object value)
    : base(value)
  {
  }

  /// <summary>The list of segments for the segmented field input control.</summary>
  public PXSegment[] Segments
  {
    get => this._Segments;
    set => this._Segments = value;
  }

  /// <summary>A value that indicates whether the segmented field input control displays a single lookup or a separate lookup for each segment.</summary>
  public bool ValidCombos
  {
    get => this._ValidCombos;
    set => this._ValidCombos = value;
  }

  public DimensionLookupMode LookupMode
  {
    get => this._LookupMode;
    set => this._LookupMode = value;
  }

  public string SegmentValueField
  {
    get => this._SegmentValueField;
    set => this._SegmentValueField = value;
  }

  public string SegmentDescriptionName
  {
    get => this._SegmentDescriptionName;
    set => this._SegmentDescriptionName = value;
  }

  public string SegmentViewName
  {
    get => this._SegmentViewName;
    set => this._SegmentViewName = value;
  }

  public string[] SegmentFieldList
  {
    get => this._SegmentFieldList;
    set => this._SegmentFieldList = value;
  }

  public string[] SegmentHeaderList
  {
    get => this._SegmentHeaderList;
    set => this._SegmentHeaderList = value;
  }

  /// <summary>The collection of characters allowed to be specified within each segment
  /// in addition to the <tt>EditMask</tt> property of <see cref="T:PX.Data.PXSegment" />.</summary>
  public string Wildcard
  {
    get => this._Wildcard;
    set => this._Wildcard = value;
  }

  internal static string GetEditMaskForSegment(PXSegment seg)
  {
    switch (seg.EditMask)
    {
      case '9':
        return new string('#', (int) seg.Length);
      case '?':
        return new string('L', (int) seg.Length);
      case 'A':
      case 'a':
        return new string('A', (int) seg.Length);
      case 'C':
      case 'c':
        return new string('C', (int) seg.Length);
      default:
        return string.Empty;
    }
  }

  /// <exclude />
  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    PXSegment[] segments,
    string segmentViewName,
    DimensionLookupMode? lookupMode,
    bool? validCombos,
    string wildcard)
  {
    switch (value)
    {
      case PXSegmentedState instance1:
label_4:
        instance1._DataType = typeof (string);
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (segments != null)
          instance1._Segments = segments;
        if (lookupMode.HasValue)
          instance1._LookupMode = lookupMode.Value;
        if (validCombos.HasValue)
          instance1._ValidCombos = validCombos.Value;
        if (wildcard != null)
          instance1._Wildcard = wildcard;
        if (string.IsNullOrEmpty(instance1._SegmentViewName))
        {
          instance1._SegmentFieldList = new string[2]
          {
            "Value",
            "Descr"
          };
          string name = Thread.CurrentThread.CurrentUICulture.Name;
          string[] orAdd = PXSegmentedState._segmentHeaderListCached.GetOrAdd(name, (Func<string, string[]>) (key => new string[2]
          {
            PXMessages.LocalizeNoPrefix("Value"),
            PXMessages.LocalizeNoPrefix("Description")
          }));
          instance1._SegmentHeaderList = orAdd;
          instance1._SegmentValueField = "Value";
          instance1._SegmentDescriptionName = "Descr";
          instance1._SegmentViewName = segmentViewName;
        }
        if (segments != null && segments.Length != 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          short num = 0;
          for (int index = 0; index < segments.Length; ++index)
          {
            PXSegment segment = segments[index];
            if (segment.Length > (short) 0)
            {
              if ((int) segment.CaseConvert != (int) num)
              {
                switch (segment.CaseConvert)
                {
                  case 0:
                    stringBuilder.Append(num == (short) 1 ? '>' : '<');
                    break;
                  case 1:
                    stringBuilder.Append('>');
                    break;
                  case 2:
                    stringBuilder.Append('<');
                    break;
                }
                num = segment.CaseConvert;
              }
              stringBuilder.Append(PXSegmentedState.GetEditMaskForSegment(segment));
            }
            if (index < segments.Length - 1)
              stringBuilder.Append(segment.Separator);
          }
          instance1._InputMask = stringBuilder.ToString();
        }
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (string))
          return instance2;
        goto default;
      default:
        instance1 = new PXSegmentedState(value);
        goto label_4;
    }
  }
}
