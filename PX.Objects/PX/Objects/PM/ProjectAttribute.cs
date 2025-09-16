// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.CT;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Selector Attribute that displays all Projects including Templates.
/// Selector also has <see cref="P:PX.Objects.PM.ProjectAttribute.WarnIfCompleted" /> property for field verification.
/// This Attribute also contains static Util methods.
/// </summary>
[PXDBInt]
[PXUIField]
[PXAttributeFamily(typeof (PXEntityAttribute))]
public class ProjectAttribute : PXEntityAttribute, IPXFieldVerifyingSubscriber
{
  public const 
  #nullable disable
  string DimensionName = "PROJECT";
  public const string DimensionNameTemplate = "TMPROJECT";

  public ProjectAttribute()
  {
    this.WarnIfCompleted = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", BqlCommand.Compose(new Type[22]
    {
      typeof (Search2<,,>),
      typeof (PMProject.contractID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<,>),
      typeof (PX.Objects.AR.Customer.bAccountID),
      typeof (Equal<>),
      typeof (PMProject.customerID),
      typeof (LeftJoin<,>),
      typeof (ContractBillingSchedule),
      typeof (On<,>),
      typeof (ContractBillingSchedule.contractID),
      typeof (Equal<>),
      typeof (PMProject.contractID),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.project>),
      typeof (Or<,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.projectTemplate>),
      typeof (And<Match<Current<AccessInfo.userName>>>)
    }), typeof (PMProject.contractCD), new Type[11]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PMProject.locationID),
      typeof (PMProject.status),
      typeof (PMProject.ownerID),
      typeof (PMProject.startDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description),
      ValidComboRequired = true,
      CacheGlobal = true
    });
    this.AddClosedProjectRestictor();
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
    this.CacheGlobal = true;
  }

  public ProjectAttribute(bool restrictClosed)
  {
    this.WarnIfCompleted = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", BqlCommand.Compose(new Type[22]
    {
      typeof (Search2<,,>),
      typeof (PMProject.contractID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<,>),
      typeof (PX.Objects.AR.Customer.bAccountID),
      typeof (Equal<>),
      typeof (PMProject.customerID),
      typeof (LeftJoin<,>),
      typeof (ContractBillingSchedule),
      typeof (On<,>),
      typeof (ContractBillingSchedule.contractID),
      typeof (Equal<>),
      typeof (PMProject.contractID),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.project>),
      typeof (Or<,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.projectTemplate>),
      typeof (And<Match<Current<AccessInfo.userName>>>)
    }), typeof (PMProject.contractCD), new Type[11]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PMProject.locationID),
      typeof (PMProject.status),
      typeof (PMProject.ownerID),
      typeof (PMProject.startDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description),
      ValidComboRequired = true,
      CacheGlobal = true
    });
    if (restrictClosed)
      this.AddClosedProjectRestictor();
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
    this.CacheGlobal = true;
  }

  /// <summary>Creates an instance of ProjectAttribute.</summary>
  /// <param name="where">BQL Where</param>
  public ProjectAttribute(Type where)
  {
    this.WarnIfCompleted = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", BqlCommand.Compose(new Type[25]
    {
      typeof (Search2<,,>),
      typeof (PMProject.contractID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<,>),
      typeof (PX.Objects.AR.Customer.bAccountID),
      typeof (Equal<>),
      typeof (PMProject.customerID),
      typeof (LeftJoin<,>),
      typeof (ContractBillingSchedule),
      typeof (On<,>),
      typeof (ContractBillingSchedule.contractID),
      typeof (Equal<>),
      typeof (PMProject.contractID),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.project>),
      typeof (Or<,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.projectTemplate>),
      typeof (And2<,>),
      typeof (Match<Current<AccessInfo.userName>>),
      typeof (And<>),
      where
    }), typeof (PMProject.contractCD), new Type[11]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PMProject.locationID),
      typeof (PMProject.status),
      typeof (PMProject.ownerID),
      typeof (PMProject.startDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description)
    });
    this.AddClosedProjectRestictor();
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.CacheGlobal = true;
  }

  /// <summary>Creates an instance of ProjectAttribute.</summary>
  /// <param name="where">BQL Where</param>
  public ProjectAttribute(Type where, bool restrictClosed)
  {
    this.WarnIfCompleted = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("PROJECT", BqlCommand.Compose(new Type[25]
    {
      typeof (Search2<,,>),
      typeof (PMProject.contractID),
      typeof (LeftJoin<,,>),
      typeof (PX.Objects.AR.Customer),
      typeof (On<,>),
      typeof (PX.Objects.AR.Customer.bAccountID),
      typeof (Equal<>),
      typeof (PMProject.customerID),
      typeof (LeftJoin<,>),
      typeof (ContractBillingSchedule),
      typeof (On<,>),
      typeof (ContractBillingSchedule.contractID),
      typeof (Equal<>),
      typeof (PMProject.contractID),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.project>),
      typeof (Or<,>),
      typeof (PMProject.baseType),
      typeof (Equal<CTPRType.projectTemplate>),
      typeof (And2<,>),
      typeof (Match<Current<AccessInfo.userName>>),
      typeof (And<>),
      where
    }), typeof (PMProject.contractCD), new Type[11]
    {
      typeof (PMProject.contractCD),
      typeof (PMProject.description),
      typeof (PMProject.customerID),
      typeof (PX.Objects.AR.Customer.acctName),
      typeof (PMProject.locationID),
      typeof (PMProject.status),
      typeof (PMProject.ownerID),
      typeof (PMProject.startDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate),
      typeof (PMProject.curyID)
    })
    {
      DescriptionField = typeof (PMProject.description)
    });
    if (restrictClosed)
      this.AddClosedProjectRestictor();
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.CacheGlobal = true;
  }

  /// <summary>
  /// If True a Warning will be shown if the Project selected is Completed.
  /// Default = True
  /// </summary>
  public bool WarnIfCompleted { get; set; }

  /// <summary>Composes VisibleInModule Type to be used in BQL</summary>
  /// <param name="Module">Module</param>
  public static Type ComposeVisibleIn(string Module)
  {
    if (Module != null && Module.Length == 2)
    {
      switch (Module[0])
      {
        case 'A':
          switch (Module)
          {
            case "AP":
              return typeof (PMProject.visibleInAP);
            case "AR":
              return typeof (PMProject.visibleInAR);
          }
          break;
        case 'C':
          switch (Module)
          {
            case "CA":
              return typeof (PMProject.visibleInCA);
            case "CR":
              return typeof (PMProject.visibleInCR);
          }
          break;
        case 'G':
          if (Module == "GL")
            return typeof (PMProject.visibleInGL);
          break;
        case 'I':
          if (Module == "IN")
            return typeof (PMProject.visibleInIN);
          break;
        case 'P':
          if (Module == "PO")
            return typeof (PMProject.visibleInPO);
          break;
        case 'S':
          if (Module == "SO")
            return typeof (PMProject.visibleInSO);
          break;
      }
    }
    throw new ArgumentOutOfRangeException("Source", (object) Module, "ProjectAttribute does not support the given module.");
  }

  public void AddClosedProjectRestictor()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(typeof (Where<BqlOperand<PMProject.status, IBqlString>.IsNotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new Type[1]
    {
      typeof (PMProject.contractCD)
    }));
  }

  /// <summary>
  /// Returns True if the given module is integrated with PM.
  /// </summary>
  /// <remarks>Always returns True if Module is null or an empty string.</remarks>
  public static bool IsPMVisible(string module)
  {
    if (string.IsNullOrEmpty(module))
      return true;
    return PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() && ServiceLocator.IsLocationProviderSet && ServiceLocator.Current.GetInstance<IProjectSettingsManager>().IsPMVisible(module);
  }

  public static bool IsAutonumbered(PXGraph graph, string dimensionID)
  {
    return DimensionMaint.IsAutonumbered(graph, dimensionID, false);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is int))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (pmProject == null || !this.WarnIfCompleted || !pmProject.IsCompleted.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling(this.FieldName, e.Row, e.NewValue, (Exception) new PXSetPropertyException("Project is Completed. It will not be available for data entry.", (PXErrorLevel) 2));
  }

  public class dimension : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectAttribute.dimension>
  {
    public dimension()
      : base("PROJECT")
    {
    }
  }

  public class dimensionTM : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectAttribute.dimensionTM>
  {
    public dimensionTM()
      : base("TMPROJECT")
    {
    }
  }
}
