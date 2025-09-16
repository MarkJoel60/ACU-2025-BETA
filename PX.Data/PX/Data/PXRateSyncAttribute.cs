// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRateSyncAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Synchronizes CuryRateID with the field to which this
/// attribute is applied.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXRateSyncAttribute : 
  PXEventSubscriberAttribute,
  IPXRowInsertingSubscriber,
  IPXRowSelectedSubscriber
{
  public void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    sender.SetValue(e.Row, this._FieldOrdinal, (object) Convert.ToInt32((object) sender.Graph.Accessinfo.CuryRateID));
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    sender.Graph.Accessinfo.CuryRateID = new int?(Convert.ToInt32(sender.GetValue(e.Row, this._FieldOrdinal)));
  }
}
