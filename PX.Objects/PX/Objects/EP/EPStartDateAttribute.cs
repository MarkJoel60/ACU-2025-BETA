// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPStartDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;
using System.Globalization;
using System.Threading;

#nullable disable
namespace PX.Objects.EP;

public class EPStartDateAttribute : EPAllDaySupportDateTimeAttribute
{
  private string _DisplayFieldName;

  public EPStartDateAttribute()
  {
    ((PXDBDateAttribute) this)._PreserveTime = true;
    ((PXDBDateAttribute) this).UseTimeZone = true;
    this.WithoutDisplayNames = true;
  }

  public string DisplayName { get; set; }

  public bool IgnoreRequireTimeOnActivity { get; set; }

  public virtual bool UseTimeZone
  {
    get => ((PXDBDateAttribute) this).UseTimeZone;
    set
    {
    }
  }

  public virtual void CacheAttached(PXCache sender)
  {
    if (!typeof (CRActivity).IsAssignableFrom(sender.GetItemType()))
      throw new ArgumentException("Invalid cache type. CRActivity is expected.");
    this._DisplayFieldName = ((PXEventSubscriberAttribute) this)._FieldName + "_Display";
    sender.Fields.Add(this._DisplayFieldName);
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._DisplayFieldName, new PXFieldSelecting((object) this, __methodptr(_DisplayFieldName_FieldSelecting)));
    base.CacheAttached(sender);
  }

  private void _DisplayFieldName_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs args)
  {
    string str1 = $"{sender.GetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal):g}";
    string str2 = this.DisplayName;
    if (!CultureInfo.InvariantCulture.Equals((object) Thread.CurrentThread.CurrentCulture))
      str2 = PXLocalizer.Localize(this.DisplayName, sender.GetItemType().FullName);
    args.ReturnState = (object) PXFieldState.CreateInstance((object) str1, typeof (string), new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, this._DisplayFieldName, (string) null, str2, (string) null, (PXErrorLevel) 0, new bool?(false), new bool?(true), new bool?(true), (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    ((PXDBDateAttribute) this).InputMask = ((PXDBDateAttribute) this).DisplayMask = this.RequireTimeOnActivity(sender) ? "g" : "d";
    ((PXDBDateAttribute) this).FieldSelecting(sender, e);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(sender.Graph.Caches[typeof (EPSetup)].Current is EPSetup epSetup1))
      epSetup1 = ((PXSelectBase<EPSetup>) new PXSetupSelect<EPSetup>(sender.Graph)).SelectSingle(Array.Empty<object>());
    EPSetup epSetup2 = epSetup1;
    if (e.NewValue != null && (epSetup2 == null || !epSetup2.RequireTimes.GetValueOrDefault()) && !this.IgnoreRequireTimeOnActivity)
    {
      DateTime newValue = (DateTime) e.NewValue;
      e.NewValue = (object) new DateTime(newValue.Year, newValue.Month, newValue.Day, newValue.Hour, 0, 0);
    }
    ((PXDBDateAttribute) this).FieldUpdating(sender, e);
  }

  protected virtual bool RequireTimeOnActivity(PXCache sender)
  {
    EPSetup epSetup1 = (EPSetup) null;
    try
    {
      if (!(sender.Graph.Caches[typeof (EPSetup)].Current is EPSetup epSetup2))
        epSetup2 = ((PXSelectBase<EPSetup>) new PXSetupSelect<EPSetup>(sender.Graph)).SelectSingle(Array.Empty<object>());
      epSetup1 = epSetup2;
    }
    catch
    {
    }
    return ((bool?) epSetup1?.RequireTimes).GetValueOrDefault();
  }
}
