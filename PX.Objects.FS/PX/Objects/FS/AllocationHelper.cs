// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.AllocationHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public static class AllocationHelper
{
  public static AllocationHelper.AllocationInfo Add(
    this Dictionary<string, AllocationHelper.AllocationInfo> splitLines,
    AllocationHelper.AllocationInfo splitLine)
  {
    AllocationHelper.AllocationInfo allocationInfo;
    if (splitLines.TryGetValue(splitLine.Key, out allocationInfo))
    {
      allocationInfo.Qty += splitLine.Qty;
      return allocationInfo;
    }
    splitLines.Add(splitLine.Key, splitLine);
    return splitLine;
  }

  public class AllocationInfo
  {
    private string _LotSerialNbr;
    private string _Key;

    public virtual string SrvOrdType { get; set; }

    public virtual string RefNbr { get; set; }

    public virtual int LineNbr { get; set; }

    public virtual int SODetID { get; set; }

    public virtual string LotSerialNbr
    {
      get => this._LotSerialNbr;
      set
      {
        if (value == string.Empty)
          this._LotSerialNbr = (string) null;
        else
          this._LotSerialNbr = value;
      }
    }

    public virtual Decimal Qty { get; set; }

    public virtual string Key => this._Key;

    public AllocationInfo(FSSODetSplit split, FSSODet srvOrdLine)
    {
      this.SetSrvOrdLineValues(srvOrdLine);
      this.LotSerialNbr = split.LotSerialNbr;
      this.Qty = split.Qty.Value;
      this.SetKey();
    }

    public AllocationInfo(FSApptLineSplit split, FSSODet srvOrdLine)
    {
      this.SetSrvOrdLineValues(srvOrdLine);
      this.LotSerialNbr = split.LotSerialNbr;
      this.Qty = split.Qty.Value;
      this.SetKey();
    }

    public AllocationInfo(Decimal? qty, FSSODet srvOrdLine)
    {
      this.SetSrvOrdLineValues(srvOrdLine);
      this.LotSerialNbr = (string) null;
      this.Qty = qty.Value;
      this.SetKey();
    }

    protected virtual void SetSrvOrdLineValues(FSSODet srvOrdLine)
    {
      this.SrvOrdType = srvOrdLine.SrvOrdType;
      this.RefNbr = srvOrdLine.RefNbr;
      this.LineNbr = srvOrdLine.LineNbr.Value;
      this.SODetID = srvOrdLine.SODetID.Value;
    }

    protected virtual void SetKey() => this._Key = $"{this.SODetID.ToString()}|{this.LotSerialNbr}";
  }
}
