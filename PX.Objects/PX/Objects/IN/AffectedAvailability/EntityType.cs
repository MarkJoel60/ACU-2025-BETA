// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.AffectedAvailability.EntityType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.AffectedAvailability;

public class EntityType
{
  public static readonly 
  #nullable disable
  string SOOrder = typeof (PX.Objects.SO.SOOrder).FullName;
  public static readonly string SOShipment = typeof (PX.Objects.SO.SOShipment).FullName;
  public static readonly string INRegister = typeof (PX.Objects.IN.INRegister).FullName;
  public static readonly string INKitRegister = typeof (PX.Objects.IN.INKitRegister).FullName;

  public class soOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EntityType.soOrder>
  {
    public soOrder()
      : base(EntityType.SOOrder)
    {
    }
  }

  public class soShipment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EntityType.soShipment>
  {
    public soShipment()
      : base(EntityType.SOShipment)
    {
    }
  }

  public class inRegister : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EntityType.inRegister>
  {
    public inRegister()
      : base(EntityType.INRegister)
    {
    }
  }

  public class inKitRegister : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EntityType.inKitRegister>
  {
    public inKitRegister()
      : base(EntityType.INKitRegister)
    {
    }
  }
}
