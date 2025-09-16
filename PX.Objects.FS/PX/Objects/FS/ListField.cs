// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.FS;

[ExcludeFromCodeCoverage]
public abstract class ListField
{
  /// <summary>EntityType for FSAddress and FSContact tables</summary>
  public abstract class ACEntityType : IBqlField, IBqlOperand
  {
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[4]
        {
          "MNFC",
          "BLOC",
          "SROR",
          "APPT"
        }, new string[4]
        {
          "Manufacturer",
          "Branch Location",
          "Service Order",
          "Appointment"
        })
      {
      }
    }

    public class Manufacturer : ID.ACEntityType.Manufacturer
    {
    }

    public class BranchLocation : ID.ACEntityType.BranchLocation
    {
    }

    public class ServiceOrder : ID.ACEntityType.ServiceOrder
    {
    }

    public class Appointment : ID.ACEntityType.Appointment
    {
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class CloningType_CloneAppointment
  {
    public const 
    #nullable disable
    string Single = "SI";
    public const string Multiple = "MU";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("SI", "Single"),
          PXStringListAttribute.Pair("MU", "Multiple")
        })
      {
      }
    }

    public class single : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.CloningType_CloneAppointment.single>
    {
      public single()
        : base("SI")
      {
      }
    }

    public class multiple : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.CloningType_CloneAppointment.multiple>
    {
      public multiple()
        : base("MU")
      {
      }
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class ServiceOrderStatus
  {
    public const string Initial = "_";
    public const string Open = "O";
    public const string Hold = "H";
    public const string Awaiting = "W";
    public const string Completed = "C";
    public const string Closed = "Z";
    public const string Canceled = "X";
    public const string Copied = "V";
    public const string Confirmed = "A";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[8]
        {
          PXStringListAttribute.Pair("O", "Open"),
          PXStringListAttribute.Pair("H", "On Hold"),
          PXStringListAttribute.Pair("W", "Awaiting"),
          PXStringListAttribute.Pair("C", "Completed"),
          PXStringListAttribute.Pair("Z", "Closed"),
          PXStringListAttribute.Pair("X", "Canceled"),
          PXStringListAttribute.Pair("V", "Copied"),
          PXStringListAttribute.Pair("A", "Confirmed")
        })
      {
      }
    }

    public class open : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.ServiceOrderStatus.open>
    {
      public open()
        : base("O")
      {
      }
    }

    public class hold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.ServiceOrderStatus.hold>
    {
      public hold()
        : base("H")
      {
      }
    }

    public class awaiting : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderStatus.awaiting>
    {
      public awaiting()
        : base("W")
      {
      }
    }

    public class completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderStatus.completed>
    {
      public completed()
        : base("C")
      {
      }
    }

    public class closed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.ServiceOrderStatus.closed>
    {
      public closed()
        : base("Z")
      {
      }
    }

    public class canceled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderStatus.canceled>
    {
      public canceled()
        : base("X")
      {
      }
    }

    public class copied : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.ServiceOrderStatus.copied>
    {
      public copied()
        : base("V")
      {
      }
    }

    public class confirmed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderStatus.confirmed>
    {
      public confirmed()
        : base("A")
      {
      }
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class ServiceOrderWorkflowTypes
  {
    public const string Quote = "QT";
    public const string Simple = "SP";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("QT", "Quote"),
          PXStringListAttribute.Pair("SP", "Simple")
        })
      {
      }
    }

    public class quote : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderWorkflowTypes.quote>
    {
      public quote()
        : base("QT")
      {
      }
    }

    public class simple : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderWorkflowTypes.simple>
    {
      public simple()
        : base("SP")
      {
      }
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class AppointmentWorkflowTypes
  {
    public const string Simple = "SP";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[1]
        {
          PXStringListAttribute.Pair("SP", "Simple")
        })
      {
      }
    }

    public class simple : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.AppointmentWorkflowTypes.simple>
    {
      public simple()
        : base("SP")
      {
      }
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class AppointmentStatus : IBqlField, IBqlOperand
  {
    public const string Initial = "_";
    public const string NotStarted = "N";
    public const string InProcess = "P";
    public const string Canceled = "X";
    public const string Completed = "C";
    public const string Closed = "Z";
    public const string Hold = "H";
    public const string Paused = "U";
    public const string Awaiting = "W";
    public const string Billed = "B";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[9]
        {
          PXStringListAttribute.Pair("N", "Not Started"),
          PXStringListAttribute.Pair("P", "In Process"),
          PXStringListAttribute.Pair("X", "Canceled"),
          PXStringListAttribute.Pair("C", "Completed"),
          PXStringListAttribute.Pair("Z", "Closed"),
          PXStringListAttribute.Pair("H", "On Hold"),
          PXStringListAttribute.Pair("U", "Paused"),
          PXStringListAttribute.Pair("W", "Awaiting"),
          PXStringListAttribute.Pair("B", "Billed")
        })
      {
      }
    }

    public class notStarted : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.AppointmentStatus.notStarted>
    {
      public notStarted()
        : base("N")
      {
      }
    }

    public class inProcess : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.AppointmentStatus.inProcess>
    {
      public inProcess()
        : base("P")
      {
      }
    }

    public class canceled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.AppointmentStatus.canceled>
    {
      public canceled()
        : base("X")
      {
      }
    }

    public class completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.AppointmentStatus.completed>
    {
      public completed()
        : base("C")
      {
      }
    }

    public class closed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.AppointmentStatus.closed>
    {
      public closed()
        : base("Z")
      {
      }
    }

    public class hold : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.AppointmentStatus.hold>
    {
      public hold()
        : base("H")
      {
      }
    }

    public class paused : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.AppointmentStatus.paused>
    {
      public paused()
        : base("U")
      {
      }
    }

    public class awaiting : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.AppointmentStatus.awaiting>
    {
      public awaiting()
        : base("W")
      {
      }
    }

    public class billed : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField.AppointmentStatus.billed>
    {
      public billed()
        : base("B")
      {
      }
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class ServiceOrderTypeBehavior : IBqlField, IBqlOperand
  {
    public const string RegularAppointment = "RE";
    public const string RouteAppointment = "RO";
    public const string InternalAppointment = "IN";
    public const string Quote = "QT";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[4]
        {
          PXStringListAttribute.Pair("RE", "Regular"),
          PXStringListAttribute.Pair("RO", "Route"),
          PXStringListAttribute.Pair("IN", "Internal"),
          PXStringListAttribute.Pair("QT", "Quote")
        })
      {
      }
    }

    public class regularAppointment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderTypeBehavior.regularAppointment>
    {
      public regularAppointment()
        : base("RE")
      {
      }
    }

    public class routeAppointment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderTypeBehavior.routeAppointment>
    {
      public routeAppointment()
        : base("RO")
      {
      }
    }

    public class internalAppointment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderTypeBehavior.internalAppointment>
    {
      public internalAppointment()
        : base("IN")
      {
      }
    }

    public class quote : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceOrderTypeBehavior.quote>
    {
      public quote()
        : base("QT")
      {
      }
    }
  }

  [ExcludeFromCodeCoverage]
  public abstract class ServiceContractBillingType : IBqlField, IBqlOperand
  {
    public const string PerformedBillings = "APFB";
    public const string StandardizedBillings = "STDB";
    public const string FixedRateBillings = "FIRB";
    public const string FixedRateAsPerformedBillings = "FRPB";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[4]
        {
          PXStringListAttribute.Pair("APFB", "At Time of Service"),
          PXStringListAttribute.Pair("STDB", "End-Period Plus"),
          PXStringListAttribute.Pair("FIRB", "Beginning-Period Fixed"),
          PXStringListAttribute.Pair("FRPB", "Beginning-Period Plus")
        })
      {
      }
    }

    public class performedBillings : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceContractBillingType.performedBillings>
    {
      public performedBillings()
        : base("APFB")
      {
      }
    }

    public class standardizedBillings : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceContractBillingType.standardizedBillings>
    {
      public standardizedBillings()
        : base("STDB")
      {
      }
    }

    public class fixedRateBillings : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceContractBillingType.fixedRateBillings>
    {
      public fixedRateBillings()
        : base("FIRB")
      {
      }
    }

    public class fixedRateAsPerformedBillings : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ListField.ServiceContractBillingType.fixedRateBillings>
    {
      public fixedRateAsPerformedBillings()
        : base("FRPB")
      {
      }
    }
  }
}
