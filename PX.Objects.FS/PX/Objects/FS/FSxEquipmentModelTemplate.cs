// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxEquipmentModelTemplate
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.FS;

public class FSxEquipmentModelTemplate : PXCacheExtension<
#nullable disable
INItemClass>
{
  protected string _EquipmentItemClass;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
  }

  [PXDBString(2, IsFixed = true)]
  [ListField_ModelType.ListAtrribute]
  [PXDefault("EQ")]
  [PXUIField(DisplayName = "Model Type")]
  public virtual string DefaultEquipmentModelType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Model Equipment Class", Visible = false)]
  public virtual bool? EQEnabled { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_EquipmentItemClass.ListAtrribute]
  [PXDefault("OI")]
  [PXUIField(DisplayName = "Equipment Class")]
  public virtual string EquipmentItemClass
  {
    get => this._EquipmentItemClass;
    set
    {
      this._EquipmentItemClass = value;
      if (this._EquipmentItemClass == "ME" || this._EquipmentItemClass == "CT")
        this.EQEnabled = new bool?(true);
      else
        this.EQEnabled = new bool?(false);
    }
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool Mem_ShowComponent { get; set; }

  public abstract class defaultEquipmentModelType : ListField_ModelType
  {
  }

  public abstract class eQEnabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEquipmentModelTemplate.eQEnabled>
  {
  }

  public abstract class equipmentItemClass : ListField_EquipmentItemClass
  {
  }

  public abstract class mem_ShowComponent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEquipmentModelTemplate.mem_ShowComponent>
  {
  }
}
