// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CarrierMethodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CarrierService;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class CarrierMethodSelectorAttribute : PXCustomSelectorAttribute
{
  public CarrierMethodSelectorAttribute()
    : base(typeof (CarrierMethodSelectorAttribute.CarrierPluginMethod.code))
  {
    ((PXSelectorAttribute) this).DescriptionField = typeof (CarrierMethodSelectorAttribute.CarrierPluginMethod.description);
  }

  public virtual void FieldVerifying(
  #nullable disable
  PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual IEnumerable GetRecords()
  {
    CarrierMethodSelectorAttribute selectorAttribute = this;
    if (selectorAttribute._Graph.Caches[typeof (Carrier)].Current is Carrier current && current.IsExternal.GetValueOrDefault() && !string.IsNullOrEmpty(current.CarrierPluginID))
    {
      CarrierResult<ICarrierService> carrierService = CarrierPluginMaint.CreateCarrierService(selectorAttribute._Graph, current.CarrierPluginID);
      if (carrierService.IsSuccess)
      {
        foreach (CarrierMethod availableMethod in carrierService.Result.AvailableMethods)
          yield return (object) new CarrierMethodSelectorAttribute.CarrierPluginMethod()
          {
            Code = availableMethod.Code,
            Description = availableMethod.Description
          };
      }
    }
  }

  [PXHidden]
  [Serializable]
  public class CarrierPluginMethod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Code;
    protected string _Description;

    [PXDefault]
    [PXString(50, IsUnicode = false, IsKey = true)]
    [PXUIField]
    public virtual string Code
    {
      get => this._Code;
      set => this._Code = value;
    }

    [PXString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    public abstract class code : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CarrierMethodSelectorAttribute.CarrierPluginMethod.code>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CarrierMethodSelectorAttribute.CarrierPluginMethod.description>
    {
    }
  }
}
