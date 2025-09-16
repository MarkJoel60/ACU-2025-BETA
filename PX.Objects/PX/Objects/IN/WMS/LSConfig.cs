// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.LSConfig
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.IN.WMS;

public readonly struct LSConfig(INLotSerClass lsClass, string tranType, short? invMult)
{
  private readonly INLotSerClass _lsClass = lsClass;
  private readonly string _tranType = tranType;
  private readonly short? _invMult = invMult;

  public bool IsTracked
  {
    get
    {
      INLotSerClass lsClass = this._lsClass;
      string tranType = this._tranType;
      short? invMult1 = this._invMult;
      int? invMult2 = invMult1.HasValue ? new int?((int) invMult1.GetValueOrDefault()) : new int?();
      return INLotSerialNbrAttribute.IsTrack(lsClass, tranType, invMult2);
    }
  }

  public bool IsTrackedSerial
  {
    get
    {
      INLotSerClass lsClass = this._lsClass;
      string tranType = this._tranType;
      short? invMult1 = this._invMult;
      int? invMult2 = invMult1.HasValue ? new int?((int) invMult1.GetValueOrDefault()) : new int?();
      return INLotSerialNbrAttribute.IsTrackSerial(lsClass, tranType, invMult2);
    }
  }

  public bool IsTrackedLot
  {
    get
    {
      INLotSerClass lsClass = this._lsClass;
      string tranType = this._tranType;
      short? invMult1 = this._invMult;
      int? invMult2 = invMult1.HasValue ? new int?((int) invMult1.GetValueOrDefault()) : new int?();
      return INLotSerialNbrAttribute.IsTrackLot(lsClass, tranType, invMult2);
    }
  }

  public bool IsEnterable
  {
    get
    {
      INLotSerClass lsClass = this._lsClass;
      string tranType = this._tranType;
      short? invMult1 = this._invMult;
      int? invMult2 = invMult1.HasValue ? new int?((int) invMult1.GetValueOrDefault()) : new int?();
      INLotSerTrack.Mode mode = INLotSerialNbrAttribute.TranTrackMode(lsClass, tranType, invMult2);
      if (mode.HasFlags(INLotSerTrack.Mode.Create))
        return true;
      if (!mode.HasFlags(INLotSerTrack.Mode.Issue))
        return false;
      return this._tranType == "TRX" || this._lsClass.LotSerIssueMethod == "U";
    }
  }

  public bool HasExpiration
  {
    get
    {
      INLotSerClass lsClass = this._lsClass;
      string tranType = this._tranType;
      short? invMult1 = this._invMult;
      int? invMult2 = invMult1.HasValue ? new int?((int) invMult1.GetValueOrDefault()) : new int?();
      return INLotSerialNbrAttribute.IsTrackExpiration(lsClass, tranType, invMult2);
    }
  }
}
