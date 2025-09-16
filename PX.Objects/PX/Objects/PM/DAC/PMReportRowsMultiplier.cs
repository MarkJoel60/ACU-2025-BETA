// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DAC.PMReportRowsMultiplier
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM.DAC;

[PXCacheName("Report Rows Multiplier")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMReportRowsMultiplier : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected int? _RowsCount;
  protected int? _RowNumber;

  [PXUIField(DisplayName = "RecordID", Visible = false, Enabled = false)]
  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBInt]
  public virtual int? RowsCount
  {
    get => this._RowsCount;
    set => this._RowsCount = value;
  }

  [PXDBInt]
  public virtual int? RowNumber
  {
    get => this._RowNumber;
    set => this._RowNumber = value;
  }

  public abstract class id : BqlType<IBqlLong, long>.Field<
  #nullable disable
  PMReportRowsMultiplier.id>
  {
  }

  public abstract class rowsCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportRowsMultiplier.rowsCount>
  {
  }

  public abstract class rowNumber : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportRowsMultiplier.rowNumber>
  {
  }
}
