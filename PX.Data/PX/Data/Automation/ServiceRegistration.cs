// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Features.AttributeFilters;
using AutoMapper;
using PX.Async.Internal;
using PX.Common;
using PX.Data.Archiving;
using PX.Data.Automation.Services;
using PX.Data.Automation.State;
using PX.Data.Process.Automation;
using PX.Data.Process.Automation.Services;
using PX.Data.ProjectDefinition.Workflow;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Automation;

public class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    System.Type serviceType = typeof (ServiceRegistration);
    System.Type[] tables = PXSubstManager.EnumTypesInAssemblies("Automation.ServiceRegistration").Where<System.Type>((Func<System.Type, bool>) (t => t.Assembly == serviceType.Assembly && t.Name.StartsWith("AU") && t.Name.EndsWith("State") && typeof (IBqlTable).IsAssignableFrom(t))).Append<System.Type>(typeof (PXGraph.FeaturesSet)).ToArray<System.Type>();
    foreach (System.Type t in tables)
      PXSubstManager.AddTypeToNamedList("Automation.ServiceRegistration", t);
    PXSubstManager.SaveTypeCache("Automation.ServiceRegistration");
    RegistrationExtensions.Register<Func<IScreenMap>>(builder, (Func<IComponentContext, Func<IScreenMap>>) (ctx =>
    {
      IPXPageIndexingService pageIndexingService = ResolutionExtensions.Resolve<IPXPageIndexingService>(ctx);
      return (Func<IScreenMap>) (() =>
      {
        if (SuppressWorkflowScope.IsScoped)
          return (IScreenMap) null;
        return (PXContext.GetSlot<PXWorkflowDbDefinition>() ?? PXContext.SetSlot<PXWorkflowDbDefinition>(PXDatabase.GetSlot<PXWorkflowDbDefinition, IPXPageIndexingService>("AutomationWorkflowDescription", pageIndexingService, ((IEnumerable<System.Type>) tables).Union<System.Type>((IEnumerable<System.Type>) PXSystemWorkflows.GetWorkflowDependedTypes()).ToArray<System.Type>())))?.Screens;
      });
    }));
    RegistrationExtensions.PreserveExistingDefaults<WorkflowService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<WorkflowService>(builder).As<IWorkflowService>().SingleInstance());
    RegistrationExtensions.RegisterType<WorkflowFieldsService>(builder).As<IWorkflowFieldsService>().SingleInstance();
    RegistrationExtensions.RegisterType<AUWorkflowEngine>(builder).As<IAUWorkflowEngine>().SingleInstance();
    RegistrationExtensions.RegisterType<AUWorkflowActionsEngine>(builder).As<IAUWorkflowActionsEngine>().SingleInstance();
    RegistrationExtensions.RegisterType<WorkflowConditionEvaluateService>(builder).As<IWorkflowConditionEvaluateService>().SingleInstance();
    RegistrationExtensions.RegisterType<WorkflowFieldExpressionEvaluator>(builder).Keyed<INavigationExpressionEvaluator>((object) "WorkflowFieldExpressionEvaluator").SingleInstance();
    RegistrationExtensions.RegisterType<AUArchivingRuleEngine>(builder).As<IAUArchivingRuleEngine>().SingleInstance();
    RegistrationExtensions.RegisterType<AUWorkflowFormsEngine>(builder).As<IAUWorkflowFormsEngine>().SingleInstance();
    RegistrationExtensions.RegisterType<AUWorkflowEventsEngine>(builder).As<IAUWorkflowEventsEngine>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<PXWorkflowService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.WithAttributeFiltering<PXWorkflowService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.AsSelf<PXWorkflowService, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<PXWorkflowService>(builder)).As<ILongOperationWorkflowAdapter>()).SingleInstance());
    RegistrationExtensions.RegisterType<ExtraActionHandlerService>(builder).As<IExtraActionHanlderService>().SingleInstance();
    RegistrationExtensions.PreserveExistingDefaults<PXWorkflowMapProfile, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<PXWorkflowMapProfile>(builder).As<Profile>());
    RegistrationExtensions.PreserveExistingDefaults<AutomationConditionEvaluator, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<AutomationConditionEvaluator>(builder).As<IConditionEvaluator>());
    RegistrationExtensions.PreserveExistingDefaults<GenericInquiryNavigationExpressionEvaluator, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<GenericInquiryNavigationExpressionEvaluator>(builder).As<INavigationExpressionEvaluator>());
    RegistrationExtensions.PreserveExistingDefaults<NavigationService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.WithParameter<NavigationService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.WithParameter<NavigationService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<NavigationService>(builder).As<INavigationService>(), "allowInsert", (object) false), "repaintControls", (object) false));
    RegistrationExtensions.PreserveExistingDefaults<NavigationService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.WithParameter<NavigationService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.WithParameter<NavigationService, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<NavigationService>(builder).Keyed<INavigationService>((object) "Automation"), "allowInsert", (object) true), "repaintControls", (object) true));
    RegistrationExtensions.Register<Func<string, INavigationScreenInfo>>(builder, (Func<IComponentContext, Func<string, INavigationScreenInfo>>) (_ => (Func<string, INavigationScreenInfo>) (s => (INavigationScreenInfo) new NavigationScreenInfo(s))));
    RegistrationExtensions.PreserveExistingDefaults<NavigationValueEvaluator, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<NavigationValueEvaluator>(builder).As<INavigationValueEvaluator>());
  }
}
